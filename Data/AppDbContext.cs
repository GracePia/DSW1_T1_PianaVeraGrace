using Microsoft.EntityFrameworkCore;
using GestorCursosApi.Models;
using GestorNivelAcademicoApi.Models;

namespace GestorCursosApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }  
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<NivelAcademico> NivelAcademicos { get; set; }
    }
}