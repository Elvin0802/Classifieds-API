using ClassifiedsApp.API.Config;
using ClassifiedsApp.API.Middlewares;
using ClassifiedsApp.Application;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog((context, config) =>
{
	config.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AuthenticationAndAuthorization(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddSwagger();

var client = builder.Configuration["ClientUrl"];

builder.Services.AddCors(options => options.AddPolicy("CORSPolicy", builder =>
{
	builder.WithOrigins(client!)
		   .AllowAnyMethod()
		   .AllowAnyHeader()
		   .AllowCredentials();
}));

builder.Services.AddAuthorizationBuilder()
				.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"))
				.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("CORSPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed roles and admin user on application startup
using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

	await SeedData.SeedRolesAndUsersAsync(roleManager, userManager, builder.Configuration);
}

app.Run();
