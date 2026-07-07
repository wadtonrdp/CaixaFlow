using Models.DTOs.Requests;
using Models.DTOs.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEquipamentoService
    {
        Task<EquipamentoResponse> CadastrarAsync(EquipamentoRequest request);
        Task<EquipamentoResponse> ObterPorIdAsync(int id);
        Task<IEnumerable<EquipamentoResponse>> ListarTodosAsync();
        Task<IEnumerable<EquipamentoResponse>> ListarPorTipoAsync(string tipo);
    }
}