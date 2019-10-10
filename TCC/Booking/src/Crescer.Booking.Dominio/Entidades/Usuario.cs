using System;
using System.Collections.Generic;

namespace Crescer.Booking.Dominio.Entidades
{
    public class Usuario
    {
        public Usuario()
        {
        }
        public Usuario(string email, string senha, string primeiroNome, string ultimoNome, string cpf, DateTime dataNascimento)
        {
            this.Email = email;
            this.Senha = senha;
            this.PrimeiroNome = primeiroNome;
            this.UltimoNome = ultimoNome;
            this.Cpf = cpf;
            this.DataNascimento = dataNascimento;
            this.TipoUsuario = "Simples";
        }
        public int Id { get; set; }

        public string Email { get; private set; }

        public string Senha { get; private set; }

        public string PrimeiroNome { get; private set; }

        public string UltimoNome { get; private set; }

        public string Cpf { get; private set; }

        public string TipoUsuario { get; set; }

        public DateTime? DataNascimento { get; private set; }

        public void Atualizar(Usuario usuario)
        {
            this.Email = usuario.Email;
            this.Senha = usuario.Senha;
            this.PrimeiroNome = usuario.PrimeiroNome;
            this.UltimoNome = usuario.UltimoNome;
            this.Cpf = usuario.Cpf;
            this.DataNascimento = usuario.DataNascimento;
        }

        public void AlterarSenha(string senha)
        {
            Senha = senha;
        }
    }
}