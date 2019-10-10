using System.Collections.Generic;

namespace SpotifyDoCrescer.Dominio.Entidades
{
    public class Album
    {
        public Album(string nome)
        {
            this.Nome = nome;
            this.Musicas = new List<Musica>();
        }
        
        public int Id { get; set; }

        public string Nome{ get; private set;}

        public List<Musica> Musicas { get; private set; }
    }
}