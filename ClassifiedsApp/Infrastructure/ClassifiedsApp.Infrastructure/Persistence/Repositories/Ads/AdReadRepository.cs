using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Ads;

public class AdReadRepository : ReadRepository<Ad>, IAdReadRepository
{
	public ApplicationDbContext _context;
	public AdReadRepository(ApplicationDbContext context) : base(context)
	{
		_context = context;
	}

	public async Task<Ad> GetAdByIdWithIncludesAsync(Guid id, bool tracking = true)
	{
		//var query = Table.AsQueryable();

		//if (!tracking)
		//	query = Table.AsNoTracking();

		//query = query.Include(ad => ad.Category)
		//			 .Include(ad => ad.Location)
		//			 .Include(ad => ad.MainCategory)
		//			 .Include(ad => ad.AppUser)
		//			 .Include(ad => ad.Images)
		//				.Include(ad => ad.SubCategoryValues)
		//				  .ThenInclude(scv => scv.SubCategory)
		//			 .AsSplitQuery();

		//return (await query.FirstOrDefaultAsync(data => data.Id == id))!;
		//-------------

		// Create a new query specifically for SubCategoryValues
		var subCategoryValuesQuery = _context.Set<AdSubCategoryValue>()
			.Include(scv => scv.SubCategory)
			.Where(scv => scv.AdId == id)
			.AsQueryable();

		if (!tracking)
			subCategoryValuesQuery = subCategoryValuesQuery.AsNoTracking();

		// Load the SubCategoryValues separately
		var subCategoryValues = await subCategoryValuesQuery.ToListAsync();

		// Load the Ad with other includes
		var query = Table.AsQueryable();

		if (!tracking)
			query = Table.AsNoTracking();

		query = query.Include(ad => ad.Category)
					 .Include(ad => ad.Location)
					 .Include(ad => ad.MainCategory)
					 .Include(ad => ad.AppUser)
					 .Include(ad => ad.Images)
					 .Include(ad => ad.FeatureTransactions)
					 .Include(ad => ad.SelectorUsers)
					 .AsSplitQuery();

		var ad = await query.FirstOrDefaultAsync(data => data.Id == id);

		if (ad != null)
		{
			// Manually assign the loaded SubCategoryValues
			ad.SubCategoryValues = subCategoryValues;
		}

		return ad!;
	}
}