using FashionStoreViewModel;

namespace FashionStoreWebApp.Services
{
    public interface IProductService
    {
        Task<PagingData<ProductVm>> SearchProduct(ProductSearchRequest productSearch, int pageIndex, int pageSize, string sortBy = "Id", bool IsAscending = false);
        Task<FormResult> DeleteProductAsync(long productId);
        Task<ProductVm> GetByIdAsync(long productId);
        Task<FormResult> CreateProductAsync(MultipartFormDataContent productCreation);
        Task<FormResult> UpdateProductAsync(MultipartFormDataContent productDto);
    }
}
