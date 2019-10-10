using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crescer.PetStore.Api.Models;
using Microsoft.AspNetCore.Mvc;
namespace Crescer.PetStore.Api.Controllers

{
    [Route("api/[controller]")]
    public class PetsController : Controller
    {      
        // GET api/values
        PetDatabase PetDatabase = new PetDatabase();

        [HttpGet]
        public IActionResult Get()
        {   
            PetDatabase Database = PetDatabase.GetDatabase();
            return Ok(Database.Pets);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            PetDatabase Database = PetDatabase.GetDatabase();
            var petEncontrado = Database.Pets.FirstOrDefault(pet => pet.Id == id);
            if(petEncontrado == null ){
                return NotFound("Não existe um pet com id informado");
            }
            return Ok(petEncontrado);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Pet pet)
        {
            PetDatabase Database = PetDatabase.GetDatabase();
            if(pet.Categoria == null){
                return BadRequest("A categoria é obrigatória");
            }
            pet.Id = Database.Id++;
            Database.Pets.Add(pet);

            Database.salvar();
            return Ok(pet);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Pet updatedPet)
        {
            PetDatabase Database = PetDatabase.GetDatabase();
            var petASerMudado = Database.Pets.FirstOrDefault(pet => pet.Id == id);
            if(petASerMudado == null ){
                return NotFound("Não existe um pet com Id informado");
            }
            updatedPet.Id = petASerMudado.Id;

            Database.Pets.Remove(petASerMudado);
            Database.Pets.Add(updatedPet);
            Database.salvar();
            return Ok(updatedPet);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            PetDatabase Database = PetDatabase.GetDatabase();
            var petParaRemover = Database.Pets.FirstOrDefault(pet => pet.Id == id);

            if(petParaRemover == null){
                return NotFound("O pet que você tentou remover não existe");
            }

            Database.Pets.Remove(petParaRemover);
            Database.salvar();
            return Ok(petParaRemover);
        }
    }
}
