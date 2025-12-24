using Evidencijanezaposlenih.Interface.Context.Modeli;
using Microsoft.AspNetCore.Identity;

namespace Evidencijanezaposlenih.Interface.Context
{
    public static class IdentityRuntimeSeeder
    {
        public static async Task SeedAsync(IServiceProvider services, IConfiguration config, IWebHostEnvironment env)
        {
            // 🔐 Only seed admin in Development
            if (!env.IsDevelopment())
                return;

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<Korisnik>>();

            // 🔹 Roles
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // 🔹 Admin credentials (from config or fallback)
            var adminUserName = config["Admin:UserName"] ?? "admin";
            var adminEmail = config["Admin:Email"] ?? "admin@admin.com";
            var adminPassword = config["Admin:Password"] ?? "Admin123!";

            var adminUser = await userManager.FindByNameAsync(adminUserName);

            if (adminUser == null)
            {
                adminUser = new Korisnik
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (!result.Succeeded)
                {
                    throw new Exception("Admin user creation failed: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            // 🔹 Ensure role assignment
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
