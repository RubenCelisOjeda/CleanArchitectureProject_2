using Domain;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

    }
}
