using Domain;

namespace Application.Core.Organizations
{
    public class GetOrganizationsRequest : IRequest<IEnumerable<Organization>>
    {
    }

    public class GetOrganizationsHandler : IRequestHandler<GetOrganizationsRequest, IEnumerable<Organization>>
    {
        private readonly IOrganizationService _organizationService;

        public GetOrganizationsHandler(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task<IEnumerable<Organization>> Handle(GetOrganizationsRequest request, CancellationToken cancellationToken)
        {
            return await _organizationService.GetAllAsync();
        }
    }
}
