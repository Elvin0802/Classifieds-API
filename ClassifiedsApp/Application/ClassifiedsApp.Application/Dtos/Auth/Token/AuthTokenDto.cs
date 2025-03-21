namespace ClassifiedsApp.Application.Dtos.Auth.Token;

public class AuthTokenDto
{
	public string AccessToken { get; set; }
	public string? RefreshToken { get; set; }
	public DateTimeOffset? RefreshTokenExpiresAt { get; set; }
}
