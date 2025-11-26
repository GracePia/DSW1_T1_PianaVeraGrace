using Microsoft.EntityFrameworkCore;
using GestorCursosApi.Models;
using GestorNivelAcademicoApi.Models;

namespace GestorCursosApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<NivelAcademico> NivelesAcademicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la relación uno a muchos
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.NivelAcademico)
                .WithMany(n => n.Cursos)
                .HasForeignKey(c => c.NivelAcademicoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuraciones adicionales
            modelBuilder.Entity<Curso>()
                .Property(c => c.CodigoCurso)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Curso>()
                .Property(c => c.NombreCurso)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<NivelAcademico>()
                .Property(n => n.Descripcion)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}