using FashionStoreViewModel;

namespace FashionStoreWebApp.Services
{
    public interface IBrandService
    {
        Task<PagingData<BrandVm>> getBrands(string name, int pageIndex, int pageSize);
    }
}
