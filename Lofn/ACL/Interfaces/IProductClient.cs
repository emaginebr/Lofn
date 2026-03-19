using Lofn.DTO.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lofn.ACL.Interfaces
{
    public interface IProductClient
    {
        Task<ProductListPagedInfo> SearchAsync(ProductSearchParam param);
        Task<ProductInfo> GetByIdAsync(string storeSlug, long productId);
        Task<ProductInfo> GetBySlugAsync(string productSlug);
        Task<ProductListPagedResult> ListActiveAsync(string storeSlug, string categorySlug = null, int pageNum = 1);
        Task<IList<ProductInfo>> ListFeaturedAsync(string storeSlug, int limit = 10);
        Task<ProductInfo> InsertAsync(string storeSlug, ProductInsertInfo product);
        Task<ProductInfo> UpdateAsync(string storeSlug, ProductUpdateInfo product);
    }
}
