using FashionStoreWebApi.Models;
using FashionStoreWebApi.Models.DTOs;
using FashionStoreWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryVm category)
        {
            return Ok(await _categoryService.CreateCategoryAsync(category));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(long categoryId)
        {
            return Ok(await _categoryService.GetCategoryById(categoryId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryVm updatedCategory)
        {
            return Ok(await _categoryService.UpdateCategoryAsync(updatedCategory));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(long categoryId)
        {
            return Ok(await _categoryService.DeleteCategoryAsync(categoryId));
        }

        [HttpGet]
        public async Task<IActionResult> SearchCategories([FromQuery] string? name, [FromQuery] PagingRequest pagingRequest)
        {
            return Ok(await _categoryService.SearchCategoriesByName(name, pagingRequest));
        }
    }
}
