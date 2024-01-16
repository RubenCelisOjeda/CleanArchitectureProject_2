using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Application.Core.Organizations;
using Domain;

namespace Api.Controllers
{
    [Authorize("AdminUserPermissionsPolicy")]
    [Route("[controller]")]
    public class OrganizationsController : BaseApiController
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationsController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        
        [HttpGet]
        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            return await Mediator.Send(new GetOrganizationsRequest());
        }

        [HttpGet("{id}")]
        public async Task<Organization> GetOrganization(int id)
        {
            return await Mediator.Send(new GetOrganizationByIdRequest(id));
        }

        [HttpPost]
        public async Task<int> PostOrganization(CreateOrganizationRequest request)
        {
            return await Mediator.Send(request);
        }

    }
}
