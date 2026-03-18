using Lofn.Domain.Models;
using Lofn.DTO.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lofn.Domain.Interfaces
{
    public interface IProductImageService
    {
        Task<IList<ProductImageInfo>> ListByProductAsync(long productId);
        Task<ProductImageModel> InsertAsync(long productId, string image, int sortOrder);
        Task<ProductImageModel> UpdateAsync(ProductImageInfo imageInfo);
        Task DeleteAsync(long imageId);
    }
}
