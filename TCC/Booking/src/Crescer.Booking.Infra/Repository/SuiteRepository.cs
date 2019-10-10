using System;
using System.Collections.Generic;
using System.Linq;
using Crescer.Booking.Dominio.Contratos;
using Crescer.Booking.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Crescer.Booking.Infra.Repository
{
    public class SuiteRepository : ISuiteRepository
    {
        private BookingContext contexto;

        public SuiteRepository(BookingContext contexto)
        {
            this.contexto = contexto;
        }

        public Suite AtualizarSuite(int id, Suite suiteAtualizada)
        {
           var suite = contexto.Suites.FirstOrDefault(p => p.Id == id);
           suite?.Atualizar(suiteAtualizada);
           return suite;
        }

        public Suite DeletarSuite(int id)
        {
            var suite = contexto.Suites.FirstOrDefault(p => p.Id == id);
            contexto.Suites.Remove(suite);
            return suite;
        }

        public List<Suite> ListarSuites()
        {
            return contexto.Suites.AsNoTracking().ToList();
        }

        public Suite Obter(int id)
        {
            return contexto.Suites.FirstOrDefault(p => p.Id == id);
        }

        public List<Suite> PesquisarSuites(DateTime dataInicio, DateTime dataFim, int numeroPessoas)
        {

            var reservas = contexto.Reservas.Where(p => p.DataInicio <= dataInicio 
            && p.DataFim >= dataInicio || p.DataInicio <= dataFim && p.DataFim >= dataFim).Select(x => x.Suite.Id).ToList();

            return contexto.Suites.Where(p => p.Capacidade >= numeroPessoas && !reservas.Contains(p.Id)).ToList();
        }

        public Suite SalvarSuite(Suite suite)
        {
            contexto.Suites.Add(suite);
            return suite;
        }

        public Reserva EstaSendoUsado(int id)
        {
            return contexto.Reservas.FirstOrDefault(p => p.Suite.Id == id);
        }
    }
}