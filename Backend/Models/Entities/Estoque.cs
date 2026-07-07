using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Estoque
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AgenciaId { get; set; }

        [ForeignKey("AgenciaId")]
        public Agencia Agencia { get; set; } = null!;

        [Required]
        public int EquipamentoId { get; set; }

        [ForeignKey("EquipamentoId")]
        public Equipamento Equipamento { get; set; } = null!;

        [StringLength(100)]
        public string NumeroSerie { get; set; } // Nulo para itens em lote

        [Required]
        public int Quantidade { get; set; }
    }
}