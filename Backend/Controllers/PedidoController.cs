using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Requests;
using Models.DTOs.Responses;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<ActionResult<PedidoResponse>> CriarPedido([FromBody] PedidoRequest request)
        {
            try
            {
                var resultado = await _pedidoService.CriarPedidoAsync(request);
                return CreatedAtAction(nameof(ObterPorId), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoResponse>> ObterPorId(int id)
        {
            var pedido = await _pedidoService.ObterPorIdAsync(id);
            if (pedido == null) return NotFound(new { mensagem = "Pedido não encontrado." });
            return Ok(pedido);
        }

        [HttpGet("global")]
        public async Task<ActionResult<IEnumerable<PedidoResponse>>> ListarTodosGlobal()
        {
            var pedidos = await _pedidoService.ListarTodosGlobalAsync();
            return Ok(pedidos);
        }

        [HttpGet("agencia/{agenciaId}")]
        public async Task<ActionResult<IEnumerable<PedidoResponse>>> ListarPorAgencia(int agenciaId)
        {
            var pedidos = await _pedidoService.ListarPorAgenciaAsync(agenciaId);
            return Ok(pedidos);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<PedidoResponse>> AlterarStatus(int id, [FromQuery] string novoStatus, [FromQuery] int usuarioId)
        {
            try
            {
                var resultado = await _pedidoService.AlterarStatusAsync(id, novoStatus, usuarioId);
                return Ok(resultado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}