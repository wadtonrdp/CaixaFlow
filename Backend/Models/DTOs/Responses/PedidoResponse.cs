using System;
using System.Collections.Generic;

namespace Models.DTOs.Responses
{
    public class PedidoResponse
    {
        public int Id { get; set; }
        public string AgenciaOrigemNome { get; set; } = string.Empty;
        public string AgenciaDestinoNome { get; set; } = string.Empty;
        public string SolicitanteNome { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public List<ItemPedidoResponse> Itens { get; set; } = new();
    }

    public class ItemPedidoResponse
    {
        public string EquipamentoNome { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Quantidade { get; set; }
    }
}