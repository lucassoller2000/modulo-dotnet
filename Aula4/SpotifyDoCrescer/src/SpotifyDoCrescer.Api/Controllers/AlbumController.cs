using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyDoCrescer.Api.Models;
using SpotifyDoCrescer.Dominio;
using SpotifyDoCrescer.Dominio.Contratos;
using SpotifyDoCrescer.Dominio.Entidades;
using SpotifyDoCrescer.Infra;

namespace SpotifyDoCrescer.Api.Controllers
{
    [Route("api/Album")]
    public class AlbumController : Controller
    {
        private AlbumRepository albumRepository = new AlbumRepository();

        private AlbumService albumService = new AlbumService();
        
        // GET api/values
        [HttpGet]
        public ActionResult BuscarAlbuns()
        {
            var albuns = albumRepository.BuscarAlbuns();
            
            return Ok(albuns);
        }

        // GET api/Album
        [HttpGet("{id}")]
        public ActionResult BuscarAlbum(int id)
        {
            var album = albumRepository.BuscarAlbumPorId(id);
            
            if (album == null) return NotFound("Album não encontrado");
            
            return Ok(album);
        }

        // POST api/values
        [HttpPost]
        public ActionResult SalvarAlbum([FromBody]AlbumRequestDTO albumDTO)
        {
            var album = new Album(albumDTO.Nome);
            
            var inconsistencias = albumService.VerificarInconsistenciasEmUmNovoAlbum(album);
            
            if(inconsistencias.Any()) return BadRequest(inconsistencias);
            
            albumRepository.CriarAlbum(album);
           
            return Ok(album);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult AlterarAlbum(int id, [FromBody]AlbumRequestDTO albumDTO)
        {
            var albumAlterado = new Album(albumDTO.Nome);
            
            if (albumAlterado == null) return NotFound("Album não encontrado");
            
            var inconsistencias = albumService.VerificarInconsistenciasEmUmNovoAlbum(albumAlterado);
            
            if(inconsistencias.Any()) return BadRequest(inconsistencias);
           
            albumRepository.AlterarAlbum(id, albumAlterado);
            
            return Ok(albumAlterado);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult DeletarAlbum(int id)
        {
            var albumParaDeletar = albumRepository.BuscarAlbumPorId(id);

            if (albumParaDeletar == null) return NotFound("Album não encontrado");
            
            albumRepository.DeletarAlbum(id);
            
            return Ok(albumParaDeletar);
        }
    }
}
