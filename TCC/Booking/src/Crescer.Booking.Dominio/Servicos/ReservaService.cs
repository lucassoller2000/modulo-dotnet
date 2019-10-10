using System;
using System.Collections.Generic;
using Crescer.Booking.Dominio.Entidades;

namespace Crescer.Booking.Dominio.Servicos
{
    public class ReservaService
    {
         public List<string> Validar(Reserva reserva)
        {
            List<string> mensagens = new List<string>();

            if(reserva == null) 
            {  
                mensagens.Add("Nenhuma suíte disponível foi encontrada");
                return mensagens;
            }

            if(DateTime.Now.Year - reserva.Usuario.DataNascimento?.Year < 18)
            {
                mensagens.Add("É necessário ser maior de idade para fazer uma reserva");
                return mensagens;
            }

            if(reserva.NumeroPessoas <= 0 || reserva.NumeroPessoas == null)
            {
                mensagens.Add("É necessário informar um número de pessoas válido");
                return mensagens;
            }

            if(reserva.DataInicio?.Year < 2018 || 
                reserva.DataInicio?.Day < 1 ||
                reserva.DataInicio?.Day > 31 ||
                reserva.DataInicio?.Month < 1 || 
                reserva.DataInicio?.Month > 12)
            {
                mensagens.Add("É necessário informar uma data de início válida");
                return mensagens;
            }

            if(reserva.DataFim?.Year < 2018 || 
                reserva.DataFim?.Day < 1 ||
                reserva.DataFim?.Day > 31 ||
                reserva.DataFim?.Month < 1 || 
                reserva.DataFim?.Month > 12 ||
                reserva.DataFim < reserva.DataInicio)
            {
                mensagens.Add("É necessário informar uma data de saída válida");
                return mensagens;
            }

            return mensagens;
        }
    }
}