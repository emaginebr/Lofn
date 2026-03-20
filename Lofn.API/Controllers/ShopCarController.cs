using Lofn.Domain.Interfaces;
using Lofn.DTO.ShopCar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Lofn.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShopCarController : ControllerBase
    {
        private readonly IShopCarService _shopCarService;

        public ShopCarController(IShopCarService shopCarService)
        {
            _shopCarService = shopCarService;
        }

        [Authorize]
        [HttpPost("insert")]
        public async Task<ActionResult<ShopCarInfo>> Insert([FromBody] ShopCarInfo shopCar)
        {
            try
            {
                var result = await _shopCarService.InsertAsync(shopCar);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
