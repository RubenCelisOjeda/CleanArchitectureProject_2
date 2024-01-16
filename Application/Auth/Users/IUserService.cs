using Application.Common.Interfaces;

using Domain;

namespace Application.Auth.Users
{
    public interface IUserService : ITransientService
    {
        Task<UserDetailsDto> GetByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(string email, string password);
        Task<IEnumerable<UserDetailsDto>> GetAllAsync();
        Task<UserDetailsDto> GetByIdAsync(int id);
        Task<int> CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
