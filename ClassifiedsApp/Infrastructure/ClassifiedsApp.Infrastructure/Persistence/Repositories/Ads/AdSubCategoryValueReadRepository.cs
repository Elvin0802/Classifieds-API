using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Ads;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Ads;

public class AdSubCategoryValueReadRepository : ReadRepository<AdSubCategoryValue>, IAdSubCategoryValueReadRepository
{
	public AdSubCategoryValueReadRepository(ApplicationDbContext context) : base(context) { }
}
