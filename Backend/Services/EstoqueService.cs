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
    public class EstoqueService : IEstoqueService
    {
        private readonly AppDbContext _context;

        public EstoqueService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EstoqueResponse> EntradaEstoqueAsync(EstoqueInputRequest request)
        {
            var equipamento = await _context.Equipamentos.FindAsync(request.EquipamentoId) ?? throw new KeyNotFoundException("Equipamento inválido.");
            var agencia = await _context.Agencias.FindAsync(request.AgenciaId) ?? throw new KeyNotFoundException("Agência inválida.");

            if (equipamento.PossuiNumeroSerie)
            {
                if (string.IsNullOrWhiteSpace(request.NumeroSerie)) throw new InvalidOperationException("Itens seriados necessitam de número de série único.");
                if (request.Quantidade != 1) throw new InvalidOperationException("Inserções seriadas devem possuir quantidade fixa = 1.");

                var serieExiste = await _context.Estoques.AnyAsync(e => e.NumeroSerie == request.NumeroSerie);
                if (serieExiste) throw new InvalidOperationException("Este número de série já se encontra ativo no sistema.");
            }

            Estoque est;

            if (equipamento.PossuiNumeroSerie)
            {
                est = new Estoque { AgenciaId = request.AgenciaId, EquipamentoId = request.EquipamentoId, NumeroSerie = request.NumeroSerie, Quantidade = 1 };
                _context.Estoques.Add(est);
            }
            else
            {
                var estExistente = await _context.Estoques.FirstOrDefaultAsync(e => e.AgenciaId == request.AgenciaId && e.EquipamentoId == request.EquipamentoId && e.NumeroSerie == null);
                if (estExistente != null)
                {
                    estExistente.Quantidade += request.Quantidade;
                    est = estExistente;
                }
                else
                {
                    est = new Estoque { AgenciaId = request.AgenciaId, EquipamentoId = request.EquipamentoId, NumeroSerie = null, Quantidade = request.Quantidade };
                    _context.Estoques.Add(est);
                }
            }

            await _context.SaveChangesAsync();
            return new EstoqueResponse { Id = est.Id, AgenciaId = est.AgenciaId, AgenciaNome = agencia.Nome, EquipamentoId = est.EquipamentoId, EquipamentoNome = equipamento.Nome, Tipo = equipamento.Tipo, Modelo = equipamento.Modelo, NumeroSerie = est.NumeroSerie, Quantidade = est.Quantidade };
        }

        public async Task<IEnumerable<EstoqueResponse>> ListarEstoquePorAgenciaAsync(int  agenciaId)
        {
            var res = await _context.Estoques.Include(e => e.Agencia).Include(e => e.Equipamento).Where(e => e.AgenciaId ==  agenciaId).ToListAsync();
            return res.Select(e => new EstoqueResponse { Id = e.Id, AgenciaId = e.AgenciaId, AgenciaNome = e.Agencia.Nome, EquipamentoId = e.EquipamentoId, EquipamentoNome = e.Equipamento.Nome, Tipo = e.Equipamento.Tipo, Modelo = e.Equipamento.Modelo, NumeroSerie = e.NumeroSerie, Quantidade = e.Quantidade });
        }

        public async Task<IEnumerable<EstoqueResponse>> ListarEstoqueGlobalAsync()
        {
            var res = await _context.Estoques.Include(e => e.Agencia).Include(e => e.Equipamento).ToListAsync();
            return res.Select(e => new EstoqueResponse { Id = e.Id, AgenciaId = e.AgenciaId, AgenciaNome = e.Agencia.Nome, EquipamentoId = e.EquipamentoId, EquipamentoNome = e.Equipamento.Nome, Tipo = e.Equipamento.Tipo, Modelo = e.Equipamento.Modelo, NumeroSerie = e.NumeroSerie, Quantidade = e.Quantidade });
        }
    }
}