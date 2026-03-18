using Lofn.Domain.Models;
using Lofn.DTO.Store;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lofn.Domain.Interfaces
{
    public interface IStoreService
    {
        Task<IList<StoreInfo>> ListAllAsync();
        Task<IList<StoreInfo>> ListByOwnerAsync(long ownerId);
        Task<StoreModel> GetByIdAsync(long storeId);
        Task<StoreModel> InsertAsync(StoreInfo store, long ownerId);
        Task<StoreModel> UpdateAsync(StoreInfo store);
        Task DeleteAsync(long storeId);
    }
}
