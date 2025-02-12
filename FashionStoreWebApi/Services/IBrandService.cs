using FashionStoreWebApi.Models.DTOs;

namespace FashionStoreWebApi.Services
{
    public interface IBrandService
    {
        Task<BrandVm> CreateBrandAsync(BrandVm brandVm);
        Task<IList<BrandVm>> SearchByName(string name);
        Task<BrandVm> GetBrandById(long brandId);
        Task<BrandVm> UpdateBrandAsync(BrandVm brandVm);
        Task<bool> DeleteBrandAsync(long brandId);
    }
}
