using ClassifiedsApp.Application.Interfaces.Repositories.Reports;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Enums;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Reports;

public class ReportReadRepository : ReadRepository<Report>, IReportReadRepository
{
	public ReportReadRepository(ApplicationDbContext context) : base(context)
	{ }

	public async Task<IList<Report>> GetAllAsync(ReportStatus? status = null)
	{
		var query = Table.Include(r => r.Ad)
						 .Include(r => r.ReportedByUser)
						 .Include(r => r.ReviewedByUser)
						 .AsQueryable();

		if (status.HasValue)
			query = query.Where(r => r.Status == status.Value);

		return await query.OrderByDescending(r => r.CreatedAt)
						  .ToListAsync();
	}

	public async Task<IList<Report>> GetByAdIdAsync(Guid adId)
	{
		return await Table.Include(r => r.ReportedByUser)
						  .Include(r => r.ReviewedByUser)
						  .Where(r => r.AdId == adId)
						  .OrderByDescending(r => r.CreatedAt)
						  .ToListAsync();
	}

	public async Task<Report> GetByIdWithIncludesAsync(Guid id)
	{
		return (await Table.Include(r => r.Ad)
						   .Include(r => r.ReportedByUser)
						   .Include(r => r.ReviewedByUser)
						   .FirstOrDefaultAsync(r => r.Id == id))!;
	}

	public async Task<IList<Report>> GetByUserIdAsync(Guid userId)
	{
		return await Table.Include(r => r.Ad)
						  .Where(r => r.ReportedByUserId == userId)
						  .OrderByDescending(r => r.CreatedAt)
						  .ToListAsync();
	}
}
