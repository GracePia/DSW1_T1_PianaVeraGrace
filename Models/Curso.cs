using System.ComponentModel.DataAnnotations;
using GestorNivelAcademicoApi.Models;

namespace GestorCursosApi.Models;

public class Curso
{
    public int CursoId { get; set; }
     [Required(ErrorMessage = "El Codigo es obligatorio")]
     [StringLength(7, ErrorMessage = "El codigo no debe superar los 7 cartacteres")]
        public string CodigoCurso { get; set; } = string.Empty;
     [Required(ErrorMessage = "El Campo es obligatorio")]
    public string NombreCurso { get; set; } = string.Empty;
     [Required(ErrorMessage = "El Campo es obligatorio")]
    public int Creditos { get; set; }
    [Required(ErrorMessage = "El Campo es obligatorio")]
    public int HorasSemanales { get; set; }
     [Required(ErrorMessage = "El Campo es obligatorio")]
    public int NivelAcademicoId { get; set; }
    
    // Navegación
    public NivelAcademico? NivelAcademico { get; set; }
}