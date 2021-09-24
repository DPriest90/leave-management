using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management
{
    public static class SeedData    
    {
        /// <summary>
        /// Static class to Create default users and user roles
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public static void Seed(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        /// <summary>
        /// Create and add an "admin" user as a default to the database, only if it doesn't already exist
        /// </summary>
        /// <param name="userManager"></param>
        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            // Create an admin user and add admin user to Administrator role
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin@localhost.com",
                    Email = "admin@localhost.com"
                };

                var result = userManager.CreateAsync(user, "P@ssword1").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        /// <summary>
        /// Create and add default 'Roles' (Administrator and Employee) to the database
        /// </summary>
        /// <param name="roleManager"></param>
        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Check to see if certain roles alread exist, if not then create them
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };

                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Employee"
                };

                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}