namespace Crescer.Spotify.Dominio.Entidades
{
    public class Avaliacao
    {
        public Avaliacao() { }
        public Avaliacao(Usuario usuario, Musica musica, decimal nota)
        {
            this.Usuario = usuario;
            this.Musica = musica;
            this.Nota = nota;
        }
        public int IdAvaliacao { get; set; }
        public Usuario Usuario { get; private set; }
        public Musica Musica { get; private set; }
        public decimal Nota { get; private set; }

        public void Atualizar(Avaliacao avaliacao)
        {
            this.Nota = avaliacao.Nota;
        }
    }
}