using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ComparacaoPrecos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaID", "Deleted" },
                values: new object[,]
                {
                    { "Artesanato", false },
                    { "Automóveis e Acessórios", false },
                    { "Bebidas", false },
                    { "Brinquedos", false },
                    { "Carnes", false },
                    { "Cosméticos", false },
                    { "Decoração", false },
                    { "Eletrodomésticos", false },
                    { "Esportes e Fitness", false },
                    { "Ferramentas", false },
                    { "Games", false },
                    { "Higiene Pessoal", false },
                    { "Iluminação", false },
                    { "Instrumentos Musicais", false },
                    { "Laticínios", false },
                    { "Material de Construção", false },
                    { "Móveis", false },
                    { "Papelaria", false },
                    { "Peixes", false },
                    { "Pet Shop", false },
                    { "Produtos de Limpeza", false },
                    { "Produtos Naturais", false },
                    { "Relógios e Joias", false },
                    { "Saúde e Bem-Estar", false }
                });

            migrationBuilder.InsertData(
                table: "Loja",
                columns: new[] { "LojaID", "Deleted", "Latitude", "Longitude", "Nome" },
                values: new object[,]
                {
                    { 1, false, 41.703041900000002, -8.8216178999999997, "Pingo Doce" },
                    { 2, false, 41.7036984, -8.8242560000000001, "Lidl" },
                    { 3, false, 41.7004442, -8.8391286999999998, "Continente" },
                    { 4, false, 41.707032099999999, -8.8442059999999998, "Mercadona" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Artesanato");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Automóveis e Acessórios");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Bebidas");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Brinquedos");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Carnes");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Cosméticos");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Decoração");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Eletrodomésticos");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Esportes e Fitness");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Ferramentas");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Games");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Higiene Pessoal");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Iluminação");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Instrumentos Musicais");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Laticínios");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Material de Construção");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Móveis");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Papelaria");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Peixes");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Pet Shop");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Produtos de Limpeza");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Produtos Naturais");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Relógios e Joias");

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: "Saúde e Bem-Estar");

            migrationBuilder.DeleteData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Loja",
                keyColumn: "LojaID",
                keyValue: 4);
        }
    }
}
