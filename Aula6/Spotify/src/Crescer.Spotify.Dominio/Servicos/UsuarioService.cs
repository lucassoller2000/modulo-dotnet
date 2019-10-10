using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Servicos
{
    public class UsuarioService
    {
        public List<string> Validar(Usuario usuario)
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(usuario.Nome))
                mensagens.Add("É necessário informar o nome do usuário");
            
            return mensagens;
        }

        public List<string> Validar(Avaliacao avaliacao)
        {
            List<string> mensagens = new List<string>();

            if (avaliacao.Nota<1 || avaliacao.Nota>5)
                mensagens.Add("A avaliação só pode receber notas de 1 até 5");
            
            return mensagens;
        }
    }
}