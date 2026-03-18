using Lofn.Domain.Models;
using Lofn.DTO.Store;

namespace Lofn.Domain.Mappers
{
    public static class StoreUserMapper
    {
        public static StoreUserInfo ToInfo(StoreUserModel md)
        {
            return new StoreUserInfo
            {
                StoreUserId = md.StoreUserId,
                StoreId = md.StoreId,
                UserId = md.UserId
            };
        }
    }
}
