using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Users;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Users;

public class UserAdSelectionReadRepository : ReadRepository<UserAdSelection>, IUserAdSelectionReadRepository
{
	public UserAdSelectionReadRepository(ApplicationDbContext context) : base(context) { }
}
