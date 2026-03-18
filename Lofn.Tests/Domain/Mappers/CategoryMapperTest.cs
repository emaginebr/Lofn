using Xunit;
using Lofn.Domain.Mappers;
using Lofn.Domain.Models;
using Lofn.DTO.Category;

namespace Lofn.Tests.Domain.Mappers
{
    public class CategoryMapperTest
    {
        [Fact]
        public void ToInfo_ShouldMapAllProperties()
        {
            var model = new CategoryModel
            {
                CategoryId = 1,
                Slug = "eletronicos",
                Name = "Eletrônicos",
                StoreId = 5
            };

            var info = CategoryMapper.ToInfo(model);

            Assert.Equal(1, info.CategoryId);
            Assert.Equal("eletronicos", info.Slug);
            Assert.Equal("Eletrônicos", info.Name);
            Assert.Equal(5, info.StoreId);
        }

        [Fact]
        public void ToModel_ShouldMapAllProperties()
        {
            var info = new CategoryInfo
            {
                CategoryId = 1,
                Name = "Eletrônicos",
                StoreId = 5
            };

            var model = CategoryMapper.ToModel(info);

            Assert.Equal(1, model.CategoryId);
            Assert.Equal("Eletrônicos", model.Name);
            Assert.Equal(5, model.StoreId);
        }
    }
}
