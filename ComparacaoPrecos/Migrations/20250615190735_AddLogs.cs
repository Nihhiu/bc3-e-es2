using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ComparacaoPrecos.Migrations
{
    /// <inheritdoc />
    public partial class AddLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: false),
                    DataHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_Logs_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 1,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 41.702711399999998, -8.8167361 });

            migrationBuilder.UpdateData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 2,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 41.706357300000001, -8.8200368999999998 });

            migrationBuilder.UpdateData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 3,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 41.704391700000002, -8.8152299000000003 });

            migrationBuilder.UpdateData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 4,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 41.707097300000001, -8.8255710999999994 });

            migrationBuilder.InsertData(
                table: "Loja",
                columns: new[] { "LojaID", "Deleted", "Latitude", "Longitude", "Nome" },
                values: new object[,]
                {
                    { 5, false, 41.694973699999998, -8.8331628999999996, "Worten" },
                    { 6, false, 41.703153200000003, -8.8243452999999992, "Dimacer" },
                    { 7, false, 41.695014, -8.8439396000000006, "ALDI" },
                    { 8, false, 41.698815799999998, -8.8464872000000003, "Toyota-Macedo & Macedo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Id",
                table: "Logs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DeleteData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 1,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 41.707027799999999, -8.8251667000000005 });

            migrationBuilder.UpdateData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 2,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 41.707027799999999, -8.8251667000000005 });

            migrationBuilder.UpdateData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 3,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 41.704268900000002, -8.8147794000000008 });

            migrationBuilder.UpdateData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 4,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 41.707032599999998, -8.8251516999999993 });
        }
    }
}
