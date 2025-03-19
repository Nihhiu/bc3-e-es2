using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComparacaoPrecos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoToAspNetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Passo 1: Adicionar a coluna "Tipo" permitindo valores nulos inicialmente
            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "AspNetUsers",
                type: "text",
                nullable: true // Permite valores nulos para evitar erros
            );

            // Passo 2: Atualizar os registros existentes para o valor padrão
            migrationBuilder.Sql("UPDATE \"AspNetUsers\" SET \"Tipo\" = 'User' WHERE \"Tipo\" IS NULL");

            // Passo 3: Alterar a coluna para não permitir nulos e definir um valor padrão
            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "User"
            );
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove a coluna "Tipo" da tabela AspNetUsers
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "AspNetUsers"
            );
        }
    }
}
