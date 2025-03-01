using ClassifiedsApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ClassifiedsApp.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
	public ApplicationDbContext CreateDbContext(string[] args)
	{
		DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder = new();
		dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
		return new(dbContextOptionsBuilder.Options);
	}
}
