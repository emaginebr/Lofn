using Lofn.Domain.Models;
using Lofn.DTO.Product;

namespace Lofn.Domain.Mappers
{
    public static class ProductMapper
    {
        public static ProductInfo ToInfo(ProductModel md)
        {
            return new ProductInfo
            {
                ProductId = md.ProductId,
                StoreId = md.StoreId,
                CategoryId = md.CategoryId,
                Name = md.Name,
                Slug = md.Slug,
                Description = md.Description,
                Price = md.Price,
                Discount = md.Discount,
                Frequency = md.Frequency,
                Limit = md.Limit,
                Status = md.Status,
                ProductType = md.ProductType,
                Featured = md.Featured,
                CreatedAt = md.CreatedAt,
                UpdatedAt = md.UpdatedAt
            };
        }

        public static ProductModel ToModel(ProductInfo dto, long userId)
        {
            return new ProductModel
            {
                ProductId = dto.ProductId,
                StoreId = dto.StoreId,
                CategoryId = dto.CategoryId,
                UserId = userId,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Discount = dto.Discount,
                Frequency = dto.Frequency,
                Limit = dto.Limit,
                Status = dto.Status,
                ProductType = dto.ProductType,
                Featured = dto.Featured
            };
        }
    }
}
