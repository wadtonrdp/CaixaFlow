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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("cadastro")]
        public async Task<ActionResult<UsuarioResponse>> Cadastrar([FromBody] UsuarioRequest request)
        {
            try
            {
                var resultado = await _usuarioService.CadastrarAsync(request);
                return CreatedAtAction(nameof(ListarTodos), new { id = resultado.Id }, resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioResponse>> Login([FromQuery] string email, [FromQuery] string senha)
        {
            var resultado = await _usuarioService.LoginAsync(email, senha);
            if (resultado == null) return Unauthorized(new { mensagem = "E-mail ou senha incorretos." });
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> ListarTodos()
        {
            var usuarios = await _usuarioService.ListarTodosAsync();
            return Ok(usuarios);
        }

        [HttpGet("agencia/{agenciaId}")]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> ListarPorAgencia(int agenciaId)
        {
            var usuarios = await _usuarioService.ListarPorAgenciaAsync(agenciaId);
            return Ok(usuarios);
        }
    }
}