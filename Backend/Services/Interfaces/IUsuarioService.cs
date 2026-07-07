using Models.DTOs.Requests;
using Models.DTOs.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse> CadastrarAsync(UsuarioRequest request);
        Task<UsuarioResponse> LoginAsync(string email, string senha);
        Task<IEnumerable<UsuarioResponse>> ListarTodosAsync();
        Task<IEnumerable<UsuarioResponse>> ListarPorAgenciaAsync(int agenciaId);
    }
}