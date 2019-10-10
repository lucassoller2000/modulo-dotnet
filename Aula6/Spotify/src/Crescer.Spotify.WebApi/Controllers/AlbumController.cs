using System;
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

        private AlbumService albumService;
        private SpotifyContext contexto;
        
        public AlbumController(IAlbumRepository albumRepository,AlbumService albumService, SpotifyContext contexto)
        {
            this.albumRepository = albumRepository;
            this.albumService = albumService;
            this.contexto = contexto;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var albuns = albumRepository.ListarAlbum();
            return Ok(albuns);
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
            return Ok(avaliacao);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Models.Request.AlbumDto albumRequest)
        {
            var album = MapearDtoParaDominio(albumRequest);
            var mensagens = albumService.Validar(album);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            var albumCadastrado = albumRepository.SalvarAlbum(album);

            contexto.SaveChanges();

            return CreatedAtRoute("GetAlbum", new { id = albumCadastrado.IdAlbum }, albumCadastrado);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Models.Request.AlbumDto albumRequest)
        {
            var album = albumRepository.Obter(id);
            
            if (album == null) return NotFound();
            
            var albumParaAlterar = MapearDtoParaDominio(albumRequest);
            
            var mensagens = albumService.Validar(albumParaAlterar);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            var albumCadastrado = albumRepository.AtualizarAlbum(id, albumParaAlterar);

            contexto.SaveChanges();

            return Ok(albumCadastrado);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var album = albumRepository.Obter(id);

            if (album == null) return NotFound();

            var albumCadastrado = albumRepository.DeletarAlbum(id);

            contexto.SaveChanges();

            return Ok(albumCadastrado);
        }

        private Album MapearDtoParaDominio(Models.Request.AlbumDto album)
        {
            return new Album(album.Nome);
        }
    }
}