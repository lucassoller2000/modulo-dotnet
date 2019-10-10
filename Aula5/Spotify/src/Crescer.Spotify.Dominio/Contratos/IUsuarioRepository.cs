using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Contratos
{
    public interface IUsuarioRepository
    {     
        void SalvarUsuario(Usuario usuario);
        int AvaliarMusica(Avaliacao avaliacao);
        void AtualizarUsuario(int id, Usuario usuario);
        void DeletarUsuario(int id);
        List<Usuario> ListarUsuario();
        Usuario Obter(int id);
    }
}