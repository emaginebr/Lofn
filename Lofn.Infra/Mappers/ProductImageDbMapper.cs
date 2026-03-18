using Lofn.Domain.Models;
using Lofn.Infra.Context;

namespace Lofn.Infra.Mappers
{
    public static class ProductImageDbMapper
    {
        public static ProductImageModel ToModel(ProductImage row)
        {
            return new ProductImageModel
            {
                ImageId = row.ImageId,
                ProductId = row.ProductId,
                Image = row.Image,
                SortOrder = row.SortOrder
            };
        }

        public static void ToEntity(ProductImageModel md, ProductImage row)
        {
            row.ImageId = md.ImageId;
            row.ProductId = md.ProductId;
            row.Image = md.Image;
            row.SortOrder = md.SortOrder;
        }
    }
}
