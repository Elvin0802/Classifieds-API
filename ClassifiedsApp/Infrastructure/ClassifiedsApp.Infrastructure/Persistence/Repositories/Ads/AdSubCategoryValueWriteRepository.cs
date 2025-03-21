using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Ads;

public class AdSubCategoryValueWriteRepository : WriteRepository<AdSubCategoryValue>, IAdSubCategoryValueWriteRepository
{
	public AdSubCategoryValueWriteRepository(ApplicationDbContext context) : base(context) { }
}
