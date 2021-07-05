using Microsoft.EntityFrameworkCore.Migrations;

namespace Colecao_Musica.Data.Migrations
{
    public partial class newAtribMusModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlbumSel",
                table: "Musicas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "b81edbd2-8efd-4d19-9280-2149d3a29442");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "ce0ba7a2-8570-41f5-bd3a-18d9aaccfdd3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumSel",
                table: "Musicas");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "0f761397-65fe-4f56-aa14-f3025377ccc9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "d44c2a6c-c3f0-46c7-b221-a1a40915d248");
        }
    }
}
