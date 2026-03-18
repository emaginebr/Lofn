using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lofn.Infra.Interfaces.Repository
{
    public interface IStoreUserRepository<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> ListByStoreAsync(long storeId);
        Task<IEnumerable<TModel>> ListByUserAsync(long userId);
        Task<TModel> InsertAsync(TModel model);
        Task DeleteAsync(long storeUserId);
        Task<bool> ExistsAsync(long storeId, long userId);
    }
}
