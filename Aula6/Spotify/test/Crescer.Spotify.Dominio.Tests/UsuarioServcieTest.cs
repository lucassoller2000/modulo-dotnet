// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Crescer.Spotify.Dominio;
// using Crescer.Spotify.Dominio.Servicos;
// using Crescer.Spotify.Dominio.Entidades;
// using System.Collections.Generic;

// namespace Crescer.Spotify.Dominio.Tests
// {
//     [TestClass]
//     public class UsuarioServiceTests
//     {
//         [TestMethod]
//         public void DeveRetornarErroSeUmNomeNaoForInformado()
//         {
//             var usuarioService = new UsuarioService();

//             var erros = usuarioService.Validar(new Usuario(null));

//             CollectionAssert.AreEqual(new List<string> { "É necessário informar o nome do usuário" }, erros);
//         }

//         [TestMethod]
//         public void DeveRetornarErroSeUmaNotaMenorQue1ForInformada()
//         {
//             var usuarioService = new UsuarioService();

//             var erros = usuarioService.Validar(new Avaliacao(1,1,0));

//             CollectionAssert.AreEqual(new List<string> { "A avaliação só pode receber notas de 1 até 5" }, erros);
//         }

//         [TestMethod]
//         public void DeveRetornarErroSeUmaNotaMaiorQue5ForInformada()
//         {
//             var usuarioService = new UsuarioService();

//             var erros = usuarioService.Validar(new Avaliacao(1,1,6));

//             CollectionAssert.AreEqual(new List<string> { "A avaliação só pode receber notas de 1 até 5" }, erros);
//         }
//     }
// }
