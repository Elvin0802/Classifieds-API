using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassifiedsApp.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class Mig_1_Init : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "AspNetRoles",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUsers",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
					RefreshTokenExpiresAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
					PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
					SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
					TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
					LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
					AccessFailedCount = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUsers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Categories",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Categories", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Locations",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					City = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Locations", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetRoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetUserClaims_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserLogins",
				columns: table => new
				{
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
					table.ForeignKey(
						name: "FK_AspNetUserLogins_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserRoles",
				columns: table => new
				{
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserTokens",
				columns: table => new
				{
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
					table.ForeignKey(
						name: "FK_AspNetUserTokens_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "MainCategories",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ParentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MainCategories", x => x.Id);
					table.ForeignKey(
						name: "FK_MainCategories_Categories_ParentCategoryId",
						column: x => x.ParentCategoryId,
						principalTable: "Categories",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Ads",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					IsNew = table.Column<bool>(type: "bit", nullable: false),
					ExpiresAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					Status = table.Column<int>(type: "int", nullable: false),
					ViewCount = table.Column<long>(type: "bigint", nullable: false),
					CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					MainCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Ads", x => x.Id);
					table.ForeignKey(
						name: "FK_Ads_AspNetUsers_AppUserId",
						column: x => x.AppUserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Ads_Categories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Categories",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Ads_Locations_LocationId",
						column: x => x.LocationId,
						principalTable: "Locations",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Ads_MainCategories_MainCategoryId",
						column: x => x.MainCategoryId,
						principalTable: "MainCategories",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateTable(
				name: "SubCategories",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Type = table.Column<int>(type: "int", nullable: false),
					IsRequired = table.Column<bool>(type: "bit", nullable: false),
					IsSearchable = table.Column<bool>(type: "bit", nullable: false),
					SortOrder = table.Column<int>(type: "int", nullable: false),
					MainCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SubCategories", x => x.Id);
					table.ForeignKey(
						name: "FK_SubCategories_MainCategories_MainCategoryId",
						column: x => x.MainCategoryId,
						principalTable: "MainCategories",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AdImages",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
					SortOrder = table.Column<int>(type: "int", nullable: false),
					AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AdImages", x => x.Id);
					table.ForeignKey(
						name: "FK_AdImages_Ads_AdId",
						column: x => x.AdId,
						principalTable: "Ads",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "UserAdSelections",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserAdSelections", x => x.Id);
					table.ForeignKey(
						name: "FK_UserAdSelections_Ads_AdId",
						column: x => x.AdId,
						principalTable: "Ads",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_UserAdSelections_AspNetUsers_AppUserId",
						column: x => x.AppUserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateTable(
				name: "AdSubCategoryValues",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
					AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					SubCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AdSubCategoryValues", x => x.Id);
					table.ForeignKey(
						name: "FK_AdSubCategoryValues_Ads_AdId",
						column: x => x.AdId,
						principalTable: "Ads",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AdSubCategoryValues_SubCategories_SubCategoryId",
						column: x => x.SubCategoryId,
						principalTable: "SubCategories",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateTable(
				name: "SubCategoryOptions",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
					SortOrder = table.Column<int>(type: "int", nullable: false),
					SubCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SubCategoryOptions", x => x.Id);
					table.ForeignKey(
						name: "FK_SubCategoryOptions_SubCategories_SubCategoryId",
						column: x => x.SubCategoryId,
						principalTable: "SubCategories",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_AdImages_AdId",
				table: "AdImages",
				column: "AdId");

			migrationBuilder.CreateIndex(
				name: "IX_Ads_AppUserId",
				table: "Ads",
				column: "AppUserId");

			migrationBuilder.CreateIndex(
				name: "IX_Ads_CategoryId",
				table: "Ads",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_Ads_LocationId",
				table: "Ads",
				column: "LocationId");

			migrationBuilder.CreateIndex(
				name: "IX_Ads_MainCategoryId",
				table: "Ads",
				column: "MainCategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_AdSubCategoryValues_AdId",
				table: "AdSubCategoryValues",
				column: "AdId");

			migrationBuilder.CreateIndex(
				name: "IX_AdSubCategoryValues_SubCategoryId",
				table: "AdSubCategoryValues",
				column: "SubCategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetRoleClaims_RoleId",
				table: "AspNetRoleClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles",
				column: "NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserClaims_UserId",
				table: "AspNetUserClaims",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserLogins_UserId",
				table: "AspNetUserLogins",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserRoles_RoleId",
				table: "AspNetUserRoles",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "EmailIndex",
				table: "AspNetUsers",
				column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "AspNetUsers",
				column: "NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_MainCategories_ParentCategoryId",
				table: "MainCategories",
				column: "ParentCategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_SubCategories_MainCategoryId",
				table: "SubCategories",
				column: "MainCategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_SubCategoryOptions_SubCategoryId",
				table: "SubCategoryOptions",
				column: "SubCategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_UserAdSelections_AdId",
				table: "UserAdSelections",
				column: "AdId");

			migrationBuilder.CreateIndex(
				name: "IX_UserAdSelections_AppUserId",
				table: "UserAdSelections",
				column: "AppUserId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "AdImages");

			migrationBuilder.DropTable(
				name: "AdSubCategoryValues");

			migrationBuilder.DropTable(
				name: "AspNetRoleClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserLogins");

			migrationBuilder.DropTable(
				name: "AspNetUserRoles");

			migrationBuilder.DropTable(
				name: "AspNetUserTokens");

			migrationBuilder.DropTable(
				name: "SubCategoryOptions");

			migrationBuilder.DropTable(
				name: "UserAdSelections");

			migrationBuilder.DropTable(
				name: "AspNetRoles");

			migrationBuilder.DropTable(
				name: "SubCategories");

			migrationBuilder.DropTable(
				name: "Ads");

			migrationBuilder.DropTable(
				name: "AspNetUsers");

			migrationBuilder.DropTable(
				name: "Locations");

			migrationBuilder.DropTable(
				name: "MainCategories");

			migrationBuilder.DropTable(
				name: "Categories");
		}
	}
}
