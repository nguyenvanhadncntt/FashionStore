using FashionStoreWebApi.Models;
using FashionStoreWebApi.Data;

namespace FashionStoreWebApi.IntegrationTest
{
    public class SeedingDataHelper
    {
        public static void InitializeDB(FashionStoreDbContext fashionStoreDb)
        {
            fashionStoreDb.Brands.AddRange(SeedBrands());
            fashionStoreDb.Categories.AddRange(SeedCategories());
            fashionStoreDb.Products.AddRange(SeedProducts());
            fashionStoreDb.Roles.AddRange(SeedRoles());
            fashionStoreDb.SaveChanges();
        }

        private static IList<Brand> SeedBrands()
        {
            IList<Brand> brands = new List<Brand>
            {
                new Brand
                {
                    Name = "Nike",
                    Description = "Just Do It"
                },
                new Brand
                {
                    Name = "Adidas",
                    Description = "Impossible is Nothing"
                },
                new Brand
                {
                    Name = "Puma",
                    Description = "Forever Faster"
                }
            };

            return brands;
        }

        private static IList<Category> SeedCategories()
        {
            IList<Category> categories = new List<Category>
            {
                new Category
                {
                    Name = "Shoes",
                    Description = "Footwear"
                },
                new Category
                {
                    Name = "T-Shirts",
                    Description = "Casual Wear"
                },
                new Category
                {
                    Name = "Shorts",
                    Description = "Casual Wear"
                }
            };
            return categories;
        }

        private static IList<Product> SeedProducts()
        {
            IList<Product> products = new List<Product>
            {
                new Product
                {
                    Name = "Air Max 90",
                    Description = "Air Max 90",
                    Price = 100,
                    CategoryId = 1,
                    BrandId = 1,
                    StockQuantity = 100,
                    ImageUrl = "https://www.nike.com/t/air-max-90-shoe-0Zy3Zz/CT4352-103",
                    CreatedAt = System.DateTime.Now,
                    UpdatedAt = System.DateTime.Now,
                },
                new Product
                {
                    Name = "Superstar",
                    Description = "Superstar",
                    Price = 80,
                    CategoryId = 1,
                    BrandId = 2,
                    StockQuantity = 100,
                    ImageUrl = "https://www.adidas.com/us/superstar-shoes/FV2808.html",
                    CreatedAt = System.DateTime.Now,
                    UpdatedAt = System.DateTime.Now,
                },
                new Product
                {
                    Name = "RS-X",
                    Description = "RS-X",
                    Price = 120,
                    CategoryId = 1,
                    BrandId = 3,
                    StockQuantity = 100,
                    ImageUrl = "https://us.puma.com/en/us/pd/rs-x3-super-mens-sneakers/193948.html",
                    CreatedAt = System.DateTime.Now,
                    UpdatedAt = System.DateTime.Now,
                }
            };
            return products;
        }

        private static IList<Role> SeedRoles()
        {
            IList<Role> roles = new List<Role>
            {
                new Role
                {
                    Name = "Admin"
                },
                new Role
                {
                    Name = "User"
                }
            };
            return roles;
        }


    }
}
