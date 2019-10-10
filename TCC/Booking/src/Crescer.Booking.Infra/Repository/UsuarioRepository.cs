using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Crescer.Booking.Dominio.Contratos;
using Crescer.Booking.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Crescer.Booking.Infra.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private BookingContext contexto;

        public UsuarioRepository(BookingContext contexto)
        {
            this.contexto = contexto;
        }

        public Usuario AtualizarUsuario(int id, Usuario usuarioAtualizado)
        {
            var usuario = contexto.Usuarios.FirstOrDefault(p => p.Id == id);
            usuario?.Atualizar(usuarioAtualizado);
            return usuario;
        }

        public Usuario DeletarUsuario(int id)
        {
            var usuario = contexto.Usuarios.FirstOrDefault(p => p.Id == id);
            contexto.Usuarios.Remove(usuario);
            return usuario;
        }

        public List<Usuario> ListarUsuarios()
        {
            return contexto.Usuarios.AsNoTracking().ToList();
        }

        public Usuario Obter(int id)
        {
            return contexto.Usuarios.FirstOrDefault(p => p.Id == id);
        }

        public Usuario ObterUsuarioPorEmail(string email)
        {
            return contexto.Usuarios.FirstOrDefault(p => p.Email == email);
        }

        public Usuario ObterUsuarioPorEmailESenha(string email, string senha)
        {
            var senhaCriptografada = CriptografarSenha(senha);

            return contexto.Usuarios.AsNoTracking()
                .FirstOrDefault(u => u.Email == email && u.Senha == senhaCriptografada);
        }

        public bool obterUsuariosIguais(string email)
        {
            var usuario = contexto.Usuarios.FirstOrDefault(p => p.Email == email);

            if(usuario != null) return true;

            return false;
        }

        public Usuario SalvarUsuario(Usuario usuario)
        {
            usuario.AlterarSenha(CriptografarSenha(usuario.Senha));
            contexto.Usuarios.Add(usuario);
            return usuario;
        }

        private string CriptografarSenha(string senha)
        {
            var inputBytes = Encoding.UTF8.GetBytes(senha);

            var hashedBytes = new SHA256CryptoServiceProvider().ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

    }
}