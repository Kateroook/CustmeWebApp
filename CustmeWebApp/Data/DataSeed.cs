using CustmeWebApp.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CustmeWebApp.Data;
using CustmeWebApp.Models;
using static System.Net.WebRequestMethods;

namespace CustmeWebApp.Data
{
    public class DataSeed
    {
        //public static async Task SeedDefaultData(IServiceProvider service)
        //{
        //    var userManager = service.GetService<UserManager<IdentityUser>>();
        //    var roleManager = service.GetService<RoleManager<IdentityRole>>();

        //    await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        //    await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));


        //    var admin = new IdentityUser()
        //    { 
        //        UserName = "admin@gmail.com",
        //        Email = "admin@gmail.com",               
        //        EmailConfirmed = true,
        //    };

        //    var userInDb = await userManager.FindByEmailAsync(admin.Email);
        //    if (userInDb is null)
        //    {
        //        await userManager.CreateAsync(admin, "Admin@123");
        //        await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
        //    }
        //}

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>())) 
            {
                if (context.Services.Any())
                {
                    return;
                }

                context.Services.AddRange(
                    new Service
                    {
                        Name = "Розпис",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",                       
                        ImageUrl = "https://i.pinimg.com/736x/11/97/69/11976974e0e5facc8643016756ceecb2.jpg"
                    },
                    new Service
                    {
                        Name = "Вишивка",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        
                        ImageUrl = "https://i.pinimg.com/736x/c7/a8/4d/c7a84df9508e99720d102bea8ba21d6b.jpg"
                    },

                    new Service
                    {
                        Name = "Ваша ідея",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        ImageUrl = "https://i.pinimg.com/736x/ee/07/cd/ee07cd90bb3d43d92e0515929ebc2567.jpg"
                    }
                    );
                if (context.Projects.Any())
                {
                    return;
                }
                context.Projects.AddRange(
                    new Project
                    {
                        Title = "Сорочка " + '"' + "Етно" + '"',,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        DateCompleted = new DateTime(2024,09,11),
                        ImagesUrl = "https://i.pinimg.com/736x/bf/72/86/bf72866fb83ca7cede291614555110e5.jpg",
                        Price = 1000,
                        //ServiceId = 1
                    },
                    new Project
                    {
                        Title = "Футболка " + '"' + "Етно" + '"',
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        DateCompleted = new DateTime(2024, 09, 11),
                        ImagesUrl = "https://i.pinimg.com/736x/b0/ee/78/b0ee786bf6dd86ebde6e5c9025b8a968.jpg",
                        Price = 1200,
                       // ServiceId = 1
                    },
                    new Project
                    {
                        Title = "The soul fighter",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        DateCompleted = new DateTime(2024,08,04),
                        ImagesUrl = "https://i.pinimg.com/736x/ab/90/46/ab904682e8b76fb5022960dea0a8c833.jpg",
                        Price = 1000,
                       // ServiceId = 3
                    },             
                    new Project
                    {
                        Title = "Качки",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        DateCompleted = new DateTime(2024, 08, 30),
                        ImagesUrl = "https://i.pinimg.com/736x/11/97/69/11976974e0e5facc8643016756ceecb2.jpg",
                        Price = 700,
                       // ServiceId = 1
                    }
                    );
            }
        }
    }
}
