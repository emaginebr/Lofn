using Lofn.Domain.Interfaces;
using Lofn.Domain.Mappers;
using Lofn.DTO.Category;
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
    public class CategoryController : ControllerBase
    {
        private readonly IUserClient _userClient;
        private readonly ICategoryService _categoryService;

        public CategoryController(
            IUserClient userClient,
            ICategoryService categoryService
        )
        {
            _userClient = userClient;
            _categoryService = categoryService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<IList<CategoryInfo>>> List()
        {
            try
            {
                return Ok(await _categoryService.ListWithProductCountAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("getById/{categoryId}")]
        public async Task<ActionResult<CategoryInfo>> GetById(long categoryId)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                var model = await _categoryService.GetByIdAsync(categoryId);
                if (model == null)
                    return NotFound();

                return Ok(CategoryMapper.ToInfo(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("insert")]
        public async Task<ActionResult<CategoryInfo>> Insert([FromBody] CategoryInfo category)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                var model = await _categoryService.InsertAsync(category);
                return Ok(CategoryMapper.ToInfo(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<ActionResult<CategoryInfo>> Update([FromBody] CategoryInfo category)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                var model = await _categoryService.UpdateAsync(category);
                return Ok(CategoryMapper.ToInfo(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete/{categoryId}")]
        public async Task<IActionResult> Delete(long categoryId)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                await _categoryService.DeleteAsync(categoryId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
