using ClassifiedsApp.Application.Features.Commands.Auth.Login;
using FluentValidation;

namespace ClassifiedsApp.Application.Features.Commands.Ads.CreateAd;

public class CreateAdCommandValidator : AbstractValidator<LoginCommand>
{
	public CreateAdCommandValidator()
	{

	}
}
