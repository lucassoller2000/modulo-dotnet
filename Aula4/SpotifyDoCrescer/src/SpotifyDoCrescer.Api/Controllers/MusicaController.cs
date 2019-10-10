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
    [Route("api/album/{idAlbum}/musica")]
    public class MusicaController : Controller
    {
        private MusicaRepository musicaRepository = new MusicaRepository();
        private AlbumRepository albumRepository = new AlbumRepository();
        private MusicaService musicaService = new MusicaService();

        // GET api/values
        [HttpGet]
        public ActionResult BuscarMusicas(int idAlbum)
        {
            var album = albumRepository.BuscarAlbumPorId(idAlbum);

            if(album == null) return NotFound("Album não encontrado");

            var musicas = musicaRepository.BuscarMusicasDeAlbum(idAlbum);
            
            return Ok(musicas);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult BuscarMusica(int idAlbum, int id)
        {
            var album = albumRepository.BuscarAlbumPorId(idAlbum);
            
            if(album == null) return NotFound("Album não encontrado");

            var musica = musicaRepository.BuscarMusicaPorId(idAlbum, id);
            
            if(musica == null) return NotFound("Música não encontrada");
            
            return Ok(musica);
        }

        // POST api/values
        [HttpPost]
        public ActionResult SalvarMusica(int idAlbum, [FromBody]MusicaRequestDTO musicaDTO)
        {
            var album = albumRepository.BuscarAlbumPorId(idAlbum);

            if(album == null) return NotFound("Album não encontrado");

            var musica = new Musica(musicaDTO.Nome, musicaDTO.Duracao);
        
            var inconsistencias = musicaService.VerificarInconsistenciasEmUmaNovaMusica(musica);

            if(inconsistencias.Any()) return BadRequest(inconsistencias);

            musicaRepository.CriarMusica(idAlbum, musica);

            return Ok(musica);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult AlterarMusica(int idAlbum, int id, [FromBody]MusicaRequestDTO musicaDTO)
        {
            var album = albumRepository.BuscarAlbumPorId(idAlbum);

            if(album == null) return NotFound("Album não encontrado");

            var musicaAlterada = new Musica(musicaDTO.Nome, musicaDTO.Duracao);

            if(musicaAlterada == null) return NotFound("Música não encontrada");

            var inconsistencias = musicaService.VerificarInconsistenciasEmUmaNovaMusica(musicaAlterada);

            if(inconsistencias.Any()) return BadRequest(inconsistencias);

            musicaRepository.AlterarMusica(idAlbum, id, musicaAlterada);

            return Ok(musicaAlterada);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult DeletarMusica(int idAlbum, int id)
        {
            var album = albumRepository.BuscarAlbumPorId(idAlbum);

            if(album == null) return NotFound("Album não encontrado");

            var musicaParaDeletar = musicaRepository.BuscarMusicaPorId(idAlbum, id);

            if(musicaParaDeletar == null) return NotFound("Música não encontrada");

            musicaRepository.DeletarMusica(idAlbum, id);

            return Ok(musicaParaDeletar);
        }
    }
}