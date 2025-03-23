using ClassifiedsApp.Application.Interfaces.Repositories.Common;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Enums;

namespace ClassifiedsApp.Application.Interfaces.Repositories.Reports;

public interface IReportReadRepository : IReadRepository<Report>
{
	Task<Report> GetByIdWithIncludesAsync(Guid id);
	Task<IList<Report>> GetAllAsync(ReportStatus? status = null);
	Task<IList<Report>> GetByAdIdAsync(Guid adId);
	Task<IList<Report>> GetByUserIdAsync(Guid userId);
}
