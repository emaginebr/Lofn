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
    public class ProductService : IProductService
    {
        private readonly ITenantResolver _tenantResolver;
        private readonly IFileClient _fileClient;
        private readonly IStringClient _stringClient;
        private readonly IProductRepository<ProductModel> _productRepository;
        private readonly IProductImageService _productImageService;

        public ProductService(
            ITenantResolver tenantResolver,
            IFileClient fileClient,
            IStringClient stringClient,
            IProductRepository<ProductModel> productRepository,
            IProductImageService productImageService
        )
        {
            _tenantResolver = tenantResolver;
            _fileClient = fileClient;
            _stringClient = stringClient;
            _productRepository = productRepository;
            _productImageService = productImageService;
        }

        public async Task<ProductModel> GetByIdAsync(long productId)
        {
            return await _productRepository.GetByIdAsync(productId);
        }

        public async Task<ProductModel> GetBySlugAsync(string productSlug)
        {
            return await _productRepository.GetBySlugAsync(productSlug);
        }

        public async Task<ProductInfo> GetProductInfoAsync(ProductModel md)
        {
            var info = ProductMapper.ToInfo(md);
            info.ImageUrl = await _fileClient.GetFileUrlAsync(_tenantResolver.BucketName, md.Image);
            info.Images = await _productImageService.ListByProductAsync(md.ProductId);
            return info;
        }

        private async Task<string> GenerateSlugAsync(long productId, string slug, string name)
        {
            string newSlug;
            int c = 0;
            do
            {
                newSlug = await _stringClient.GenerateSlugAsync(!string.IsNullOrEmpty(slug) ? slug : name);
                if (c > 0)
                {
                    newSlug += c.ToString();
                }
                c++;
            } while (await _productRepository.ExistSlugAsync(productId, newSlug));
            return newSlug;
        }

        public async Task<ProductModel> InsertAsync(ProductInfo product, long userId)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                throw new Exception("Name is empty");
            }
            if (!(product.Price > 0))
            {
                throw new Exception("Price cant be 0");
            }

            var model = ProductMapper.ToModel(product, userId);
            model.Slug = await GenerateSlugAsync(product.ProductId, product.Slug, product.Name);

            return await _productRepository.InsertAsync(model);
        }

        public async Task<ProductModel> UpdateAsync(ProductInfo product, long userId)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                throw new Exception("Name is empty");
            }
            if (!(product.Price > 0))
            {
                throw new Exception("Price cant be 0");
            }

            var model = ProductMapper.ToModel(product, userId);
            model.Slug = await GenerateSlugAsync(product.ProductId, product.Slug, product.Name);

            return await _productRepository.UpdateAsync(model);
        }

        public async Task<ProductListPagedResult> SearchAsync(ProductSearchInternalParam param)
        {
            var (items, pageCount) = await _productRepository.SearchAsync(
                param.StoreId <= 0 ? null : param.StoreId,
                param.UserId <= 0 ? null : param.UserId,
                param.Keyword,
                param.OnlyActive,
                param.PageNum
            );

            var products = new List<ProductInfo>();
            foreach (var item in items)
            {
                products.Add(await GetProductInfoAsync(item));
            }

            return new ProductListPagedResult
            {
                Sucesso = true,
                Products = products,
                PageNum = param.PageNum,
                PageCount = pageCount
            };
        }

        public async Task<IList<ProductModel>> ListByStoreAsync(long storeId)
        {
            var items = await _productRepository.ListByStoreAsync(storeId);
            return items.OrderBy(x => x.Price).ToList();
        }
    }
}
