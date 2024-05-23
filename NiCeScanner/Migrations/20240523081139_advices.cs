using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NiCeScanner.Migrations
{
    /// <inheritdoc />
    public partial class advices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advices",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Data = table.Column<string>(nullable: false),
					AdditionalLink = table.Column<string>(nullable: true),
					AdditionalLinkName = table.Column<string>(nullable: true),
					CreatedAt = table.Column<DateTime>(nullable: false),
					UpdatedAt = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Advices", x => x.Id);
				});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advices");
        }
    }
}
