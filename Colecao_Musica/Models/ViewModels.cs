using System;
using System.Collections.Generic;

namespace Colecao_Musica.Models
{
    /// <summary>
    /// ViewModel para transportar os dados dos albuns 
    /// dos artistas, na API
    /// </summary>
    public class ListaAlbunsAPIViewModel {

        /// <summary>
        /// Id do album
        /// </summary>
        public int IdAlbum { get; set; }
        
        /// <summary>
        /// Título do album
        /// </summary>
        public string TituloAlbum { get; set; }
        
        /// <summary>
        /// Duração total do album
        /// </summary>
        public string DuracaoAlbum { get; set; }
        
        /// <summary>
        /// Numero de faixas que o album contém
        /// </summary>
        public string NrFaixasAlbum { get; set; }
        
        /// <summary>
        /// Ano que o album foi editado
        /// </summary>
        public string AnoAlbum { get; set; }
        
        /// <summary>
        /// Nome da editora que editou o album
        /// </summary>
        public string EditoraAlbum { get; set; }
        
        /// <summary>
        /// Imagem da capa do album
        /// </summary>
        public string CoverAlbum { get; set; }

        /// <summary>
        /// Genero do album
        /// </summary>
        public string GeneroAlbum { get; set; }

        /// <summary>
        /// Nome do artista do album
        /// </summary>
        public string NomeArtista { get; set; }

    }

    //public class ListaAlbunsAPIViewModel
    //{
    ////    /// <summary>
    ////    /// lista dos albuns
    ////    /// </summary>
    //    public ICollection<Albuns> listaDadosAlbuns { get; set; }
        
    ////    /// <summary>
    ////    /// lista de artistas
    ////    /// </summary>
    //    public ICollection<int>ListaArtistas{ get; set; }

    //    public ICollection<int>ListaGeneros{ get; set; }
    //}



    public class ListaArtistasAPIViewModel
    {
        /// <summary>
        /// Id do Artista
        /// </summary>
        public int IdArtista { get; set; }
        
        /// <summary>
        /// Nome do artista
        /// </summary>
        public string NomeArtista { get; set; }

    }

    public class ListaGenerosAPIViewModel
    {
        /// <summary>
        /// Id do Artista
        /// </summary>
        public int IdGenero { get; set; }

        /// <summary>
        /// Nome do artista
        /// </summary>
        public string GeneroAlbum { get; set; }

    }



    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
