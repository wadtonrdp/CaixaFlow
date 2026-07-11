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
    public class EquipamentoController : ControllerBase
    {
        private readonly IEquipamentoService _equipamentoService;

        public EquipamentoController(IEquipamentoService equipamentoService)
        {
            _equipamentoService = equipamentoService;
        }

        [HttpPost]
        public async Task<ActionResult<EquipamentoResponse>> Cadastrar([FromBody] EquipamentoRequest request)
        {
            try
            {
                var resultado = await _equipamentoService.CadastrarAsync(request);
                return CreatedAtAction(nameof(ObterPorId), new { id = resultado.Id }, resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipamentoResponse>> ObterPorId(int id)
        {
            var equipamento = await _equipamentoService.ObterPorIdAsync(id);
            if (equipamento == null) return NotFound(new { mensagem = "Equipamento não encontrado." });
            return Ok(equipamento);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipamentoResponse>>> ListarTodos()
        {
            var equipamentos = await _equipamentoService.ListarTodosAsync();
            return Ok(equipamentos);
        }

        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<EquipamentoResponse>>> ListarPorTipo(string tipo)
        {
            var equipamentos = await _equipamentoService.ListarPorTipoAsync(tipo);
            return Ok(equipamentos);
        }
    }
}