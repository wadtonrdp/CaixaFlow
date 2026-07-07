using System.ComponentModel.DataAnnotations;

namespace Models.DTOs.Requests
{
    public class UsuarioRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres.")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nível de acesso é obrigatório.")]
        public string Nivel { get; set; } = string.Empty; // ANALISTA, REGIONAL, GERENTE

        public int? AgenciaId { get; set; }
    }
}