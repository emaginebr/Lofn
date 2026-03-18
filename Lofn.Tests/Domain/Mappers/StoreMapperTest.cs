using Xunit;
using Lofn.Domain.Mappers;
using Lofn.Domain.Models;
using Lofn.DTO.Store;

namespace Lofn.Tests.Domain.Mappers
{
    public class StoreMapperTest
    {
        [Fact]
        public void ToInfo_ShouldMapAllProperties()
        {
            var model = new StoreModel
            {
                StoreId = 1,
                Slug = "minha-loja",
                Name = "Minha Loja",
                OwnerId = 5
            };

            var info = StoreMapper.ToInfo(model);

            Assert.Equal(1, info.StoreId);
            Assert.Equal("minha-loja", info.Slug);
            Assert.Equal("Minha Loja", info.Name);
            Assert.Equal(5, info.OwnerId);
        }

        [Fact]
        public void ToInsertModel_ShouldMapNameAndOwnerId()
        {
            var dto = new StoreInsertInfo { Name = "Nova Loja" };

            var model = StoreMapper.ToInsertModel(dto, 3);

            Assert.Equal("Nova Loja", model.Name);
            Assert.Equal(3, model.OwnerId);
            Assert.Equal(0, model.StoreId);
        }

        [Fact]
        public void ToUpdateModel_ShouldMapAllProperties()
        {
            var dto = new StoreUpdateInfo { StoreId = 10, Name = "Atualizada" };

            var model = StoreMapper.ToUpdateModel(dto, 3);

            Assert.Equal(10, model.StoreId);
            Assert.Equal("Atualizada", model.Name);
            Assert.Equal(3, model.OwnerId);
        }
    }
}
