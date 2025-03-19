using ClassifiedsApp.Application.Services;
using ClassifiedsApp.Core.Dtos.Auth.Token;
using ClassifiedsApp.Core.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
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
				Description = "\nExample: \"Bearer key-key-key-key\""
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
			options.Events = new JwtBearerEvents
			{
				OnMessageReceived = context =>
				{
					if (context.Request.Cookies.ContainsKey("accessToken"))
						context.Token = context.Request.Cookies["accessToken"];

					return Task.CompletedTask;
				}
			};

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
				= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),
				RoleClaimType = ClaimsIdentity.DefaultRoleClaimType
			};
		});

		return services;
	}
}
