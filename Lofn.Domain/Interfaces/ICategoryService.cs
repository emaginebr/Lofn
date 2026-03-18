using Lofn.Domain.Models;
using Lofn.DTO.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lofn.Domain.Interfaces
{
    public interface ICategoryService
    {
        Task<IList<CategoryInfo>> ListAllAsync();
        Task<IList<CategoryInfo>> ListWithProductCountAsync();
        Task<CategoryModel> GetByIdAsync(long categoryId);
        Task<CategoryModel> InsertAsync(CategoryInfo category);
        Task<CategoryModel> UpdateAsync(CategoryInfo category);
        Task DeleteAsync(long categoryId);
    }
}
