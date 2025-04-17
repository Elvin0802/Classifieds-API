using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Auth.Users;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.Register;

public class RegisterCommand : IRequest<Result>
{
	public CreateAppUserDto CreateAppUserDto { get; set; }
}
