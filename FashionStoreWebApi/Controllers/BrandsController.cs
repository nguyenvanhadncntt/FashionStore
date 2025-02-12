using FashionStoreWebApi.Models.DTOs;
using FashionStoreWebApi.Services;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            return Ok(await _brandService.SearchByName(name));
        }

        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetBrandById(long brandId)
        {
            return Ok(await _brandService.GetBrandById(brandId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] BrandVm brandVm)
        {
            return Ok(await _brandService.CreateBrandAsync(brandVm));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand([FromBody] BrandVm brandVm)
        {
            return Ok(await _brandService.UpdateBrandAsync(brandVm));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBrand(long brandId)
        {
            return Ok(await _brandService.DeleteBrandAsync(brandId));
        }
    }
}
