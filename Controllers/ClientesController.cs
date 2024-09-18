using Microsoft.AspNetCore.Mvc;
using PIMWebAPILocal.Models;
using PIMWebAPILocal.Repositories;
using System.Collections.Generic;

namespace PIMWebAPILocal.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteRepository _clienteRepository;

        public ClientesController(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> GetClientes()
        {
            var clientes = _clienteRepository.GetClientes();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> GetCliente(int id)
        {
            var cliente = _clienteRepository.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult AddCliente(Cliente cliente)
        {
            _clienteRepository.AddCliente(cliente);
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.ClienteId }, cliente);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCliente(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            _clienteRepository.UpdateCliente(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            _clienteRepository.DeleteCliente(id);
            return NoContent();
        }
    }
}
