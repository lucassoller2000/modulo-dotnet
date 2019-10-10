using System.Collections.Generic;
using Crescer.Booking.Dominio.Entidades;

namespace Crescer.Booking.Dominio.Contratos
{
    public interface IUsuarioRepository
    {
        Usuario SalvarUsuario(Usuario usuario);
        Usuario AtualizarUsuario(int id, Usuario usuarioAtualizado);
        Usuario DeletarUsuario(int id);
        List<Usuario> ListarUsuarios();
        Usuario Obter(int id);
        Usuario ObterUsuarioPorEmailESenha(string email, string senha);

        Usuario ObterUsuarioPorEmail(string email);

        bool obterUsuariosIguais(string email);
    }
}