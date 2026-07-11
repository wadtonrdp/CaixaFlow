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
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueService _estoqueService;

        public EstoqueController(IEstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        [HttpPost("entrada")]
        public async Task<ActionResult<EstoqueResponse>> EntradaEstoque([FromBody] EstoqueInputRequest request)
        {
            try
            {
                var resultado = await _estoqueService.EntradaEstoqueAsync(request);
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

        [HttpGet("global")]
        public async Task<ActionResult<IEnumerable<EstoqueResponse>>> ListarEstoqueGlobal()
        {
            var estoque = await _estoqueService.ListarEstoqueGlobalAsync();
            return Ok(estoque);
        }

        [HttpGet("agencia/{agenciaId}")]
        public async Task<ActionResult<IEnumerable<EstoqueResponse>>> ListarEstoquePorAgencia(int agenciaId)
        {
            var estoque = await _estoqueService.ListarEstoquePorAgenciaAsync(agenciaId);
            return Ok(estoque);
        }
    }
}