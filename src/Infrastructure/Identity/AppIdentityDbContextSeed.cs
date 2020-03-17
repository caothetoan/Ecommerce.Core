using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Vnit.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "caothetoan@gmail.com",
                Email = "caothetoan@gmail.com"
            };
            await userManager.CreateAsync(defaultUser, "Pass@word1");
        }

        // Initialize some test roles. In the real world, these would be setup explicitly by a role manager
        private static string[] roles = new[] { "User", "Manager", "Administrator" };
        public static async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var newRole = new IdentityRole(role);
                    await roleManager.CreateAsync(newRole);
                    // In the real world, there might be claims associated with roles
                    // _roleManager.AddClaimAsync(newRole, new )
                }
            }
        }
    }
}
