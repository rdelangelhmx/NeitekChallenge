#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Entities;

[Table("tblTareas")]
public partial class TblTareas
{
    [Key]
    public int TareaId { get; set; }

    public int MetaId { get; set; }

    [Required]
    [StringLength(80)]
    [Unicode(false)]
    public string Nombre { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Creada { get; set; }

    public bool Estado { get; set; }

    public bool Favorita { get; set; }

    [ForeignKey("MetaId")]
    [InverseProperty("TblTareas")]
    public virtual TblMetas Meta { get; set; }
}