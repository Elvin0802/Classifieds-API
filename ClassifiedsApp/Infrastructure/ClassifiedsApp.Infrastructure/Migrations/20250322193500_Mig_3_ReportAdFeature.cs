using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassifiedsApp.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class Mig_3_ReportAdFeature : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Reports",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ReportedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Reason = table.Column<int>(type: "int", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Status = table.Column<int>(type: "int", nullable: false),
					ReviewedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					ReviewedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
					ReviewNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Reports", x => x.Id);
					table.ForeignKey(
						name: "FK_Reports_Ads_AdId",
						column: x => x.AdId,
						principalTable: "Ads",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Reports_AspNetUsers_ReportedByUserId",
						column: x => x.ReportedByUserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Reports_AspNetUsers_ReviewedByUserId",
						column: x => x.ReviewedByUserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Reports_AdId",
				table: "Reports",
				column: "AdId");

			migrationBuilder.CreateIndex(
				name: "IX_Reports_ReportedByUserId",
				table: "Reports",
				column: "ReportedByUserId");

			migrationBuilder.CreateIndex(
				name: "IX_Reports_ReviewedByUserId",
				table: "Reports",
				column: "ReviewedByUserId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Reports");
		}
	}
}
