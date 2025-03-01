using ClassifiedsApp.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ClassifiedsApp.Application.Features.Commands.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
{
	private readonly UserManager<AppUser> _userManager;

	public RegisterCommandHandler(UserManager<AppUser> userManager)
	{
		_userManager = userManager;
	}

	public async Task<RegisterCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
	{
		var exisitingUser = await _userManager.FindByEmailAsync(request.Email);

		if (exisitingUser is not null) return new() { IsSucceeded = false, Message = "This user already registered." };

		var user = new AppUser
		{
			Email = request.Email,
			UserName = request.Email,
			RefreshToken = Guid.NewGuid().ToString("N").ToLower(),
			RefreshTokenExpiresAt = DateTimeOffset.UtcNow.AddHours(12),
			Name = request.Name,
			Surname = request.Surname,
			PhoneNumber = request.PhoneNumber,
			CreatedAt = DateTimeOffset.UtcNow,
			UpdatedAt = DateTimeOffset.UtcNow,
			ArchivedAt = DateTimeOffset.MinValue
		};

		var result = await _userManager.CreateAsync(user, request.Password);

		if (!result.Succeeded) return new() { IsSucceeded = false, Message = "User not registered." };

		return new() { IsSucceeded = true, Message = "User registered." };
	}
}
