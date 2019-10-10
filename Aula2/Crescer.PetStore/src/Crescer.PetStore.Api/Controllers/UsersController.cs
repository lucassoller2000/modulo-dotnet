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

        private static List<User> users =  new List<User>();

        private static int id = 1;
        
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(users);
        }

        // GET api/values/5
        [HttpGet("{login}")]
        public ActionResult Get(string login)
        {
            var userEncontrado = users.FirstOrDefault(user => user.Login == login);
            if(userEncontrado == null ){
                return NotFound("Não existe um usuário com login informado");
            }
            return Ok(userEncontrado);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody]User user)
        {
            var userComMesmoLogin = users.FirstOrDefault(userNaLista => userNaLista.Login == user.Login);
            if(userComMesmoLogin != null){
                return BadRequest("Login já está em uso");
            }
            user.Id = id++;
            users.Add(user);

            return Created($"api/User{user.Id}", user);
        }

        // PUT api/values/5
        [HttpPut("{login}")]
        public ActionResult Put(string login, [FromBody]User updatedUser)
        {
            var userASerMudado = users.FirstOrDefault(user => user.Login == login);
            if(userASerMudado == null ){
                return NotFound("Não existe um usuário com login informado");
            }
            // userASerMudado = value;
            updatedUser.Id = userASerMudado.Id;

            users.Remove(userASerMudado);
            users.Add(updatedUser);
            return Ok(updatedUser);
        }

        // DELETE api/values/5
        [HttpDelete("{login}")]
        public ActionResult Delete(string login)
        {
            var userParaRemover = users.FirstOrDefault(user => user.Login == login);

            if(userParaRemover == null){
                return NotFound("O usuário que você tentou remover não existe");
            }

            users.Remove(userParaRemover);

            return Ok(userParaRemover);
        }
    }
}
