using Core.Domain.Repository;
using DB.Infra.Context;
using NSales.Domain.Impl.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Infra.Repository
{
    public class OrderItemRepository : IOrderItemRepository<OrderItemModel>
    {
        private readonly NSalesContext _context;

        public OrderItemRepository(NSalesContext context)
        {
            _context = context;
        }

        private static OrderItemModel DbToModel(OrderItem row)
        {
            return new OrderItemModel
            {
                ItemId = row.ItemId,
                OrderId = row.OrderId,
                ProductId = row.ProductId,
                Quantity = row.Quantity
            };
        }

        private static void ModelToDb(OrderItemModel md, OrderItem row)
        {
            row.ItemId = md.ItemId;
            row.OrderId = md.OrderId;
            row.ProductId = md.ProductId;
            row.Quantity = md.Quantity;
        }

        public async Task<OrderItemModel> InsertAsync(OrderItemModel model)
        {
            var row = new OrderItem();
            ModelToDb(model, row);
            _context.Add(row);
            await _context.SaveChangesAsync();
            model.ItemId = row.ItemId;
            return model;
        }

        public async Task<IEnumerable<OrderItemModel>> ListByOrderAsync(long orderId)
        {
            var rows = await _context.OrderItems
                .Where(x => x.OrderId == orderId)
                .ToListAsync();
            return rows.Select(DbToModel);
        }
    }
}
