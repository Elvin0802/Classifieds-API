using ClassifiedsApp.Application.Services;
using ClassifiedsApp.Core.Dtos.Auth.Token;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Services.Auth;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ClassifiedsApp.API.Config;

public static class DIConfig
{
	public static IServiceCollection AddSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(setup =>
		{
			setup.SwaggerDoc("v1",
				new OpenApiInfo
				{
					Title = "Invoicer API",
					Version = "v: 1.0"
				});

			setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
			{
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer key-key-key-key\""
			});

			setup.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								}
							}, new string[]{}
						}
				});
		});

		return services;
	}

	public static IServiceCollection AuthenticationAndAuthorization(
					this IServiceCollection services,
					IConfiguration configuration)
	{

		services.AddIdentity<AppUser, IdentityRole<Guid>>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

		services.AddScoped<ITokenService, TokenService>();

		var jwtConfig = new JwtConfigDto();

		configuration.Bind("JWT", jwtConfig);

		services.AddSingleton(jwtConfig);

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters =
			new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ClockSkew = TimeSpan.Zero,
				ValidIssuer = jwtConfig.Issuer,
				ValidAudience = jwtConfig.Audience,
				IssuerSigningKey
				= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
			};
		});

		return services;
	}
}
