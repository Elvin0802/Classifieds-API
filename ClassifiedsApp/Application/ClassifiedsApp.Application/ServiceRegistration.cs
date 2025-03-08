using ClassifiedsApp.Application.Common.Behaviors;
using ClassifiedsApp.Application.Features.Commands.Auth.Login;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ClassifiedsApp.Application;

public static class ServiceRegistration
{
	public static void AddApplicationServices(this IServiceCollection services)
	{
		services.AddMediatR(cfg =>
			cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
		);

		services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();

		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

	}
}
