using Microsoft.AspNetCore.Mvc;
using PIMWebAPILocal.Models;
using PIMWebAPILocal.Models;
using PIMWebAPILocal.Repositories;
using System.Collections.Generic;

namespace PIMWebAPILocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutosController(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {
            var produtos = _produtoRepository.GetProdutos();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public ActionResult<Produto> GetProduto(int id)
        {
            var produto = _produtoRepository.GetProdutoById(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost]
        public IActionResult AddProduto(Produto produto)
        {
            _produtoRepository.AddProduto(produto);
            return CreatedAtAction(nameof(GetProduto), new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduto(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _produtoRepository.UpdateProduto(produto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(int id)
        {
            _produtoRepository.DeleteProduto(id);
            return NoContent();
        }
    }
}
