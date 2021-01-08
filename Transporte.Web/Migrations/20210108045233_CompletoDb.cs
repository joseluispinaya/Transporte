using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Transporte.Web.Migrations
{
    public partial class CompletoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Sindicatos",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Afiliados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombres = table.Column<string>(maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(maxLength: 100, nullable: false),
                    NroDocumento = table.Column<string>(maxLength: 10, nullable: false),
                    Celular = table.Column<string>(maxLength: 10, nullable: false),
                    Foto = table.Column<string>(nullable: true),
                    Imgqr = table.Column<string>(nullable: true),
                    SindicatoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afiliados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Afiliados_Sindicatos_SindicatoId",
                        column: x => x.SindicatoId,
                        principalTable: "Sindicatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nroplaca = table.Column<string>(maxLength: 8, nullable: false),
                    Nrochasis = table.Column<string>(maxLength: 20, nullable: false),
                    Nromotor = table.Column<string>(maxLength: 20, nullable: false),
                    Marca = table.Column<string>(maxLength: 20, nullable: false),
                    AfiliadoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Afiliados_AfiliadoId",
                        column: x => x.AfiliadoId,
                        principalTable: "Afiliados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Afiliados_SindicatoId",
                table: "Afiliados",
                column: "SindicatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_AfiliadoId",
                table: "Vehiculos",
                column: "AfiliadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Afiliados");

            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Sindicatos",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);
        }
    }
}
