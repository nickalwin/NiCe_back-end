using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NiCeScanner.Migrations
{
    /// <inheritdoc />
    public partial class advice_conditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.CreateTable(
				name: "AdviceConditions",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Condition = table.Column<int>(type: "int", nullable: false),
					CreatedAt = table.Column<DateTime>(nullable: false),
					UpdatedAt = table.Column<DateTime>(nullable: true),
					AdviceId = table.Column<long>(type: "bigint", nullable: false),
					QuestionId = table.Column<long>(type: "bigint", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AdviceConditions", x => x.Id);
					table.ForeignKey(
						name: "FK_AdviceConditions_Advices_AdviceId",
						column: x => x.AdviceId,
						principalTable: "Advices",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AdviceConditions_Questions_QuestionId",
						column: x => x.QuestionId,
						principalTable: "Questions",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropTable(
				name: "AdviceConditions");
        }
    }
}
