using System.Collections.Generic;
using System.Linq;
using Crescer.Booking.Dominio.Contratos;
using Crescer.Booking.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Crescer.Booking.Infra.Repository
{
    public class OpcionalRepository : IOpcionalRepository
    {
        private BookingContext contexto;

        public OpcionalRepository(BookingContext contexto)
        {
            this.contexto = contexto;
        }

        public Opcional AtualizarOpcional(int id, Opcional opcionalAtualizado)
        {
            var opcional = contexto.Opcionais.FirstOrDefault(p => p.Id == id);
            opcional?.Atualizar(opcionalAtualizado);
            return opcional;
        }

        public Opcional DeletarOpcional(int id)
        {
            var opcionalNaReservaOpcional = contexto.ReservasOpcionais.FirstOrDefault(p => p.Opcional.Id == id);
            if(opcionalNaReservaOpcional != null) return null;
            var opcional = contexto.Opcionais.FirstOrDefault(p => p.Id == id);
            contexto.Opcionais.Remove(opcional);
            return opcional;
        }

        public List<Opcional> ListarOpcionais()
        {
            return contexto.Opcionais.AsNoTracking().ToList();
        }

        public Opcional Obter(int id)
        {
            return contexto.Opcionais.FirstOrDefault(p => p.Id == id);
        }

        public Opcional SalvarOpcional(Opcional opcional)
        {
            contexto.Opcionais.Add(opcional);
            return opcional;
        }

        public ReservaOpcional EstaSendoUsado(int id)
        {
            return contexto.ReservasOpcionais.FirstOrDefault(p => p.Opcional.Id == id);
        }
    }
}