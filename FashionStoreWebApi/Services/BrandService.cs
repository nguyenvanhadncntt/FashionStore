using FashionStoreViewModel;
using FashionStoreWebApi.Data;
using FashionStoreWebApi.Helpers;
using FashionStoreWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreWebApi.Services
{
    public class BrandService : IBrandService
    {
        private readonly FashionStoreDbContext _context;

        public BrandService(FashionStoreDbContext context)
        {
            _context = context;
        }

        // Create a new brand
        public async Task<BrandVm> CreateBrandAsync(BrandVm brandVm)
        {
            var brand = new Brand
            {
                Name = brandVm.Name,
                Description = brandVm.Description
            };
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            brandVm.Id = brand.Id;
            return brandVm;
        }

        public async Task<PagingData<BrandVm>> SearchByName(string name, PagingRequest pagingRequest)
        {
            var query = _context.Brands.AsQueryable();
            if (pagingRequest.IsAscending)
            {
                query = query.OrderBy(p => EF.Property<object>(p, pagingRequest.SortBy));
            }
            else
            {
                query = query.OrderByDescending(p => EF.Property<object>(p, pagingRequest.SortBy));
            }
            if (!string.IsNullOrEmpty(name))
            {
                var lowerCaseName = name.ToLower();
                query = query.Where(b => b.Name.ToLower().Contains(lowerCaseName));
            }

            var brands = await query
                .Select(b => ConvertVmHelper.ConvertToBrandVm(b))
                .ToListAsync();

            return new PagingData<BrandVm>(brands, brands.Count, pagingRequest.PageNumber, pagingRequest.PageSize);
        }

        // Get brand by ID
        public async Task<BrandVm> GetBrandById(long brandId)
        {
            var brand = await _context.Brands.FindAsync(brandId);
            if (brand == null)
            {
                throw new Exception("Brand not found!");
            }
            return ConvertVmHelper.ConvertToBrandVm(brand);
        }

        // Update brand details
        public async Task<BrandVm> UpdateBrandAsync(BrandVm brandVn)
        {
            var brand = await _context.Brands.FindAsync(brandVn.Id);
            if (brand == null)
                throw new Exception("Brand not found!");

            brand.Name = brandVn.Name;
            brand.Description = brandVn.Description;

            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
            return ConvertVmHelper.ConvertToBrandVm(brand);
        }

        // Delete brand
        public async Task<bool> DeleteBrandAsync(long brandId)
        {
            _context.Brands.Remove(new Brand() { Id = brandId });
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
