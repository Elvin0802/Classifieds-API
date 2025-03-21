using ClassifiedsApp.Application.Interfaces.Repositories.Common;
using ClassifiedsApp.Core.Entities;

namespace ClassifiedsApp.Application.Interfaces.Repositories.Ads;

public interface IAdReadRepository : IReadRepository<Ad>
{
	Task<Ad> GetAdByIdWithIncludesAsync(Guid id, bool tracking = true);
}
