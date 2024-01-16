using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Initialization
{
    internal class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task InitializeDatabasesAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>().InitializeAsync(cancellationToken);
        }
    }
}
