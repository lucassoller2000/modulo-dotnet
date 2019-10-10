using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Dominio.Servicos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crescer.Spotify.Dominio.Tests
{
    [TestClass]
    public class MusicaServiceTests
    {
        [TestMethod]
        public void DeveRetornarErroSeUmNomeNaoForInformado()
        {
            var musicaService = new MusicaService();

            Album album = new Album();

            var erros = musicaService.Validar(new Musica(null, 200, album));

            CollectionAssert.AreEqual(new List<string> { "É necessário informar o nome da música" }, erros);
        }

        [TestMethod]
        public void DeveRetornarErroSeUmaDuracaoNaoForInformada()
        {
            var musicaService = new MusicaService();

            Album album = new Album();

            var erros = musicaService.Validar(new Musica("Musica 1", 0, album));

            CollectionAssert.AreEqual(new List<string> { "É necessário informar a duração da música" }, erros);
        }

        [TestMethod]
        public void EmCasoDeMaisDeUmErroTodosDevemSerRetornados()
        {
            var musicaService = new MusicaService();

            Album album = new Album();

            var erros = musicaService.Validar(new Musica(null, 0, album));

            CollectionAssert.AreEqual(new List<string> { "É necessário informar o nome da música", "É necessário informar a duração da música" }, erros);
        }
    }
}
