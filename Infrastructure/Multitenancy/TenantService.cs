using Application.Multitenancy;

using Infrastructure.Persistence.Context;

using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Multitenancy
{
    public class TenantService: ITenantService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetConnectionString()
        {
            var _httpContext = _httpContextAccessor.HttpContext;
            if (_httpContext != null)
            {
                var organizationName = GetOrganizationNameFromRequest(_httpContext);
                if (!string.IsNullOrWhiteSpace(organizationName))
                {
                    var baseConnectionString = _configuration.GetConnectionString("BaseConnection");
                    var connectionStringBuilder = new SqlConnectionStringBuilder(baseConnectionString)
                    {
                        InitialCatalog = organizationName
                    };
                    var connectionString = connectionStringBuilder.ConnectionString;
                    var dbProductContext = _httpContext.RequestServices.GetRequiredService<ProductDbContext>();
                    dbProductContext.Database.SetConnectionString(connectionString);
                }
                else
                {
                    throw new Exception("Invalid Tenant!");
                }
            }
        }

        private string GetOrganizationNameFromRequest(HttpContext context)
        {
            var segments = context.Request.Path.Value?.Split('/');

            if (segments != null && segments.Length > 1)
            {
                var organizationName = segments[1];
                context.Items["CurrentOrganization"] = organizationName;
                return organizationName;
            }

            return null;
        }
    }
}
