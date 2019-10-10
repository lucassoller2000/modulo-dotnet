using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Contratos
{
    public interface IUsuarioRepository
    {     
        Usuario SalvarUsuario(Usuario usuario);
        Avaliacao AvaliarMusica(Avaliacao avaliacao);
        Usuario AtualizarUsuario(int id, Usuario usuario);
        Usuario DeletarUsuario(int id);
        List<Usuario> ListarUsuario();
        Usuario Obter(int id);
    }
}