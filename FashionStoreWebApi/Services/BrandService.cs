using FashionStoreWebApi.Data;
using FashionStoreWebApi.Models;
using FashionStoreWebApi.Models.DTOs;
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
                query = query.OrderBy(p => EF.Property<object>(p, pagingRequest.SortBy ?? "Id"));
            }
            else
            {
                query = query.OrderByDescending(p => EF.Property<object>(p, pagingRequest.SortBy ?? "Id"));
            }
            if (!string.IsNullOrEmpty(name))
            {
                var lowerCaseName = name.ToLower();
                query = query.Where(b => b.Name.ToLower().Contains(lowerCaseName));
            }

            var brands = await query
                .Select(b => ConvertToBrandVm(b))
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
            return ConvertToBrandVm(brand);
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
            return ConvertToBrandVm(brand);
        }

        // Delete brand
        public async Task<bool> DeleteBrandAsync(long brandId)
        {
            _context.Brands.Remove(new Brand() { Id = brandId });
            return await _context.SaveChangesAsync() > 0;
        }

        private BrandVm ConvertToBrandVm(Brand? brand)
        {
            return new BrandVm
            {
                Id = brand.Id,
                Name = brand.Name,
                Description = brand.Description
            };
        }
    }
}
