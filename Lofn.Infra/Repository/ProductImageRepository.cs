using Lofn.Infra.Interfaces.Repository;
using Lofn.Infra.Context;
using Lofn.Infra.Mappers;
using Lofn.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lofn.Infra.Repository
{
    public class ProductImageRepository : IProductImageRepository<ProductImageModel>
    {
        private readonly LofnContext _context;

        public ProductImageRepository(LofnContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductImageModel>> ListByProductAsync(long productId)
        {
            var rows = await _context.ProductImages
                .Where(x => x.ProductId == productId)
                .OrderBy(x => x.SortOrder)
                .ToListAsync();
            return rows.Select(ProductImageDbMapper.ToModel);
        }

        public async Task<ProductImageModel> GetByIdAsync(long imageId)
        {
            var row = await _context.ProductImages.FindAsync(imageId);
            if (row == null)
                return null;
            return ProductImageDbMapper.ToModel(row);
        }

        public async Task<ProductImageModel> InsertAsync(ProductImageModel model)
        {
            var row = new ProductImage();
            ProductImageDbMapper.ToEntity(model, row);
            _context.Add(row);
            await _context.SaveChangesAsync();
            model.ImageId = row.ImageId;
            return model;
        }

        public async Task<ProductImageModel> UpdateAsync(ProductImageModel model)
        {
            var row = await _context.ProductImages.FindAsync(model.ImageId);
            ProductImageDbMapper.ToEntity(model, row);
            _context.ProductImages.Update(row);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task DeleteAsync(long imageId)
        {
            var row = await _context.ProductImages.FindAsync(imageId);
            if (row != null)
            {
                _context.ProductImages.Remove(row);
                await _context.SaveChangesAsync();
            }
        }
    }
}
