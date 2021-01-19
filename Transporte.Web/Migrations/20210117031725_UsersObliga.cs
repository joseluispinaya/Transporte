using Microsoft.EntityFrameworkCore.Migrations;

namespace Transporte.Web.Migrations
{
    public partial class UsersObliga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Celular",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Celular",
                table: "AspNetUsers",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
