using FashionStoreWebApi.Models.DTOs;

namespace FashionStoreWebApi.Services
{
    public interface IProductService
    {
        Task<List<ProductVm>> GetProductsAsync();
        Task<ProductCreationDTO> GetProductByIdAsync(long productId);
        Task<ProductVm> UpdateProduct(ProductCreationDTO productVm);
        Task<ProductVm> createProductAsync(ProductCreationDTO productVm);
        Task<bool> DeleteProductAsync(long productId);
    }
}
