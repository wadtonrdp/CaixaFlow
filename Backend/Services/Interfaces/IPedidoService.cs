using Models.DTOs.Requests;
using Models.DTOs.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPedidoService
    {
        Task<PedidoResponse> CriarPedidoAsync(PedidoRequest request);
        Task<PedidoResponse> AlterarStatusAsync(int pedidoId, string novoStatus, int usuarioId);
        Task<PedidoResponse> ObterPorIdAsync(int id);
        Task<IEnumerable<PedidoResponse>> ListarPorAgenciaAsync(int  agenciaId);
        Task<IEnumerable<PedidoResponse>> ListarTodosGlobalAsync();
    }
}