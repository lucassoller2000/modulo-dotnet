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
    public class BaseReservaController : Controller
    {
        private IReservaRepository reservaRepository;
        private IReservaOpcionalRepository reservaOpcionalRepository;
        private IUsuarioRepository usuarioRepository;
        private ISuiteRepository suiteRepository;
        private IOpcionalRepository opcionalRepository;
        private ReservaService reservaService;
        private BookingContext contexto;

        public BaseReservaController(
            IReservaRepository reservaRepository, 
            ISuiteRepository suiteRepository, 
            IUsuarioRepository usuarioRepository, 
            ReservaService reservaService, 
            BookingContext contexto, 
            IOpcionalRepository opcionalRepository,
            IReservaOpcionalRepository reservaOpcionalRepository
            )
        {
            this.reservaRepository = reservaRepository;
            this.reservaService = reservaService;
            this.contexto = contexto;
            this.usuarioRepository = usuarioRepository;
            this.suiteRepository = suiteRepository;
            this.opcionalRepository = opcionalRepository;
            this.reservaOpcionalRepository = reservaOpcionalRepository;
        }


        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(reservaRepository.ListarReservas());
        }

        [HttpGet("usuario")]
        public IActionResult ListarReservasPorUsuario() 
        {
            var usuario = usuarioRepository.ObterUsuarioPorEmail(User.Identity.Name);
            
            return Ok(reservaRepository.ListarReservasPorUsuario(usuario));
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetReserva")]
        public IActionResult Get(int id)
        {
            var reserva = reservaRepository.Obter(id);

            var mensagem = reservaService.Validar(reserva);

            if(mensagem.Any()) return BadRequest(mensagem);

            return Ok(reserva);
        }

        [HttpDelete("{id}/usuario")]
        public IActionResult DeletarPorIdUsuario(int id)
        {
            var usuario = usuarioRepository.ObterUsuarioPorEmail(User.Identity.Name);
            
            var reserva = reservaRepository.DeletarReservaPorUsuario(usuario, id);

            var mensagem = reservaService.Validar(reserva);

            if(mensagem.Any()) return BadRequest(mensagem);

            contexto.SaveChanges();

            return Ok();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ReservaDto reservaRequest)
        {

            var usuario = usuarioRepository.ObterUsuarioPorEmail(User.Identity.Name);

            var suite = suiteRepository.Obter(reservaRequest.IdSuite);

            if(usuario == null) return NotFound("Nenhum usuário foi encontrado");
            
            var opcionais = new List<Opcional>();

            foreach (var opcional in reservaRequest.IdOpcionais)
            {
                var opcionalCadastrado = opcionalRepository.Obter(opcional);
                if(opcionalCadastrado == null) return NotFound("Nenhum opcional foi encontrado");
                opcionais.Add(opcionalCadastrado);
            }
            
            var reserva = new Reserva(usuario, suite, reservaRequest.NumeroPessoas, reservaRequest.DataInicio, reservaRequest.DataFim, opcionais);
            
            var reservaCadastrada = reservaRepository.SalvarReserva(reserva);
            
            var mensagem = reservaService.Validar(reservaCadastrada);
            
            if(mensagem.Any()) return BadRequest(mensagem);
            
            reservaOpcionalRepository.SalvarReservaOpcional(reserva);
            
            contexto.SaveChanges();
            
            return Ok(reserva);
        }
    }
}