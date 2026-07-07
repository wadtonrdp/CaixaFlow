using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Equipamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty; // MAQUINA, CHIP, BOBINA

        [Required]
        [StringLength(50)]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        public bool PossuiNumeroSerie { get; set; }

        // Mapeado no DbContext para virar JSONB nativo no banco
        public string Especificacoes { get; set; } 
    }
}