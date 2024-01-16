using Application.Common.Exceptions;
using Domain;

namespace Application.Core.Organizations
{
    public class CreateOrganizationRequest : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateOrganizationRequestHandler : IRequestHandler<CreateOrganizationRequest, int>
    {
        private readonly IOrganizationService _organizationService;

        public CreateOrganizationRequestHandler(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task<int> Handle(CreateOrganizationRequest request, CancellationToken cancellationToken)
        {
            var existingOrganization = await _organizationService.GetByNameAsync(request.Name);

            if (existingOrganization != null)
            {
                throw new BadRequestException("The organization already exists.");
            }

            var result = await _organizationService.CreateAsync(new Organization()
            {
                Name = request.Name,
                Description = request.Name,
                TenantId = request.Name
            });
            return result;
        }
    }
}
