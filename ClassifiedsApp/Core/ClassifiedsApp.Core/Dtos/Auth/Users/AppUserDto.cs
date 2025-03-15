using ClassifiedsApp.Core.Dtos.Common;

namespace ClassifiedsApp.Core.Dtos.Auth.Users;

public class AppUserDto : BaseEntityDto
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }
}
