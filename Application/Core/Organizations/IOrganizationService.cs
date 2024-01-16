using Application.Common.Interfaces;

using Domain;

namespace Application.Core.Organizations
{
    public interface IOrganizationService : ITransientService
    {
        Task<IEnumerable<Organization>> GetAllAsync();
        Task<Organization> GetByIdAsync(int id);
        Task<Organization> GetByNameAsync(string name);
        Task<int> CreateAsync(Organization organization);
        Task UpdateAsync(Organization organization);
        Task DeleteAsync(int id);
    }
}
