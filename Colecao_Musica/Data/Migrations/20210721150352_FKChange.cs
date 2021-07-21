using Microsoft.EntityFrameworkCore.Migrations;

namespace Colecao_Musica.Data.Migrations
{
    public partial class FKChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicas_Artistas_ArtistasFK",
                table: "Musicas");

            migrationBuilder.RenameColumn(
                name: "ArtistasFK",
                table: "Musicas",
                newName: "ArtistaFK");

            migrationBuilder.RenameIndex(
                name: "IX_Musicas_ArtistasFK",
                table: "Musicas",
                newName: "IX_Musicas_ArtistaFK");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "2abed0dd-e787-4e5d-99a5-5ee0c2e6c4a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "3a7d62de-eaff-40fc-8211-75aab9a3fa58");

            migrationBuilder.AddForeignKey(
                name: "FK_Musicas_Artistas_ArtistaFK",
                table: "Musicas",
                column: "ArtistaFK",
                principalTable: "Artistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicas_Artistas_ArtistaFK",
                table: "Musicas");

            migrationBuilder.RenameColumn(
                name: "ArtistaFK",
                table: "Musicas",
                newName: "ArtistasFK");

            migrationBuilder.RenameIndex(
                name: "IX_Musicas_ArtistaFK",
                table: "Musicas",
                newName: "IX_Musicas_ArtistasFK");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "e5cfcf25-dbb6-4ed2-a1af-3f464694f9fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "946c86e8-0ec4-4e32-8424-9496bd7bd09d");

            migrationBuilder.AddForeignKey(
                name: "FK_Musicas_Artistas_ArtistasFK",
                table: "Musicas",
                column: "ArtistasFK",
                principalTable: "Artistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
