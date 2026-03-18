using Lofn.Infra.Interfaces.Repository;
using NAuth.ACL.Interfaces;
using Lofn.Domain.Mappers;
using Lofn.Domain.Models;
using Lofn.Domain.Interfaces;
using Lofn.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lofn.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUserClient _userClient;
        private readonly IOrderRepository<OrderModel> _orderRepository;
        private readonly IOrderItemRepository<OrderItemModel> _orderItemRepository;
        private readonly IProductService _productService;

        public OrderService(
            IUserClient userClient,
            IOrderRepository<OrderModel> orderRepository,
            IOrderItemRepository<OrderItemModel> orderItemRepository,
            IProductService productService
        )
        {
            _userClient = userClient;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _productService = productService;
        }

        public async Task<OrderModel> InsertAsync(OrderInfo order)
        {
            if (!(order.StoreId > 0))
            {
                throw new Exception("Store is empty");
            }
            if (!(order.UserId > 0))
            {
                throw new Exception("User is empty");
            }
            if (order.Items == null || !order.Items.Any())
            {
                throw new Exception("Order is empty");
            }

            var model = OrderMapper.ToModel(order);
            var newOrder = await _orderRepository.InsertAsync(model);

            foreach (var item in order.Items)
            {
                await _orderItemRepository.InsertAsync(OrderMapper.ToItemModel(item, newOrder.OrderId));
            }

            return newOrder;
        }

        public async Task<OrderModel> UpdateAsync(OrderInfo order)
        {
            if (!(order.OrderId > 0))
            {
                throw new Exception("Order ID is empty");
            }

            var model = await _orderRepository.GetByIdAsync(order.OrderId);
            model.Status = order.Status;
            return await _orderRepository.UpdateAsync(model);
        }

        public async Task<OrderModel> GetByIdAsync(long orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId);
        }

        public async Task<OrderModel> GetAsync(long productId, long userId, long? sellerId, OrderStatusEnum status)
        {
            return await _orderRepository.GetAsync(productId, userId, sellerId, (int)status);
        }

        public async Task<OrderInfo> GetOrderInfoAsync(OrderModel order, string token)
        {
            var items = await _orderItemRepository.ListByOrderAsync(order.OrderId);
            var info = OrderMapper.ToInfo(order);
            info.User = await _userClient.GetByIdAsync(order.UserId, token);
            info.Seller = order.SellerId.HasValue ? await _userClient.GetByIdAsync(order.SellerId.Value, token) : null;
            info.Items = await GetOrderItemInfosAsync(items.ToList());
            return info;
        }

        private async Task<List<OrderItemInfo>> GetOrderItemInfosAsync(List<OrderItemModel> items)
        {
            var result = new List<OrderItemInfo>();
            foreach (var x in items)
            {
                var productModel = await _productService.GetByIdAsync(x.ProductId);
                var product = productModel != null ? await _productService.GetProductInfoAsync(productModel) : null;
                var itemInfo = OrderMapper.ToItemInfo(x);
                itemInfo.Product = product;
                result.Add(itemInfo);
            }
            return result;
        }

        public async Task<IList<OrderModel>> ListAsync(long storeId, long userId, OrderStatusEnum? status)
        {
            var items = await _orderRepository.ListAsync(storeId, userId, status.HasValue ? (int)status : 0);
            return items.ToList();
        }

        public async Task<OrderListPagedResult> SearchAsync(long storeId, long? userId, long? sellerId, int pageNum, string token)
        {
            var (items, pageCount) = await _orderRepository.SearchAsync(storeId, userId, sellerId, pageNum);
            var orders = new List<OrderInfo>();
            foreach (var item in items)
            {
                orders.Add(await GetOrderInfoAsync(item, token));
            }
            return new OrderListPagedResult
            {
                Sucesso = true,
                Orders = orders,
                PageNum = pageNum,
                PageCount = pageCount
            };
        }
    }
}
