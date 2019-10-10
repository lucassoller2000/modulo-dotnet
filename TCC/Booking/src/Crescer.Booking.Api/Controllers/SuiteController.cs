using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crescer.Booking.Api.Models.Resquest;
using Crescer.Booking.Dominio.Contratos;
using Crescer.Booking.Dominio.Entidades;
using Crescer.Booking.Dominio.Servicos;
using Crescer.Booking.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crescer.Booking.Api.Controllers
{
    [Authorize, Route("api/[controller]")]
    public class SuiteController : Controller
    {
        private ISuiteRepository suiteRepository;
        private SuiteService suiteService;
        private BookingContext contexto;

        public SuiteController(ISuiteRepository suiteRepositor, SuiteService suiteService, BookingContext contexto)
        {
            this.suiteRepository = suiteRepositor;
            this.suiteService = suiteService;
            this.contexto = contexto;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(suiteRepository.ListarSuites());
        }

        // GET api/values/5
        [HttpGet("Pesquisa")]
        public IActionResult PesquisarSuites(PesquisarDto pesquisarRequest)
        {
            var pesquisa = MapearDtoParaDominio(pesquisarRequest);

            var mensagemPesquisa =suiteService.ValidarPesquisa(pesquisa);

            if(mensagemPesquisa.Any()) return BadRequest(mensagemPesquisa);

            var suites = suiteRepository.PesquisarSuites(pesquisarRequest.DataInicio, pesquisarRequest.DataFim, pesquisarRequest.NumeroPessoas);
            
            var mensagem = suiteService.Validar(suites);

            if(mensagem.Any()) return BadRequest(mensagem);

            return Ok(suites);
        }

        [HttpGet("{id}", Name = "GetSuite")]
        public IActionResult Get(int id)
        {
            var suite = suiteRepository.Obter(id);

            var mensagem = suiteService.Validar(suite);

            if(mensagem.Any()) return BadRequest(mensagem);

            return Ok(suite);
        }

        // POST api/values
        [Authorize(Roles="Admin"), HttpPost]
        public IActionResult Post([FromBody]SuiteDto suiteRequest)
        {
            var suite = MapearDtoParaDominio(suiteRequest);
            var suiteCadastrada = suiteRepository.SalvarSuite(suite);

            var mensagem = suiteService.Validar(suiteCadastrada);

            if(mensagem.Any()) return BadRequest(mensagem);

            contexto.SaveChanges();
            return CreatedAtRoute("GetSuite", new {id = suite.Id}, suite);
        }

        // PUT api/values/5
        [Authorize(Roles="Admin"), HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]SuiteDto suiteRequest)
        {
            var suite = MapearDtoParaDominio(suiteRequest);
            var suiteCadastrada = suiteRepository.AtualizarSuite(id, suite);
            
            var mensagem = suiteService.Validar(suiteCadastrada);

            if(mensagem.Any()) return BadRequest(mensagem);

            contexto.SaveChanges();
            return Ok();
        }

        // DELETE api/values/5
        [Authorize(Roles="Admin"), HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var suiteCadastrada = suiteRepository.DeletarSuite(id);

            var mensagemSuiteUsada = suiteService.ValidarUsado(id, suiteRepository);
            if(mensagemSuiteUsada.Any()) return BadRequest(mensagemSuiteUsada);
            
            var mensagem = suiteService.Validar(suiteCadastrada);
            if(mensagem.Any()) return BadRequest(mensagem);
            
            contexto.SaveChanges();
            return Ok();
        }

        private Suite MapearDtoParaDominio(SuiteDto suiteRequest)
        {
            return new Suite(suiteRequest.Nome, suiteRequest.Descricao, suiteRequest.Capacidade, suiteRequest.ValorDiaria);
        }

        private Pesquisa MapearDtoParaDominio(PesquisarDto pesquisarRequest)
        {
            return new Pesquisa(pesquisarRequest.DataInicio, pesquisarRequest.DataFim, pesquisarRequest.NumeroPessoas);
        }
    }
}