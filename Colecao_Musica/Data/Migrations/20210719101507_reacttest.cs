using Microsoft.EntityFrameworkCore.Migrations;

namespace Colecao_Musica.Data.Migrations
{
    public partial class reacttest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albuns_Artistas_ArtistasFK",
                table: "Albuns");

            migrationBuilder.DropForeignKey(
                name: "FK_Albuns_Generos_GenerosFK",
                table: "Albuns");

            migrationBuilder.RenameColumn(
                name: "GenerosFK",
                table: "Albuns",
                newName: "GeneroFK");

            migrationBuilder.RenameColumn(
                name: "ArtistasFK",
                table: "Albuns",
                newName: "ArtistaFK");

            migrationBuilder.RenameIndex(
                name: "IX_Albuns_GenerosFK",
                table: "Albuns",
                newName: "IX_Albuns_GeneroFK");

            migrationBuilder.RenameIndex(
                name: "IX_Albuns_ArtistasFK",
                table: "Albuns",
                newName: "IX_Albuns_ArtistaFK");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Musicas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Duracao",
                table: "Musicas",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Compositor",
                table: "Musicas",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ano",
                table: "Musicas",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Albuns",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NrFaixas",
                table: "Albuns",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Editora",
                table: "Albuns",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Duracao",
                table: "Albuns",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Ano",
                table: "Albuns",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "0674cfa0-4279-4cc6-a6f5-320728a7e7cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "f2bd7d28-8fab-421b-ac93-e5617d831853");

            migrationBuilder.AddForeignKey(
                name: "FK_Albuns_Artistas_ArtistaFK",
                table: "Albuns",
                column: "ArtistaFK",
                principalTable: "Artistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Albuns_Generos_GeneroFK",
                table: "Albuns",
                column: "GeneroFK",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albuns_Artistas_ArtistaFK",
                table: "Albuns");

            migrationBuilder.DropForeignKey(
                name: "FK_Albuns_Generos_GeneroFK",
                table: "Albuns");

            migrationBuilder.RenameColumn(
                name: "GeneroFK",
                table: "Albuns",
                newName: "GenerosFK");

            migrationBuilder.RenameColumn(
                name: "ArtistaFK",
                table: "Albuns",
                newName: "ArtistasFK");

            migrationBuilder.RenameIndex(
                name: "IX_Albuns_GeneroFK",
                table: "Albuns",
                newName: "IX_Albuns_GenerosFK");

            migrationBuilder.RenameIndex(
                name: "IX_Albuns_ArtistaFK",
                table: "Albuns",
                newName: "IX_Albuns_ArtistasFK");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Musicas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Duracao",
                table: "Musicas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Compositor",
                table: "Musicas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(35)",
                oldMaxLength: 35,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ano",
                table: "Musicas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Albuns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "NrFaixas",
                table: "Albuns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Editora",
                table: "Albuns",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Duracao",
                table: "Albuns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Ano",
                table: "Albuns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "d12fc354-74d1-4d2f-8cc0-46981fe67824");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "e6a6969a-ee64-412b-8df0-b3887edc856d");

            migrationBuilder.AddForeignKey(
                name: "FK_Albuns_Artistas_ArtistasFK",
                table: "Albuns",
                column: "ArtistasFK",
                principalTable: "Artistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Albuns_Generos_GenerosFK",
                table: "Albuns",
                column: "GenerosFK",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
