using System;
using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Dapper;
using System;
using Microsoft.EntityFrameworkCore;

namespace Crescer.Spotify.Infra.Repository
{
    public class MusicaRepository : IMusicaRepository
    {

        private SpotifyContext contexto;

        public MusicaRepository()
        {
        }

        public MusicaRepository(SpotifyContext contexto)
        {
            this.contexto = contexto;
        }
        
        public Musica AtualizarMusica(int id, Musica musica)
        {
            var musicaCadastrada = contexto.Musicas.FirstOrDefault(p => p.IdMusica == id);
            if(musicaCadastrada != null)
                musicaCadastrada.Atualizar(musica);

            return musicaCadastrada;
        }

        public Musica DeletarMusica(int id)
        {
            var musicaCadastrada = contexto.Musicas.FirstOrDefault(p => p.IdMusica == id);
            if(musicaCadastrada != null)
                contexto.Musicas.Remove(musicaCadastrada);
            
            return musicaCadastrada;
        }

        public decimal GetAvaliacaoMusica(int id)
        {
            return Decimal.Round(contexto.Avaliacoes.Where(p => p.Musica.IdMusica == id).Average(x => x.Nota),1);
        }

        public List<Musica> ListarMusicas(int idAlbum)
        {
            return contexto.Musicas.AsNoTracking().Where(p => p.Album.IdAlbum == idAlbum).ToList();
        }

        public Musica Obter(int idMusica)
        {
            return contexto.Musicas.FirstOrDefault(p => p.IdMusica == idMusica);
        }

        public Musica SalvarMusica(int idAlbum, Musica musica)
        {
            contexto.Musicas.Add(musica);
            return musica;
        }
    }
}