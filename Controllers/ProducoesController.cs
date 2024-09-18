using Microsoft.AspNetCore.Mvc;
using PIMWebAPILocal.Models;
using PIMWebAPILocal.Repositories;
using System.Collections.Generic;

namespace PIMWebAPILocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducoesController : ControllerBase
    {
        private readonly ProducaoRepository _producaoRepository;

        public ProducoesController(ProducaoRepository producaoRepository)
        {
            _producaoRepository = producaoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Producao>> GetProducoes()
        {
            var producoes = _producaoRepository.GetProducoes();
            return Ok(producoes);
        }

        [HttpGet("{id}")]
        public ActionResult<Producao> GetProducao(int id)
        {
            var producao = _producaoRepository.GetProducaoById(id);
            if (producao == null)
            {
                return NotFound();
            }
            return Ok(producao);
        }

        [HttpPost]
        public IActionResult AddProducao(Producao producao)
        {
            _producaoRepository.AddProducao(producao);
            return CreatedAtAction(nameof(GetProducao), new { id = producao.ProducaoId }, producao);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProducao(int id, Producao producao)
        {
            if (id != producao.ProducaoId)
            {
                return BadRequest();
            }

            _producaoRepository.UpdateProducao(producao);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProducao(int id)
        {
            _producaoRepository.DeleteProducao(id);
            return NoContent();
        }
    }
}
