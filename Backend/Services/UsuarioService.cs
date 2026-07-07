using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.Requests;
using Models.DTOs.Responses;
using Models.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioResponse> CadastrarAsync(UsuarioRequest request)
        {
            var emailExiste = await _context.Usuarios.AnyAsync(u => u.Email.ToLower() == request.Email.ToLower());
            if (emailExiste) throw new InvalidOperationException("E-mail corporativo já cadastrado.");

            string senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(request.Senha);

            var usuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = senhaCriptografada,
                Nivel = request.Nivel.ToUpper(),
                AgenciaId = request.AgenciaId
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            string nomeAgencia = null;
            if (usuario.AgenciaId.HasValue)
            {
                var ag = await _context.Agencias.FindAsync(usuario.AgenciaId.Value);
                nomeAgencia = ag?.Nome;
            }

            return new UsuarioResponse { Id = usuario.Id, Nome = usuario.Nome, Email = usuario.Email, Nivel = usuario.Nivel, AgenciaId = usuario.AgenciaId, AgenciaNome = nomeAgencia };
        }

        public async Task<UsuarioResponse> LoginAsync(string email, string senha)
        {
            var usuario = await _context.Usuarios.Include(u => u.Agencia).FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash)) return null;

            return new UsuarioResponse { Id = usuario.Id, Nome = usuario.Nome, Email = usuario.Email, Nivel = usuario.Nivel, AgenciaId = usuario.AgenciaId, AgenciaNome = usuario.Agencia?.Nome };
        }

        public async Task<IEnumerable<UsuarioResponse>> ListarTodosAsync()
        {
            var dados = await _context.Usuarios.Include(u => u.Agencia).ToListAsync();
            return dados.Select(u => new UsuarioResponse { Id = u.Id, Nome = u.Nome, Email = u.Email, Nivel = u.Nivel, AgenciaId = u.AgenciaId, AgenciaNome = u.Agencia?.Nome });
        }

        public async Task<IEnumerable<UsuarioResponse>> ListarPorAgenciaAsync(int  agenciaId)
        {
            var dados = await _context.Usuarios.Include(u => u.Agencia).Where(u => u.AgenciaId ==  agenciaId).ToListAsync();
            return dados.Select(u => new UsuarioResponse { Id = u.Id, Nome = u.Nome, Email = u.Email, Nivel = u.Nivel, AgenciaId = u.AgenciaId, AgenciaNome = u.Agencia?.Nome });
        }
    }
}