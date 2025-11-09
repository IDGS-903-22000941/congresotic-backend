namespace CongresoTicAPI.Models
{
    public class Participante
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Twitter { get; set; } = string.Empty;
        public string Ocupacion { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}