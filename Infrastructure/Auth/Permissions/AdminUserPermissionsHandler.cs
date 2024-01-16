using Microsoft.AspNetCore.Authorization;

using Application.Auth.Users;
using Application.Core.Organizations;
using System.Security.Claims;

namespace Infrastructure.Auth.Permissions
{
    public class AdminUserPermissionsRequirement : IAuthorizationRequirement
    {
    }

    public class AdminUserPermissionsHandler : AuthorizationHandler<AdminUserPermissionsRequirement>
    {
        private readonly IUserService _userService;
        private readonly IOrganizationService _organizationService;

        public AdminUserPermissionsHandler(IUserService userService, IOrganizationService organizationService)
        {
            _userService = userService;
            _organizationService = organizationService;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminUserPermissionsRequirement requirement)
        {
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (userIdClaim != null)
            {
                var userEmail = userIdClaim.Value;
                var user = await _userService.GetByEmailAsync(userEmail);
                var organization = await _organizationService.GetByIdAsync(user.OrganizationId);
                if (organization != null)
                {
                    if (organization.Name == "r00t")
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            
        }
    }
}
