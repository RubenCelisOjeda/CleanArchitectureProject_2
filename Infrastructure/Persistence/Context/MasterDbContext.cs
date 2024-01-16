using Domain;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options)
        : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
