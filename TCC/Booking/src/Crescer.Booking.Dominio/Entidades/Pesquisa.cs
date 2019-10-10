using System;

namespace Crescer.Booking.Dominio.Entidades
{
    public class Pesquisa
    {
        public Pesquisa(DateTime dataInicio, DateTime datafim, int numeroPessoas)
        {
            this.DataInicio = dataInicio;
            this.DataFim = datafim;
            this.NumeroPessoas= numeroPessoas;
        }



        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public int? NumeroPessoas { get; set; }
    }
}