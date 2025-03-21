using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Ads;

public class AdWriteRepository : WriteRepository<Ad>, IAdWriteRepository
{
	public AdWriteRepository(ApplicationDbContext context) : base(context) { }
}