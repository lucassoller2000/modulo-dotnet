using System.Collections.Generic;
using SpotifyDoCrescer.Dominio.Entidades;

namespace SpotifyDoCrescer.Dominio.Contratos
{
    public interface IMusicaRepository
    {
        void CriarMusica (int idAlbum, Musica musica);

        List<Musica> BuscarMusicasDeAlbum(int idAlbum);
        
        Musica BuscarMusicaPorId(int idAlbum, int idMusica);
        
        void AlterarMusica (int idAlbum, int idMusica, Musica musica);

        void DeletarMusica(int idAlbum, int idMusica);
    }
}