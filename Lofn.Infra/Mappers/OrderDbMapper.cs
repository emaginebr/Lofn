using Lofn.Domain.Models;
using Lofn.DTO.Order;
using Lofn.Infra.Context;

namespace Lofn.Infra.Mappers
{
    public static class OrderDbMapper
    {
        public static OrderModel ToModel(Order row)
        {
            return new OrderModel
            {
                OrderId = row.OrderId,
                StoreId = row.StoreId,
                UserId = row.UserId,
                SellerId = row.SellerId,
                CreatedAt = row.CreatedAt,
                UpdatedAt = row.UpdatedAt,
                Status = (OrderStatusEnum)row.Status
            };
        }

        public static void ToEntity(OrderModel md, Order row)
        {
            row.OrderId = md.OrderId;
            row.StoreId = md.StoreId;
            row.UserId = md.UserId;
            row.SellerId = md.SellerId;
            row.CreatedAt = md.CreatedAt;
            row.UpdatedAt = md.UpdatedAt;
            row.Status = (int)md.Status;
        }
    }
}
