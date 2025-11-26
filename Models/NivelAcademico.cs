using GestorCursosApi.Models;
using System.ComponentModel.DataAnnotations;

namespace GestorNivelAcademicoApi.Models;

public class NivelAcademico
{
public int NivelAcademicoId { get; set; }
    public string? Descripcion { get; set; } = string.Empty;
    public int Orden { get; set; }
    
    // Propiedad de navegación - Relación uno a muchos
    public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
    // Navegación
   
}