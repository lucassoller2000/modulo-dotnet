using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Crescer.Aula.Dominio.Tests
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void calcularDistaciaEmMilhas()
        {
            Ponto pontoA = new Ponto(40.76, -73.984);
            Ponto pontoB = new Ponto(41.89, 12.492);

            Rota rota = new Rota(pontoA, pontoB);

            double esperado = Math.Floor(4279.9454);
            double obtido = rota.calcularDistancia();

            Assert.AreEqual(esperado,obtido);
        }

        [TestMethod]
        public void calcularValorBaseDoAviaoTurboJato(){
            Aviao turboJato = new TurboJato("Turbo-Jato", 2, 5);
            
            double esperado = 15;
            double obtido = turboJato.CalcularValorBase();
            
            Assert.AreEqual(esperado,obtido);
        }

        [TestMethod]
        public void calcularValorBaseDoAviaoTurboHelice(){
            Aviao turboHelice = new TurboHelice("Turbo-Hélice", 4, 8);
           
            double esperado = 38.4;
            double obtido = turboHelice.CalcularValorBase();
            
            Assert.AreEqual(esperado,obtido);
        }

        [TestMethod]
        public void calcularValorTotalDeVooDoAviaoTurboJato(){
            Ponto pontoA = new Ponto(40.76, -73.984);
            Ponto pontoB = new Ponto(41.89, 12.492);
            Rota rota = new Rota(pontoA, pontoB);
            Aviao turboJato = new TurboJato("Turbo-Jato", 2, 5);
            Voo voo = new Voo(rota, turboJato);

            double esperado = 64185;
            double obtido = voo.calcularValorTotalDoVoo();

            Assert.AreEqual(esperado,obtido);
        }
        
        [TestMethod]
        public void calcularValorTotalDeVooDoAviaoTurboHelice(){
            Ponto pontoA = new Ponto(40.76, -73.984);
            Ponto pontoB = new Ponto(41.89, 12.492);
            Rota rota = new Rota(pontoA, pontoB);
            Aviao turboHelice = new TurboHelice("Turbo-Hélice", 4, 8);
            Voo voo = new Voo(rota, turboHelice);

            double esperado = 164313.6;
            double obtido = voo.calcularValorTotalDoVoo();

            Assert.AreEqual(esperado,obtido);
        }
    }
}
