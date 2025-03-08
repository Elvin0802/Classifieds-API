using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Common;

namespace ClassifiedsApp.Core.Interfaces.Repositories.Categories;

public interface IMainCategoryReadRepository : IReadRepository<MainCategory>
{
	Task<MainCategory> GetMainCategoryByIdWithIncludesAsync(Guid id, bool tracking = true);
}
