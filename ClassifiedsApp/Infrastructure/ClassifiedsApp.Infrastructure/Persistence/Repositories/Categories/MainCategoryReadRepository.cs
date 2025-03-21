using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Categories;

public class MainCategoryReadRepository : ReadRepository<MainCategory>, IMainCategoryReadRepository
{
	public MainCategoryReadRepository(ApplicationDbContext context) : base(context) { }

	public async Task<MainCategory> GetMainCategoryByIdWithIncludesAsync(Guid id, bool tracking = true)
	{
		var query = Table.AsQueryable();

		if (!tracking)
			query = Table.AsNoTracking();

		query = query.Include(mc => mc.SubCategories)
					 .ThenInclude(sc => sc.Options);

		return (await query.FirstOrDefaultAsync(data => data.Id == id))!;
	}
}