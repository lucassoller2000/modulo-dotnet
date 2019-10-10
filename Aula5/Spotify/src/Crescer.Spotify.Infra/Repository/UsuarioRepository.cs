using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Dapper;

namespace Crescer.Spotify.Infra.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private Database database;
        
        // public UsuarioRepository(){}

        public UsuarioRepository(Database database)
        {
            this.database = database;
        }

        public void AtualizarUsuario(int id, Usuario usuario)
        {         
            database.Connection.Execute(@"
                UPDATE [dbo].[Usuario] SET
                    [Nome] = @Nome               
                WHERE
                    [idUsuario] = @Id", new { id, usuario.Nome}, database.Transaction);
        }

        public int AvaliarMusica(Avaliacao avaliacao)
        {
            if(this.BuscarAvaliacao(avaliacao) != null){
                database.Connection.Execute(@"
                UPDATE [dbo].[Avaliacao] SET
                    [Nota] = @Nota               
                WHERE
                    [idUsuario] = @IdUsuario
                AND [idMusica] = @IdMusica", new {avaliacao.Nota, avaliacao.IdUsuario, avaliacao.IdMusica}, database.Transaction);
                return 1;
            }
            
            int id = database.Connection.Query<int>(@"
                INSERT INTO [dbo].[Avaliacao]
                    ([IdUsuario]
                    ,[IdMusica]
                    ,[Nota])
                VALUES
                    (
                    @IdUsuario
                    ,@IdMusica
                    ,@Nota);
                SELECT CAST(SCOPE_IDENTITY() as int)", new {avaliacao.IdUsuario, avaliacao.IdMusica, avaliacao.Nota}, database.Transaction).Single();
            avaliacao.IdAvaliacao = id;
            return 0;         
        }

        public void DeletarUsuario(int id)
        {
            this.DeletarAvaliacaoPorIdUsuario(id);
            database.Connection.Execute(@"
                DELETE FROM [dbo].[Usuario]              
                WHERE
                    [idUsuario] = @id", new { id}, database.Transaction);
        }

        public List<Usuario> ListarUsuario()
        {
           return database.Connection.Query<Usuario>(@"
               SELECT  
                    [idUsuario]
                   ,[Nome]
               FROM [dbo].[Usuario]
               ", new { },
                   database.Transaction).ToList();
        }

        public Usuario Obter(int id)
        {
             return database.Connection.Query<Usuario>(@"
                SELECT   
                     [idUsuario]
                    ,[Nome]
                FROM [dbo].[Usuario]
                WHERE
                    [idUsuario] = @Id", new { id }, database.Transaction).FirstOrDefault();
        }

        public void SalvarUsuario(Usuario usuario)
        {
            int id = database.Connection.Query<int>(@"
                INSERT INTO [dbo].[Usuario]
                    ([Nome])
                VALUES
                    (
                      @Nome);
                SELECT CAST(SCOPE_IDENTITY() as int)", new { usuario.Nome}, database.Transaction).Single();
            usuario.IdUsuario = id;          
        }

        public Avaliacao BuscarAvaliacao(Avaliacao avaliacao)
        {
            return database.Connection.Query<Avaliacao>(@"
                SELECT   
                     [idUsuario]
                    ,[idMusica]
                FROM [dbo].[Avaliacao]
                WHERE
                    [idUsuario] = @IdUsuario
                AND [idMusica] = @IdMusica", new {avaliacao.IdUsuario, avaliacao.IdMusica}, database.Transaction).FirstOrDefault();
        }
        
        public void DeletarAvaliacaoPorIdMusica(int id)
        {
            database.Connection.Execute(@"
                DELETE FROM [dbo].[Avaliacao]              
                WHERE
                    [idMusica] = @id", new { id}, database.Transaction);
        }

        public void DeletarAvaliacaoPorIdUsuario(int id)
        {
            database.Connection.Execute(@"
                DELETE FROM [dbo].[Avaliacao]              
                WHERE
                    [idUsuario] = @id", new { id}, database.Transaction);
        }
    }
}