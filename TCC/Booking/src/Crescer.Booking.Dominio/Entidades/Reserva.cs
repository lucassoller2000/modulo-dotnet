using System;
using System.Collections.Generic;

namespace Crescer.Booking.Dominio.Entidades
{
    public class Reserva
    {
        public Reserva()
        {
        }

        public Reserva(Usuario usuario, Suite suite, int numeroPessoas, DateTime dataInicio, DateTime dataFim, List<Opcional> opcionais)
        {
            this.Usuario = usuario;
            this.Suite = suite;
            this.NumeroPessoas = numeroPessoas;
            this.DataInicio = dataInicio;
            this.DataFim = dataFim;
            this.Opcionais = opcionais;
        }
        public int Id { get; set; }

        public int? NumeroPessoas { get; private set; }

        public DateTime? DataInicio { get; private set; }

        public DateTime? DataFim { get; private set; }

        public decimal? subTotal { get; set; }
        public decimal? valorTotal { get; set; }
        public Usuario Usuario { get; private set; }

        public Suite Suite { get; private set; }

        public List<Opcional> Opcionais {get; set; }

        private decimal totalOpcionais = 0;

        public void calcularTotal(Reserva reserva, Suite suite)
        {
            var diariaSuite = suite.ValorDiaria;
            var numeroPessoas = reserva.NumeroPessoas;
            var quantidadeDias = reserva.DataFim?.Day - reserva.DataInicio?.Day + 1;
            foreach (var opcional in reserva.Opcionais)
            {
                totalOpcionais += (decimal)opcional.Valor;
            }
            this.subTotal = Math.Round((((decimal)diariaSuite + ((decimal)totalOpcionais * (decimal)numeroPessoas)) * (decimal)quantidadeDias),2);
            var taxaTurismo = (decimal)Taxas.TAXATURISMO * quantidadeDias;
            var taxaServico = (decimal)Taxas.TAXASERVICO * subTotal;
            var iss = (decimal)Taxas.ISS * subTotal;
            this.valorTotal = Math.Round(((decimal)subTotal + (decimal)taxaTurismo + (decimal)taxaServico + (decimal)iss),2);
        }
    }
}
