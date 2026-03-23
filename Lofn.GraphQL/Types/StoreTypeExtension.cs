using HotChocolate.Types;
using Lofn.Domain.Interfaces;
using Lofn.Infra.Context;
using zTools.ACL.Interfaces;

namespace Lofn.GraphQL.Types;

public class StoreTypeExtension : ObjectTypeExtension<Store>
{
    protected override void Configure(IObjectTypeDescriptor<Store> descriptor)
    {
        descriptor.Field(t => t.Logo).IsProjected(true);

        descriptor
            .Field("logoUrl")
            .Type<StringType>()
            .Resolve(async ctx =>
            {
                var store = ctx.Parent<Store>();
                if (string.IsNullOrEmpty(store.Logo)) return null;
                var fileClient = ctx.Service<IFileClient>();
                var tenantResolver = ctx.Service<ITenantResolver>();
                return await fileClient.GetFileUrlAsync(tenantResolver.BucketName, store.Logo);
            });
    }
}
