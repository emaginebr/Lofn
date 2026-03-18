using Lofn.Domain.Models;
using Lofn.DTO.Order;

namespace Lofn.Domain.Mappers
{
    public static class OrderMapper
    {
        public static OrderInfo ToInfo(OrderModel md)
        {
            return new OrderInfo
            {
                OrderId = md.OrderId,
                StoreId = md.StoreId,
                UserId = md.UserId,
                SellerId = md.SellerId,
                Status = md.Status,
                CreatedAt = md.CreatedAt,
                UpdatedAt = md.UpdatedAt
            };
        }

        public static OrderModel ToModel(OrderInfo dto)
        {
            return new OrderModel
            {
                OrderId = dto.OrderId,
                StoreId = dto.StoreId,
                UserId = dto.UserId,
                SellerId = dto.SellerId,
                Status = dto.Status
            };
        }

        public static OrderItemInfo ToItemInfo(OrderItemModel md)
        {
            return new OrderItemInfo
            {
                ItemId = md.ItemId,
                OrderId = md.OrderId,
                ProductId = md.ProductId,
                Quantity = md.Quantity
            };
        }

        public static OrderItemModel ToItemModel(OrderItemInfo dto, long orderId)
        {
            return new OrderItemModel
            {
                OrderId = orderId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };
        }
    }
}
