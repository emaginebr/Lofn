using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lofn.Infra.Interfaces.Repository
{
    public interface IProductImageRepository<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> ListByProductAsync(long productId);
        Task<TModel> GetByIdAsync(long imageId);
        Task<TModel> InsertAsync(TModel model);
        Task<TModel> UpdateAsync(TModel model);
        Task DeleteAsync(long imageId);
    }
}
