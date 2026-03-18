using Lofn.Domain.Interfaces;
using Lofn.DTO.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NAuth.ACL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lofn.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUserClient _userClient;
        private readonly IOrderService _orderService;

        public OrderController(
            IUserClient userClient,
            IOrderService orderService
        )
        {
            _userClient = userClient;
            _orderService = orderService;
        }

        private string GetBearerToken()
        {
            return HttpContext.Request.Headers["Authorization"]
                .FirstOrDefault()?.Replace("Bearer ", "") ?? string.Empty;
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<ActionResult<OrderInfo>> Update([FromBody] OrderInfo order)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                var newOrder = await _orderService.UpdateAsync(order);
                return Ok(await _orderService.GetOrderInfoAsync(newOrder, GetBearerToken()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("search")]
        [Authorize]
        public async Task<ActionResult<OrderListPagedResult>> Search([FromBody] OrderSearchParam param)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                return Ok(await _orderService.SearchAsync(param.StoreId, param.UserId, param.SellerId, param.PageNum, GetBearerToken()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("list")]
        public async Task<ActionResult<IList<OrderInfo>>> List([FromBody] OrderParam param)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                var token = GetBearerToken();
                var orders = await _orderService.ListAsync(param.StoreId, param.UserId, param.Status);
                var orderInfos = new List<OrderInfo>();
                foreach (var x in orders)
                {
                    orderInfos.Add(await _orderService.GetOrderInfoAsync(x, token));
                }
                return Ok(orderInfos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("getById/{orderId}")]
        public async Task<ActionResult<OrderInfo>> GetById(long orderId)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                var order = await _orderService.GetByIdAsync(orderId);
                if (order == null)
                    return NotFound();

                return Ok(await _orderService.GetOrderInfoAsync(order, GetBearerToken()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
