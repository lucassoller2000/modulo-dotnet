using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Dapper;

namespace Crescer.Spotify.Infra.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private Database database;
       
        public AlbumRepository(Database database)
        {
            this.database = database;
        }

        public void AtualizarAlbum(int id, Album album)
        {         
            database.Connection.Execute(@"
                UPDATE [dbo].[Album] SET
                    [Nome] = @Nome               
                WHERE
                    [idAlbum] = @Id", new { id, album.Nome}, database.Transaction);
        }

        public void DeletarAlbum(int id)
        {
            MusicaRepository musicaRepository = new MusicaRepository(database);
            musicaRepository.DeletarMusicaPorIdAlbum(id);
    
            database.Connection.Execute(@"
                DELETE FROM [dbo].[Album]              
                WHERE
                    [idAlbum] = @id", new { id}, database.Transaction);
        }

        public double GetAvaliacaoAlbum(int id)
        {
            return database.Connection.Query<double>(@"
                SELECT   
                    ROUND(avg(convert(float, a.[Nota])) *1,1)
                FROM [dbo].[Avaliacao] AS a
                INNER JOIN
                    [dbo].[Musica] m
                ON a.[idMusica] = m.[idMusica]
                INNER JOIN
                    [dbo].[Album] al
                ON al.[idAlbum] = m.[idAlbum]
                WHERE
                    al.[idAlbum] = @Id
                GROUP BY al.[idAlbum]", new { id }, database.Transaction).FirstOrDefault();
        }

        public List<Album> ListarAlbum()
        {
           return database.Connection.Query<Album>(@"
               SELECT  
                    [idAlbum]
                   ,[Nome]
               FROM [dbo].[Album]
               ", new { },
                   database.Transaction).ToList();
        }

        public Album Obter(int id)
        {
             return database.Connection.Query<Album>(@"
                SELECT   
                     [idAlbum]
                    ,[Nome]
                FROM [dbo].[Album]
                WHERE
                    [idAlbum] = @Id", new { id }, database.Transaction).FirstOrDefault();
        }

        public void SalvarAlbum(Album album)
        {
            int id = database.Connection.Query<int>(@"
                INSERT INTO [dbo].[Album]
                    ([Nome])
                VALUES
                    (
                      @Nome);
                SELECT CAST(SCOPE_IDENTITY() as int)", new { album.Nome}, database.Transaction).Single();
            album.IdAlbum = id;          
        }     
    }
}