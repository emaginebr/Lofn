using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Domain.Repository
{
    public interface IOrderItemRepository<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> ListByOrderAsync(long orderId);
        Task<TModel> InsertAsync(TModel model);
    }
}
