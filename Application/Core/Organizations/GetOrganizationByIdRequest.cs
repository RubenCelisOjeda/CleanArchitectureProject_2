using Application.Common.Exceptions;
using Domain;

namespace Application.Core.Organizations
{
    public class GetOrganizationByIdRequest : IRequest<Organization>
    {
        public int Id { get; set; }
        public GetOrganizationByIdRequest(int id) => Id = id;
    }

    public class GetOrganizationByIdRequestHandler : IRequestHandler<GetOrganizationByIdRequest, Organization>
    {
        private readonly IOrganizationService _organizationService;

        public GetOrganizationByIdRequestHandler(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task<Organization> Handle(GetOrganizationByIdRequest request, CancellationToken cancellationToken)
        {
            var organization = await _organizationService.GetByIdAsync(request.Id);

            if (organization == null)
            {
                throw new NotFoundException("Organization not found");
            }

            return organization;
        }
    }
}
