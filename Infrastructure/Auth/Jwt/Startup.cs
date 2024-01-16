using Application.Common.Exceptions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace Infrastructure.Auth.Jwt
{
    internal static class Startup
    {
        internal static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
            var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>();
            if (jwtSettings == null)
                throw new InvalidOperationException("No Key defined in JwtSettings config.");
            if (string.IsNullOrEmpty(jwtSettings.Key))
                throw new InvalidOperationException("No Key defined in JwtSettings config.");

            return services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(bearer =>
                {
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key)),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                    };
                    bearer.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            if (!context.Response.HasStarted)
                            {
                                throw new UnauthorizedException("Authentication Failed.");
                            }

                            return Task.CompletedTask;
                        },
                        OnForbidden = _ => throw new ForbiddenException("You are not authorized to access this resource."),
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_t0ken"];

                            if (!string.IsNullOrEmpty(accessToken) &&
                                context.HttpContext.Request.Path.StartsWithSegments("/notifications"))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                })
                .Services;
        }
    }
}
