using ClassifiedsApp.Application.Interfaces.Repositories.Common;
using ClassifiedsApp.Core.Entities;

namespace ClassifiedsApp.Application.Interfaces.Repositories.Categories;

public interface ICategoryReadRepository : IReadRepository<Category>
{
	Task<Category> GetCategoryByIdWithIncludesAsync(Guid id, bool tracking = true);
}
