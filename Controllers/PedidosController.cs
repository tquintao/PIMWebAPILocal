using Microsoft.AspNetCore.Mvc;
using PIMWebAPILocal.Models;
using PIMWebAPILocal.Repositories;
using System.Collections.Generic;

namespace PIMWebAPILocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoRepository _pedidoRepository;

        public PedidosController(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pedido>> GetPedidos()
        {
            var pedidos = _pedidoRepository.GetPedidos();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public ActionResult<Pedido> GetPedido(int id)
        {
            var pedido = _pedidoRepository.GetPedidoById(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpPost]
        public IActionResult AddPedido(Pedido pedido)
        {
            _pedidoRepository.AddPedido(pedido);
            return CreatedAtAction(nameof(GetPedido), new { id = pedido.PedidoId }, pedido);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePedido(int id, Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return BadRequest();
            }

            _pedidoRepository.UpdatePedido(pedido);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePedido(int id)
        {
            _pedidoRepository.DeletePedido(id);
            return NoContent();
        }
    }
}
