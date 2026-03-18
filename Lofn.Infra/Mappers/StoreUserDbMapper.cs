using Lofn.Domain.Models;
using Lofn.Infra.Context;

namespace Lofn.Infra.Mappers
{
    public static class StoreUserDbMapper
    {
        public static StoreUserModel ToModel(StoreUser row)
        {
            return new StoreUserModel
            {
                StoreUserId = row.StoreUserId,
                StoreId = row.StoreId,
                UserId = row.UserId
            };
        }
    }
}
