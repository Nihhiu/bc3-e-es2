using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComparacaoPrecos.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProdutoLojaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Produto_Loja");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Produto_Loja",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
