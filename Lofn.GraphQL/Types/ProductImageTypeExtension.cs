using HotChocolate.Types;
using Lofn.Domain.Interfaces;
using Lofn.Infra.Context;
using zTools.ACL.Interfaces;

namespace Lofn.GraphQL.Types;

public class ProductImageTypeExtension : ObjectTypeExtension<ProductImage>
{
    protected override void Configure(IObjectTypeDescriptor<ProductImage> descriptor)
    {
        descriptor.Field(t => t.Image).IsProjected(true);

        descriptor
            .Field("imageUrl")
            .Type<StringType>()
            .Resolve(async ctx =>
            {
                var productImage = ctx.Parent<ProductImage>();
                if (string.IsNullOrEmpty(productImage.Image)) return null;
                var fileClient = ctx.Service<IFileClient>();
                var tenantResolver = ctx.Service<ITenantResolver>();
                return await fileClient.GetFileUrlAsync(tenantResolver.BucketName, productImage.Image);
            });
    }
}
