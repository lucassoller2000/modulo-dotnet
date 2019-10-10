namespace Crescer.Spotify.Dominio.Entidades
{
    public class Avaliacao
    {
        public Avaliacao() { }
        public Avaliacao(int idUsuario, int idMusica, int nota)
        {
            this.IdUsuario = idUsuario;
            this.IdMusica = idMusica;
            this.Nota = nota;
        }
        public int IdAvaliacao { get; set; }
        public int IdUsuario { get; private set; }
        public int IdMusica { get; private set; }
        public int Nota { get; private set; }
    }
}