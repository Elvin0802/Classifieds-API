using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassifiedsApp.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class Mig_2_AdFeature : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<DateTimeOffset>(
				name: "FeatureEndDate",
				table: "Ads",
				type: "datetimeoffset",
				nullable: true);

			migrationBuilder.AddColumn<int>(
				name: "FeaturePriority",
				table: "Ads",
				type: "int",
				nullable: true);

			migrationBuilder.AddColumn<DateTimeOffset>(
				name: "FeatureStartDate",
				table: "Ads",
				type: "datetimeoffset",
				nullable: true);

			migrationBuilder.AddColumn<bool>(
				name: "IsFeatured",
				table: "Ads",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.CreateTable(
				name: "FeaturedAdTransactions",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					DurationDays = table.Column<int>(type: "int", nullable: false),
					PaymentReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
					IsCompleted = table.Column<bool>(type: "bit", nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_FeaturedAdTransactions", x => x.Id);
					table.ForeignKey(
						name: "FK_FeaturedAdTransactions_Ads_AdId",
						column: x => x.AdId,
						principalTable: "Ads",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_FeaturedAdTransactions_AspNetUsers_AppUserId",
						column: x => x.AppUserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateIndex(
				name: "IX_FeaturedAdTransactions_AdId",
				table: "FeaturedAdTransactions",
				column: "AdId");

			migrationBuilder.CreateIndex(
				name: "IX_FeaturedAdTransactions_AppUserId",
				table: "FeaturedAdTransactions",
				column: "AppUserId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "FeaturedAdTransactions");

			migrationBuilder.DropColumn(
				name: "FeatureEndDate",
				table: "Ads");

			migrationBuilder.DropColumn(
				name: "FeaturePriority",
				table: "Ads");

			migrationBuilder.DropColumn(
				name: "FeatureStartDate",
				table: "Ads");

			migrationBuilder.DropColumn(
				name: "IsFeatured",
				table: "Ads");
		}
	}
}
