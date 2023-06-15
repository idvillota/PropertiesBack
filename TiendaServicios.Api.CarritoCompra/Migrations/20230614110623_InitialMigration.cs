using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaServicios.Api.CarritoCompra.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarritoSesion",
                columns: table => new
                {
                    CarritoSesionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoSesion", x => x.CarritoSesionId);
                });

            migrationBuilder.CreateTable(
                name: "CarritosesionDetalle",
                columns: table => new
                {
                    CarritoSesionDetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductoSeleccionado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarritoSessionId = table.Column<int>(type: "int", nullable: false),
                    CarritoSesionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritosesionDetalle", x => x.CarritoSesionDetalleId);
                    table.ForeignKey(
                        name: "FK_CarritosesionDetalle_CarritoSesion_CarritoSesionId",
                        column: x => x.CarritoSesionId,
                        principalTable: "CarritoSesion",
                        principalColumn: "CarritoSesionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarritosesionDetalle_CarritoSesionId",
                table: "CarritosesionDetalle",
                column: "CarritoSesionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritosesionDetalle");

            migrationBuilder.DropTable(
                name: "CarritoSesion");
        }
    }
}
