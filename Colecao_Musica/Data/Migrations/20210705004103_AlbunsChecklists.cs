using Microsoft.EntityFrameworkCore.Migrations;

namespace Colecao_Musica.Data.Migrations
{
    public partial class AlbunsChecklists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AlbumSel",
                table: "Musicas",
                newName: "Albuns");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "666b9606-5f5d-451f-8c01-d6a355171c0c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "4de656eb-914c-4f3c-b67b-0c381a79d2ef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Albuns",
                table: "Musicas",
                newName: "AlbumSel");

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
    }
}
