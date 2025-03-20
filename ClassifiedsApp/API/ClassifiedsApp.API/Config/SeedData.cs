using ClassifiedsApp.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ClassifiedsApp.API.Config;

public static class SeedData
{
	public static async Task SeedRolesAndUsersAsync(RoleManager<AppRole> roleManager,
												UserManager<AppUser> userManager,
												IConfigurationManager configuration)
	{
		if (!await roleManager.RoleExistsAsync("Admin"))
			await roleManager.CreateAsync(new AppRole("Admin"));

		if (!await roleManager.RoleExistsAsync("User"))
			await roleManager.CreateAsync(new AppRole("User"));

		var adminEmail = configuration["Admin:Email"]!;
		var adminUser = await userManager.FindByEmailAsync(adminEmail);

		if (adminUser is null)
		{
			var admin = new AppUser()
			{
				UserName = adminEmail,
				Email = adminEmail,
				EmailConfirmed = true,
				Name = configuration["Admin:Name"]!,
				PhoneNumber = configuration["Admin:PhoneNumber"]!,
			};

			var result = await userManager.CreateAsync(admin, configuration["Admin:Password"]!);

			if (result.Succeeded)
				await userManager.AddToRoleAsync(admin, "Admin");
		}
	}
}
