using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models;

public static class IdentitySeedData
{
   private const string adminUser = "Admin";
   private const string adminPassword = "Secret123$";

   public static async void EnsurePopulated(IApplicationBuilder app)
   {
      AppIdentityDbContext context = app.ApplicationServices
         .CreateScope().ServiceProvider
         .GetRequiredService<AppIdentityDbContext>();
      
      if ((await context.Database.GetPendingMigrationsAsync()).Any())
      {
         await context.Database.MigrateAsync();
      } 
      
      UserManager<IdentityUser> userManager = app.ApplicationServices
         .CreateScope().ServiceProvider
         .GetRequiredService<UserManager<IdentityUser>>();
      
      IdentityUser? user = await userManager.FindByNameAsync(adminUser);
      if (user == null)
      {
         user = new IdentityUser("Admin"); 
         user.Email = "admin@example.com"; 
         user.PhoneNumber = "555-1234"; 
         await userManager.CreateAsync(user, adminPassword);
      }
   }
}