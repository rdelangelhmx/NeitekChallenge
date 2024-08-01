#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Entities;

[Table("tblMetas")]
public partial class TblMetas
{
    [Key]
    public int MetaId { get; set; }

    [Required]
    [StringLength(80)]
    [Unicode(false)]
    public string Nombre { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Creada { get; set; }

    public int TotalTareas { get; set; }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal Porcentaje { get; set; }

    [InverseProperty("Meta")]
    public virtual ICollection<TblTareas> TblTareas { get; set; } = new List<TblTareas>();
}