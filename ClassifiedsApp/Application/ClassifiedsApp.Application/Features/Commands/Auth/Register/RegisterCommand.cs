using ClassifiedsApp.Core.Dtos.Auth.Users;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.Register;

public class RegisterCommand : IRequest<RegisterCommandResponse>
{
	public CreateAppUserDto CreateAppUserDto { get; set; }
}
