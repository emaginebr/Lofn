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
                Name = md.Name,
                OwnerId = md.OwnerId
            };
        }

        public static StoreModel ToModel(StoreInfo dto, long ownerId)
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
