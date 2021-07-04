using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Colecao_Musica.Models
{
    /// <summary>
    /// Dados de um artista
    /// </summary>
    public class Artistas
    {
        /// <summary>
        /// Construtor da classe Artistas
        /// </summary>
        public Artistas()
        {
            //aceder à BD, e selecionar todos as musicas do artista
            ListaDeMusicas = new HashSet<Musicas>();
        }

        /// <summary>
        /// Chave primária para os artistas
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome de artista ou banda
        /// </summary>
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        //[StringLength (50, ErrorMessage ="O {0} do artista/banda não deve ser maior que {1} caracteres.")]
        //[RegularExpression("[A-Za-zàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð '-]+", ErrorMessage = "O {0} só aceita letras...")]
        public string Nome { get; set; }

        /// <summary>
        /// Nacionalidade de um artista
        /// </summary>
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        //[StringLength(25, ErrorMessage = "A {0} do artista/banda não deve ter mais que {1} caracteres.")]
        public string Nacionalidade { get; set; }

        /// <summary>
        /// link da pagina de um artista
        /// </summary>
      
        [Display(Name ="Página Web do Artista")]
        public string Url { get; set; }

        //###########################################################################
        // FK para a tabela de Autenticação
        //###########################################################################
        /// <summary>
        /// Chave de ligação entre a Autenticação e os Artistas
        /// Consegue-se, por exemplo, filtrar os dados dos Artistas qd se autenticam
        /// </summary>
        public string UserNameId { get; set; }
        //###########################################################################

 


        //***************************************************************
        //Criar a lista de musicas a que um artista está associado
        //***************************************************************
        public ICollection<Musicas> ListaDeMusicas { get; set; }
    }
} //Fim classe Artistas
