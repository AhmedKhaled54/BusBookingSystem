using Data.Enums;
using Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.SeedData
{
    public static class UserSeeder
    {
        public static async Task SeedUsers(UserManager<User> userManager)
        {
            var UserCount = await userManager.Users.CountAsync();
            if (UserCount <= 0)
            {
                var user = new User
                {
                    CreatedAt = DateTime.UtcNow,
                    PhoneNumber = "011212749053",
                    Gender = GenderType.Male,
                    UserName = "AhmedKhaled",
                    Email = "ahmed.khaled0132@gmail.com",
                    EmailConfirmed = true

                };
                user.SetName("Ahmed", "Khaled ");
                //user.SetEmail("ahmed.khaled0132@gmail.com");
                user.SetDateOfBith(new DateOnly(2001, 12, 15));
                user.SetNationalId("30112151304637");

                var result = await userManager.CreateAsync(user, "0552324167Aa/*");
                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(user,
                    [
                        RolesPermession.Admin.ToString(),
                        RolesPermession.User.ToString(),
                    ]);
                }
                else
                {
                    foreach (var item in result.Errors)
                        Console.WriteLine(item.Description);
                }

            }
        }
    }
}
