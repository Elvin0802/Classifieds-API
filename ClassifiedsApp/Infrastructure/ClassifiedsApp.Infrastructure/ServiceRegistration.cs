using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.AdImages;
using ClassifiedsApp.Core.Interfaces.Repositories.Ads;
using ClassifiedsApp.Core.Interfaces.Repositories.Categories;
using ClassifiedsApp.Core.Interfaces.Repositories.Locations;
using ClassifiedsApp.Core.Interfaces.Repositories.Users;
using ClassifiedsApp.Core.Interfaces.Services.Mail;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.AdImages;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Ads;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Categories;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Locations;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Users;
using ClassifiedsApp.Infrastructure.Services.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClassifiedsApp.Infrastructure;

public static class ServiceRegistration
{
	public static void AddInfrastructureServices(this IServiceCollection services)
	{
		services.AddDbContext<ApplicationDbContext>(
		options =>
		{
			options.UseSqlServer(Configuration.ConnectionString);
		});

		services.AddIdentity<AppUser, AppRole>(options =>
		{
			options.Password.RequireDigit = true;
			options.Password.RequireLowercase = true;
			options.Password.RequireUppercase = true;
			options.Password.RequireNonAlphanumeric = true;
			options.Password.RequiredLength = 6;

			options.User.RequireUniqueEmail = true;
		})
		.AddEntityFrameworkStores<ApplicationDbContext>()
		.AddDefaultTokenProviders();

		services.AddScoped<IAdReadRepository, AdReadRepository>();
		services.AddScoped<IAdWriteRepository, AdWriteRepository>();
		services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
		services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
		services.AddScoped<ILocationReadRepository, LocationReadRepository>();
		services.AddScoped<ILocationWriteRepository, LocationWriteRepository>();
		services.AddScoped<IAdImageReadRepository, AdImageReadRepository>();
		services.AddScoped<IAdImageWriteRepository, AdImageWriteRepository>();
		services.AddScoped<IMainCategoryReadRepository, MainCategoryReadRepository>();
		services.AddScoped<IMainCategoryWriteRepository, MainCategoryWriteRepository>();
		services.AddScoped<IAdSubCategoryValueReadRepository, AdSubCategoryValueReadRepository>();
		services.AddScoped<IAdSubCategoryValueWriteRepository, AdSubCategoryValueWriteRepository>();
		services.AddScoped<IMainCategoryReadRepository, MainCategoryReadRepository>();
		services.AddScoped<IMainCategoryWriteRepository, MainCategoryWriteRepository>();
		services.AddScoped<ISubCategoryReadRepository, SubCategoryReadRepository>();
		services.AddScoped<ISubCategoryWriteRepository, SubCategoryWriteRepository>();
		services.AddScoped<ISubCategoryOptionReadRepository, SubCategoryOptionReadRepository>();
		services.AddScoped<ISubCategoryOptionWriteRepository, SubCategoryOptionWriteRepository>();
		services.AddScoped<IMainCategoryReadRepository, MainCategoryReadRepository>();
		services.AddScoped<IMainCategoryWriteRepository, MainCategoryWriteRepository>();
		services.AddScoped<IUserAdSelectionWriteRepository, UserAdSelectionWriteRepository>();

		services.AddScoped<IUserAdSelectionReadRepository, UserAdSelectionReadRepository>();
		services.AddScoped<IMailService, MailService>();
	}
}
