using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Common;

namespace ClassifiedsApp.Core.Interfaces.Repositories.Categories;

public interface ISubCategoryReadRepository : IReadRepository<SubCategory>
{
	Task<SubCategory> GetSubCategoryByIdWithIncludesAsync(Guid id, bool tracking = true);
}