namespace Models.DTOs.Responses
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
        public int? AgenciaId { get; set; }
        public string AgenciaNome { get; set; }
    }
}