using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComparacaoPrecos.Migrations
{
    /// <inheritdoc />
    public partial class AddCredibilidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data",
                table: "Produto_Loja");

            migrationBuilder.DropColumn(
                name: "hora",
                table: "Produto_Loja");

            migrationBuilder.DropColumn(
                name: "Localizacao",
                table: "Loja");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHora",
                table: "Produto_Loja",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "credibilidade",
                table: "Produto_Loja",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Loja",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Loja",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataHora",
                table: "Produto_Loja");

            migrationBuilder.DropColumn(
                name: "credibilidade",
                table: "Produto_Loja");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Loja");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Loja");

            migrationBuilder.AddColumn<DateOnly>(
                name: "data",
                table: "Produto_Loja",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "hora",
                table: "Produto_Loja",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Localizacao",
                table: "Loja",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
