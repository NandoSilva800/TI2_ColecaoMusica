using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Colecao_Musica.Models
{   
    /// <summary>
    /// Identifica os géneros de uma música
    /// </summary>
    public class Generos
    {
        /// <summary>
        /// Construtor da classe Genero
        /// </summary>
        public Generos()
        {
            //aceder à BD, e selecionar todos os albuns do genero
            ListaDeAlbuns = new HashSet<Albuns>();
        }

        /// <summary>
        /// Chave primaria 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// nome do genero de musica
        /// </summary>
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [StringLength(20, ErrorMessage = "A {0} não deve ter mais que {1} caracteres.")]
        public string Designacao { get; set; }

        //***************************************************************
        //Criar a lista de Albuns a que um Genero está associada
        //***************************************************************
        public ICollection <Albuns> ListaDeAlbuns{ get; set; }

    }
}// Fim da classe Géneros
