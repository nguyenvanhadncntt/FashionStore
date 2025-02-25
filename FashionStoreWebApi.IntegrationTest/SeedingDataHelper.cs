using FashionStoreWebApi.Models;
using FashionStoreWebApi.Data;
using Microsoft.AspNetCore.Identity;

namespace FashionStoreWebApi.IntegrationTest
{
    public class SeedingDataHelper
    {
        public static void InitializeDB(FashionStoreDbContext fashionStoreDb, RoleManager<Role> roleManager)
        {
            fashionStoreDb.Brands.AddRange(SeedBrands());
            fashionStoreDb.Categories.AddRange(SeedCategories());
            fashionStoreDb.Products.AddRange(SeedProducts());
            fashionStoreDb.Roles.AddRange(SeedRoles());
            fashionStoreDb.Users.AddRange(SeedUsers());
            fashionStoreDb.SaveChanges();
            AddRoles(roleManager);
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

        private static void AddRoles(RoleManager<Role> roleManager)
        {
            roleManager.CreateAsync(new Role("Admin-Policy")).Wait();
            roleManager.CreateAsync(new Role("User-Policy")).Wait();
            roleManager.CreateAsync(new Role("New-User-Policy")).Wait();
        }

        private static IList<Role> SeedRoles()
        {
            return new List<Role>()
            {
                new Role
                {
                    Id = "f53d4d27-1c1f-47ee-9e72-0aaf95535b2f",
                    Name = "Admin"
                },
                new Role
                {
                    Id = "af3ff471-ee3b-4de1-a09a-2c1e7f33f918",
                    Name = "User"
                },
                new Role
                {
                    Id = "f53d4d27-1c1f-47ee-9e72-0aaf95535b3d", 
                    Name = "Super_User"
                }
            };
        }

        private static IList<User> SeedUsers()
        {
            return new List<User>()
            {
                new User
                {
                    Id = "4578316a-c937-4fe0-85d2-1d4f488f3a58",
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "admin@gmail.com",
                    PhoneNumber = "0123456789",
                    PasswordHash = "AQAAAAEAACcQAAAAEJ9Zz5Z6"
                },
                new User
                {
                    Id = "7848f387-81a9-4757-91f4-f74e10d69f46",
                    Email = "user@gmail.com",
                    FirstName = "User",
                    LastName = "User",
                    UserName = "user@gmail.com",
                    PhoneNumber = "0123456789",
                    PasswordHash = "AQAAAA"
                },
                new User
                {
                    Id = "7848f387-81a9-4757-91f4-f74e10d69c15",
                    Email = "superuser@gmail.com",
                    FirstName = "Super",
                    LastName = "User",
                    UserName = "superuser@gmail.com",
                    PhoneNumber = "0123456789",
                    PasswordHash = "AQAAAACCCDDD"
                }
            };
        }

        }
}
