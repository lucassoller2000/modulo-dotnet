namespace Crescer.Booking.Api.Models.Resquest
{
    public class UsuarioComTokenDto
    {

        public UsuarioComTokenDto(string tipoUsuario, string token, string email)
        {
            this.TipoUsuario = tipoUsuario;
            this.Token = token;
            this.Email = email;

        }
        public string TipoUsuario { get; set; }

        public string Token { get; set; }

        public string Email {get; set; }
    }
}