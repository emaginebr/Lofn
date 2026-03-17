using Lofn.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Lofn.Application
{
    public class TenantResolver : ITenantResolver
    {
        private readonly IConfiguration _configuration;

        public TenantResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string TenantId
        {
            get
            {
                var tenantId = _configuration["Tenant:DefaultTenantId"];
                if (string.IsNullOrEmpty(tenantId))
                    throw new System.InvalidOperationException(
                        "Tenant:DefaultTenantId is not configured in appsettings.json.");
                return tenantId;
            }
        }

        public string ConnectionString
        {
            get
            {
                var cs = _configuration[$"Tenants:{TenantId}:ConnectionString"];
                if (string.IsNullOrEmpty(cs))
                    throw new System.InvalidOperationException(
                        $"ConnectionString not found for tenant '{TenantId}'. " +
                        $"Expected key: Tenants:{TenantId}:ConnectionString");
                return cs;
            }
        }

        public string JwtSecret
        {
            get
            {
                var secret = _configuration[$"Tenants:{TenantId}:JwtSecret"];
                if (string.IsNullOrEmpty(secret))
                    throw new System.InvalidOperationException(
                        $"JwtSecret not found for tenant '{TenantId}'. " +
                        $"Expected key: Tenants:{TenantId}:JwtSecret");
                return secret;
            }
        }
    }
}
