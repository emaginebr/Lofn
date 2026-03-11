using NSales.Domain.Impl.Models;
using NSales.DTO.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSales.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductListPagedResult> SearchAsync(ProductSearchInternalParam param);
        Task<IList<ProductModel>> ListByNetworkAsync(long networkId);
        Task<ProductModel> GetByIdAsync(long productId);
        Task<ProductModel> GetBySlugAsync(string productSlug);
        Task<ProductInfo> GetProductInfoAsync(ProductModel product);
        Task<ProductModel> InsertAsync(ProductInfo product, long userId);
        Task<ProductModel> UpdateAsync(ProductInfo product, long userId);
    }
}
