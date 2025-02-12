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

        public async Task<IList<BrandVm>> SearchByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.Brands.Select(b => ConvertToBrandVm(b)).ToListAsync();
            }

            var lowerCaseName = name.ToLower();
            return await _context.Brands
                .Where(b => b.Name.ToLower().Contains(lowerCaseName))
                .Select(b => ConvertToBrandVm(b))
                .ToListAsync();
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
