using Microsoft.AspNetCore.Identity;
using AuthAPI.Models;

namespace AuthAPI.Data.SeedData
{
    public static class IdentityData
    {
        public static async Task SeedAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            const string adminEmail = "admin@healthriskengine.com";
            const string adminPassword = "P@ssword123";

            var userExisting = await userManager.FindByEmailAsync(adminEmail);
            if (userExisting == null)
            {
                Console.WriteLine("Création de l'utilisateur admin...");

                var userAdmin = new ApplicationUser
                {
                    UserName = "Steven",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(userAdmin, adminPassword);
                if (!result.Succeeded)
                    throw new Exception("Failed to create the admin user: " + string.Join(", ", result.Errors));

                Console.WriteLine("Admin créé avec succès !");
            }
            else
            {
                Console.WriteLine("Admin déjà présent en base");
            }
        }
    }
}