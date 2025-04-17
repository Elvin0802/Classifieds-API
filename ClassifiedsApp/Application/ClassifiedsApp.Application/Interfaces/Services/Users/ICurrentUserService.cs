namespace ClassifiedsApp.Application.Interfaces.Services.Users;

public interface ICurrentUserService
{
	Guid? UserId { get; }
	string? UserName { get; }
	string? Email { get; }
	bool IsAuthenticated { get; }
	List<string> Roles { get; }
	bool HasRole(string role);
}

