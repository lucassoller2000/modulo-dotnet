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
    [ Route("api/[controller]")]
    public class OpcionalController : Controller
    {
        private IOpcionalRepository opcionalRepository;
        private OpcionalService opcionalService;
        private BookingContext contexto;

        public OpcionalController(IOpcionalRepository opcionalRepository, OpcionalService opcionalService, BookingContext contexto)
        {
            this.opcionalRepository = opcionalRepository;
            this.opcionalService = opcionalService;
            this.contexto = contexto;
        }
        
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(opcionalRepository.ListarOpcionais());
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetOpcional")]
        public IActionResult Get(int id)
        {
            var opcional = opcionalRepository.Obter(id);

            var mensagem = opcionalService.Validar(opcional);

            if(mensagem.Any()) return BadRequest(mensagem);

            return Ok(opcional);
        }

        // POST api/values
        [Authorize(Roles="Admin"), HttpPost]
        public IActionResult Post([FromBody]OpcionalDto opcionalRequest)
        {
            var opcional = MapearDtoParaDominio(opcionalRequest);
            var opcionalCadastrado = opcionalRepository.SalvarOpcional(opcional);
            
            var mensagem = opcionalService.Validar(opcional);
            if(mensagem.Any()) return BadRequest(mensagem);
            
            contexto.SaveChanges();
            return CreatedAtRoute("GetOpcional", new {id = opcional.Id}, opcional);
        }

        // PUT api/values/5
        [Authorize(Roles="Admin"), HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]OpcionalDto opcionalRequest)
        {
            var opcional = MapearDtoParaDominio(opcionalRequest);
            var opcionalCadastrado = opcionalRepository.AtualizarOpcional(id, opcional);
            
            var mensagem = opcionalService.Validar(opcionalCadastrado);
            if(mensagem.Any()) return BadRequest(mensagem);

            contexto.SaveChanges();
            return Ok();
        }

        // DELETE api/values/5
        [Authorize(Roles="Admin"), HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var opcionalCadastrado = opcionalRepository.DeletarOpcional(id);
            
            var mensagemOpcionalUsado = opcionalService.ValidarUsado(id, opcionalRepository);

            if(mensagemOpcionalUsado.Any()) return BadRequest(mensagemOpcionalUsado);
            
            var mensagem = opcionalService.Validar(opcionalCadastrado);
            if(mensagem.Any()) return BadRequest(mensagem);

            contexto.SaveChanges();
            return Ok();
        }

        private Opcional MapearDtoParaDominio(OpcionalDto opcionalRequest)
        {
            return new Opcional(opcionalRequest.Nome, opcionalRequest.Descricao, opcionalRequest.Valor);
        }
    }
}