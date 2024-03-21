using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NiCeScanner.Migrations
{
    /// <inheritdoc />
    public partial class scan_codes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.CreateTable(
				name: "ScanCodes",
				columns: table => new
				{
					Id = table.Column<long>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Code = table.Column<Guid>(nullable: false),
					CanEdit = table.Column<bool>(nullable: false),
					ScanId = table.Column<long>(nullable: false),
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ScanCodes", x => x.Id);
					table.ForeignKey("FK_ScanCodes_Scans_ScanId", x => x.ScanId, "Scans", "Id");
				});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropTable(
				name: "ScanCodes"
			);
        }
    }
}
