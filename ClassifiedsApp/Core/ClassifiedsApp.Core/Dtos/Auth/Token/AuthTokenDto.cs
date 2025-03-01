namespace ClassifiedsApp.Core.Dtos.Auth.Token;

public class AuthTokenDto
{
	public string AccessToken { get; set; }
	public DateTimeOffset Expiration { get; set; }
	public string RefreshToken { get; set; }
}
