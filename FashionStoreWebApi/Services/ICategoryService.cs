using FashionStoreWebApi.Models.DTOs;

namespace FashionStoreWebApi.Services
{
    public interface ICategoryService
    {
        Task<CategoryVm> CreateCategoryAsync(CategoryVm categoryVm);
        Task<IList<CategoryVm>> SearchCategoriesByName(string name);
        Task<CategoryVm> GetCategoryById(long categoryId);
        Task<CategoryVm> UpdateCategoryAsync(CategoryVm categoryVm);
        Task<bool> DeleteCategoryAsync(long categoryId);
    }
}
