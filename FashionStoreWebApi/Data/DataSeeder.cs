using FashionStoreViewModel.Constants;
using FashionStoreWebApi.Models;
using Microsoft.AspNetCore.Identity;

namespace FashionStoreWebApi.Data
{
    public class DataSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
        }

        private static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            var adminEmail = "admin@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var adminPassword = "Admin@123";
                adminUser = new User
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    PhoneNumber = "1234567890",
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, RoleConstants.ROLE_ADMIN);
                }
            }

            var userEmail = "user@gmail.com";
            var userAccount = await userManager.FindByEmailAsync(userEmail);
            if (userAccount == null)
            {
                var userPassword = "User@123";
                userAccount = new User
                {
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com",
                    FirstName = "User",
                    LastName = "User",
                    PhoneNumber = "1234567890",
                };

                var result = await userManager.CreateAsync(userAccount, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userAccount, RoleConstants.ROLE_USER);
                }
            }
                
        }

        private static async Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            string[] roleNames = { RoleConstants.ROLE_ADMIN, RoleConstants.ROLE_USER };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new Role(roleName));
                }
            }
        }
    }
}
