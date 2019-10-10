using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Crescer.Spotify.Infra.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private SpotifyContext contexto;

        public UsuarioRepository(SpotifyContext contexto)
        {
            this.contexto = contexto;
        }

        public Usuario AtualizarUsuario(int id, Usuario usuario)
        {         
            var usuarioCadastrado = contexto.Usuarios.FirstOrDefault(p => p.IdUsuario == id);
            if(usuarioCadastrado != null)
                usuarioCadastrado.Atualizar(usuario);
            
            return usuarioCadastrado;
        }

        public Avaliacao AvaliarMusica (Avaliacao avaliacao)
        {
            var avaliacaoCadastrada = this.BuscarAvaliacao(avaliacao);
            if(avaliacaoCadastrada != null){
               avaliacaoCadastrada.Atualizar(avaliacao);
               return avaliacaoCadastrada;
            }
            else{
                contexto.Avaliacoes.Add(avaliacao);
                return avaliacao;
            }
        }

        public Usuario DeletarUsuario(int id)
        {
            var usuarioCadastrado = contexto.Usuarios.FirstOrDefault(p => p.IdUsuario == id);
            if(usuarioCadastrado != null)
                contexto.Usuarios.Remove(usuarioCadastrado);
            
            return usuarioCadastrado;
        }

        public List<Usuario> ListarUsuario()
        {
           return contexto.Usuarios.AsNoTracking().ToList();
        }

        public Usuario Obter(int id)
        {
            return contexto.Usuarios.FirstOrDefault(p => p.IdUsuario == id);    
        }

        public Usuario SalvarUsuario(Usuario usuario)
        {
            contexto.Usuarios.Add(usuario);
            return usuario;
        }

        public Avaliacao BuscarAvaliacao(Avaliacao avaliacao)
        {
            return contexto.Avaliacoes.FirstOrDefault(p => p.Usuario.IdUsuario == avaliacao.Usuario.IdUsuario && p.Musica.IdMusica == avaliacao.Musica.IdMusica);
        }
    }
}