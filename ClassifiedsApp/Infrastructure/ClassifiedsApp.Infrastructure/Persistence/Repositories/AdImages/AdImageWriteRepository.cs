using ClassifiedsApp.Application.Interfaces.Repositories.AdImages;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.AdImages;

public class AdImageWriteRepository : WriteRepository<AdImage>, IAdImageWriteRepository
{
	public AdImageWriteRepository(ApplicationDbContext context) : base(context) { }
}
