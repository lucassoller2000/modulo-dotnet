using System.Collections.Generic;
using Crescer.Booking.Dominio.Entidades;

namespace Crescer.Booking.Dominio.Contratos
{
    public interface IOpcionalRepository
    {
        Opcional SalvarOpcional(Opcional opcional);
        Opcional AtualizarOpcional(int id, Opcional opcionalAtualizado);
        Opcional DeletarOpcional(int id);
        List<Opcional> ListarOpcionais();
        Opcional Obter(int id);
        ReservaOpcional EstaSendoUsado(int id);
    }
}