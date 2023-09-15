using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DddoTest.Models;

public partial class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MonedaDdo> MonedaDdos { get; set; }

    public virtual DbSet<SucursalesDdo> SucursalesDdos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MonedaDdo>(entity =>
        {
            entity.HasKey(e => e.IdMoneda).HasName("PK__Moneda_D__AA69067100A9203E");

            entity.ToTable("Moneda_Ddo");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Simbolo)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SucursalesDdo>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__BFB6CD99536CC2A3");

            entity.ToTable("Sucursales_Ddo");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMonedaNavigation).WithMany(p => p.SucursalesDdos)
                .HasForeignKey(d => d.IdMoneda)
                .HasConstraintName("FK__Sucursale__IdMon__6B0FDBE9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
