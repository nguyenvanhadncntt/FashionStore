using FashionStoreWebApi.Data;
using FashionStoreWebApi.Models;
using FashionStoreWebApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreWebApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly FashionStoreDbContext _context;

        public CategoryService(FashionStoreDbContext context)
        {
            _context = context;
        }

        // Create a new category
        public async Task<CategoryVm> CreateCategoryAsync(CategoryVm categoryVm)
        {
            var category = new Category
            {
                Name = categoryVm.Name,
                Description = categoryVm.Description
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return ConvertToCategoryVm(category);
        }

        // Get all categories
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        // Get category by ID
        public async Task<CategoryVm> GetCategoryById(long categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                throw new Exception("Category not found!");
            }
            return ConvertToCategoryVm(category);
        }

        // Update category details
        public async Task<CategoryVm> UpdateCategoryAsync(CategoryVm categoryVm)
        {
            var category = await _context.Categories.FindAsync(categoryVm.Id);
            if (category == null)
            {
                throw new Exception("Category not found!");
            }

            category.Name = categoryVm.Name;
            category.Description = categoryVm.Description;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return ConvertToCategoryVm(category);
        }

        // Delete category
        public async Task<bool> DeleteCategoryAsync(long categoryId)
        {
            _context.Categories.Remove(new Category() { Id = categoryId });
            return await _context.SaveChangesAsync() > 0;
        }

        // Search categories by name (case-insensitive)
        public async Task<PagingData<CategoryVm>> SearchCategoriesByName(string name, PagingRequest pagingRequest)
        {
            var query = _context.Categories.AsQueryable();
            if (pagingRequest.IsAscending)
            {
                query = query.OrderBy(p => EF.Property<object>(p, pagingRequest.SortBy ?? "Id"));
            }
            else
            {
                query = query.OrderByDescending(p => EF.Property<object>(p, pagingRequest.SortBy ?? "Id"));
            }
            if (!string.IsNullOrEmpty(name))
            {
                var lowerCaseName = name.ToLower();
                query = query.Where(b => b.Name.ToLower().Contains(lowerCaseName));
            }

            var categories = await query
                .Select(b => ConvertToCategoryVm(b))
                .ToListAsync();

            return new PagingData<CategoryVm>(categories, categories.Count, pagingRequest.PageNumber, pagingRequest.PageSize);
        }

        private CategoryVm ConvertToCategoryVm(Category? category)
        {
            return new CategoryVm
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
