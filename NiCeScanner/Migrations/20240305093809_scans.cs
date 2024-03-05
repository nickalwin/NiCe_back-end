using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NiCeScanner.Migrations
{
    /// <inheritdoc />
    public partial class scans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {	
			migrationBuilder.CreateTable(
                name: "Scans",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<Guid>(nullable: false),
                    ContactName = table.Column<string>(maxLength: 100, nullable: false),
                    ContactEmail = table.Column<string>(maxLength: 100, nullable: false),
                    SectorId = table.Column<short>(nullable: false),
                    Results = table.Column<string>(type: "json", nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scans", x => x.Id);
					table.ForeignKey("FK_Scans_Sectors", x => x.SectorId, "Sectors", "Id");
                }
			);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropTable(
				name: "Scans"
			);
        }
    }
}
