namespace Models.DTOs.Responses
{
    public class EstoqueResponse
    {
        public int Id { get; set; }
        public int AgenciaId { get; set; }
        public string AgenciaNome { get; set; } = string.Empty;
        public int EquipamentoId { get; set; }
        public string EquipamentoNome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string NumeroSerie { get; set; }
        public int  Quantidade { get; set; }
    }
}