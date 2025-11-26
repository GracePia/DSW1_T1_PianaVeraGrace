using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestorCursosApi.Models;
using GestorCursosApi.Data; 

namespace GestorCursosApi.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class CursosController : ControllerBase
{
    private readonly AppDbContext _context;

    // 👉 1. Inyección del DbContext en el constructor
    public CursosController(AppDbContext context)
    {
        _context = context;
    }

    // 👉 2. Método solicitado en el examen
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Curso>>> ObetenerCursos()
        {
            var cursos = await _context.Cursos
                .Include(c => c.NivelAcademico)
                .ToListAsync();

            return Ok(cursos);
        }

        //Listar cursos por nivel académico
         [HttpGet("nivel/{nivelAcademicoId}")]
        public async Task<ActionResult<IEnumerable<Curso>>> ListarCursosPorNivel(int nivelAcademicoId)
        {
            var cursos = await _context.Cursos
                .Include(c => c.NivelAcademico)
                .Where(c => c.NivelAcademicoId == nivelAcademicoId)
                .ToListAsync();

            if (!cursos.Any())
            {
                return NotFound($"No se encontraron cursos para el nivel académico {nivelAcademicoId}");
            }

            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.NivelAcademico)
                .FirstOrDefaultAsync(c => c.CursoId == id);

            if (curso == null)
            {
                return NotFound($"Curso con ID {id} no encontrado");
            }

            return Ok(curso);
        }

        //Crear un nuevo curso

         [HttpPost]
        public async Task<ActionResult<Curso>> CrearCurso(Curso curso)
        {
            // Validar que el nivel académico existe
            var nivelExiste = await _context.NivelesAcademicos
                .AnyAsync(n => n.NivelAcademicoId == curso.NivelAcademicoId);

            if (!nivelExiste)
            {
                return BadRequest("El nivel académico especificado no existe");
            }

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCurso), 
                new { id = curso.CursoId }, 
                curso);
        }

        //Actualizar un curso existente
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCurso(int id, Curso curso)
        {
            if (id != curso.CursoId)
            {
                return BadRequest("El ID del curso no coincide");
            }

            // Verificar que el curso existe
            var cursoExistente = await _context.Cursos.FindAsync(id);
            if (cursoExistente == null)
            {
                return NotFound($"Curso con ID {id} no encontrado");
            }

            // Validar que el nivel académico existe
            var nivelExiste = await _context.NivelesAcademicos
                .AnyAsync(n => n.NivelAcademicoId == curso.NivelAcademicoId);

            if (!nivelExiste)
            {
                return BadRequest("El nivel académico especificado no existe");
            }

            // Actualizar propiedades
            cursoExistente.CodigoCurso = curso.CodigoCurso;
            cursoExistente.NombreCurso = curso.NombreCurso;
            cursoExistente.Creditos = curso.Creditos;
            cursoExistente.HorasSemanales = curso.HorasSemanales;
            cursoExistente.NivelAcademicoId = curso.NivelAcademicoId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error al actualizar el curso");
            }

            return NoContent();
        }

         [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            
            if (curso == null)
            {
                return NotFound($"Curso con ID {id} no encontrado");
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}



