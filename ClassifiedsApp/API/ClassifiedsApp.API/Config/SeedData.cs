using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using ClassifiedsApp.Application.Interfaces.Repositories.Locations;
using ClassifiedsApp.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ClassifiedsApp.API.Config;

public static class SeedData
{
	public static async Task AddSeedRolesAndUsersAsync(RoleManager<AppRole> roleManager,
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

	public static async Task AddSeedDataAsync(UserManager<AppUser> userManager,
											  IConfigurationManager configuration,
											  ICategoryReadRepository categoryReadRepository,
											  ICategoryWriteRepository categoryWriteRepository,
											  IMainCategoryReadRepository mainCategoryReadRepository,
											  IMainCategoryWriteRepository mainCategoryWriteRepository,
											  ISubCategoryReadRepository subCategoryReadRepository,
											  ISubCategoryWriteRepository subCategoryWriteRepository,
											  ISubCategoryOptionReadRepository subCategoryOptionReadRepository,
											  ISubCategoryOptionWriteRepository subCategoryOptionWriteRepository,
											  IAdReadRepository adReadRepository,
											  IAdWriteRepository adWriteRepository,
											  ILocationReadRepository locationReadRepository,
											  ILocationWriteRepository locationWriteRepository)
	{
		var adminEmail = configuration["Admin:Email"]!;
		var admin = await userManager.FindByEmailAsync(adminEmail);

		if (admin is null) return;

		//-----------

		List<Category> categories = [
			new() { Name="Transportation", Slug="Transportation".ToLower() },
			new() { Name="Electronics", Slug="Electronics".ToLower() },
			//new() { Name="Real estate", Slug="Real estate".ToLower().Replace(" ", "-") },
			//new() { Name="Hobbies", Slug="Hobbies".ToLower().Replace(" ", "-") },
			//new() { Name="Services and business", Slug="Services and business".ToLower().Replace(" ", "-") },
			//new() { Name="Other", Slug="Other".ToLower().Replace(" ", "-") },
			//new() { Name="Childrens world", Slug="Childrens world".ToLower().Replace(" ", "-") },
			//new() { Name="Personal items", Slug="Personal items".ToLower().Replace(" ", "-") }
		];

		foreach (var c in categories)
		{
			var cat = categoryReadRepository.GetWhere(ca => ca.Name == c.Name);

			if (cat is null)
				await categoryWriteRepository.AddAsync(c);
		}
		await categoryWriteRepository.SaveAsync();

		//-----------

		List<MainCategory> mainCategories = [
			new() { Name="Cars", Slug="Cars".ToLower(), ParentCategoryId=categories[0].Id },
			new() { Name="Motors", Slug="Motors".ToLower(), ParentCategoryId=categories[0].Id },
			//new() { Name="Accessories", Slug="Accessories".ToLower(), ParentCategoryId=categories[0].Id },
			new() { Name="Phones", Slug="Phones".ToLower(), ParentCategoryId=categories[1].Id },
			new() { Name="Phone numbers", Slug="Phone numbers".ToLower().Replace(" ", "-"), ParentCategoryId=categories[1].Id },
		];

		foreach (var c in mainCategories)
		{
			var cat = mainCategoryReadRepository.GetWhere(ca => ca.Name == c.Name);

			if (cat is null)
				await mainCategoryWriteRepository.AddAsync(c);
		}
		await mainCategoryWriteRepository.SaveAsync();

		//-----------

		//List<SubCategory> subCategories = [
		//	new() { Name="Marka", IsRequired=true, IsSearchable=true, Type=SubCategoryType.Select, MainCategoryId=mainCategories[0].Id },
		//	new() { Name="Model", IsRequired=true, IsSearchable=true, Type=SubCategoryType.Select, MainCategoryId=mainCategories[0].Id },
		//	new() { Name="Color", IsRequired=true, IsSearchable=true, Type=SubCategoryType.Select, MainCategoryId=mainCategories[0].Id },

		//	new() { Name="Gas Type", IsRequired=true, IsSearchable=true, Type=SubCategoryType.Select, MainCategoryId=mainCategories[1].Id },
		//	new() { Name="Km", IsRequired=true, IsSearchable=true, Type=SubCategoryType.Select, MainCategoryId=mainCategories[1].Id },
		//	new() { Name="Color", IsRequired=true, IsSearchable=true, Type=SubCategoryType.Select, MainCategoryId=mainCategories[1].Id },

		//	new() { Name="Marka", IsRequired=true, IsSearchable=true, Type=SubCategoryType.Select, MainCategoryId=mainCategories[2].Id },
		//	new() { Name="Model", IsRequired=true, IsSearchable=true, Type=SubCategoryType.Select, MainCategoryId=mainCategories[2].Id },
		//	new() { Name="Ram and Rom", IsRequired=true, IsSearchable=true, Type=SubCategoryType.Select, MainCategoryId=mainCategories[2].Id },

		//	new() { Name="Operator", IsRequired=true, IsSearchable=true, Type=SubCategoryType.Select, MainCategoryId=mainCategories[3].Id },
		//];

		//subCategories[0].Options = [
		//	new() { SortOrder=0, SubCategoryId=subCategories[0].Id, Value="Bmw" },
		//	new() { SortOrder=1, SubCategoryId=subCategories[0].Id, Value="Opel" },
		//	new() { SortOrder=2, SubCategoryId=subCategories[0].Id, Value="Volvo" },
		//];

		//subCategories[1].Options = [
		//	new() { SortOrder=0, SubCategoryId=subCategories[1].Id, Value="Bmw" },
		//	new() { SortOrder=1, SubCategoryId=subCategories[1].Id, Value="Opel" },
		//	new() { SortOrder=2, SubCategoryId=subCategories[1].Id, Value="Volvo" },
		//];

		//-----------

		/*
		Options=[new() { SortOrder=0, SubCategoryId= }, ]
		SubCategory newSubCategory = new()
			{
				Name = request.Name.Trim(),
				IsRequired = request.IsRequired,
				IsSearchable = true,
				Type = request.Type,
				MainCategoryId = request.MainCategoryId,
			};

			if (request.Options is not null && request.Options.Count > 0)
			{
				int sortOrderIndex = 0;
				newSubCategory.Options = new List<SubCategoryOption>();

				foreach (var str in request.Options)
				{
					newSubCategory.Options.Add(new()
					{
						SortOrder = sortOrderIndex++,
						SubCategoryId = newSubCategory.Id,
						Value = str
					});
				}
			}
		
		*/


	}
}
