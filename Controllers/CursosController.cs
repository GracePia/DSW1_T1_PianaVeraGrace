using Microsoft.AspNetCore.Mvc;
using GestorCursosApi.Data;
using GestorCursosApi.Models;

namespace GestorCursosApi.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : ControllerBase
    {
        private readonly CursoRepository _repository;

        public CursosController(CursoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/cursos?nivelId=1
        [HttpGet]
        public ActionResult<List<Curso>> ObtenerCursos([FromQuery] int? nivelId)
        {
            if (nivelId.HasValue)
            {
                return Ok(_repository.FiltrarPorNivelAcademicoId(nivelId.Value));
            }

            return Ok(_repository.ObtenerCursos());
        }

        // GET api/cursos/5
        [HttpGet("{id}")]
        public ActionResult<Curso> ObtenerPorId(int id)
        {
            var curso = _repository.ObtenerPorId(id);

            return curso == null
                ? NotFound(new { message = $"Curso {id} no encontrado" })
                : Ok(curso);
        }

        // POST
        [HttpPost]
        public ActionResult<Curso> Crear([FromBody] Curso curso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creado = _repository.Crear(curso);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = creado.CursoId },
                creado
            );
        }

        // PUT api/cursos/5
        [HttpPut("{id}")]
        public ActionResult Actualizar(int id, [FromBody] Curso curso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actualizado = _repository.Actualizar(id, curso);

            if (!actualizado)
                return NotFound(new { message = $"Curso con ID {id} no encontrado" });

            return NoContent();
        }

        // DELETE api/cursos/5
        [HttpDelete("{id}")]
        public ActionResult Eliminar(int id)
        {
            var eliminado = _repository.Eliminar(id);

            if (!eliminado)
                return NotFound(new { message = $"Curso con ID {id} no encontrado" });

            return NoContent();
        }
    }
