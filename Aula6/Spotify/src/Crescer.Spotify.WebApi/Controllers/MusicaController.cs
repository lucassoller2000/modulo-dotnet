using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.Infra;
using Microsoft.AspNetCore.Mvc;

namespace Crescer.Spotify.WebApi.Controllers
{
    [Route("api/album")]
    public class MusicaController : Controller
    {
        private IMusicaRepository musicaRepository;
        private IAlbumRepository albumRepository;
        private MusicaService musicaService;
        private SpotifyContext contexto;

        public MusicaController(IMusicaRepository musicaRepository, MusicaService musicaService, IAlbumRepository albumRepository, SpotifyContext contexto)
        {
            this.musicaRepository = musicaRepository;
            this.musicaService = musicaService;
            this.albumRepository = albumRepository;
            this.contexto = contexto;
        }
        
        // GET api/values
        [HttpGet("{idAlbum}/musica/lista")]
        public IActionResult GetLista(int idAlbum)
        {
            if (albumRepository.Obter(idAlbum) == null) return NotFound();

            var listaMusicas = musicaRepository.ListarMusicas(idAlbum);

            if (listaMusicas == null) return NotFound();
            return Ok(listaMusicas);
        }

        // GET api/values/5
        [HttpGet("musica/{id}", Name = "GetMusica")]
        public IActionResult GetMusicaPorId(int id)
        {
            var musica = musicaRepository.Obter(id);

            if (musica == null) return NotFound();

            return Ok(musica);
        }

        [HttpGet("musica/{id}/avaliacao")]
        public IActionResult GetAvaliacaoMusica(int id)
        {
            var musica = musicaRepository.Obter(id);
            if (musica == null) return NotFound();
            var avaliacao = musicaRepository.GetAvaliacaoMusica(id);
            return Ok(avaliacao);
        }

        // POST api/values
        [HttpPost("{idAlbum}/musica")]
        public IActionResult Post(int idAlbum, [FromBody]Models.Request.MusicaDto musicaRequest)
        {
            var album = albumRepository.Obter(idAlbum);

            if(album == null) return NotFound();

            var musica = new Musica(musicaRequest.Nome, musicaRequest.Duracao, album);

            var mensagens = musicaService.Validar(musica);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            var musicaCadastrada = musicaRepository.SalvarMusica(idAlbum, musica);

            contexto.SaveChanges();

            return CreatedAtRoute("GetMusica", new {id = musicaCadastrada.IdMusica }, musicaCadastrada);
        }

        // PUT api/values/5
        [HttpPut("{idAlbum}/musica/{id}")]
        public IActionResult Put(int idAlbum, int id, [FromBody]Models.Request.MusicaDto musicaRequest)
        {
            var album = albumRepository.Obter(idAlbum);

            if (album == null) return NotFound();

            var musica = new Musica(musicaRequest.Nome, musicaRequest.Duracao, album);

            var mensagens = musicaService.Validar(musica);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            var musicaCadastrada = musicaRepository.AtualizarMusica(id, musica);

            contexto.SaveChanges();

            return Ok(musicaCadastrada);
        }

        // DELETE api/values/5
        [HttpDelete("{idAlbum}/musica/{id}")]
        public IActionResult Delete(int idAlbum, int id)
        {
            if (albumRepository.Obter(idAlbum) == null) return NotFound();

            var musicaCadastrada = musicaRepository.DeletarMusica(id);
            
            contexto.SaveChanges();
            
            return Ok(musicaCadastrada);
        }
    }
}