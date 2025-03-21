using ClassifiedsApp.Application.Interfaces.Repositories.AdImages;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.AdImages;

public class AdImageReadRepository : ReadRepository<AdImage>, IAdImageReadRepository
{
	public AdImageReadRepository(ApplicationDbContext context) : base(context) { }
}
