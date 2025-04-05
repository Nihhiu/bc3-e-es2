using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComparacaoPrecos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 1,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 41.703041900000002, -8.8216178999999997 });

            migrationBuilder.UpdateData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 2,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 41.7036984, -8.8242560000000001 });

            migrationBuilder.UpdateData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 3,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 41.7004442, -8.8391286999999998 });

            migrationBuilder.UpdateData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 4,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 41.707032099999999, -8.8442059999999998 });
        }
    }
}
