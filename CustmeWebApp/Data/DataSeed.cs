using CustmeWebApp.Constants;
using CustmeWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                if (!(context.Roles.Any()))
                {
                    var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
                    var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

                    roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString())).GetAwaiter().GetResult(); 
                    roleManager.CreateAsync(new IdentityRole(Roles.Owner.ToString())).GetAwaiter().GetResult(); 
                    roleManager.CreateAsync(new IdentityRole(Roles.User.ToString())).GetAwaiter().GetResult(); ;
                    var admin = new IdentityUser()
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        EmailConfirmed = true,
                    };

                    var userInDb = userManager.FindByEmailAsync(admin.Email).GetAwaiter().GetResult(); ;
                    if (userInDb is null)
                    {
                        userManager.CreateAsync(admin, "Admin@123").GetAwaiter().GetResult(); ;
                        userManager.AddToRoleAsync(admin, Roles.Admin.ToString()).GetAwaiter().GetResult(); ;
                    }

                    var owner = new IdentityUser()
                    {
                        UserName = "severyna.kate@gmail.com",
                        Email = "severyna.kate@gmail.com",
                        EmailConfirmed = true,
                    };
                    var ownerInDb = userManager.FindByEmailAsync(owner.Email).GetAwaiter().GetResult(); ;
                    if (ownerInDb is null)
                    {
                        userManager.CreateAsync(owner, "Admin@123").GetAwaiter().GetResult(); ;
                        userManager.AddToRoleAsync(owner, Roles.Owner.ToString()).GetAwaiter().GetResult(); ;
                    }
                    else
                    {
                        var userRoles = userManager.GetRolesAsync(ownerInDb).GetAwaiter().GetResult(); ;

                        if (!userRoles.Contains(Roles.Owner.ToString()))
                        {
                            foreach (var role in userRoles)
                            {
                                userManager.RemoveFromRoleAsync(ownerInDb, role).GetAwaiter().GetResult(); ;
                            }

                            userManager.AddToRoleAsync(ownerInDb, Roles.Owner.ToString()).GetAwaiter().GetResult(); ;
                        }
                    }
                }

                if (!(context.Services.Any()))
                {
                context.Services.AddRange(
                    new Service
                    {
                        Name = "Розпис",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ",
                        ImageUrl = "https://i.pinimg.com/736x/11/97/69/11976974e0e5facc8643016756ceecb2.jpg"
                    },
                    new Service
                    {
                        Name = "Вишивка",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ",

                        ImageUrl = "https://i.pinimg.com/736x/c7/a8/4d/c7a84df9508e99720d102bea8ba21d6b.jpg"
                    },

                    new Service
                    {
                        Name = "Ваша ідея",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ",
                        ImageUrl = "https://i.pinimg.com/736x/ee/07/cd/ee07cd90bb3d43d92e0515929ebc2567.jpg"
                    }
                    );
                    context.SaveChanges();
                }

                if (!(context.Projects.Any()))
                {
                    context.Projects.AddRange(
                        new Project
                        {
                            Title = "Сорочка Етно",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                            DateCompleted = DateTime.Parse("2024-09-11"),
                            ImagesUrl = "https://i.pinimg.com/736x/bf/72/86/bf72866fb83ca7cede291614555110e5.jpg",
                            ServiceId = 1,
                            Price = 1000
                        },
                        new Project
                        {
                            Title = "Футболка Етно",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                            DateCompleted = DateTime.Parse("2024-8-24"),
                            ImagesUrl = "https://i.pinimg.com/736x/b0/ee/78/b0ee786bf6dd86ebde6e5c9025b8a968.jpg",
                            ServiceId = 1,
                            Price = 1200
                        },
                        new Project
                        {
                            Title = "The soul fighter",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ",
                            DateCompleted = DateTime.Parse("2024-8-4"),
                            ImagesUrl = "https://i.pinimg.com/736x/ab/90/46/ab904682e8b76fb5022960dea0a8c833.jpg",
                            ServiceId = 1,
                            Price = 1000

                        },
                        new Project
                        {
                            Title = "Качки",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ",
                            DateCompleted = DateTime.Parse("2024-7-30"),
                            ImagesUrl = "https://i.pinimg.com/736x/11/97/69/11976974e0e5facc8643016756ceecb2.jpg",
                            ServiceId = 1,
                            Price = 700

                        }
                        );
                    context.SaveChanges() ;
                }
            }
        }
    }
}
