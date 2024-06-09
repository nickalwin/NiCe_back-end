using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NiCeScanner.Migrations
{
    /// <inheritdoc />
    public partial class create_pdf_template_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PdfTemplates",
                columns: table => new
				{
					Id = table.Column<long>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(type: "json", nullable: false),
					Introduction = table.Column<string>(type: "json", nullable: false),
					ImageData = table.Column<byte[]>(type: "BLOB", nullable: false),
					BeforePlotText = table.Column<string>(type: "json", nullable: true),
					AfterPlotText = table.Column<string>(type: "json", nullable: true),
				},
                constraints: table =>
				{
					table.PrimaryKey("PK_PdfTemplates", x => x.Id);
				});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PdfTemplates");
        }
    }
}
