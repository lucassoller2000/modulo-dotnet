using System.Collections.Generic;
using SpotifyDoCrescer.Dominio.Entidades;

namespace SpotifyDoCrescer.Dominio.Contratos
{
    public interface IAlbumRepository
    {
        void CriarAlbum (Album album);
        
        List<Album> BuscarAlbuns();

        Album BuscarAlbumPorId(int idAlbum);
            
        void AlterarAlbum (int idAlbum, Album albumAlterado); 

        void DeletarAlbum (int idAlbum);
    }
}