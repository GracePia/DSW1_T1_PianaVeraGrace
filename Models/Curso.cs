using System.ComponentModel.DataAnnotations;
using GestorNivelAcademicoApi.Models;

namespace GestorCursosApi.Models;

public class Curso

{
    public int CursoId { get; set; }

        [Required(ErrorMessage = "El Código es obligatorio")]
        [StringLength(7, ErrorMessage = "El Código no debe superar los 7 caracteres")]
        public string CodigoCurso { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string NombreCurso { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los créditos son obligatorios")]
        public int Creditos { get; set; }

        [Required(ErrorMessage = "Las horas semanales son obligatorias")]
        public int HorasSemanales { get; set; }

        [Required(ErrorMessage = "El Nivel Académico es obligatorio")]
        public int NivelAcademicoId { get; set; }

    
    // Navegación
    public NivelAcademico? NivelAcademico { get; set; }
}