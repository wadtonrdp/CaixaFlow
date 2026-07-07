using System.ComponentModel.DataAnnotations;

namespace Models.DTOs.Requests
{
    public class EstoqueInputRequest
    {
        [Required]
        public int AgenciaId { get; set; }

        [Required]
        public int EquipamentoId { get; set; }

        public string NumeroSerie { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior do que zero.")]
        public int Quantidade { get; set; }
    }
}