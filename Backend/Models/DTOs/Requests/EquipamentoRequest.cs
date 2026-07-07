using System.ComponentModel.DataAnnotations;

namespace Models.DTOs.Requests
{
    public class EquipamentoRequest
    {
        [Required(ErrorMessage = "Nome do equipamento é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo é obrigatório.")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Modelo é obrigatório.")]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        public bool PossuiNumeroSerie { get; set; }

        public string Especificacoes { get; set; } // String contendo JSON válido
    }
}