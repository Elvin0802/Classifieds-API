using ClassifiedsApp.Application.Interfaces.Repositories.Common;
using ClassifiedsApp.Core.Entities.Common;
using ClassifiedsApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClassifiedsApp.Infrastructure.Persistence.Repositories.Common;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
	private readonly ApplicationDbContext _context;
	public ReadRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public DbSet<T> Table => _context.Set<T>();

	public IQueryable<T> GetAll(bool tracking = true)
	{
		var query = Table.AsQueryable();
		if (!tracking)
			query = query.AsNoTracking();
		return query;
	}
	public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
	{
		var query = Table.Where(method);
		if (!tracking)
			query = query.AsNoTracking();
		return query;
	}
	public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
	{
		var query = Table.AsQueryable();
		if (!tracking)
			query = Table.AsNoTracking();
		return (await query.FirstOrDefaultAsync(method))!;
	}
	public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
	{
		var query = Table.AsQueryable();
		if (!tracking)
			query = Table.AsNoTracking();
		return (await query.FirstOrDefaultAsync(data => data.Id == id))!;
	}

}
