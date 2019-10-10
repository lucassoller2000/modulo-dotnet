using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyDoCrescer.Api.Controllers;
using SpotifyDoCrescer.Dominio;
using SpotifyDoCrescer.Dominio.Entidades;

namespace test
{
    [TestClass]
    public class AlbumControllerTest
    {
        AlbumService albumService = new AlbumService();
        [TestMethod]
        public void Um_Album_Com_Nome_Invalido_Gera_Inconsistencia()
        {
            var esperado = new List<string>();
            var album = new Album("");
            var obtido = albumService.VerificarInconsistenciasEmUmNovoAlbum(album);
            esperado.Add("O campo Nome deve ser preenchido");
            for(int i=0; i<obtido.Count; i++){
                Assert.AreEqual(obtido[i], esperado[i]);
            }
        }

        [TestMethod]
        public void Um_Album_Com_Nome_Valido_Nao_Gera_Inconsistencia()
        {
            var album = new Album("Teste");
            var obtido = albumService.VerificarInconsistenciasEmUmNovoAlbum(album);
            Assert.IsFalse(obtido.Any());
        }
    }
}