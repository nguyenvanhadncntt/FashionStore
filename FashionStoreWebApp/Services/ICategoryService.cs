using FashionStoreViewModel;

namespace FashionStoreWebApp.Services
{
    public interface ICategoryService
    {
        Task<CategoryVm> GetCategoryByIdAsync(long id);
        Task<PagingData<CategoryVm>> GetCategoriesAsync(string name, int pageNumber, int pageSize);
        Task<FormResult> CreateCategoryAsync(CategoryVm categoryVm);
        Task<FormResult> DeleteCategoryAsync(long categoryId);
        Task<FormResult> UpdateCategoryAsync(CategoryVm categoryVm);
    }
}
