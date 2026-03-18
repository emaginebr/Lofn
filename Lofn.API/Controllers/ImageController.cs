using Lofn.Domain.Interfaces;
using Lofn.DTO.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NAuth.ACL.Interfaces;
using zTools.ACL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lofn.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IUserClient _userClient;
        private readonly IProductImageService _productImageService;
        private readonly IFileClient _fileClient;
        private readonly ITenantResolver _tenantResolver;

        public ImageController(
            IUserClient userClient,
            IProductImageService productImageService,
            IFileClient fileClient,
            ITenantResolver tenantResolver
        )
        {
            _userClient = userClient;
            _productImageService = productImageService;
            _fileClient = fileClient;
            _tenantResolver = tenantResolver;
        }

        [Authorize]
        [RequestSizeLimit(100_000_000)]
        [HttpPost("upload/{productId}")]
        public async Task<ActionResult<ProductImageInfo>> Upload(long productId, IFormFile file, [FromQuery] int sortOrder = 0)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded");

                var fileName = await _fileClient.UploadFileAsync(_tenantResolver.BucketName, file);
                var model = await _productImageService.InsertAsync(productId, fileName, sortOrder);
                var imageUrl = await _fileClient.GetFileUrlAsync(_tenantResolver.BucketName, fileName);

                return Ok(new ProductImageInfo
                {
                    ImageId = model.ImageId,
                    ProductId = model.ProductId,
                    Image = model.Image,
                    ImageUrl = imageUrl,
                    SortOrder = model.SortOrder
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("list/{productId}")]
        public async Task<ActionResult<IList<ProductImageInfo>>> List(long productId)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                return Ok(await _productImageService.ListByProductAsync(productId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete/{imageId}")]
        public async Task<IActionResult> Delete(long imageId)
        {
            try
            {
                var userSession = _userClient.GetUserInSession(HttpContext);
                if (userSession == null)
                    return Unauthorized();

                await _productImageService.DeleteAsync(imageId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
