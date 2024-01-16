using Application.Auth.Users;

using Domain;

using Infrastructure.Persistence.Context;

using Mapster;

using Microsoft.EntityFrameworkCore;

namespace MultiTenant.Infrastructure.Auth
{
    public class UserService : IUserService
    {
        private readonly MasterDbContext _context;

        public UserService(MasterDbContext context)
        {
            _context = context;
        }

        public async Task<UserDetailsDto> GetByEmailAsync(string email)
        {
            var userFromRepo = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (userFromRepo == null)
            {
                return null;
            }
            return userFromRepo.Adapt<UserDetailsDto>();
        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            return await _context.Users.AnyAsync(x => x.Email == email && x.Password == password);
        }

        public async Task<IEnumerable<UserDetailsDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Adapt<IEnumerable<UserDetailsDto>>();
        }

        public async Task<UserDetailsDto> GetByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user.Adapt<UserDetailsDto>();
        }

        public async Task<int> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
