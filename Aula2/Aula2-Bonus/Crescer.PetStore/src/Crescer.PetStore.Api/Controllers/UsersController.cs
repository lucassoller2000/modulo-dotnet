using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crescer.PetStore.Api.Models;
using Microsoft.AspNetCore.Mvc;
namespace Crescer.PetStore.Api.Controllers

{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        UserDatabase UserDatabase = new UserDatabase();
        
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            UserDatabase Database = UserDatabase.GetDatabase();
            return Ok(Database.Users);
        }

        // GET api/values/5
        [HttpGet("{login}")]
        public IActionResult Get(string login)
        {
            UserDatabase Database = UserDatabase.GetDatabase();
            var userEncontrado = Database.Users.FirstOrDefault(user => user.Login == login);
            if(userEncontrado == null ){
                return NotFound("Não existe um usuário com login informado");
            }
            return Ok(new{
                userEncontrado.Id,
                userEncontrado.PrimeiroNome,
                userEncontrado.UltimoNome,
                userEncontrado.Telefone,
                userEncontrado.Email,
                userEncontrado.Login
            });
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            UserDatabase Database = UserDatabase.GetDatabase();
            if(user == null) return BadRequest($"O parametro {nameof(user)} não pode ser nulo");

            var usuario = Database.Users.FirstOrDefault(x => x.Login == user.Login);
            if(usuario != null){
                return BadRequest($"O parametro {nameof(user.Login)} ja existe");
            }

            user.Id = Database.Id++;
            Database.Users.Add(user);

            Database.salvar();
            return Ok(new{
                user.Id,
                user.PrimeiroNome,
                user.UltimoNome,
                user.Telefone,
                user.Email,
                user.Login
            });
        }
       

        // PUT api/values/5
        [HttpPut("{login}")]
        public IActionResult Put(string login, [FromBody]User updatedUser)
        {
            UserDatabase Database = UserDatabase.GetDatabase();
            var userASerMudado = Database.Users.FirstOrDefault(user => user.Login == login);
            if(userASerMudado == null ){
                return NotFound("Não existe um usuário com login informado");
            }
            updatedUser.Id = userASerMudado.Id;

            Database.Users.Remove(userASerMudado);
            Database.Users.Add(updatedUser);
            Database.salvar();
            return Ok(new{
                updatedUser.Id,
                updatedUser.PrimeiroNome,
                updatedUser.UltimoNome,
                updatedUser.Telefone,
                updatedUser.Email,
                updatedUser.Login
            });
        }

        // DELETE api/values/5
        [HttpDelete("{login}")]
        public IActionResult Delete(string login)
        {
            UserDatabase Database = UserDatabase.GetDatabase();
            var userParaRemover = Database.Users.FirstOrDefault(user => user.Login == login);

            if(userParaRemover == null){
                return NotFound("O usuário que você tentou remover não existe");
            }

            Database.Users.Remove(userParaRemover);
            Database.salvar();
            return Ok(new{
                userParaRemover.Id,
                userParaRemover.PrimeiroNome,
                userParaRemover.UltimoNome,
                userParaRemover.Telefone,
                userParaRemover.Email,
                userParaRemover.Login
            });
        }
    }
}
