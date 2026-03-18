using Lofn.Domain.Models;
using Lofn.DTO.Store;

namespace Lofn.Domain.Mappers
{
    public static class StoreMapper
    {
        public static StoreInfo ToInfo(StoreModel md)
        {
            return new StoreInfo
            {
                StoreId = md.StoreId,
                Slug = md.Slug,
                Name = md.Name,
                OwnerId = md.OwnerId
            };
        }

        public static StoreModel ToInsertModel(StoreInsertInfo dto, long ownerId)
        {
            return new StoreModel
            {
                Name = dto.Name,
                OwnerId = ownerId
            };
        }

        public static StoreModel ToUpdateModel(StoreUpdateInfo dto, long ownerId)
        {
            return new StoreModel
            {
                StoreId = dto.StoreId,
                Name = dto.Name,
                OwnerId = ownerId
            };
        }
    }
}
