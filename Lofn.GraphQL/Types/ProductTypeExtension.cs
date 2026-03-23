using System.Linq;
using HotChocolate.Types;
using Lofn.Domain.Interfaces;
using Lofn.Infra.Context;
using zTools.ACL.Interfaces;

namespace Lofn.GraphQL.Types;

public class ProductTypeExtension : ObjectTypeExtension<Product>
{
    protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
    {
        descriptor.Field(t => t.ProductImages).IsProjected(true);

        descriptor
            .Field("imageUrl")
            .Type<StringType>()
            .Resolve(async ctx =>
            {
                var product = ctx.Parent<Product>();
                var firstImage = product.ProductImages?
                    .OrderBy(i => i.SortOrder)
                    .FirstOrDefault();

                if (firstImage == null || string.IsNullOrEmpty(firstImage.Image))
                    return null;

                var fileClient = ctx.Service<IFileClient>();
                var tenantResolver = ctx.Service<ITenantResolver>();
                return await fileClient.GetFileUrlAsync(tenantResolver.BucketName, firstImage.Image);
            });
    }
}
