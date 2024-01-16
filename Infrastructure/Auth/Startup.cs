using Infrastructure.Auth.Jwt;
using Infrastructure.Auth.Permissions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Auth
{
    internal static class Startup
    {
        internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddPermissions()
                .AddJwtAuth(config);
        }

        private static IServiceCollection AddPermissions(this IServiceCollection services) =>
        services
            .AddAuthorization(options =>
            {
                options.AddPolicy("BasicUserPermissionsPolicy", policy =>
                {
                    policy.Requirements.Add(new BasicUserPermissionsRequirement());
                });
                options.AddPolicy("AdminUserPermissionsPolicy", policy =>
                {
                    policy.Requirements.Add(new AdminUserPermissionsRequirement());
                });
            })
            .AddScoped<IAuthorizationHandler, AdminUserPermissionsHandler>()
            .AddScoped<IAuthorizationHandler, BasicUserPermissionsHandler>();
    }
}
