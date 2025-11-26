using System.Data;
using GestorCursosApi.Models;

namespace GestorCursosApi.Data;

public class CursoRepository
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
    public List<Curso> ObtenerCursos()
    {
        return Cursos;
    }

    public Curso? ObtenerPorId(int id)
    {
        return Cursos.FirstOrDefault(c => c.CursoId == id);
    }
    public Curso Crear(Curso curso)
    {
        curso.CursoId = nextId++;
        Cursos.Add(curso);
        return curso;
    }
    public bool Actualizar(int id, Curso cursoActualizado)
    {
        var existingCurso = ObtenerPorId(id);
        if (existingCurso == null)
        {
            return false;
        }
        existingCurso.CodigoCurso = cursoActualizado.CodigoCurso;
        existingCurso.NombreCurso = cursoActualizado.NombreCurso;
        existingCurso.Creditos = cursoActualizado.Creditos;
        existingCurso.HorasSemanales = cursoActualizado.HorasSemanales;
        existingCurso.NivelAcademicoId = cursoActualizado.NivelAcademicoId;
        return true;
    }  
    public bool Eliminar(int id)
    {
        var curso = ObtenerPorId(id);
        if (curso == null)
        {
            return false;
        }
        Cursos.Remove(curso);
        return true;
    }
    public List<Curso> FiltrarPorNivelAcademicoId(int nivelAcademicoId)
    {
        return Cursos.Where(c => c.NivelAcademicoId == nivelAcademicoId).ToList();
    }
    public List<Curso> FiltrarPorCreditos(int creditos)
    {
        return Cursos.Where(c => c.Creditos == creditos).ToList();
    }
}