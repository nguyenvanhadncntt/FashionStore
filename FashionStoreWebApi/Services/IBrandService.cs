using FashionStoreViewModel;

namespace FashionStoreWebApi.Services
{
    public interface IBrandService
    {
        Task<BrandVm> CreateBrandAsync(BrandVm brandVm);
        Task<PagingData<BrandVm>> SearchByName(string name, PagingRequest pagingRequest);
        Task<BrandVm> GetBrandById(long brandId);
        Task<BrandVm> UpdateBrandAsync(BrandVm brandVm);
        Task<bool> DeleteBrandAsync(long brandId);
    }
}
