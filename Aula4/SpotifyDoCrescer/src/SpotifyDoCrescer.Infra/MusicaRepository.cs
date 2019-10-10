using System.Collections.Generic;
using SpotifyDoCrescer.Dominio.Contratos;
using SpotifyDoCrescer.Dominio.Entidades;
using System.Linq;

namespace SpotifyDoCrescer.Infra
{
    public class MusicaRepository : IMusicaRepository
    {
        AlbumRepository albumRepository = new AlbumRepository();
        private static int idMusicaAtual = 1;
        
        public void CriarMusica(int idAlbum, Musica musica)
        {
            musica.Id = idMusicaAtual++;
            var album = albumRepository.BuscarAlbumPorId(idAlbum);
            album.Musicas.Add(musica);
        }

        public void AlterarMusica(int idAlbum, int idMusica, Musica musicaAlterada)
        {
            var album = albumRepository.BuscarAlbumPorId(idAlbum);
            var musicaParaAlterar = album.Musicas.FirstOrDefault(musica => musica.Id == idMusica);
            musicaAlterada.Id = idMusica;
            album.Musicas.Remove(musicaParaAlterar);
            album.Musicas.Add(musicaAlterada);
        }

        public Musica BuscarMusicaPorId(int idAlbum, int idMusica)
        {
            var album = albumRepository.BuscarAlbumPorId(idAlbum);
            return album.Musicas.FirstOrDefault(musica => musica.Id == idMusica);
        }
        
        public List<Musica> BuscarMusicasDeAlbum(int idAlbum)
        {
            var album = albumRepository.BuscarAlbumPorId(idAlbum);
            return album.Musicas;
        }

        public void DeletarMusica(int idAlbum, int idMusica)
        {
            var album = albumRepository.BuscarAlbumPorId(idAlbum);
            var musicaParaDeletar = album.Musicas.FirstOrDefault(musica => musica.Id == idMusica);
            album.Musicas.Remove(musicaParaDeletar);
        }
    }
}