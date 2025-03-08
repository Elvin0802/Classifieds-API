using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Common;

namespace ClassifiedsApp.Core.Interfaces.Repositories.Categories;

public interface ICategoryReadRepository : IReadRepository<Category>
{
	Task<Category> GetCategoryByIdWithIncludesAsync(Guid id, bool tracking = true);
}
