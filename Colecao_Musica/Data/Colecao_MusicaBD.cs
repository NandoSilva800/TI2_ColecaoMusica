using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Colecao_Musica.Models;

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Colecao_Musica.Data
{
    /// <summary>
    /// representa a DB da colecao de musica
    /// </summary>
    public class Colecao_MusicaBD : IdentityDbContext
    {

        //onde está guardada a BD --> appsettings.json
        // tipo da BD ---->    startup.cs

        public Colecao_MusicaBD(DbContextOptions<Colecao_MusicaBD> options) : base(options) { }


        /// <summary>
        /// Método para assistir a criaçºao da BD que representa o modelo
        /// </summary>
        /// <param name="modelBuilder">opção de configuração da criação do modelo</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // importa todo o comportamento deste método
            //Definido na classe DbContext
            base.OnModelCreating(modelBuilder);

            //********************************************************
            //Adicionar dados para as tabelas (seed)
            //********************************************************


            // adicionar os Roles
            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "a", Name = "Artista", NormalizedName = "ARTISTA" },
               new IdentityRole { Id = "g", Name = "Gestor", NormalizedName = "GESTOR" }
            );


           // // criar utilizadores
           // var appUser = new IdentityUser { Id = "Gestor", Email = "Gestor@g.gg", EmailConfirmed = true, UserName = "gestor@g.gg", NormalizedEmail = "GESTOR@G.GG", NormalizedUserName = "GESTOR@G.GG", LockoutEnabled = true, LockoutEnd = DateTimeOffset.Now };
           // var appUser1 = new IdentityUser { Id = "Queen", Email = "queen@q.uk", EmailConfirmed = true, UserName = "queen@q.uk", NormalizedEmail = "QUEEN@Q.UK", NormalizedUserName = "QUEEN@Q.UK", LockoutEnabled = true, LockoutEnd = DateTimeOffset.Now };
           // var appUser2 = new IdentityUser { Id = "PinkFloyd", Email = "pinkfloyd@pf.uk", EmailConfirmed = true, UserName = "pinkfloyd@pf.uk", NormalizedEmail = "PINKFLOYD@PF.UK", NormalizedUserName = "PINKFLOYD@PF.UK", LockoutEnabled = true, LockoutEnd = DateTimeOffset.Now };
           // var appUser3 = new IdentityUser { Id = "Eagles", Email = "eagles@e.us", EmailConfirmed = true, UserName = "eagles@e.us", NormalizedEmail = "EAGLES@E.US", NormalizedUserName = "EAGLES@E.US", LockoutEnabled = true, LockoutEnd = DateTimeOffset.Now };
           // var appUser4 = new IdentityUser { Id = "DireStraits", Email = "direstraits@ds.uk", EmailConfirmed = true, UserName = "direstraits@ds.uk", NormalizedEmail = "DIRESTRAITS@DS.UK", NormalizedUserName = "DIRESTRAITS@DS.UK", LockoutEnabled = true, LockoutEnd = DateTimeOffset.Now };
           // var appUser5 = new IdentityUser { Id = "LedZeppelin", Email = "ledzeppelin@lz.uk", EmailConfirmed = true, UserName = "ledzeppelin@lz.uk", NormalizedEmail = "LEDZEPPELIN@LZ.UK", NormalizedUserName = "LEDZEPPELIN@LZ.UK", LockoutEnabled = true, LockoutEnd = DateTimeOffset.Now };
           // var appUser6 = new IdentityUser { Id = "ACDC", Email = "acdc@a.au", EmailConfirmed = true, UserName = "acdc@a.au", NormalizedEmail = "ACDC@A.AU", NormalizedUserName = "ACDC@A.AU", LockoutEnabled = true, LockoutEnd = DateTimeOffset.Now };
           // var appUser7 = new IdentityUser { Id = "SimplyRed", Email = "simplyred@sr.uk", EmailConfirmed = true, UserName = "simplyred@sr.uk", NormalizedEmail = "SIMPLYRED@SR.UK", NormalizedUserName = "SIMPLYRED@SR.UK", LockoutEnabled = true, LockoutEnd = DateTimeOffset.Now };


           // //set user password
           // PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
           // appUser.PasswordHash = ph.HashPassword(appUser, "12345Fs#");
           // appUser1.PasswordHash = ph.HashPassword(appUser1, "12345Fs#");
           // appUser2.PasswordHash = ph.HashPassword(appUser2, "12345Fs#");
           // appUser3.PasswordHash = ph.HashPassword(appUser3, "12345Fs#");
           // appUser4.PasswordHash = ph.HashPassword(appUser4, "12345Fs#");
           // appUser5.PasswordHash = ph.HashPassword(appUser5, "12345Fs#");
           // appUser6.PasswordHash = ph.HashPassword(appUser6, "12345Fs#");
           // appUser7.PasswordHash = ph.HashPassword(appUser7, "12345Fs#");

           // //seed user
           // modelBuilder.Entity<IdentityUser>().HasData(appUser);
           // modelBuilder.Entity<IdentityUser>().HasData(appUser1);
           // modelBuilder.Entity<IdentityUser>().HasData(appUser2);
           // modelBuilder.Entity<IdentityUser>().HasData(appUser3);
           // modelBuilder.Entity<IdentityUser>().HasData(appUser4);
           // modelBuilder.Entity<IdentityUser>().HasData(appUser5);
           // modelBuilder.Entity<IdentityUser>().HasData(appUser6);
           // modelBuilder.Entity<IdentityUser>().HasData(appUser7);

           // //set user role 
           // modelBuilder.Entity<IdentityUserRole<string>>().HasData(
           //     new IdentityUserRole<string> { RoleId = "g", UserId = "Gestor" },
           //     new IdentityUserRole<string> { RoleId = "a", UserId = "Queen" },
           //     new IdentityUserRole<string> { RoleId = "a", UserId = "PinkFloyd" },
           //     new IdentityUserRole<string> { RoleId = "a", UserId = "DireStraits" },
           //     new IdentityUserRole<string> { RoleId = "a", UserId = "LedZeppelin" },
           //     new IdentityUserRole<string> { RoleId = "a", UserId = "ACDC" },
           //     new IdentityUserRole<string> { RoleId = "a", UserId = "Eagles" },
           //     new IdentityUserRole<string> { RoleId = "a", UserId = "SimplyRed" }
           // );
           // //Adicionar artistas
           // modelBuilder.Entity<Artistas>().HasData(
           //   new Artistas { Id = 1, Nome = "Eagles", Nacionalidade = "USA", Url = "https://eagles.com/", UserNameId = "Eagles" },
           //   new Artistas { Id = 8, Nome = "Queen", Nacionalidade = "UK", Url = "https://queen.com/", UserNameId = "Queen" },
           //   new Artistas { Id = 3, Nome = "Pink Floyd", Nacionalidade = "UK", Url = "https://pinkfloyd.com/", UserNameId = "PinkFloyd" },
           //   new Artistas { Id = 4, Nome = "Dire Straits", Nacionalidade = "UK", Url = "https://direstraits.com/", UserNameId = "DireStraits" },
           //   new Artistas { Id = 5, Nome = "Led Zeppelin", Nacionalidade = "UK", Url = "https://ledzeppelin.com/", UserNameId = "LedZeppelin" },
           //   new Artistas { Id = 6, Nome = "AC/DC", Nacionalidade = "AUS", Url = "https://acdc.com/", UserNameId = "ACDC" },
           //   new Artistas { Id = 7, Nome = "Simply Red", Nacionalidade = "Uk", Url = "https://simplyred.com/", UserNameId = "SimplyRed" }
           //);


           //// Adicionar dados às tabelas da BD
           // modelBuilder.Entity<Generos>().HasData(
           //    new Generos { Id = 1, Designacao = "Rock" },
           //    new Generos { Id = 2, Designacao = "Pop" },
           //    new Generos { Id = 3, Designacao = "Dance" },
           //    new Generos { Id = 4, Designacao = "Classica" },
           //    new Generos { Id = 5, Designacao = "Fado" },
           //    new Generos { Id = 6, Designacao = "Ópera" },
           //    new Generos { Id = 7, Designacao = "Heavy Metal" },
           //    new Generos { Id = 8, Designacao = "Jazz" }
           // );


            ////Adicionar Albuns
            //modelBuilder.Entity<Albuns>().HasData(
            //   new Albuns { Id = 1, Titulo = "Hotel california", Duracao = "43", NrFaixas = "9", Ano = "1976", Editora = "Asylom Records", Cover = "HotelCaliforniaAlbumCover.png", GenerosFK = 1, ArtistasFK = 1 },
            //   new Albuns { Id = 2, Titulo = "A Kind of Magic", Duracao = "40", NrFaixas = "14", Ano = "1986", Editora = "Emi", Cover = "A_Kind_of_MagicCover.png", GenerosFK = 1, ArtistasFK = 8 },
            //   new Albuns { Id = 3, Titulo = "Division Bell", Duracao = "58", NrFaixas = "11", Ano = "1993", Editora = "Emi",  Cover = "divisionbellCover.png", GenerosFK = 1, ArtistasFK = 3 },
            //   new Albuns { Id = 4, Titulo = "Brothers in Arms", Duracao = "43",NrFaixas = "9", Ano = "1985", Editora = "Warner",  Cover = "BrotherInArmsCover.png", GenerosFK = 1, ArtistasFK = 4 },
            //   new Albuns { Id = 5, Titulo = "Led Zeppelin IV", Duracao = "42", NrFaixas = "8", Ano = "1971", Editora = "Atlantic Records", Cover = "LedZeppelinIV.png", GenerosFK = 1, ArtistasFK = 5 },
            //   new Albuns { Id = 6, Titulo = "Back in Black", Duracao = "41", NrFaixas = "10", Ano = "1980", Editora = "Atlantic Records", Cover = "Back_in_Black.png", GenerosFK = 1, ArtistasFK = 6 },
            //   new Albuns { Id = 7, Titulo = "Whish You Were Here", Duracao = "42", NrFaixas = "5", Ano = "1975", Editora = "Emi", Cover = "Wish_You_Were_Here.png", GenerosFK = 1, ArtistasFK = 3 },
            //   new Albuns { Id = 8, Titulo = "A Night at Opera", Duracao = "43",NrFaixas = "12", Ano = "1975", Editora = "Emi",  Cover = "anightoperaCover.png", GenerosFK = 1, ArtistasFK = 8 },
            //   new Albuns { Id = 9, Titulo = "Stars", Duracao = "41",NrFaixas = "10", Ano = "1991", Editora = "East",  Cover = "starsCover.png", GenerosFK = 1, ArtistasFK = 7 },
            //   new Albuns { Id = 10, Titulo = "P.U.L.S.E", Duracao = "75", NrFaixas = "24", Ano = "1995", Editora = "Emi", Cover = "P•U•L•S•E.jpg", GenerosFK = 1, ArtistasFK = 3 }
            //);


            ////Adicionar músicas
            //modelBuilder.Entity<Musicas>().HasData(
            //   new Musicas { Id = 1, Titulo = "Hotel California", Duracao = "6", Ano = "1976", Compositor = "", ArtistasFK = 1 },
            //   new Musicas { Id = 2, Titulo = "A Kind of Magic", Duracao = "4", Ano = "1986", Compositor = "", ArtistasFK = 8 },
            //   new Musicas { Id = 3, Titulo = "What Do You Want from Me", Duracao = "4", Ano = "1993", Compositor = "", ArtistasFK = 3 },
            //   new Musicas { Id = 4, Titulo = "Brothers in Arms", Duracao = "7", Ano = "1985", Compositor = "", ArtistasFK = 4 },
            //   new Musicas { Id = 5, Titulo = "Stairway to Heaven", Duracao = "8", Ano = "1971", Compositor = "", ArtistasFK = 5 },
            //   new Musicas { Id = 6, Titulo = "Hells Bells", Duracao = "5", Ano = "1980", Compositor = "", ArtistasFK = 6 },
            //   new Musicas { Id = 7, Titulo = "Comfortably Numb", Duracao = "9", Ano = "1979", Compositor = "", ArtistasFK = 3 },
            //   new Musicas { Id = 8, Titulo = "Bohemian Rhapsody", Duracao = "5", Ano = "1975", Compositor = "", ArtistasFK = 8 },
            //   new Musicas { Id = 9, Titulo = "Wish You Were Here", Duracao = "5", Ano = "1975", Compositor = "", ArtistasFK = 3 },
            //   new Musicas { Id = 10, Titulo = "The Invisible Man", Duracao = "3", Ano = "1989", Compositor = "", ArtistasFK = 8 },
            //   new Musicas { Id = 11, Titulo = "Another One Bites the Dust", Duracao = "3", Ano = "1980", Compositor = "", ArtistasFK = 8 },
            //   new Musicas { Id = 12, Titulo = "Stars", Duracao = "4", Ano = "1991", Compositor = "", ArtistasFK = 7 }

            //);


        }


        //*********************************************
        //especificar as tabelas na BD
        //*********************************************
        public DbSet<Musicas> Musicas { get; set; }
                public DbSet<Albuns> Albuns { get; set; }
                public DbSet<Artistas> Artistas { get; set; }
                public DbSet<Generos> Generos { get; set; }
              	//*********************************************
        

    }
}