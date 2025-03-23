using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassifiedsApp.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class Mig_4_DecimalPropsFix : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<decimal>(
				name: "Amount",
				table: "FeaturedAdTransactions",
				type: "decimal(18,6)",
				precision: 18,
				scale: 6,
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "decimal(18,2)");

			migrationBuilder.AlterColumn<decimal>(
				name: "Price",
				table: "Ads",
				type: "decimal(18,6)",
				precision: 18,
				scale: 6,
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "decimal(18,2)");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<decimal>(
				name: "Amount",
				table: "FeaturedAdTransactions",
				type: "decimal(18,2)",
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "decimal(18,6)",
				oldPrecision: 18,
				oldScale: 6);

			migrationBuilder.AlterColumn<decimal>(
				name: "Price",
				table: "Ads",
				type: "decimal(18,2)",
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "decimal(18,6)",
				oldPrecision: 18,
				oldScale: 6);
		}
	}
}
