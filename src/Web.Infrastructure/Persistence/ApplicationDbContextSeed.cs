using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Web.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(RoleManager<IdentityRole> roleManager)
        {
            var listenerRole = new IdentityRole("Listener");
            var artistRole = new IdentityRole("Artist");
            var roles = roleManager.Roles;
            if (roles.All(r => r.Name != listenerRole.Name))
            {
                await roleManager.CreateAsync(listenerRole);
            }
            if (roles.All(r => r.Name != artistRole.Name))
            {
                await roleManager.CreateAsync(artistRole);
            }

        }
    }
}