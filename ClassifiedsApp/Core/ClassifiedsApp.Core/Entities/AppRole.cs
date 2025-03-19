using Microsoft.AspNetCore.Identity;

namespace ClassifiedsApp.Core.Entities;

public class AppRole : IdentityRole<Guid>
{
	public AppRole(string name) : base(name)
	{ }
}
