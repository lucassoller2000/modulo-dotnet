using System;
using System.Collections.Generic;

namespace Crescer.Booking.Api.Models.Resquest
{
    public class ReservaDto
    {
        public int IdSuite { get; set; }

        public int NumeroPessoas { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public List<int> IdOpcionais { get; set; }
    }
}