using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NiCeScanner.Migrations
{
    /// <inheritdoc />
    public partial class links : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
				name: "Links",
				columns: table => new
				{
					Id = table.Column<long>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(nullable: false, maxLength: 400),
					Href = table.Column<string>(nullable: false, maxLength: 400),
					CategoryId = table.Column<long>(nullable: false),
					CreatedAt = table.Column<DateTime>(nullable: false),
					UpdatedAt = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Links", x => x.Id);
				}
			);

			migrationBuilder.AddForeignKey(
				name: "FK_Links_Categories_CategoryId",
				table: "Links",
				column: "CategoryId",
				principalTable: "Categories",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade
			);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropForeignKey(
				name: "FK_Links_Categories_CategoryId",
				table: "Links"
			);

			migrationBuilder.DropTable(name: "Links");
        }
    }
}
