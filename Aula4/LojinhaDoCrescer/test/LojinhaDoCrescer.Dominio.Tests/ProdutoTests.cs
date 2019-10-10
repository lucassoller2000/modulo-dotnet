using Microsoft.VisualStudio.TestTools.UnitTesting;
using LojinhaDoCrescer.Dominio.Entidades;
namespace LojinhaDoCrescer.Dominio.Tests
{
    [TestClass]
    public class ProdutoTests
    {
        [TestMethod]
        public void O_Valor_Total_Do_Carrinho_Deve_Ser_Calculado_Corretamente()
        {
            var carrinho = new Carrinho();
            var produto = new Produto("novo", 10);

            carrinho.AdicionarProduto(produto, 5);

            var esperado = 50;
            var obtido = carrinho.ValorTotal;

            Assert.AreEqual(esperado, obtido);
        }
    }
}
