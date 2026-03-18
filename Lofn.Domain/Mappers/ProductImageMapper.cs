using Lofn.Domain.Models;
using Lofn.DTO.Product;

namespace Lofn.Domain.Mappers
{
    public static class ProductImageMapper
    {
        public static ProductImageInfo ToInfo(ProductImageModel md)
        {
            return new ProductImageInfo
            {
                ImageId = md.ImageId,
                ProductId = md.ProductId,
                Image = md.Image,
                SortOrder = md.SortOrder
            };
        }
    }
}
