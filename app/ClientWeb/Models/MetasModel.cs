using System.Text.Json.Serialization;

namespace Client.Models;

public class MetasModel
{
    public int? MetaId { get; set; }
    public string? Nombre { get; set; }
    public DateTime? Creada { get; set; }
    public int? TotalTareas { get; set; }
    public decimal? Porcentaje { get; set; }
    [JsonPropertyName("tblTareas")]
    public ICollection<TareasModel> Tareas { get; set; } = new List<TareasModel>();
}
