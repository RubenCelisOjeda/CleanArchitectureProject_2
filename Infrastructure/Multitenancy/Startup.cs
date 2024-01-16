using Application.Multitenancy;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Multitenancy
{
    internal static class Startup
    {
        internal static IServiceCollection AddMultitenancy(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddTransient<ITenantService, TenantService>();
        }

        internal static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MultiTenantMiddleware>();
        }
    }
}
