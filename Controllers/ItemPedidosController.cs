using Microsoft.AspNetCore.Mvc;
using PIMWebAPILocal.Models;
using PIMWebAPILocal.Repositories;
using System.Collections.Generic;

namespace PIMWebAPILocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPedidosController : ControllerBase
    {
        private readonly ItemPedidoRepository _itemPedidoRepository;

        public ItemPedidosController(ItemPedidoRepository itemPedidoRepository)
        {
            _itemPedidoRepository = itemPedidoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemPedido>> GetItemPedidos()
        {
            var itens = _itemPedidoRepository.GetItemPedidos();
            return Ok(itens);
        }

        [HttpGet("{id}")]
        public ActionResult<ItemPedido> GetItemPedido(int id)
        {
            var item = _itemPedidoRepository.GetItemPedidoById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult AddItemPedido(ItemPedido item)
        {
            _itemPedidoRepository.AddItemPedido(item);
            return CreatedAtAction(nameof(GetItemPedido), new { id = item.ItemPedidoId }, item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateItemPedido(int id, ItemPedido item)
        {
            if (id != item.ItemPedidoId)
            {
                return BadRequest();
            }

            _itemPedidoRepository.UpdateItemPedido(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItemPedido(int id)
        {
            _itemPedidoRepository.DeleteItemPedido(id);
            return NoContent();
        }
    }
}
