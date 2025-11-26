using System.ComponentModel.DataAnnotations;
using GestorNivelAcademicoApi.Models;

namespace GestorCursosApi.Models;

public class Curso
{
    public int CursoId { get; set; }
     [Required(ErrorMessage = "El Codigo es obligatorio")]
    
    public string CodigoCurso { get; set; } = string.Empty;
    public string NombreCurso { get; set; } = string.Empty;
    public int Creditos { get; set; }
    public int HorasSemanales { get; set; }
    public int NivelAcademicoId { get; set; }
    
    // Navegación
    public NivelAcademico? NivelAcademico { get; set; }
}