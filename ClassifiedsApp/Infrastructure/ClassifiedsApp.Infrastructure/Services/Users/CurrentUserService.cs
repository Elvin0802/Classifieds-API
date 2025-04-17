using ClassifiedsApp.Application.Interfaces.Services.Users;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClassifiedsApp.Infrastructure.Services.Users;

public class CurrentUserService : ICurrentUserService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public CurrentUserService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public Guid? UserId =>
			Guid.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub)!);

	public string? UserName =>
			_httpContextAccessor.HttpContext?.User?.Identity?.Name;

	public string? Email =>
			_httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Email);

	public bool IsAuthenticated =>
			_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

	public List<string> Roles =>
			_httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)
			.Select(r => r.Value)
			.ToList() ?? new List<string>();

	public bool HasRole(string role) =>
			Roles.Contains(role, StringComparer.OrdinalIgnoreCase);

}
