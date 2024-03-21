using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecars.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "baseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelYear = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Colors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mileage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_baseDetails", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "baseDetails",
                columns: new[] { "Id", "Colors", "Mileage", "ModelYear", "Name", "Price" },
                values: new object[] { 1, "[\"Sunshine Orange\",\"Lepord Black\"]", 12.5, 2023, "Honda WTX", 65000.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "baseDetails");
        }
    }
}
