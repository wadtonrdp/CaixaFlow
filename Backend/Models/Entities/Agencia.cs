using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Agencia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Codigo { get; set; } = string.Empty; // Ex: AG-001, SEDE-01
    }
}