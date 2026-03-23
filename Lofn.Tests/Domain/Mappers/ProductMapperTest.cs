using Xunit;
using Lofn.Domain.Mappers;
using Lofn.Domain.Models;
using Lofn.DTO.Product;

namespace Lofn.Tests.Domain.Mappers
{
    public class ProductMapperTest
    {
        [Fact]
        public void ToInfo_ShouldMapAllProperties()
        {
            var model = new ProductModel
            {
                ProductId = 1,
                StoreId = 2,
                CategoryId = 3,
                Name = "Produto",
                Slug = "produto",
                Description = "Desc",
                Price = 99.90,
                Frequency = 1,
                Limit = 10,
                Status = ProductStatusEnum.Active
            };

            var info = ProductMapper.ToInfo(model);

            Assert.Equal(1, info.ProductId);
            Assert.Equal(2, info.StoreId);
            Assert.Equal(3, info.CategoryId);
            Assert.Equal("Produto", info.Name);
            Assert.Equal("produto", info.Slug);
            Assert.Equal("Desc", info.Description);
            Assert.Equal(99.90, info.Price);
            Assert.Equal(1, info.Frequency);
            Assert.Equal(10, info.Limit);
            Assert.Equal(ProductStatusEnum.Active, info.Status);
        }

        [Fact]
        public void ToModel_ShouldMapAllProperties()
        {
            var info = new ProductInfo
            {
                ProductId = 1,
                StoreId = 2,
                CategoryId = 3,
                Name = "Produto",
                Description = "Desc",
                Price = 49.90,
                Frequency = 0,
                Limit = 5,
                Status = ProductStatusEnum.Inactive
            };

            var model = ProductMapper.ToModel(info, 10);

            Assert.Equal(1, model.ProductId);
            Assert.Equal(2, model.StoreId);
            Assert.Equal(3, model.CategoryId);
            Assert.Equal(10, model.UserId);
            Assert.Equal("Produto", model.Name);
            Assert.Equal("Desc", model.Description);
            Assert.Equal(49.90, model.Price);
            Assert.Equal(0, model.Frequency);
            Assert.Equal(5, model.Limit);
            Assert.Equal(ProductStatusEnum.Inactive, model.Status);
        }
    }
}
