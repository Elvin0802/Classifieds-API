using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Categories;

public class SubCategoryWriteRepository : WriteRepository<SubCategory>, ISubCategoryWriteRepository
{
	public SubCategoryWriteRepository(ApplicationDbContext context) : base(context) { }
}
