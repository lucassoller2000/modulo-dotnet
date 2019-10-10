using System;

namespace Crescer.Booking.Dominio.Entidades
{
    public class UsuarioSemSenha
    {
        public UsuarioSemSenha()
        {
        }

        public UsuarioSemSenha(int id, string email, string primeiroNome, string ultimoNome, string cpf, DateTime? dataNascimento)
        {
            this.Id = id;
            this.Email = email;
            this.PrimeiroNome = primeiroNome;
            this.UltimoNome = ultimoNome;
            this.Cpf = cpf;
            this.TipoUsuario = "Simples";
            this.DataNascimento = dataNascimento;
        }

        public int Id { get; set; }

        public string Email { get; set; }

        public string PrimeiroNome { get; set; }

        public string UltimoNome { get; set; }

        public string Cpf { get; set; }

        public string TipoUsuario { get; set; }

        public DateTime? DataNascimento { get; set; }

        public UsuarioSemSenha GerarUsuarioSemSenha(Usuario usuario)
        {
            return new UsuarioSemSenha(usuario.Id, usuario.Email, usuario.PrimeiroNome,
            usuario.UltimoNome, usuario.Cpf, (DateTime)usuario.DataNascimento);
        }

    }
}