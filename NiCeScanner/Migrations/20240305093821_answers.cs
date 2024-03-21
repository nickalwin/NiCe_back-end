using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NiCeScanner.Migrations
{
    /// <inheritdoc />
    public partial class answers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScanId = table.Column<long>(nullable: false),
                    QuestionId = table.Column<long>(nullable: false),
                    Score = table.Column<short>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
				constraints: table =>
				{
					table.PrimaryKey("PK_Answers", x => x.Id);
					table.ForeignKey("FK_Answers_Scans", x => x.ScanId, "Scans", "Id");
					table.ForeignKey("FK_Answers_Questions", x => x.QuestionId, "Questions", "Id");
				}
			);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropTable(
				name: "Answers"
			);
        }
    }
}
