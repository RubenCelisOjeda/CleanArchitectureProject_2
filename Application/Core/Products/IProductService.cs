using Application.Common.Interfaces;

using Domain;

namespace Application.Core.Products
{
    public interface IProductService : ITransientService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<int> CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
