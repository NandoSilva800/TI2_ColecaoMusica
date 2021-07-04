using System;
using System.Collections.Generic;

namespace Colecao_Musica.Models
{
   


    public class ViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }









    ///// <summary>
    ///// classe usada para transportar os dados necessários 
    ///// à correta visualização dos albuns de cada artista
    ///// </summary>
    //public class ListarAlbunsViewModel
    //{

        
    //    /// <summary>
    //    /// Lista dos albuns do artista
    //    /// </summary>
    //    public ICollection<Albuns> ListaAlbuns { get; set; }
    //}

    ///// <summary>
    ///// classe usada para transportar os dados necessários 
    ///// à correta visualização dos albuns de cada artista
    ///// </summary>
    //public class ListarMusicasViewModel
    //{

    //    /// <summary>
    //    /// lista das músicas dos artisatas
    //    /// </summary>
    //    public ICollection<Musicas> ListaMusicas { get; set; }

       
    //}


}
