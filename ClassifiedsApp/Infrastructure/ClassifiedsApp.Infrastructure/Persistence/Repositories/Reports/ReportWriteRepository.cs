using ClassifiedsApp.Application.Interfaces.Repositories.Reports;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Reports;

public class ReportWriteRepository : WriteRepository<Report>, IReportWriteRepository
{
	public ReportWriteRepository(ApplicationDbContext context) : base(context) { }
}
