using System.Collections.Generic;
using System.Linq;
using Crescer.Booking.Dominio.Contratos;
using Crescer.Booking.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Crescer.Booking.Infra.Repository
{
    public class ReservaOpcionalRepository : IReservaOpcionalRepository
    {
        private BookingContext contexto;

        public ReservaOpcionalRepository(BookingContext contexto)
        {
            this.contexto = contexto;
        }

        public List<ReservaOpcional> ListarReservasOpcionais()
        {
            return contexto.ReservasOpcionais.AsNoTracking().ToList();
        }

        public ReservaOpcional Obter(int id)
        {
            return contexto.ReservasOpcionais.Include(p => p.Opcional).FirstOrDefault(p => p.Id == id);
        }

        public void SalvarReservaOpcional(Reserva reserva)
        {
            foreach (var opcional in reserva.Opcionais)
            {
                ReservaOpcional reservaOpcional = new ReservaOpcional(reserva,opcional);
                contexto.ReservasOpcionais.Add(reservaOpcional);
            }
        }
    }
}