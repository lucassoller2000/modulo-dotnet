using System;
using System.Collections.Generic;
using Crescer.Booking.Dominio.Contratos;
using Crescer.Booking.Dominio.Entidades;

namespace Crescer.Booking.Dominio.Servicos
{
    public class OpcionalService
    {
       public List<string> Validar(Opcional opcional)
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(opcional.Nome))
            {
                mensagens.Add("É necessário informar o nome do opcional");
                return mensagens;
            }
            
            if (string.IsNullOrEmpty(opcional.Descricao))
            {   
                mensagens.Add("É necessário informar a descrição");
                return mensagens;
            }

            if (opcional.Valor <= 0 || opcional.Valor == null)
            {   
                mensagens.Add("É necessário informar um valor válido");
                return mensagens;
            }

            if(opcional == null)
            {   
                mensagens.Add("Nenhum opcional foi encontrado");
                return mensagens;
            }

            return mensagens;
        }

        public List<string> ValidarUsado(int id, IOpcionalRepository opcionalRepository)
        {
            List<string> mensagens = new List<string>();

            var opcionalUsado = opcionalRepository.EstaSendoUsado(id);

            if(opcionalUsado != null)
                mensagens.Add("Não é possível deletar esse opcional porque está sendo utilizado");
            
            return mensagens;
        }
    }
}