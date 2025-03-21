using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Categories;

public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
{
	public CategoryReadRepository(ApplicationDbContext context) : base(context) { }

	public async Task<Category> GetCategoryByIdWithIncludesAsync(Guid id, bool tracking = true)
	{
		var query = Table.AsQueryable();

		if (!tracking)
			query = Table.AsNoTracking();

		query = query.Include(c => c.MainCategories)
					 .ThenInclude(mc => mc.SubCategories)
					 .ThenInclude(sc => sc.Options);

		return (await query.FirstOrDefaultAsync(data => data.Id == id))!;
	}
}