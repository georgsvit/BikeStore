using BikeStore.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace BikeStore.Services
{
    public static class DataInitializer
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("admin.bikestore@gmail.com").Result == null)
            {
                User user = new User()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin.bikestore@gmail.com",
                    EmailConfirmed = true,
                    UserName = "admin.bikestore@gmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssword1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByNameAsync("seller.bikestore@gmail.com").Result == null)
            {
                User user = new User()
                {
                    FirstName = "Seller",
                    LastName = "Seller",
                    Email = "seller.bikestore@gmail.com",
                    EmailConfirmed = true,
                    UserName = "seller.bikestore@gmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssword1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Seller").Wait();
                }
            }
        }
    }
}
