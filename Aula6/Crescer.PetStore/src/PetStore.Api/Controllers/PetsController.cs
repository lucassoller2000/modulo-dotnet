﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStore.Api.Models;
using PetStore.Dominio.Entidades;
using PetStore.Infra;

namespace PetStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class PetsController : Controller
    {
        private PetStoreContext contexto;
        public PetsController(PetStoreContext contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public IActionResult BuscarPets()
        {
            return Ok();
        }

        [HttpGet("{id}", Name = "GetPet")]
        public IActionResult BuscarPetPorId(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CriarPet([FromBody]PetRequestDTO pet)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult AlterarPet(int id, [FromBody]PetRequestDTO petAtualizado)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverPet(int id)
        {
            return Ok();
        }
    }
}