using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOs.Requests
{
    public class PedidoRequest
    {
        [Required]
        public int AgenciaOrigemId { get; set; }

        [Required]
        public int AgenciaDestinoId { get; set; }

        [Required]
        public int SolicitanteId { get; set; }

        [Required]
        public List<ItemPedidoInputDto> Itens { get; set; } = new();
    }

    public class ItemPedidoInputDto
    {
        [Required]
        public int EquipamentoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade mínima de itens é 1.")]
        public int Quantidade { get; set; }
    }
}