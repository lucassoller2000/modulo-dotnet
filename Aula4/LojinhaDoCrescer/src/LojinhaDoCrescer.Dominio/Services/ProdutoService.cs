using System.Collections.Generic;
using LojinhaDoCrescer.Dominio.Entidades;

namespace LojinhaDoCrescer.Dominio.Services
{
    public class ProdutoService
    {
        public List<string> VerificarInconsistenciaEmUmNovoProduto(Produto produto)
        {
            var inconsistencia = new List<string>();
            if(produto.Valor <= 0) inconsistencia.Add($"O campo {nameof(produto.Valor)} do produto deve ser maior que 0");
            
            if(string.IsNullOrEmpty(produto.Descricao)){
                inconsistencia.Add($"O campo {nameof(produto.Descricao)} não pode ser nulo");
                return inconsistencia; 
            }

            if(produto.Descricao.Contains("REF:")){
                inconsistencia.Add($"A descrição deve conter a referência do produto");
            }

            return inconsistencia;
            // valor > 0
            //descrição é obrigatória
            //descrição tem que conter um código de referencia
        }
    }
}