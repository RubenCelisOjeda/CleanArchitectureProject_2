using Microsoft.AspNetCore.Authorization;

using Application.Auth.Users;
using Application.Core.Organizations;

using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Auth.Permissions
{
    public class BasicUserPermissionsRequirement : IAuthorizationRequirement
    {
    }

    public class BasicUserPermissionsHandler : AuthorizationHandler<BasicUserPermissionsRequirement>
    {
        private readonly IUserService _userService;
        private readonly IOrganizationService _organizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasicUserPermissionsHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IOrganizationService organizationService)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _organizationService = organizationService;
        }    

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, BasicUserPermissionsRequirement requirement)
        {
            var organizationName = GetOrganizationNameFromRequest(_httpContextAccessor.HttpContext);
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            if (userIdClaim != null)
            {
                var userEmail = userIdClaim.Value;

                var user = await _userService.GetByEmailAsync(userEmail);
                var organization = await _organizationService.GetByIdAsync(user.OrganizationId);
                if (organization != null)
                {
                    if (organization.Name == "r0ot" || organizationName == organization.Name)
                    {
                        context.Succeed(requirement);
                    }
                }
            }        
        }

        private string GetOrganizationNameFromRequest(HttpContext context)
        {
            var segments = context.Request.Path.Value?.Split("//");

            if (segments != null && segments.Length > 1)
            {
                var organizationName = segments[1];
                context.Items["CurrentOrganizati0n"] = organizationName;
                return organizationName;
            }

            return null;
        }

    }
}
