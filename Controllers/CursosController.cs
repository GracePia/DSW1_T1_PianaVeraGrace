using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestorCursosApi.Models;
using GestorCursosApi.Data; 

[Route("api/[controller]")]
[ApiController]
public class CursosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    // 👉 1. Inyección del DbContext en el constructor
    public CursosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 👉 2. Método solicitado en el examen
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
        {
            var cursos = await _context.Cursos
                .Include(c => c.NivelAcademico)
                .ToListAsync();

            return Ok(cursos);
        }

        //Listar cursos por nivel académico
        [HttpGet("por-nivel/{nivelId}")]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursosPorNivel(int nivelId)
        {
            var cursos = await _context.Cursos
                .Where(c => c.NivelAcademicoId == nivelId)
                .Include(c => c.NivelAcademico)
                .ToListAsync();

            if (cursos.Count == 0)
                return NotFound("No hay cursos para ese nivel académico.");

            return Ok(cursos);
        }

        //Crear un nuevo curso

         [HttpPost]
        public async Task<ActionResult> CrearCurso(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCursos), new { id = curso.CursoId }, curso);
        }
        
        //Actualizar un curso existente
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarCurso(int id, Curso curso)
        {
            if (id != curso.CursoId)
                return BadRequest("El ID del curso no coincide.");

            _context.Entry(curso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cursos.Any(e => e.CursoId == id))
                    return NotFound("Curso no encontrado.");

                throw;
            }

            return NoContent();
        }
    }




}
