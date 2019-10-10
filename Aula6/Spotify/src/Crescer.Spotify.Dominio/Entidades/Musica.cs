using System;

namespace Crescer.Spotify.Dominio.Entidades
{
    public class Musica
    {
        public Musica() { }
        public Musica(string nome, double duracao, Album album)
        {
            this.Nome = nome;
            this.Duracao = duracao;
            this.Album = album;
        }
        public int IdMusica { get; set; }
        public string Nome { get; private set; }
        public double Duracao { get; private set;} 
        public Album Album {get; set;}  

        public void Atualizar(Musica musica)
        {
            this.Nome = musica.Nome;
            this.Duracao = musica.Duracao;
        }
    }
}