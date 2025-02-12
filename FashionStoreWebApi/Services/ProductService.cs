using FashionStoreWebApi.Data;
using FashionStoreWebApi.Models;
using FashionStoreWebApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreWebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly FashionStoreDbContext _dbContext;
        private readonly IFileStorageService _fileStorageService;
        public ProductService(FashionStoreDbContext dbContext, IFileStorageService fileStorageService)
        {
            _dbContext = dbContext;
            _fileStorageService = fileStorageService;
        }

        public async Task<ProductVm> createProductAsync(ProductCreationDTO productDto)
        {

            var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Id == productDto.BrandId);
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == productDto.CategoryId);

            if (productDto.Image != null)
            {
                productDto.ImageUrl = _fileStorageService.saveImageToPath(productDto.Image);
            }

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = category.Id,
                BrandId = brand.Id,
                StockQuantity = productDto.StockQuantity,
                ImageUrl = productDto.ImageUrl,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Brand = brand,
                Category = category
            };

            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return new ProductVm
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public async Task<bool> DeleteProductAsync(long productId)
        {
            _dbContext.Products.Remove(new Product { Id = productId });
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<ProductCreationDTO> GetProductByIdAsync(long productId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return new ProductCreationDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
            };
        }

        public Task<List<ProductVm>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductVm> UpdateProduct(ProductCreationDTO productDto)
        {
            var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Id == productDto.BrandId);
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == productDto.CategoryId);

            if (productDto.Image != null)
            {
                productDto.ImageUrl = _fileStorageService.saveImageToPath(productDto.Image);
            }

            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productDto.Id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.CategoryId = productDto.CategoryId;
            product.BrandId = productDto.BrandId;
            product.StockQuantity = productDto.StockQuantity;
            product.ImageUrl = productDto.ImageUrl;
            product.UpdatedAt = DateTime.Now;
            product.Brand = brand;
            product.Category = category;
            

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return new ProductVm
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}
