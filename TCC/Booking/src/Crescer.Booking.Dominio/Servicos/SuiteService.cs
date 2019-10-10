using System.Collections.Generic;
using Crescer.Booking.Dominio.Contratos;
using Crescer.Booking.Dominio.Entidades;

namespace Crescer.Booking.Dominio.Servicos
{
    public class SuiteService
    {
        public List<string> Validar(Suite suite)
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(suite.Nome))
            {
                mensagens.Add("É necessário informar o nome da suíte");
                return mensagens;
            }
            if (string.IsNullOrEmpty(suite.Descricao))
            {
                mensagens.Add("É necessário informar a descrição da suíte");
                return mensagens;
            }

            if (suite.Capacidade <= 0 || suite.Capacidade == null)
            {   mensagens.Add("É necessário uma capacidade válida");
                return mensagens;
            }

            if (suite.ValorDiaria <= 0 || suite.ValorDiaria == null)
            {
                mensagens.Add("É necessário um valor da diária válido");
                return mensagens;
            }

            if(suite == null)
            {
                mensagens.Add("Nenhuma suíte foi encontrada");
                return mensagens;
            }

            return mensagens;
        }

        public List<string> Validar(List<Suite> suites)
        {
            List<string> mensagens = new List<string>();

            if (suites.Count < 1)
                mensagens.Add("Não foi encontrado nenhuma suíte disponível");

            return mensagens;
        }

        public List<string> ValidarUsado(int id, ISuiteRepository suiteRepository)
        {
            List<string> mensagens = new List<string>();

            var suiteNaReserva = suiteRepository.EstaSendoUsado(id);
            
            if(suiteNaReserva != null)
                mensagens.Add("Não é possível deletar essa suíte porque está sendo utilizada");
           
            
            return mensagens;
        }
        public List<string> ValidarPesquisa(Pesquisa pesquisa)
        {
            List<string> mensagens = new List<string>();

            if(pesquisa.DataInicio?.Year < 2018 || 
                pesquisa.DataInicio?.Day < 1 ||
                pesquisa.DataInicio?.Day > 31 ||
                pesquisa.DataInicio?.Month < 1 || 
                pesquisa.DataInicio?.Month > 12)
            {
                mensagens.Add("É necessário informar uma data de início válida");
                return mensagens;
            }

            if(pesquisa.DataFim?.Year < 2018 || 
                pesquisa.DataFim?.Day < 1 ||
                pesquisa.DataFim?.Day > 31 ||
                pesquisa.DataFim?.Month < 1 || 
                pesquisa.DataFim?.Month > 12 |
                pesquisa.DataFim < pesquisa.DataInicio)
            {
                mensagens.Add("É necessário informar uma data de fim válida");
                return mensagens;
            }

            if(pesquisa.NumeroPessoas == null || pesquisa.NumeroPessoas <= 0)
            {
                mensagens.Add("É necessário informar um número de pessoas válido");
                return mensagens;
            }

            return mensagens;
        }
    }
}