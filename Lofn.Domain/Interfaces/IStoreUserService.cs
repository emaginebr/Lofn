using Lofn.DTO.Store;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lofn.Domain.Interfaces
{
    public interface IStoreUserService
    {
        Task<IList<StoreUserInfo>> ListByStoreAsync(long storeId, long ownerId, string token);
        Task<StoreUserInfo> InsertAsync(long storeId, long userId, long ownerId, string token);
        Task DeleteAsync(long storeId, long storeUserId, long ownerId);
    }
}
