using FashionStoreViewModel;

namespace FashionStoreWebApp.Services
{
    public interface IProductService
    {
        Task<PagingData<ProductVm>> SearchProduct(string productName, int pageIndex, int pageSize);
        Task<FormResult> DeleteProductAsync(long productId);
        Task<ProductVm> GetByIdAsync(long productId);
        Task<FormResult> CreateProductAsync(MultipartFormDataContent productCreation);
        Task<FormResult> UpdateProductAsync(MultipartFormDataContent productDto);
    }
}
