using System.Collections.Generic;
using SpotifyDoCrescer.Dominio.Entidades;

namespace SpotifyDoCrescer.Dominio
{
    public class AlbumService
    {
        public List<string> VerificarInconsistenciasEmUmNovoAlbum(Album album)
        {
            var inconsistencias = new List<string>();
            if (string.IsNullOrEmpty(album.Nome))
            {
                inconsistencias.Add($"O campo {nameof(album.Nome)} deve ser preenchido");
            }
            return inconsistencias;
        }
    }
}