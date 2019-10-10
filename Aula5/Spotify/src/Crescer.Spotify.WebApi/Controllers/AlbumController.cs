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
    public class AlbumController : Controller
    {
        private IAlbumRepository albumRepository;
        private Database database;
        private IMusicaRepository musicaRepository;
        private AlbumService albumService;
        
        public AlbumController(IAlbumRepository albumRepository, IMusicaRepository musicaRepository, AlbumService albumService, Database database)
        {
            this.albumRepository = albumRepository;
            this.musicaRepository = musicaRepository;
            this.albumService = albumService;
            this.database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(albumRepository.ListarAlbum());
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetAlbum")]
        public IActionResult Get(int id)
        {
            var album = albumRepository.Obter(id);

            if (album == null) return NotFound();

            return Ok(album);
        }

        [HttpGet("{id}/avaliacao")]
        public IActionResult GetAvaliacaoAlbum(int id)
        {
            var album = albumRepository.Obter(id);
            if (album == null) return NotFound();
            var avaliacao = albumRepository.GetAvaliacaoAlbum(id);
            AlbumComNota albumComNota = new AlbumComNota();
            albumComNota.IdAlbum = album.IdAlbum;
            albumComNota.Nome = album.Nome;
            albumComNota.Nota = avaliacao;
            return Ok(albumComNota);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Models.Request.AlbumDto albumRequest)
        {
            var album = MapearDtoParaDominio(albumRequest);
            var mensagens = albumService.Validar(album);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            albumRepository.SalvarAlbum(album);
            database.Commit();
            return CreatedAtRoute("GetAlbum", new { id = album.IdAlbum }, album);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Models.Request.AlbumDto albumRequest)
        {
            var album = MapearDtoParaDominio(albumRequest);
            var mensagens = albumService.Validar(album);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            albumRepository.AtualizarAlbum(id, album);
            database.Commit();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            albumRepository.DeletarAlbum(id);
             database.Commit();
            return Ok();
        }

        private Album MapearDtoParaDominio(Models.Request.AlbumDto album)
        {
            return new Album(album.Nome);
        }
    }
}