using ClassifiedsApp.Application.Interfaces.Repositories.Locations;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Locations;

public class LocationReadRepository : ReadRepository<Location>, ILocationReadRepository
{
	public LocationReadRepository(ApplicationDbContext context) : base(context) { }
}
