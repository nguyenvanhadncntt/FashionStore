using FashionStoreWebApi.Models.DTOs;
using FashionStoreViewModel;

namespace FashionStoreWebApi.Services
{
    public interface IProductService
    {
        Task<ProductVm> GetProductByIdAsync(long productId);
        Task<ProductVm> UpdateProduct(ProductCreationDTO productVm);
        Task<ProductVm> createProductAsync(ProductCreationDTO productVm);
        Task<bool> DeleteProductAsync(long productId);
        Task<PagingData<ProductVm>> searchProductsAsync(ProductSearchRequest searchRequest, PagingRequest pagingRequest);
    }
}
