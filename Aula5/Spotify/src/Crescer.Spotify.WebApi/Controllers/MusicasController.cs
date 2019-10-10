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
    public class MusicasController : Controller
    {
        private IMusicaRepository musicaRepository;
        private IAlbumRepository albumRepository;
        private MusicaService musicaService;
        private Database database;

        public MusicasController(IMusicaRepository musicaRepository, MusicaService musicaService, IAlbumRepository albumRepository, Database database)
        {
            this.musicaRepository = musicaRepository;
            this.musicaService = musicaService;
            this.albumRepository = albumRepository;
            this.database = database;
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
            var musica = musicaRepository.Obter( id);

            if (musica == null) return NotFound();
            return Ok(musica);
        }

        [HttpGet("musica/{id}/avaliacao")]
        public IActionResult GetAvaliacaoMusica(int id)
        {
            var musica = musicaRepository.Obter(id);
            if (musica == null) return NotFound();
            var avaliacao = musicaRepository.GetAvaliacaoMusica(id);
            MusicaComNota musicaComNota = new MusicaComNota();
            musicaComNota.IdMusica = musica.IdMusica;
            musicaComNota.Nome = musica.Nome;
            musicaComNota.Duracao = musica.Duracao;
            musicaComNota.Nota = avaliacao;
            return Ok(musicaComNota);
        }

        // POST api/values
        [HttpPost("{idAlbum}/musica")]
        public IActionResult Post(int idAlbum, [FromBody]Models.Request.MusicaDto musicaRequest)
        {
            if (albumRepository.Obter(idAlbum) == null) return NotFound();

            var musica = MapearDtoParaDominio(musicaRequest);
            var mensagens = musicaService.Validar(musica);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            musicaRepository.SalvarMusica(idAlbum, musica);
            database.Commit();
            return CreatedAtRoute("GetMusica", new { idAlbum = idAlbum, id = musica.IdMusica }, musica);
        }

        // PUT api/values/5
        [HttpPut("{idAlbum}/musica/{id}")]
        public IActionResult Put(int idAlbum, int id, [FromBody]Models.Request.MusicaDto musicaRequest)
        {
            if (albumRepository.Obter(idAlbum) == null) return NotFound();

            var musica = MapearDtoParaDominio(musicaRequest);
            var mensagens = musicaService.Validar(musica);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            musicaRepository.AtualizarMusica(id, musica);
            database.Commit();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{idAlbum}/musica/{id}")]
        public IActionResult Delete(int idAlbum, int id)
        {
            if (albumRepository.Obter(idAlbum) == null) return NotFound();

            musicaRepository.DeletarMusica(id);
            database.Commit();
            return Ok();
        }

        private Musica MapearDtoParaDominio(Models.Request.MusicaDto musica)
        {
            return new Musica(musica.Nome, musica.Duracao);
        }
    }
}