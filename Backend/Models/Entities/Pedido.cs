using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AgenciaOrigemId { get; set; }
        public Agencia AgenciaOrigem { get; set; } = null!;

        [Required]
        public int AgenciaDestinoId { get; set; }
        public Agencia AgenciaDestino { get; set; } = null!;

        [Required]
        public int SolicitanteId { get; set; }
        public Usuario Solicitante { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string Status { get; set; } = "PENDENTE"; // PENDENTE, ENVIADO, ENTREGUE

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public List<ItemPedido> ItensPedido { get; set; } = new();
    }
}