using System;
using System.Collections.Generic;
using Crescer.Booking.Dominio.Entidades;

namespace Crescer.Booking.Dominio.Contratos
{
    public interface ISuiteRepository
    {
        Suite SalvarSuite(Suite suite);
        Suite AtualizarSuite(int id, Suite suiteAtualizada);
        Suite DeletarSuite(int id);
        List<Suite> ListarSuites();
        Suite Obter(int id);
        List<Suite> PesquisarSuites(DateTime dataInicio, DateTime dataFim, int numeroPessoas);

        Reserva EstaSendoUsado(int id);
    }
}