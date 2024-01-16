using Application.Core.Organizations;

using Domain;

using Infrastructure.Persistence.Context;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Core
{
    public class OrganizationService : IOrganizationService
    {
        private readonly MasterDbContext _dbContext;
        private readonly DbContextOptions<ProductDbContext> _productsDbContextOptions;
        private readonly IConfiguration _configuration;

        public OrganizationService(MasterDbContext dbContext, IConfiguration configuration, DbContextOptions<ProductDbContext> productsDbContextOptions)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _productsDbContextOptions = productsDbContextOptions;
        }

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            return await _dbContext.Organizations.ToListAsync();
        }

        public async Task<Organization> GetByIdAsync(int id)
        {
            return await _dbContext.Organizations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Organization> GetByNameAsync(string name)
        {
            return await _dbContext.Organizations.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<int> CreateAsync(Organization organization)
        {
            var baseConnectionString = _configuration.GetConnectionString("BaseConnection");
            var connectionStringBuilder = new SqlConnectionStringBuilder(baseConnectionString)
            {
                InitialCatalog = organization.Name
            };

            var updatedOptions = new DbContextOptionsBuilder<ProductDbContext>(_productsDbContextOptions)
                .UseSqlServer(connectionStringBuilder.ConnectionString)
                .Options;

            using (var dbContext = new ProductDbContext(updatedOptions))
            {
                await dbContext.Database.EnsureCreatedAsync();

                if (!dbContext.Products.Any())
                {
                    Random random = new Random();
                    var defaultProduct = new Product
                    {
                        Name = $"Product for {organization.Name}",
                        Price = random.Next(10, 100)
                    };

                    dbContext.Products.Add(defaultProduct);
                    dbContext.SaveChanges();
                }

                _dbContext.Organizations.Add(organization);
                await _dbContext.SaveChangesAsync();
            }

            return organization.Id;
        }

        public async Task UpdateAsync(Organization organization)
        {
            _dbContext.Entry(organization).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var organization = await _dbContext.Organizations.FindAsync(id);
            if (organization != null)
            {
                _dbContext.Organizations.Remove(organization);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
