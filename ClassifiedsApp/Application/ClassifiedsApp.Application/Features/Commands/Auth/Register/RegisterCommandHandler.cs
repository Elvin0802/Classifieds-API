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
		var exisitingUser = await _userManager.FindByEmailAsync(request.CreateAppUserDto.Email);

		if (exisitingUser is not null) return new() { IsSucceeded = false, Message = "This user already registered." };

		var user = new AppUser
		{
			Email = request.CreateAppUserDto.Email,
			UserName = request.CreateAppUserDto.Email, // gelecekde Phone number et ki , username login mumkun olsun.
			Name = request.CreateAppUserDto.Name,
			PhoneNumber = request.CreateAppUserDto.PhoneNumber,
			CreatedAt = DateTimeOffset.UtcNow,
			UpdatedAt = DateTimeOffset.UtcNow,
			ArchivedAt = DateTimeOffset.MinValue
		};

		var result = await _userManager.CreateAsync(user, request.CreateAppUserDto.Password);

		if (!result.Succeeded)
			return new()
			{
				IsSucceeded = false,
				Message = $"User not registered.  {string.Join(" |-------| ", result.Errors.Select(e => e.Description))}"
			};

		return new()
		{
			IsSucceeded = true,
			Message = "User registered successfully."
		};
	}
}
