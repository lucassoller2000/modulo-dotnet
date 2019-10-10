using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.Infra;
using Crescer.Spotify.Infra.Repository;
using Crescer.Spotify.WebApi.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Crescer.Spotify.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository usuarioRepository;
        private UsuarioService usuarioService;
        private SpotifyContext contexto;
        private IMusicaRepository musicaRepository;
        
        public UsuarioController(IUsuarioRepository usuarioRepository, IMusicaRepository musicaRepository, UsuarioService usuarioService, SpotifyContext contexto)
        {
            this.usuarioRepository = usuarioRepository;
            this.usuarioService = usuarioService;
            this.contexto = contexto;
            this.musicaRepository = musicaRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var usuarios = usuarioRepository.ListarUsuario();
            return Ok(usuarios);
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

            var usuarioCadastrado = usuarioRepository.SalvarUsuario(usuario);

            contexto.SaveChanges();

            return CreatedAtRoute("GetUsuario", new { id = usuarioCadastrado.IdUsuario }, usuarioCadastrado);
        }

        [HttpPost("avaliacao", Name = "GetAvaliacao")]
        public IActionResult AvaliarMusica([FromBody]Models.Request.AvaliacaoDto avaliacaoRequest)
        {   
            var usuario = usuarioRepository.Obter(avaliacaoRequest.IdUsuario);
            if (usuario == null) return NotFound();
           
            var musica = musicaRepository.Obter(avaliacaoRequest.IdMusica);
            if(musica == null) return NotFound();
            
            var avaliacao = new Avaliacao(usuario, musica, avaliacaoRequest.Nota);
            
            var mensagens = usuarioService.Validar(avaliacao);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);
            
            var avaliacaoCadastrada = usuarioRepository.AvaliarMusica(avaliacao);
            
            contexto.SaveChanges();
            
            return Ok(avaliacaoCadastrada);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Models.Request.UsuarioDto usuarioRequest)
        {
            var usuario = usuarioRepository.Obter(id);

            if (usuario == null) return NotFound();

            var usuarioParaAlterar = MapearDtoParaDominio(usuarioRequest);
            
            var mensagens = usuarioService.Validar(usuarioParaAlterar);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            var usuarioCadastrado = usuarioRepository.AtualizarUsuario(id, usuarioParaAlterar);

            contexto.SaveChanges();

            return Ok(usuarioCadastrado);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = usuarioRepository.Obter(id);

            if (usuario == null) return NotFound();

            var usuarioCadastrado = usuarioRepository.DeletarUsuario(id);
            
            contexto.SaveChanges();

            return Ok(usuarioCadastrado);
        }

        private Usuario MapearDtoParaDominio(Models.Request.UsuarioDto usuario)
        {
            return new Usuario(usuario.Nome);
        }
    }
}