using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lofn.Infra.Interfaces.Repository
{
    public interface ICategoryRepository<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> ListAllAsync();
        Task<TModel> GetByIdAsync(long id);
        Task<TModel> GetBySlugAsync(string slug);
        Task<TModel> InsertAsync(TModel model);
        Task<TModel> UpdateAsync(TModel model);
        Task DeleteAsync(long id);
        Task<bool> ExistSlugAsync(long categoryId, string slug);
        Task<IDictionary<long, int>> CountProductsByCategoryAsync();
    }
}
