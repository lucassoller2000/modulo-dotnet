using System;
using System.Collections.Generic;
using System.Linq;
using Crescer.Booking.Dominio.Contratos;
using Crescer.Booking.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Crescer.Booking.Infra.Repository
{
    public class ReservaRepository : IReservaRepository
    {
        private BookingContext contexto;

        public ReservaRepository(BookingContext contexto)
        {
            this.contexto = contexto;
        }

        public Reserva DeletarReservaPorUsuario(Usuario usuario, int id)
        {
           var reserva = contexto.Reservas.FirstOrDefault(p => p.Usuario.Id == usuario.Id && p.Id == id);
           contexto.Reservas.Remove(reserva);
           return reserva;
        }

        public List<Reserva> ListarReservas()
        {
            return contexto.Reservas.AsNoTracking().ToList();
        }

        public List<Reserva> ListarReservas(int numeroPessoas, DateTime dataInicio, DateTime dataFim)
        {
            return contexto.Reservas.Where(p => p.DataFim == dataFim).ToList();
        }

        public List<Reserva> ListarReservasPorUsuario(Usuario usuario)
        {
            var opcionais = contexto.Reservas.Where(p => p.Id == usuario.Id).Select(p => p.Opcionais).FirstOrDefault();

            if(opcionais == null)
                return contexto.Reservas.Where(p => p.Usuario.Id == usuario.Id).Include(p => p.Suite).ToList();

            return contexto.Reservas.Where(p => p.Usuario.Id == usuario.Id).Include(p => p.Suite)
            .Include(p => p.Opcionais).ToList();
        }
        public Reserva Obter(int id)
        {
            var opcionais = contexto.Reservas.Where(p => p.Id == id).Select(p => p.Opcionais).FirstOrDefault();
            
            if(opcionais == null)
                return contexto.Reservas.Include(p => p.Usuario).Include(p => p.Suite).FirstOrDefault(p => p.Id == id);

            return contexto.Reservas.Include(p => p.Usuario).Include(p => p.Suite).Include(p => p.Opcionais).FirstOrDefault(p => p.Id == id);
        }

        public Reserva SalvarReserva(Reserva reserva)
        {
            var reservas = contexto.Reservas.Where(
                p => p.DataInicio <= reserva.DataInicio && p.DataFim >= reserva.DataInicio ||
                p.DataInicio <= reserva.DataFim && p.DataFim >= reserva.DataFim).Select(p => p.Suite.Id).ToList();

            var suite = contexto.Suites.FirstOrDefault(p => p.Id == reserva.Suite.Id && p.Capacidade >= reserva.NumeroPessoas && !reservas.Contains(p.Id));
            
            if(suite != null){
                reserva.calcularTotal(reserva, suite);
                contexto.Reservas.Add(reserva);
                return reserva;
            }
            else{
                return null;
            }
        }
    }
}