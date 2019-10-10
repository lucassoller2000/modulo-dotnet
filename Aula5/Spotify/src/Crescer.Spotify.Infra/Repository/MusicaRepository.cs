using System;
using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Dapper;

namespace Crescer.Spotify.Infra.Repository
{
    public class MusicaRepository : IMusicaRepository
    {
        private Database database;

        public MusicaRepository(Database database)
        {
            this.database = database;
        }
        
        public void AtualizarMusica(int id, Musica musica)
        {
            database.Connection.Execute(@"
                UPDATE [dbo].[Musica] SET
                    [Nome] = @Nome,
                    [Duracao] = @Duracao              
                WHERE
                    [idMusica] = @id", new {id, musica.Nome, musica.Duracao}, database.Transaction);
        }

        public void DeletarMusica(int id)
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository(database);
            usuarioRepository.DeletarAvaliacaoPorIdMusica(id);
            database.Connection.Execute(@"
                DELETE FROM [dbo].[Musica]              
                WHERE
                    [idMusica] = @id", new {id}, database.Transaction);
        }

        public double GetAvaliacaoMusica(int id)
        {
            return database.Connection.Query<double>(@"
                SELECT 
                    ROUND(avg(convert(float, [Nota])) *1, 1) AS Nota
                FROM [dbo].[Avaliacao]
                WHERE
                    [idMusica] = @Id
                GROUP BY [idMusica]", new { id }, database.Transaction).FirstOrDefault();
        }

        public void DeletarMusicaPorIdAlbum(int id)
        {
            database.Connection.Execute(@"
                DELETE FROM [dbo].[Avaliacao]              
                WHERE
                    [idMusica] in
                (SELECT [idMusica] from [dbo].[Musica]
                    WHERE [idAlbum] = @id)", new {id}, database.Transaction);
            
            database.Connection.Execute(@"
                DELETE FROM [dbo].[Musica]              
                WHERE
                    [idAlbum] = @id", new {id}, database.Transaction);
        }

        public List<Musica> ListarMusicas(int idAlbum)
        {
            return database.Connection.Query<Musica>(@"
               SELECT  
                    [idMusica]
                   ,[Nome]
                   ,[Duracao]
               FROM [dbo].[Musica]
               WHERE
                    [idAlbum] = @idAlbum
               ", new {idAlbum},
                   database.Transaction).ToList();
        }

        public Musica Obter(int idMusica)
        {
             return database.Connection.Query<Musica>(@"
                SELECT   
                     [idMusica]
                    ,[Nome]
                    ,[Duracao]
                FROM [dbo].[Musica]
                WHERE
                    [idMusica] = @idMusica", new {idMusica }, database.Transaction).FirstOrDefault();
        }

        public void SalvarMusica(int idAlbum, Musica musica)
        {
            int id = database.Connection.Query<int>(@"
                INSERT INTO [dbo].[Musica]
                    ([Nome],
                    [Duracao],
                    [IdAlbum])
                VALUES
                    (@Nome,
                    @Duracao,
                    @idAlbum);
                SELECT CAST(SCOPE_IDENTITY() as int)", new {idAlbum, musica.Nome, musica.Duracao}, database.Transaction).Single();
            musica.IdMusica = id;   
        }
    }
}