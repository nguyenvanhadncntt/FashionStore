using FashionStoreWebApi.Models.DTOs;
using FashionStoreWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> searchProduct(
            [FromQuery] ProductSearchRequest productSearchRequest, 
            [FromQuery] PagingRequest pagingRequest)
        {
            return Ok(await _productService.searchProductsAsync(productSearchRequest, pagingRequest));
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductDetail(long productId)
        {
            return Ok(await _productService.GetProductByIdAsync(productId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreationDTO productDto)
        {
            return Ok(await _productService.createProductAsync(productDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductCreationDTO productDto)
        {
            return Ok(await _productService.UpdateProduct(productDto));
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(long productId)
        {
            return Ok(await _productService.DeleteProductAsync(productId));
        }

    }
}
