using Data.Enums;
using Data.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.SeedData
{
    public static  class RoleSeeder
    {
        public static async Task SeedRoles (RoleManager<Role> roleManager)
        {
            var roles = Enum.GetValues<RolesPermession>().Select(c=>c.ToString());
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new Role { Name = role });
                }
            }
        }
    }
}