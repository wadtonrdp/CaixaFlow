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
    public class EquipamentoService : IEquipamentoService
    {
        private readonly AppDbContext _context;

        public EquipamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EquipamentoResponse> CadastrarAsync(EquipamentoRequest request)
        {
            var modeloDuplicado = await _context.Equipamentos.AnyAsync(e => e.Modelo.ToLower() == request.Modelo.ToLower() && e.Tipo.ToLower() == request.Tipo.ToLower());
            if (modeloDuplicado) throw new InvalidOperationException("Este modelo já foi registrado para esse respectivo tipo.");

            var eq = new Equipamento
            {
                Nome = request.Nome,
                Tipo = request.Tipo.ToUpper(),
                Modelo = request.Modelo,
                PossuiNumeroSerie = request.PossuiNumeroSerie,
                Especificacoes = request.Especificacoes
            };

            _context.Equipamentos.Add(eq);
            await _context.SaveChangesAsync();

            return new EquipamentoResponse { Id = eq.Id, Nome = eq.Nome, Tipo = eq.Tipo, Modelo = eq.Modelo, PossuiNumeroSerie = eq.PossuiNumeroSerie, Especificacoes = eq.Especificacoes };
        }

        public async Task<EquipamentoResponse> ObterPorIdAsync(int id)
        {
            var e = await _context.Equipamentos.FindAsync(id);
            if (e == null) return null;
            return new EquipamentoResponse { Id = e.Id, Nome = e.Nome, Tipo = e.Tipo, Modelo = e.Modelo, PossuiNumeroSerie = e.PossuiNumeroSerie, Especificacoes = e.Especificacoes };
        }

        public async Task<IEnumerable<EquipamentoResponse>> ListarTodosAsync()
        {
            var dados = await _context.Equipamentos.ToListAsync();
            return dados.Select(e => new EquipamentoResponse { Id = e.Id, Nome = e.Nome, Tipo = e.Tipo, Modelo = e.Modelo, PossuiNumeroSerie = e.PossuiNumeroSerie, Especificacoes = e.Especificacoes });
        }

        public async Task<IEnumerable<EquipamentoResponse>> ListarPorTipoAsync(string tipo)
        {
            var dados = await _context.Equipamentos.Where(e => e.Tipo.ToLower() == tipo.ToLower()).ToListAsync();
            return dados.Select(e => new EquipamentoResponse { Id = e.Id, Nome = e.Nome, Tipo = e.Tipo, Modelo = e.Modelo, PossuiNumeroSerie = e.PossuiNumeroSerie, Especificacoes = e.Especificacoes });
        }
    }
}