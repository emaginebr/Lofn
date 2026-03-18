using Lofn.Infra.Interfaces.Repository;
using Lofn.Domain.Mappers;
using Lofn.Domain.Models;
using Lofn.Domain.Interfaces;
using Lofn.DTO.Category;
using zTools.ACL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lofn.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IStringClient _stringClient;
        private readonly ICategoryRepository<CategoryModel> _categoryRepository;

        public CategoryService(
            IStringClient stringClient,
            ICategoryRepository<CategoryModel> categoryRepository
        )
        {
            _stringClient = stringClient;
            _categoryRepository = categoryRepository;
        }

        public async Task<IList<CategoryInfo>> ListAllAsync()
        {
            var items = await _categoryRepository.ListAllAsync();
            return items.Select(CategoryMapper.ToInfo).ToList();
        }

        public async Task<IList<CategoryInfo>> ListWithProductCountAsync()
        {
            var items = await _categoryRepository.ListAllAsync();
            var counts = await _categoryRepository.CountProductsByCategoryAsync();
            return items.Select(x =>
            {
                var info = CategoryMapper.ToInfo(x);
                info.ProductCount = counts.ContainsKey(x.CategoryId) ? counts[x.CategoryId] : 0;
                return info;
            }).ToList();
        }

        public async Task<CategoryModel> GetByIdAsync(long categoryId)
        {
            return await _categoryRepository.GetByIdAsync(categoryId);
        }

        private async Task<string> GenerateSlugAsync(long categoryId, string slug, string name)
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
            } while (await _categoryRepository.ExistSlugAsync(categoryId, newSlug));
            return newSlug;
        }

        public async Task<CategoryModel> InsertAsync(CategoryInfo category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                throw new Exception("Name is empty");
            }

            var model = CategoryMapper.ToModel(category);
            model.Slug = await GenerateSlugAsync(0, category.Slug, category.Name);

            return await _categoryRepository.InsertAsync(model);
        }

        public async Task<CategoryModel> UpdateAsync(CategoryInfo category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                throw new Exception("Name is empty");
            }

            var model = CategoryMapper.ToModel(category);
            model.Slug = await GenerateSlugAsync(category.CategoryId, category.Slug, category.Name);

            return await _categoryRepository.UpdateAsync(model);
        }

        public async Task DeleteAsync(long categoryId)
        {
            await _categoryRepository.DeleteAsync(categoryId);
        }
    }
}
