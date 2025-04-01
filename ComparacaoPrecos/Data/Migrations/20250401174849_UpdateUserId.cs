using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComparacaoPrecos.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Loja_AspNetUsers_UserId",
                table: "Produto_Loja");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Produto_Loja",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Produto_Loja_UserId",
                table: "Produto_Loja",
                newName: "IX_Produto_Loja_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Loja_AspNetUsers_Id",
                table: "Produto_Loja",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Loja_AspNetUsers_Id",
                table: "Produto_Loja");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Produto_Loja",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Produto_Loja_Id",
                table: "Produto_Loja",
                newName: "IX_Produto_Loja_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Loja_AspNetUsers_UserId",
                table: "Produto_Loja",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
