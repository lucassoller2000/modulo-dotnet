namespace Crescer.Spotify.Dominio.Entidades
{
    public class Usuario
    {
        public Usuario(){}
        public Usuario(string nome)
        {
            this.Nome = nome;
        }
        public int IdUsuario { get; set; }
        public string Nome { get; private set; }

        public void Atualizar(Usuario usuario)
        {
            this.Nome = usuario.Nome;
        }
    }
}