using CustmeWebApp.Constants;
using Microsoft.AspNetCore.Identity;

namespace CustmeWebApp.Data
{
    public class DataSeed
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<IdentityUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));


            var admin = new IdentityUser()
            { 
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",               
                EmailConfirmed = true,
            };

            var userInDb = await userManager.FindByEmailAsync(admin.Email);
            if (userInDb is null)
            {
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }
    }
}
