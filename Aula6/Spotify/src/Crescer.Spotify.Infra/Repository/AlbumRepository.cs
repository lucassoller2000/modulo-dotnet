using System;
using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Crescer.Spotify.Infra.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private SpotifyContext contexto;
       
       public AlbumRepository(){ }
        public AlbumRepository(SpotifyContext contexto)
        {
            this.contexto = contexto;
        }

        public Album AtualizarAlbum(int id, Album album)
        {         
            var albumCadastrado = contexto.Albuns.FirstOrDefault(p => p.IdAlbum == id);
            if(albumCadastrado != null)
                albumCadastrado.Atualizar(album);
            
            return albumCadastrado;
        }

        public Album DeletarAlbum(int id)
        {
            var albumCadastrado = contexto.Albuns.FirstOrDefault(p => p.IdAlbum == id);
            if(albumCadastrado != null)
                contexto.Albuns.Remove(albumCadastrado);
            
            return albumCadastrado;
        }

        public decimal GetAvaliacaoAlbum(int id)
        {
            return Decimal.Round(contexto.Avaliacoes.Where(p => p.Musica.Album.IdAlbum == id).Average(x => x.Nota),1);
        }

        public List<Album> ListarAlbum()
        {
            return contexto.Albuns.AsNoTracking().ToList();
        }

        public Album Obter(int id)
        {
            var musicas = contexto.Musicas.Where(p => p.Album.IdAlbum == id).ToList();
            return contexto.Albuns.FirstOrDefault(p => p.IdAlbum == id);    
        }

        public Album SalvarAlbum(Album album)
        {
            contexto.Albuns.Add(album);
            return album;
        }     
    }
}