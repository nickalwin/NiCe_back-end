using Microsoft.AspNetCore.Identity;

namespace NiCeScanner.Data
{
	public class UserSeeder
	{
		private static async Task SeedUser(string name, string email, string password, string roleStr, ApplicationDbContext dbContext, IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

				var user = new IdentityUser { 
					UserName = email, 
					Email = email, 
					EmailConfirmed = true,
					NormalizedEmail = email.ToUpper(),
					NormalizedUserName = email.ToUpper(),
					LockoutEnabled = false,
					SecurityStamp = Guid.NewGuid().ToString(),
					PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, password)
				};
				
				await userManager.CreateAsync(user);
				await userManager.AddToRoleAsync(user, roleStr);
				
				await dbContext.SaveChangesAsync();
			}
		}
		
		public static async void SeedUsers(ApplicationDbContext context, IApplicationBuilder app)
		{
			if (context.Users.Any() || context.Roles.Any())
				return;

			using (var scope = app.ApplicationServices.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				var roles = new List<string> { "Admin", "Manager", "Researcher", "Student", "Member" };

				foreach (var role in roles)
				{
					if (!await roleManager.RoleExistsAsync(role))
					{
						roleManager.CreateAsync(new IdentityRole(role)).Wait();
					}
				}
			}
			
			await SeedUser("NiCE", "nicescanningtool@gmail.com", "NiCE@123", "Admin", context, app);
			await SeedUser("Anne", "a.van.vulpen@windesheim.nl", "Start@123", "Admin", context, app);
		}
	}
}
