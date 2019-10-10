using System.Collections.Generic;
using Crescer.Booking.Dominio.Entidades;

namespace Crescer.Booking.Dominio.Contratos
{
    public interface IReservaRepository
    {
        Reserva SalvarReserva(Reserva reserva);
        Reserva DeletarReservaPorUsuario(Usuario usuario, int id);
        List<Reserva> ListarReservas();
        List<Reserva> ListarReservasPorUsuario(Usuario usuario);
        Reserva Obter(int id);
    }
}