using System.Collections.Generic;
namespace Crescer.PetStore.Api.Models{
    public class User{
        public int Id {get; set;}
        public string Login{ get; set; }
        public string PrimeiroNome{ get; set; }
        public string UltimoNome{ get; set; }
        public string Email{ get; set; }
        public string Senha{get; set; }
        public string Telefone{ get; set; }
        public StatusUsuario StatusUsuario{ get; set; }
    }
}