using Infrastructure.Auth;
using Infrastructure.Common;
using Infrastructure.Mapping;
using Infrastructure.Middleware;
using Infrastructure.Multitenancy;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Initialization;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            MapsterSettings.Configure();
            return services
                .AddAuth(config)
                .AddExceptionMiddleware()
                .AddMultitenancy(config)
                .AddPersistence(config)
                .AddServices();
        }

        public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();

            await scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>().InitializeDatabasesAsync(cancellationToken);
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config)
        {
            return builder
                .UseExceptionMiddleware();
        }
    }
}
