using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojinhaDoCrescer.Api.Models;
using LojinhaDoCrescer.Dominio.Entidades;
using LojinhaDoCrescer.Dominio.Services;
using LojinhaDoCrescer.Infra;
using Microsoft.AspNetCore.Mvc;

namespace LojinhaDoCrescer.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private IProdutoRepository produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }

        // GET api/produto
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/produto/5
        [HttpGet("{id}", Name="GetProduto")]
        public ActionResult Get(int id)
        {
            var produto = produtoRepository.BuscarPorId(id);

            return Ok(produto);
        }

        // POST api/produto
        [HttpPost]
        public ActionResult Post([FromBody]ProdutoRequestDTO produtoDTO)
        {
            var produto = new Produto(produtoDTO.Descricao, produtoDTO.Valor);
            
            var produtoService = new ProdutoService();

            var inconsistencias = produtoService.VerificarInconsistenciaEmUmNovoProduto(produto);

            if(inconsistencias.Any()) return BadRequest(inconsistencias);

            produtoRepository.Salvar(produto);

            return CreatedAtRoute("GetProduto", new {id = produto.Id}, produto);
        }

        // PUT api/produto/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/produto/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
