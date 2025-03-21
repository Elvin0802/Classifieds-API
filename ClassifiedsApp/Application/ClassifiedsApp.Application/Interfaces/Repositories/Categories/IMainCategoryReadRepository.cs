using ClassifiedsApp.Application.Interfaces.Repositories.Common;
using ClassifiedsApp.Core.Entities;

namespace ClassifiedsApp.Application.Interfaces.Repositories.Categories;

public interface IMainCategoryReadRepository : IReadRepository<MainCategory>
{
	Task<MainCategory> GetMainCategoryByIdWithIncludesAsync(Guid id, bool tracking = true);
}
