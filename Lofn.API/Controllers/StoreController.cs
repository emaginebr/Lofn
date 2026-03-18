using Lofn.Domain.Interfaces;
using Lofn.Domain.Mappers;
using Lofn.DTO.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NAuth.ACL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lofn.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IUserClient _userClient;
        private readonly IStoreService _storeService;

        public StoreController(
            IUserClient userClient,
            IStoreService storeService
        )
        {
            _userClient = userClient;
            _storeService = storeService;
        }

        [Authorize]
        [HttpGet("list")]
        public async Task<ActionResult<IList<StoreInfo>>> List()
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                return Ok(await _storeService.ListByOwnerAsync(userSession.UserId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("getById/{storeId}")]
        public async Task<ActionResult<StoreInfo>> GetById(long storeId)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                var model = await _storeService.GetByIdAsync(storeId);
                if (model == null)
                    return NotFound();

                return Ok(StoreMapper.ToInfo(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("insert")]
        public async Task<ActionResult<StoreInfo>> Insert([FromBody] StoreInsertInfo store)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                var model = await _storeService.InsertAsync(store, userSession.UserId);
                return Ok(StoreMapper.ToInfo(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<ActionResult<StoreInfo>> Update([FromBody] StoreUpdateInfo store)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                var model = await _storeService.UpdateAsync(store, userSession.UserId);
                return Ok(StoreMapper.ToInfo(model));
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete/{storeId}")]
        public async Task<IActionResult> Delete(long storeId)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                await _storeService.DeleteAsync(storeId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
