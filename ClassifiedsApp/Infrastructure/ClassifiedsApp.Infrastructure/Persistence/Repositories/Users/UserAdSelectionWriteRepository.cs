using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Users;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Users;

public class UserAdSelectionWriteRepository : WriteRepository<UserAdSelection>, IUserAdSelectionWriteRepository
{
	public UserAdSelectionWriteRepository(ApplicationDbContext context) : base(context) { }
}
