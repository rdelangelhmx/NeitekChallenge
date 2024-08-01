namespace Client.Models;

public class MetasModel
{
    public int? MetaId { get; set; }
    public string? Nombre { get; set; }
    public DateTime? Creada { get; set; }
    public int? TotalTareas { get; set; }
    public decimal? Porcentaje { get; set; }
    public ICollection<TareasModel> Tareas { get; set; } = new List<TareasModel>();
}
