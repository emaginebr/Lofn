using Lofn.Domain.Models;
using Lofn.DTO.Product;
using Lofn.Infra.Context;

namespace Lofn.Infra.Mappers
{
    public static class ProductDbMapper
    {
        public static ProductModel ToModel(Product row)
        {
            return new ProductModel
            {
                ProductId = row.ProductId,
                UserId = row.UserId,
                StoreId = row.StoreId,
                Name = row.Name,
                Slug = row.Slug,
                Image = row.Image,
                Description = row.Description,
                Price = row.Price,
                Frequency = row.Frequency,
                Limit = row.Limit,
                Status = (ProductStatusEnum)row.Status,
                CategoryId = row.CategoryId
            };
        }

        public static void ToEntity(ProductModel md, Product row)
        {
            row.ProductId = md.ProductId;
            row.UserId = md.UserId;
            row.StoreId = md.StoreId;
            row.Name = md.Name;
            row.Slug = md.Slug;
            row.Image = md.Image;
            row.Description = md.Description;
            row.Price = md.Price;
            row.Frequency = md.Frequency;
            row.Limit = md.Limit;
            row.Status = (int)md.Status;
            row.CategoryId = md.CategoryId;
        }
    }
}
