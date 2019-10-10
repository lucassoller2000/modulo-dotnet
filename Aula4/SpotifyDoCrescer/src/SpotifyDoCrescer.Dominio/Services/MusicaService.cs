using System.Collections.Generic;
using SpotifyDoCrescer.Dominio.Entidades;

namespace SpotifyDoCrescer.Dominio
{
    public class MusicaService
    {
        public List<string> VerificarInconsistenciasEmUmaNovaMusica(Musica musica)
        {
            var inconsistencias = new List<string>();
            if (musica.Duracao == 0)
            {
                inconsistencias.Add($"O campo {nameof(musica.Duracao)} deve ser preenchido");
            }

            if (musica.Duracao < 0)
            {
                inconsistencias.Add($"O campo {nameof(musica.Duracao)} deve ser maior que 0");
            }

            if (string.IsNullOrEmpty(musica.Nome))
            {
                inconsistencias.Add($"O campo {nameof(musica.Nome)} deve ser preenchido");
            }
            return inconsistencias;
        }
    }
}