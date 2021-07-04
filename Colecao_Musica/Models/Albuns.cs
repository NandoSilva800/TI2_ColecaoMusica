using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Colecao_Musica.Models
{
    /// <summary>
    /// Dados de um album
    /// </summary>
    public class Albuns
    {
        /// <summary>
        /// Construtor da classe Album
        /// </summary>
        public Albuns()
        {
            //aceder à BD, e selecionar todos as musicas do album
            ListaDeMusicas = new HashSet<Musicas>();
        }

        /// <summary>
        /// Chave primaria para os albuns
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Titulo de um album
        /// </summary>
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        //[StringLength(50, ErrorMessage = "O {0} não deve ter mais que {1} caracteres.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        /// <summary>
        /// Duração total de um album
        /// </summary>
        [Required(ErrorMessage = "Preenchimento obrigatório no formato 00 minutos")]
        //[RegularExpression("[0-9]{1,2}", ErrorMessage = "Insira a duração do album em minutos")]
        //[StringLength(2, MinimumLength = 1)]
        [Display(Name = "Duração minutos")]
        public string Duracao { get; set; }

        /// <summary>
        /// Numero total de faixas de um album
        /// </summary>
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        //[RegularExpression("[1-9][0-9]?", ErrorMessage = "Insira o numero de faixas do album")]
        //[StringLength(2, MinimumLength = 1)]
        [Display(Name = "Total de faixas")]
        public string NrFaixas { get; set; }

        /// <summary>
        /// Ano em que foi editado o album
        /// </summary>
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        //[RegularExpression("[0-9]{4}", ErrorMessage = "Insira o ano do album" )]
        //[StringLength(4, MinimumLength = 4, ErrorMessage = "O {0} deve conter {1} caracteres.")]
        public string Ano { get; set; }

        /// <summary>
        /// Nome da editora que editou o album
        /// </summary>
        //[StringLength(40, ErrorMessage = "A {0} não deve ter mais que {1} caracteres.")]
        public string Editora { get; set; }

        /// <summary>
        /// Imagem referente a capa do album
        /// </summary>
        public string Cover { get; set; }



        //************************************************************************
        //FK para Generos
        //************************************************************************
        //Para facilitar o programador a criar os controlers as linhas seguintes
        
        [ForeignKey(nameof(Genero))] //Anotador para o Entity Framework (com nome do objeto em vez do objeto
        [Required(ErrorMessage = "Seleção obrigatória")]
        [Display(Name = "Género")]
        public int GenerosFK { get; set; }      //FK para Generos np SGBD(SQL) 
        public Generos Genero { get; set; }     //FK para Generos no C#


        //***********************************************************************
        //FK para Artistas
        //***********************************************************************
        //Para facilitar o programador a criar os controlers as linhas seguintes
        [ForeignKey(nameof(Artista))] //Anotador para o Entity Framework (com nome do objeto em vez do objeto)
        [Required(ErrorMessage = "Seleção obrigatória")]
        public int ArtistasFK { get; set; }      //FK para Artistas np SGBD(SQL)
        public Artistas Artista { get; set; }    //FK para Artistas no C#


        //**********************************************************************
        //Criar a lista de Musicas a que um Album está associado
        //**********************************************************************
        [Required(ErrorMessage = "É obrigatório escolher uma música.")]
        public ICollection<Musicas> ListaDeMusicas { get; set; }
    }
}//Fim da classe Albuns
