using FashionStoreViewModel;

namespace FashionStoreWebApp.Services
{
    public interface IBrandService
    {
        Task<PagingData<BrandVm>> GetBrands(string name, int pageIndex, int pageSize);
        Task<FormResult> Delete(long id);
        Task<BrandVm> GetBrandByIdAsync(long id);
        Task<FormResult> CreateBrandAsync(BrandVm brandVm);
        Task<FormResult> UpdateBrandAsync(BrandVm brandVm);
    }
}
