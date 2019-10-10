using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Contratos
{
    public interface IMusicaRepository
    {
        Musica SalvarMusica(int idAlbum, Musica musica);
        Musica AtualizarMusica(int id, Musica musica);
        Musica DeletarMusica(int id);
        List<Musica> ListarMusicas(int idAlbum);             
        Musica Obter(int id);
        decimal GetAvaliacaoMusica(int id);
    }
}