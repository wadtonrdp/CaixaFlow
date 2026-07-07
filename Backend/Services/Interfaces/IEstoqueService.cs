using Models.DTOs.Requests;
using Models.DTOs.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEstoqueService
    {
        Task<EstoqueResponse> EntradaEstoqueAsync(EstoqueInputRequest request);
        Task<IEnumerable<EstoqueResponse>> ListarEstoquePorAgenciaAsync(int agenciaId);
        Task<IEnumerable<EstoqueResponse>> ListarEstoqueGlobalAsync();
    }
}