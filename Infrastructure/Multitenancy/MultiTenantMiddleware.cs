using Application.Multitenancy;

using Microsoft.AspNetCore.Http;

namespace Infrastructure.Multitenancy
{
    public class MultiTenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITenantService _tenantService;

        public MultiTenantMiddleware(RequestDelegate next, ITenantService tenantService)
        {
            _next = next;
            _tenantService = tenantService;
        }

        public async Task Invoke(HttpContext context)
        {
            _tenantService.SetConnectionString();
            await _next(context);
        }
    }
}
