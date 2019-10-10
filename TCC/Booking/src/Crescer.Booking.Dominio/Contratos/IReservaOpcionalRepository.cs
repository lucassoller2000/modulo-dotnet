using System.Collections.Generic;
using Crescer.Booking.Dominio.Entidades;

namespace Crescer.Booking.Dominio.Contratos
{
    public interface IReservaOpcionalRepository
    {
        void SalvarReservaOpcional(Reserva reserva);
        List<ReservaOpcional> ListarReservasOpcionais();
        ReservaOpcional Obter(int id);
    }
}