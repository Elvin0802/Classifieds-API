using ClassifiedsApp.Application.Interfaces.Repositories.Common;
using ClassifiedsApp.Core.Entities;

namespace ClassifiedsApp.Application.Interfaces.Repositories.Categories;

public interface ISubCategoryReadRepository : IReadRepository<SubCategory>
{
	Task<SubCategory> GetSubCategoryByIdWithIncludesAsync(Guid id, bool tracking = true);
}