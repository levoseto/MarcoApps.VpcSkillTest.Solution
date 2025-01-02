namespace MarcoApps.VpcSkillTest.Services.Api.Data
{
    using MarcoApps.VpcSkillTest.Services.Api.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets para las tablas
        public DbSet<Taller> Talleres { get; set; }

        public DbSet<Mecanico> Mecanicos { get; set; }
        public DbSet<Refaccion> Refacciones { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<Instalacion> Instalaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración adicional
            modelBuilder.Entity<Mecanico>()
                .HasOne(m => m.Taller)
                .WithMany()
                .HasForeignKey(m => m.TallerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inventario>()
                .HasOne(i => i.Taller)
                .WithMany()
                .HasForeignKey(i => i.TallerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inventario>()
                .HasOne(i => i.Refaccion)
                .WithMany()
                .HasForeignKey(i => i.RefaccionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Solicitud>()
                .HasOne(s => s.TallerSolicitante)
                .WithMany()
                .HasForeignKey(s => s.TallerSolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Solicitud>()
                .HasOne(s => s.TallerProveedor)
                .WithMany()
                .HasForeignKey(s => s.TallerProveedorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Solicitud>()
                .HasOne(s => s.MecanicoSolicitante)
                .WithMany()
                .HasForeignKey(s => s.MecanicoSolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Solicitud)
                .WithMany()
                .HasForeignKey(e => e.SolicitudId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Envio>()
                .HasOne(e => e.MecanicoEnvia)
                .WithMany()
                .HasForeignKey(e => e.MecanicoEnviaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}