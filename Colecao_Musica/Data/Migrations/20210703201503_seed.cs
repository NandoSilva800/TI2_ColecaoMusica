using Microsoft.EntityFrameworkCore.Migrations;

namespace Colecao_Musica.Data.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artistas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nacionalidade = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserNameId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artistas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designacao = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Duracao = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Ano = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Compositor = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ArtistasFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musicas_Artistas_ArtistasFK",
                        column: x => x.ArtistasFK,
                        principalTable: "Artistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Albuns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Duracao = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    NrFaixas = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Ano = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Editora = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenerosFK = table.Column<int>(type: "int", nullable: false),
                    ArtistasFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albuns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albuns_Artistas_ArtistasFK",
                        column: x => x.ArtistasFK,
                        principalTable: "Artistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Albuns_Generos_GenerosFK",
                        column: x => x.GenerosFK,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlbunsMusicas",
                columns: table => new
                {
                    ListaDeAlbunsId = table.Column<int>(type: "int", nullable: false),
                    ListaDeMusicasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbunsMusicas", x => new { x.ListaDeAlbunsId, x.ListaDeMusicasId });
                    table.ForeignKey(
                        name: "FK_AlbunsMusicas_Albuns_ListaDeAlbunsId",
                        column: x => x.ListaDeAlbunsId,
                        principalTable: "Albuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlbunsMusicas_Musicas_ListaDeMusicasId",
                        column: x => x.ListaDeMusicasId,
                        principalTable: "Musicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albuns_ArtistasFK",
                table: "Albuns",
                column: "ArtistasFK");

            migrationBuilder.CreateIndex(
                name: "IX_Albuns_GenerosFK",
                table: "Albuns",
                column: "GenerosFK");

            migrationBuilder.CreateIndex(
                name: "IX_AlbunsMusicas_ListaDeMusicasId",
                table: "AlbunsMusicas",
                column: "ListaDeMusicasId");

            migrationBuilder.CreateIndex(
                name: "IX_Musicas_ArtistasFK",
                table: "Musicas",
                column: "ArtistasFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbunsMusicas");

            migrationBuilder.DropTable(
                name: "Albuns");

            migrationBuilder.DropTable(
                name: "Musicas");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Artistas");
        }
    }
}
