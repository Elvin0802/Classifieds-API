using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Entities.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Infrastructure.Persistence.Context;

public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{ }

	public DbSet<AppUser> AppUsers { get; set; }
	public DbSet<AppRole> AppRoles { get; set; }
	public DbSet<Report> Reports { get; set; }
	public DbSet<Ad> Ads { get; set; }
	public DbSet<AdImage> AdImages { get; set; }
	public DbSet<AdSubCategoryValue> AdSubCategoryValues { get; set; }
	public DbSet<UserAdSelection> UserAdSelections { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Location> Locations { get; set; }
	public DbSet<MainCategory> MainCategories { get; set; }
	public DbSet<SubCategory> SubCategories { get; set; }
	public DbSet<SubCategoryOption> SubCategoryOptions { get; set; }
	public DbSet<FeaturedAdTransaction> FeaturedAdTransactions { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);


		//--

		builder.Entity<FeaturedAdTransaction>()
			.HasOne(ft => ft.Ad)
			.WithMany(a => a.FeatureTransactions)
			.HasForeignKey(ft => ft.AdId);

		builder.Entity<FeaturedAdTransaction>()
			.HasOne(ft => ft.AppUser)
			.WithMany(u => u.FeatureTransactions)
			.HasForeignKey(ft => ft.AppUserId);

		//--

		builder.Entity<Report>()
			.HasOne(r => r.Ad)
			.WithMany()
			.HasForeignKey(r => r.AdId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.Entity<Report>()
			.HasOne(r => r.ReportedByUser)
			.WithMany()
			.HasForeignKey(r => r.ReportedByUserId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.Entity<Report>()
			.HasOne(r => r.ReviewedByUser)
			.WithMany()
			.HasForeignKey(r => r.ReviewedByUserId)
			.IsRequired(false)
			.OnDelete(DeleteBehavior.Restrict);

		//--

		builder.Entity<Ad>()
			.Property(a => a.Price)
			.HasPrecision(18, 6);

		builder.Entity<FeaturedAdTransaction>()
			.Property(f => f.Amount)
			.HasPrecision(18, 6);

		//--

	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var collection = ChangeTracker.Entries<BaseEntity>();

		foreach (var item in collection)
		{
			_ = item.State switch
			{
				EntityState.Added => item.Entity.CreatedAt = DateTime.UtcNow,
				EntityState.Modified => item.Entity.UpdatedAt = DateTime.UtcNow,
				_ => DateTime.UtcNow
			};
		}

		return await base.SaveChangesAsync(cancellationToken);
	}
}
