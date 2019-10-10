using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.Infra;
using Crescer.Spotify.WebApi.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Crescer.Spotify.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository usuarioRepository;
        private Database database;
        private UsuarioService usuarioService;
        
        public UsuarioController(IUsuarioRepository usuarioRepository, UsuarioService usuarioService, Database database)
        {
            this.usuarioRepository = usuarioRepository;
            this.usuarioService = usuarioService;
            this.database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(usuarioRepository.ListarUsuario());
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetUsuario")]
        public IActionResult Get(int id)
        {
            var usuario = usuarioRepository.Obter(id);

            if (usuario == null) return NotFound();
            
            return Ok(usuario);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Models.Request.UsuarioDto usuarioRequest)
        {
            var usuario = MapearDtoParaDominio(usuarioRequest);
            var mensagens = usuarioService.Validar(usuario);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            usuarioRepository.SalvarUsuario(usuario);
            database.Commit();
            return CreatedAtRoute("GetUsuario", new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPost("avaliacao", Name = "GetAvaliacao")]
        public IActionResult AvaliarMusica([FromBody]Models.Request.AvaliacaoDto avaliacaoRequest)
        {
            var avaliacao = MapearDtoParaDominio(avaliacaoRequest);
            var mensagens = usuarioService.Validar(avaliacao);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);
            if(usuarioRepository.AvaliarMusica(avaliacao)==1){
                database.Commit();
                return Ok();
            }else{
                database.Commit();
                return CreatedAtRoute("GetAvaliacao", new { id = avaliacao.IdAvaliacao }, avaliacao);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Models.Request.UsuarioDto usuarioRequest)
        {
            var usuario = MapearDtoParaDominio(usuarioRequest);
            var mensagens = usuarioService.Validar(usuario);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            usuarioRepository.AtualizarUsuario(id, usuario);
            database.Commit();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            usuarioRepository.DeletarUsuario(id);
            database.Commit();
            return Ok();
        }

        private Usuario MapearDtoParaDominio(Models.Request.UsuarioDto usuario)
        {
            return new Usuario(usuario.Nome);
        }

        private Avaliacao MapearDtoParaDominio(Models.Request.AvaliacaoDto avaliacao)
        {
            return new Avaliacao(avaliacao.IdUsuario, avaliacao.IdMusica, avaliacao.Nota);
        }
    }
}