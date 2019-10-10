using System;

namespace Crescer.Booking.Api.Models.Resquest
{
    public class PesquisarDto
    {
        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public int NumeroPessoas { get; set; }
    }
}