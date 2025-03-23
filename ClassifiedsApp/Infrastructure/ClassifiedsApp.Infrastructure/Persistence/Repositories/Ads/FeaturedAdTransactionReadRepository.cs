using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Ads;

public class FeaturedAdTransactionReadRepository : ReadRepository<FeaturedAdTransaction>, IFeaturedAdTransactionReadRepository
{
	public FeaturedAdTransactionReadRepository(ApplicationDbContext context) : base(context)
	{ }
}
