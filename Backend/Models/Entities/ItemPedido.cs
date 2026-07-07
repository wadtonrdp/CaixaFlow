using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class ItemPedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PedidoId { get; set; }

        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; } = null!;

        [Required]
        public int EquipamentoId { get; set; }

        [ForeignKey("EquipamentoId")]
        public Equipamento Equipamento { get; set; } = null!;

        [Required]
        public int Quantidade { get; set; }
    }
}