using Lofn.Domain.Interfaces;
using Lofn.DTO.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NAuth.ACL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lofn.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StoreUserController : ControllerBase
    {
        private readonly IUserClient _userClient;
        private readonly IStoreService _storeService;
        private readonly IStoreUserService _storeUserService;

        public StoreUserController(
            IUserClient userClient,
            IStoreService storeService,
            IStoreUserService storeUserService
        )
        {
            _userClient = userClient;
            _storeService = storeService;
            _storeUserService = storeUserService;
        }

        private string GetBearerToken()
        {
            return HttpContext.Request.Headers["Authorization"]
                .FirstOrDefault()?.Replace("Bearer ", "") ?? string.Empty;
        }

        [Authorize]
        [HttpGet("{storeSlug}/list")]
        public async Task<ActionResult<IList<StoreUserInfo>>> List(string storeSlug)
        {
            var userSession = _userClient.GetUserInSession(HttpContext);
            if (userSession == null)
                return Unauthorized();

            var store = await _storeService.GetBySlugAsync(storeSlug);
            if (store == null)
                return NotFound("Store not found");

            return Ok(await _storeUserService.ListByStoreAsync(store.StoreId, userSession.UserId, GetBearerToken()));
        }

        [Authorize]
        [HttpPost("{storeSlug}/insert")]
        public async Task<ActionResult<StoreUserInfo>> Insert(string storeSlug, [FromBody] StoreUserInsertInfo storeUser)
        {
            var userSession = _userClient.GetUserInSession(HttpContext);
            if (userSession == null)
                return Unauthorized();

            var store = await _storeService.GetBySlugAsync(storeSlug);
            if (store == null)
                return NotFound("Store not found");

            var result = await _storeUserService.InsertAsync(store.StoreId, storeUser.UserId, userSession.UserId, GetBearerToken());
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{storeSlug}/delete/{storeUserId}")]
        public async Task<IActionResult> Delete(string storeSlug, long storeUserId)
        {
            var userSession = _userClient.GetUserInSession(HttpContext);
            if (userSession == null)
                return Unauthorized();

            var store = await _storeService.GetBySlugAsync(storeSlug);
            if (store == null)
                return NotFound("Store not found");

            await _storeUserService.DeleteAsync(store.StoreId, storeUserId, userSession.UserId);
            return NoContent();
        }
    }
}
