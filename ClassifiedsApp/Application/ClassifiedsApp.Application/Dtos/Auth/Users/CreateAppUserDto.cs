namespace ClassifiedsApp.Application.Dtos.Auth.Users;

public class CreateAppUserDto
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }
	public string Password { get; set; }
}
