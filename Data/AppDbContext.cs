using System.Data;
using GestorCursosApi.Models;   

namespace GestorCursosApi.Data;

public class AppDbContext
{
    private static List<Curso> Cursos = new List<Curso>
    {
        new Curso
        {
            CursoId = 1,
            CodigoCurso = "CS101",
            NombreCurso = "Introducción a la Programación",
            Creditos = 3,
            HorasSemanales = 4,
            NivelAcademicoId = 1
        },
        new Curso
        {
            CursoId = 2,
            CodigoCurso = "CS102",
            NombreCurso = "Base de Datos",
            Creditos = 4,
            HorasSemanales = 5,
            NivelAcademicoId = 2
        },
         new Curso
        {
            CursoId = 3,
            CodigoCurso = "CS103",
            NombreCurso = "Desarrollo de Servicios Web",
            Creditos = 4,
            HorasSemanales = 4,
            NivelAcademicoId = 3
        },
         new Curso
        {
            CursoId = 4,
            CodigoCurso = "CS104",
            NombreCurso = "Aplicaciones Móviles",
            Creditos = 3,
            HorasSemanales = 5,
            NivelAcademicoId = 1
        },
        new Curso
        {
            CursoId = 5,
            CodigoCurso = "CS105",
            NombreCurso = "Seguridad Informática",
            Creditos = 3,
            HorasSemanales = 4,
            NivelAcademicoId = 2
        }
    };
    private static int nextId = 6;

    //logica repositorio
    public List<Curso> GetCursos()
    {
        return Cursos;
    }

    public Curso? GetCursoById(int id)
    {
        return Cursos.FirstOrDefault(c => c.CursoId == id);
    }
    public Curso Create(Curso curso)
    {
        curso.CursoId = nextId++;
        Cursos.Add(curso);
        return curso;
    }
    public bool Update(Curso curso)
    {
        var existingCurso = GetCursoById(curso.CursoId);
        if (existingCurso == null)
        {
            return false;
        }
        existingCurso.CodigoCurso = curso.CodigoCurso;
        existingCurso.NombreCurso = curso.NombreCurso;
        existingCurso.Creditos = curso.Creditos;
        existingCurso.HorasSemanales = curso.HorasSemanales;
        existingCurso.NivelAcademicoId = curso.NivelAcademicoId;
        return true;
    }  
    public bool Delete(int id)
    {
        var curso = GetCursoById(id);
        if (curso == null)
        {
            return false;
        }
        Cursos.Remove(curso);
        return true;
    }
    public List<Curso> GetCursosByNivelAcademicoId(int nivelAcademicoId)
    {
        return Cursos.Where(c => c.NivelAcademicoId == nivelAcademicoId).ToList();
    }
    public List<Curso> GetCursosByCreditos(int creditos)
    {
        return Cursos.Where(c => c.Creditos == creditos).ToList();
    }
    public List<Curso> GetCursosByNombre(string nombre)
    {
        return Cursos.Where(c => c.NombreCurso.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();
    } 
    public List<Curso> GetCursosByHorasSemanales(int horasSemanales)
    {
        return Cursos.Where(c => c.HorasSemanales == horasSemanales).ToList();
    }
}


