using FashionStoreViewModel;
using FashionStoreWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandsController(IBrandService brandService) 
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchByName(
            [FromQuery] string? name, 
            [FromQuery] PagingRequest pagingRequest)
        {
            return Ok(await _brandService.SearchByName(name, pagingRequest));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetBrandById(long brandId)
        {
            return Ok(await _brandService.GetBrandById(brandId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] BrandVm brandVm)
        {
            return Ok(await _brandService.CreateBrandAsync(brandVm));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateBrand([FromBody] BrandVm brandVm)
        {
            return Ok(await _brandService.UpdateBrandAsync(brandVm));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{brandId}")]
        public async Task<IActionResult> DeleteBrand(long brandId)
        {
            return Ok(await _brandService.DeleteBrandAsync(brandId));
        }
    }
}
