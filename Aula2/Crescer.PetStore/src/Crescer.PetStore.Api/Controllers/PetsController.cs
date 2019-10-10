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

        private static List<Pet> pets =  new List<Pet>();

        private static int id = 1;
        
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(pets);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var petEncontrado = pets.FirstOrDefault(pet => pet.Id == id);
            if(petEncontrado == null ){
                return NotFound("Não existe um pet com id informado");
            }
            return Ok(petEncontrado);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody]Pet pet)
        {
            if(pet.Categoria == null){
                return BadRequest("A categoria é obrigatória");
            }
            pet.Id = id++;
            pets.Add(pet);

            // return Ok(pet);
            return Created($"api/pet{pet.Id}", pet);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var petParaRemover = pets.FirstOrDefault(pet => pet.Id == id);

            if(petParaRemover == null){
                return NotFound("O pet que você tentou remover não existe");
            }

            pets.Remove(petParaRemover);

            return Ok(petParaRemover);
        }
    }
}
