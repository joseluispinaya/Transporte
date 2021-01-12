using Microsoft.EntityFrameworkCore.Migrations;

namespace Transporte.Web.Migrations
{
    public partial class AddIndexplaca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_Nroplaca",
                table: "Vehiculos",
                column: "Nroplaca",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehiculos_Nroplaca",
                table: "Vehiculos");
        }
    }
}
