using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Contratos
{
    public interface IAlbumRepository
    {       
        Album SalvarAlbum(Album album);
        Album AtualizarAlbum(int id, Album album);
        Album DeletarAlbum(int id);
        List<Album> ListarAlbum();
        Album Obter(int id);
        decimal GetAvaliacaoAlbum(int id);
    }
}