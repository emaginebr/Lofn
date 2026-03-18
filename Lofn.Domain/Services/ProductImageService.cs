using Lofn.Infra.Interfaces.Repository;
using Lofn.Domain.Mappers;
using Lofn.Domain.Models;
using Lofn.Domain.Interfaces;
using Lofn.DTO.Product;
using zTools.ACL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lofn.Domain.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly ITenantResolver _tenantResolver;
        private readonly IFileClient _fileClient;
        private readonly IProductImageRepository<ProductImageModel> _productImageRepository;

        public ProductImageService(
            ITenantResolver tenantResolver,
            IFileClient fileClient,
            IProductImageRepository<ProductImageModel> productImageRepository
        )
        {
            _tenantResolver = tenantResolver;
            _fileClient = fileClient;
            _productImageRepository = productImageRepository;
        }

        public async Task<IList<ProductImageInfo>> ListByProductAsync(long productId)
        {
            var items = await _productImageRepository.ListByProductAsync(productId);
            var result = new List<ProductImageInfo>();
            foreach (var item in items)
            {
                var info = ProductImageMapper.ToInfo(item);
                info.ImageUrl = await _fileClient.GetFileUrlAsync(_tenantResolver.BucketName, item.Image);
                result.Add(info);
            }
            return result;
        }

        public async Task<ProductImageModel> InsertAsync(long productId, string image, int sortOrder)
        {
            if (string.IsNullOrEmpty(image))
            {
                throw new Exception("Image is empty");
            }

            var model = new ProductImageModel
            {
                ProductId = productId,
                Image = image,
                SortOrder = sortOrder
            };

            return await _productImageRepository.InsertAsync(model);
        }

        public async Task<ProductImageModel> UpdateAsync(ProductImageInfo imageInfo)
        {
            var model = new ProductImageModel
            {
                ImageId = imageInfo.ImageId,
                ProductId = imageInfo.ProductId,
                Image = imageInfo.Image,
                SortOrder = imageInfo.SortOrder
            };

            return await _productImageRepository.UpdateAsync(model);
        }

        public async Task DeleteAsync(long imageId)
        {
            await _productImageRepository.DeleteAsync(imageId);
        }
    }
}
