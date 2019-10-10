using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyDoCrescer.Dominio;
using SpotifyDoCrescer.Dominio.Entidades;

namespace test
{
    [TestClass]
    public class MusicaControllerTest
    {
        private MusicaService musicaService = new MusicaService();
        
        [TestMethod]
        public void Uma_Musica_Com_Nome_Valido_Nao_Gera_Inconsistencia()
        {
            var musica = new Musica("Teste", 1);
            var obtido = musicaService.VerificarInconsistenciasEmUmaNovaMusica(musica);
            Assert.IsFalse(obtido.Any());
        }

        [TestMethod]
        public void Uma_Musica_Com_Nome_Invalido_Gera_Inconsistencia()
        {
            var esperado = new List<string>();
            var musica = new Musica("", 1);
            var obtido = musicaService.VerificarInconsistenciasEmUmaNovaMusica(musica);
            esperado.Add("O campo Nome deve ser preenchido");
            for(int i=0; i<obtido.Count; i++){
                Assert.AreEqual(obtido[i], esperado[i]);
            }
        }

        [TestMethod]
        public void Uma_Musica_Com_Duracao_Igual_A_0_Gera_Inconsistencia()
        {
            var musica = new Musica("Teste", 0);
            var obtido = musicaService.VerificarInconsistenciasEmUmaNovaMusica(musica);
            var esperado = new List<string>();
            esperado.Add("O campo Duracao deve ser preenchido");
            for(int i=0; i<obtido.Count; i++){
                Assert.AreEqual(obtido[i], esperado[i]);
            }
        }

        [TestMethod]
        public void Uma_Musica_Com_Duracao_Negativa_Gera_Inconsistencia()
        {
            var esperado = new List<string>();
            var musica = new Musica("Teste", -1);
            var obtido = musicaService.VerificarInconsistenciasEmUmaNovaMusica(musica);
            esperado.Add("O campo Duracao deve ser maior que 0");
            for(int i=0; i<obtido.Count; i++){
                Assert.AreEqual(obtido[i], esperado[i]);
            }
        }

        [TestMethod]
        public void Uma_Musica_Com_Duracao_E_Nome_Invalidos_Geram_Inconsistencia()
        {
            var esperado = new List<string>();
            var musica = new Musica("Teste", 0);
            var obtido = musicaService.VerificarInconsistenciasEmUmaNovaMusica(musica);
            esperado.Add("O campo Duracao deve ser preenchido");
            esperado.Add("O campo Nome deve ser preenchido");
            for(int i=0; i<obtido.Count; i++){
                Assert.AreEqual(obtido[i], esperado[i]);
            }
        }
    }
}