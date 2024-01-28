using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProvinceCity.Data.Migrations
{
    /// <inheritdoc />
    public partial class Rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProvinceName",
                table: "Province",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "Citiess",
                table: "Province",
                newName: "Cities");

            migrationBuilder.RenameColumn(
                name: "ProvinceCode",
                table: "Province",
                newName: "Province Code");

            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "City",
                newName: "City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Province",
                newName: "ProvinceName");

            migrationBuilder.RenameColumn(
                name: "Cities",
                table: "Province",
                newName: "Citiess");

            migrationBuilder.RenameColumn(
                name: "Province Code",
                table: "Province",
                newName: "ProvinceCode");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "City",
                newName: "CityName");
        }
    }
}
