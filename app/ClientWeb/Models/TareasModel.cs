namespace Client.Models;

public class TareasModel
{
    public int? TareaId { get; set; }
    public int? MetaId { get; set; }
    public string? Nombre { get; set; }
    public DateTime? Creada { get; set; }
    public bool? Estado { get; set; }
    public bool? Favorita { get; set; }
}
