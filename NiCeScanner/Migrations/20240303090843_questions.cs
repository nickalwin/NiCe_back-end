using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NiCeScanner.Migrations
{
    /// <inheritdoc />
    public partial class questions : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Questions",
				columns: table => new
				{
					Id = table.Column<long>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Uuid = table.Column<Guid>(nullable: false),
					Data = table.Column<string>(type: "json", nullable: false),
					CategoryId = table.Column<long>(nullable: false),
					Weight = table.Column<short>(nullable: false),
					Statement = table.Column<bool>(nullable: false),
					Show = table.Column<bool>(nullable: false),
					Image = table.Column<string>(nullable: true),
					CreatedAt = table.Column<DateTime>(nullable: false),
					UpdatedAt = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Questions", x => x.Id);
					table.ForeignKey("FK_Questions_Categories", x => x.CategoryId, "Categories", "Id");
				}
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropTable(
				name: "Questions"
			);
		}
    }
}
