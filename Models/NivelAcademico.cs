using GestorCursosApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestorNivelAcademicoApi.Models;

public class NivelAcademico
{
public int NivelAcademicoId { get; set; }
    public string? Descripcion { get; set; } = string.Empty;
    public int Orden { get; set; }
    
    // Propiedad de navegación - Relación uno a muchos
     [JsonIgnore] 
    public ICollection<Curso>? Cursos { get; set; }
}
   
