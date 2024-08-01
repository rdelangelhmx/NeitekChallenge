using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.Persistence;

public partial class NeitekContext : DbContext
{
     public NeitekContext(DbContextOptions<NeitekContext> options) : base(options) { }

    public virtual DbSet<TblMetas> TblMetas { get; set; }
    public virtual DbSet<TblTareas> TblTareas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblMetas>(entity =>
        {
            entity.Property(e => e.Creada).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblTareas>(entity =>
        {
            entity.Property(e => e.Creada).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Meta).WithMany(p => p.TblTareas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblTareas_tblMetas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
