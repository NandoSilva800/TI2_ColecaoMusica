using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Colecao_Musica.Models
{
    /// <summary>
    /// Dados de uma musica
    /// </summary>
    public class Musicas
    {
        /// <summary>
        /// Construtor da classe Musicas
        /// </summary>
        public Musicas()
        {
            //aceder à BD, e selecionar todos os albuns que contém a musica
            //Inicialização da lista de albuns
            ListaDeAlbuns = new HashSet<Albuns>();
        }

        /// <summary>
        /// Chave primaria para as musicas
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Titulo de uma musica
        /// </summary>
        [Required(ErrorMessage ="Preenchimento obrigatório")]
        [StringLength(50, ErrorMessage = "O {0} não deve ter mais que {1} caracteres.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        /// <summary>
        /// Duração de uma musica
        /// </summary>
        [Required(ErrorMessage = "Preenchimento obrigatório no formato 00 minutos")]
        [RegularExpression("[0-9]{1,2}", ErrorMessage = "Insira a duração do album em minutos")]
        [StringLength(2, MinimumLength = 1)]
        [Display(Name = "Duração minutos")]
        public string Duracao { get; set; }

        /// <summary>
        /// Ano em que foi editada uma musica
        /// </summary>
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [RegularExpression("[0-9]{4}", ErrorMessage = "Insira o ano do album")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "O {0} deve conter {1} caracteres.")]
        public string Ano { get; set; }

        /// <summary>
        /// Nome do compositor de uma música
        /// </summary>
        [StringLength(35, ErrorMessage = "O {0} não deve ter mais que {1} caracteres.")]
        public string Compositor { get; set; }


        /// <summary>
        /// Album em que está inserido a música
        /// </summary>
        [Display(Name = "Albuns")]
        public string AlbunSel { get; set; }


        //********************************************************************************
        //FK para Artistas
        //********************************************************************************
        //Para facilitar o programador a criar os controlers as linhas seguintes
        [ForeignKey(nameof(Artista))] //Anotador para o Entity Framework (com nome do objeto em vez do objeto)
        public int ArtistasFK { get; set; }      //FK para Artistas np SGBD(SQL)
        public Artistas Artista { get; set; }     //FK para Artistas no C#
        //********************************************************************************

        

        //*******************************************************************************
        //Criar a lista de Albuns a que uma musica está associada
        //*******************************************************************************
        public ICollection<Albuns> ListaDeAlbuns { get; set; }
        //*******************************************************************************

    }
} //Fim da classe Musicas
