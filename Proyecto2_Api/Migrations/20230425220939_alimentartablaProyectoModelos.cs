using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Proyecto2Api.Migrations
{
    /// <inheritdoc />
    public partial class alimentartablaProyectoModelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProyectoModelos",
                columns: new[] { "Id", "Amenidad", "Cantidades", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Tarifa", "nombre" },
                values: new object[,]
                {
                    { 1, "", 5, "Detalle de la Vista..", new DateTime(2023, 4, 25, 17, 9, 39, 152, DateTimeKind.Local).AddTicks(6686), new DateTime(2023, 4, 25, 17, 9, 39, 152, DateTimeKind.Local).AddTicks(6669), "", 90, 1500.0, "Vista Panorámica" },
                    { 2, "", 1, "Detalle de la Vista..", new DateTime(2023, 4, 25, 17, 9, 39, 152, DateTimeKind.Local).AddTicks(6689), new DateTime(2023, 4, 25, 17, 9, 39, 152, DateTimeKind.Local).AddTicks(6688), "", 140, 5800.0, "Vista Premiun Panorámica" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProyectoModelos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProyectoModelos",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
