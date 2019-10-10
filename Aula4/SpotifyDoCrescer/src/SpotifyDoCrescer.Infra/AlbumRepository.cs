using System;
using System.Collections.Generic;
using System.Linq;
using SpotifyDoCrescer.Dominio.Contratos;
using SpotifyDoCrescer.Dominio.Entidades;

namespace SpotifyDoCrescer.Infra
{
    public class AlbumRepository : IAlbumRepository
    {
        private static List<Album> albuns = new List<Album>();

        private static int idAlbumAtual = 1;

        public void CriarAlbum(Album album)
        {
            album.Id = idAlbumAtual++;
            albuns.Add(album);
        }

        public Album BuscarAlbumPorId(int idAlbum)
        {
            return albuns.FirstOrDefault(album => album.Id == idAlbum);
        }

        public void DeletarAlbum(int idAlbum)
        {
            var albumParaDeletar = albuns.FirstOrDefault(album => album.Id == idAlbum);
            albuns.Remove(albumParaDeletar);
        }

        public List<Album> BuscarAlbuns()
        {
            return albuns;
        }

        public void AlterarAlbum(int idAlbum, Album albumAlterado)
        {
            var albumParaAlterar = albuns.FirstOrDefault(album => album.Id == idAlbum);
            var musicas = albumParaAlterar.Musicas;
            albumAlterado.Musicas.AddRange(musicas);
            albumAlterado.Id = idAlbum;
            albuns.Remove(albumParaAlterar);
            albuns.Add(albumAlterado);
        }
    }
}
