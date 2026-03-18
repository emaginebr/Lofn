using Lofn.Domain.Models;
using Lofn.Infra.Context;

namespace Lofn.Infra.Mappers
{
    public static class StoreDbMapper
    {
        public static StoreModel ToModel(Store row)
        {
            return new StoreModel
            {
                StoreId = row.StoreId,
                Name = row.Name,
                OwnerId = row.OwnerId
            };
        }

        public static void ToEntity(StoreModel md, Store row)
        {
            row.StoreId = md.StoreId;
            row.Name = md.Name;
            row.OwnerId = md.OwnerId;
        }
    }
}
