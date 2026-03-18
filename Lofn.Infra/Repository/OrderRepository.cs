using Lofn.Infra.Interfaces.Repository;
using Lofn.Infra.Context;
using Lofn.Infra.Mappers;
using Lofn.Domain.Models;
using Lofn.DTO.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lofn.Infra.Repository
{
    public class OrderRepository : IOrderRepository<OrderModel>
    {
        private const int PAGE_SIZE = 15;
        private readonly LofnContext _context;

        public OrderRepository(LofnContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<OrderModel> Items, int PageCount)> SearchAsync(long storeId, long? userId, long? sellerId, int pageNum)
        {
            var q = _context.Orders.Where(x => x.StoreId == storeId);
            if (userId.HasValue && userId.Value > 0)
            {
                q = q.Where(x => x.UserId == userId.Value);
            }
            if (sellerId.HasValue && sellerId.Value > 0)
            {
                q = q.Where(x => x.SellerId == sellerId.Value);
            }
            var totalCount = await q.CountAsync();
            var pageCount = (int)Math.Ceiling((double)totalCount / PAGE_SIZE);
            var rows = await q
                .Skip((pageNum - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToListAsync();
            return (rows.Select(OrderDbMapper.ToModel), pageCount);
        }

        public async Task<OrderModel> InsertAsync(OrderModel model)
        {
            var row = new Order();
            OrderDbMapper.ToEntity(model, row);
            row.CreatedAt = DateTime.Now;
            row.UpdatedAt = DateTime.Now;
            _context.Add(row);
            await _context.SaveChangesAsync();
            model.OrderId = row.OrderId;
            return model;
        }

        public async Task<OrderModel> UpdateAsync(OrderModel model)
        {
            var row = await _context.Orders.FindAsync(model.OrderId);
            OrderDbMapper.ToEntity(model, row);
            row.UpdatedAt = DateTime.Now;
            _context.Orders.Update(row);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<OrderModel>> ListAsync(long storeId, long userId, int status)
        {
            var q = _context.Orders.AsQueryable();
            if (storeId > 0)
            {
                q = q.Where(x => x.StoreId == storeId);
            }
            if (userId > 0)
            {
                q = q.Where(x => x.UserId == userId);
            }
            if (status > 0)
            {
                q = q.Where(x => x.Status == status);
            }
            var rows = await q.ToListAsync();
            return rows.Select(OrderDbMapper.ToModel);
        }

        public async Task<OrderModel> GetByIdAsync(long id)
        {
            var row = await _context.Orders.FindAsync(id);
            if (row == null)
                return null;
            return OrderDbMapper.ToModel(row);
        }

        public async Task<OrderModel> GetAsync(long productId, long userId, long? sellerId, int status)
        {
            var q = _context.Orders
                .Where(x => x.OrderItems.Any(y => y.ProductId == productId)
                    && x.UserId == userId && x.Status == status);
            if (sellerId.HasValue && sellerId.Value >= 0)
            {
                q = q.Where(x => x.SellerId == sellerId.Value);
            }
            var row = await q.FirstOrDefaultAsync();
            if (row == null)
                return null;
            return OrderDbMapper.ToModel(row);
        }
    }
}
