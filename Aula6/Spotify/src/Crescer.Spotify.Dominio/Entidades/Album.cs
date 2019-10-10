using System.Collections.Generic;

namespace Crescer.Spotify.Dominio.Entidades
{
    public class Album
    {
        public Album(){}
        public Album(string nome)
        {
            Nome = nome;
        }
        public int IdAlbum { get; set; }
        public string Nome { get; set; }  
        public void Atualizar(Album album)
        {
            this.Nome = album.Nome;
        }
         
    }
}