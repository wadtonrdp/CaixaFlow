namespace Models.DTOs.Responses
{
    public class EquipamentoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public bool PossuiNumeroSerie { get; set; }
        public string Especificacoes { get; set; }
    }
}