using FashionStoreWebApi.Data;
using FashionStoreWebApi.Helpers;
using FashionStoreWebApi.Models;
using FashionStoreViewModel;
using Microsoft.EntityFrameworkCore;
using FashionStoreWebApi.Exceptions;

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
            return ConvertVmHelper.ConvertToCategoryVm(category);
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
                throw new EntityNotFoundException("Category", categoryId);
            }
            return ConvertVmHelper.ConvertToCategoryVm(category);
        }

        // Update category details
        public async Task<CategoryVm> UpdateCategoryAsync(CategoryVm categoryVm)
        {
            var category = await _context.Categories.FindAsync(categoryVm.Id);
            if (category == null)
            {
                throw new EntityNotFoundException("Category", categoryVm.Id);
            }

            category.Name = categoryVm.Name;
            category.Description = categoryVm.Description;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return ConvertVmHelper.ConvertToCategoryVm(category);
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
                query = query.OrderBy(p => EF.Property<object>(p, pagingRequest.SortBy));
            }
            else
            {
                query = query.OrderByDescending(p => EF.Property<object>(p, pagingRequest.SortBy));
            }
            if (!string.IsNullOrEmpty(name))
            {
                var lowerCaseName = name.ToLower();
                query = query.Where(b => b.Name.ToLower().Contains(lowerCaseName));
            }

            var categories = await query
                .Select(b => ConvertVmHelper.ConvertToCategoryVm(b))
                .ToListAsync();

            return new PagingData<CategoryVm>(ConvertVmHelper.ExtractItemsPaging(categories, pagingRequest.PageNumber, pagingRequest.PageSize), 
                categories.Count, pagingRequest.PageNumber, pagingRequest.PageSize);
        }
    }
}
