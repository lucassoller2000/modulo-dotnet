using System;

namespace Crescer.Booking.Api.Models.Resquest
{
    public class UsuarioDto
    {
        public string Email { get; set; }

        public string Senha { get; set; }

        public string PrimeiroNome { get; set; }

        public string UltimoNome { get; set; }

        public string Cpf { get; set; }
        
        public DateTime DataNascimento { get; set; }
    }
}