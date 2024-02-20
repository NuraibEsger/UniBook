using Microsoft.AspNetCore.Identity;
using UniBook.Entities;

namespace UniBook.Data
{
    public class DataSeed
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using var scope = serviceProvider.CreateScope();

            var roles = new string[] { "Rector", "Teacher" };

            var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach ( var role in roles )
            {
                var existingRole = await rolemanager.FindByNameAsync(role);

                if (existingRole is not null) continue;

                await rolemanager.CreateAsync(new IdentityRole(role));
            }

            string adminUserName = (string)configuration["DefaultRector:UserName"]!;
            string adminPassword = (string)configuration["DefaultRector:Password"]!;

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var rector = await userManager.FindByNameAsync(adminUserName);

            if (rector is not null) { return; }

            rector = new AppUser
            {
                Name = "Nuraib",
                Surname = "Esgerov",
                Email = adminUserName,
                UserName = adminUserName,
            };

            var token = await userManager.GenerateEmailConfirmationTokenAsync(rector);

            await userManager.ConfirmEmailAsync(rector, token);

            var result = await userManager.CreateAsync(rector, adminPassword);

            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(rector, roles[0]);
            }
        }
    }
}
