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
    public class PedidoService : IPedidoService
    {
        private readonly AppDbContext _context;

        public PedidoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PedidoResponse> CriarPedidoAsync(PedidoRequest request)
        {
            var pedido = new Pedido
            {
                AgenciaOrigemId = request.AgenciaOrigemId,
                AgenciaDestinoId = request.AgenciaDestinoId,
                SolicitanteId = request.SolicitanteId,
                Status = "PENDENTE",
                DataCriacao = DateTime.UtcNow,
                ItensPedido = request.Itens.Select(i => new ItemPedido { EquipamentoId = i.EquipamentoId, Quantidade = i.Quantidade }).ToList()
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return await ObterPorIdAsync(pedido.Id) ?? throw new Exception("Erro interno ao consolidar pedido.");
        }

        public async Task<PedidoResponse> AlterarStatusAsync(int pedidoId, string novoStatus, int usuarioId)
        {
            var pedido = await _context.Pedidos.Include(p => p.ItensPedido).ThenInclude(i => i.Equipamento).FirstOrDefaultAsync(p => p.Id == pedidoId) ?? throw new KeyNotFoundException("Pedido inexistente.");

            string atual = pedido.Status.ToUpper();
            string destino = novoStatus.ToUpper();

            if (atual == "PENDENTE" && destino == "ENVIADO")
            {
                foreach (var item in pedido.ItensPedido)
                {
                    var estoqueOrigem = await _context.Estoques.Where(e => e.AgenciaId == pedido.AgenciaOrigemId && e.EquipamentoId == item.EquipamentoId).ToListAsync();
                    if (estoqueOrigem.Sum(e => e.Quantidade) < item.Quantidade)
                        throw new InvalidOperationException($"Estoque indisponível na origem para suprir o item: {item.Equipamento.Nome}.");

                    int decremento = item.Quantidade;
                    foreach (var est in estoqueOrigem.OrderBy(e => e.NumeroSerie))
                    {
                        if (decremento <= 0) break;
                        if (est.Quantidade <= decremento) { decremento -= est.Quantidade; _context.Estoques.Remove(est); }
                        else { est.Quantidade -= decremento; decremento = 0; }
                    }
                }
            }
            else if (atual == "ENVIADO" && destino == "ENTREGUE")
            {
                foreach (var item in pedido.ItensPedido)
                {
                    var estDest = await _context.Estoques.FirstOrDefaultAsync(e => e.AgenciaId == pedido.AgenciaDestinoId && e.EquipamentoId == item.EquipamentoId && e.NumeroSerie == null);
                    if (estDest != null) estDest.Quantidade += item.Quantidade;
                    else _context.Estoques.Add(new Estoque { AgenciaId = pedido.AgenciaDestinoId, EquipamentoId = item.EquipamentoId, Quantidade = item.Quantidade, NumeroSerie = null });
                }
            }

            pedido.Status = destino;
            await _context.SaveChangesAsync();

            return await ObterPorIdAsync(pedido.Id) ?? throw new Exception("Erro ao mapear resposta.");
        }

        public async Task<PedidoResponse> ObterPorIdAsync(int id)
        {
            var p = await _context.Pedidos.Include(p => p.AgenciaOrigem).Include(p => p.AgenciaDestino).Include(p => p.Solicitante).Include(p => p.ItensPedido).ThenInclude(i => i.Equipamento).FirstOrDefaultAsync(p => p.Id == id);
            if (p == null) return null;

            return new PedidoResponse { Id = p.Id, AgenciaOrigemNome = p.AgenciaOrigem.Nome, AgenciaDestinoNome = p.AgenciaDestino.Nome, SolicitanteNome = p.Solicitante.Nome, Status = p.Status, DataCriacao = p.DataCriacao, Itens = p.ItensPedido.Select(i => new ItemPedidoResponse { EquipamentoNome = i.Equipamento.Nome, Modelo = i.Equipamento.Modelo, Quantidade = i.Quantidade }).ToList() };
        }

        public async Task<IEnumerable<PedidoResponse>> ListarPorAgenciaAsync(int  agenciaId)
        {
            var dados = await _context.Pedidos.Include(p => p.AgenciaOrigem).Include(p => p.AgenciaDestino).Include(p => p.Solicitante).Include(p => p.ItensPedido).ThenInclude(i => i.Equipamento).Where(p => p.AgenciaOrigemId ==  agenciaId || p.AgenciaDestinoId ==  agenciaId).ToListAsync();
            return dados.Select(p => new PedidoResponse { Id = p.Id, AgenciaOrigemNome = p.AgenciaOrigem.Nome, AgenciaDestinoNome = p.AgenciaDestino.Nome, SolicitanteNome = p.Solicitante.Nome, Status = p.Status, DataCriacao = p.DataCriacao, Itens = p.ItensPedido.Select(i => new ItemPedidoResponse { EquipamentoNome = i.Equipamento.Nome, Modelo = i.Equipamento.Modelo, Quantidade = i.Quantidade }).ToList() });
        }

        public async Task<IEnumerable<PedidoResponse>> ListarTodosGlobalAsync()
        {
            var dados = await _context.Pedidos.Include(p => p.AgenciaOrigem).Include(p => p.AgenciaDestino).Include(p => p.Solicitante).Include(p => p.ItensPedido).ThenInclude(i => i.Equipamento).ToListAsync();
            return dados.Select(p => new PedidoResponse { Id = p.Id, AgenciaOrigemNome = p.AgenciaOrigem.Nome, AgenciaDestinoNome = p.AgenciaDestino.Nome, SolicitanteNome = p.Solicitante.Nome, Status = p.Status, DataCriacao = p.DataCriacao, Itens = p.ItensPedido.Select(i => new ItemPedidoResponse { EquipamentoNome = i.Equipamento.Nome, Modelo = i.Equipamento.Modelo, Quantidade = i.Quantidade }).ToList() });
        }
    }
}