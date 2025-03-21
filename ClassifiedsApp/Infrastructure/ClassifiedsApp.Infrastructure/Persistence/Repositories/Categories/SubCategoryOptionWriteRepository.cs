using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Categories;

public class SubCategoryOptionWriteRepository : WriteRepository<SubCategoryOption>, ISubCategoryOptionWriteRepository
{
	public SubCategoryOptionWriteRepository(ApplicationDbContext context) : base(context) { }
}
