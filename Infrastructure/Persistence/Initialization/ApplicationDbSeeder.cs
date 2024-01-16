using Domain;

using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Initialization
{
    internal class ApplicationDbSeeder
    {
        public async Task SeedDatabaseAsync(MasterDbContext dbContext, CancellationToken cancellationToken)
        {
            await SeedAdminUserAsync(dbContext);
        }

        private async Task SeedAdminUserAsync(MasterDbContext dbContext)
        {
            if (!dbContext.Organizations.Any())
            {
                var defaultOrganization = new Organization
                {
                    Name = "root",
                    Description = "root",
                    TenantId = "root",
                };

                dbContext.Organizations.Add(defaultOrganization);
                dbContext.SaveChanges();

                var defaulUser = new User
                {
                    FirstName = "root",
                    LastName = "root",
                    Email = "root@root.com",
                    Password = "root",
                    OrganizationId = defaultOrganization.Id,
                };
                dbContext.Users.Add(defaulUser);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
