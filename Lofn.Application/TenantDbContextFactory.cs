using Lofn.Domain.Interfaces;
using Lofn.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Lofn.Application
{
    public class TenantDbContextFactory
    {
        private readonly ITenantResolver _tenantResolver;

        public TenantDbContextFactory(ITenantResolver tenantResolver)
        {
            _tenantResolver = tenantResolver;
        }

        public LofnContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LofnContext>();
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_tenantResolver.ConnectionString);
            return new LofnContext(optionsBuilder.Options);
        }
    }
}
