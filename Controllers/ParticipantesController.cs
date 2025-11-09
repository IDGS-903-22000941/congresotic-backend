using CongresoTicAPI.Data;
using CongresoTicAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CongresoTicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParticipantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Participantes/listado
        // GET: api/Participantes/listado?q=carlos
        [HttpGet("listado")]
        public async Task<ActionResult<IEnumerable<Participante>>> GetListado([FromQuery] string? q)
        {
            try
            {
                if (string.IsNullOrEmpty(q))
                {
                    // Devolver todos los participantes
                    return await _context.Participantes.ToListAsync();
                }
                else
                {
                    // Buscar por nombre o apellidos
                    var query = q.ToLower();
                    var resultados = await _context.Participantes
                        .Where(p => p.Nombre.ToLower().Contains(query) ||
                                    p.Apellidos.ToLower().Contains(query))
                        .ToListAsync();
                    return resultados;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al obtener participantes", detalle = ex.Message });
            }
        }

        // GET: api/Participantes/participante/5
        [HttpGet("participante/{id}")]
        public async Task<ActionResult<Participante>> GetParticipante(int id)
        {
            try
            {
                var participante = await _context.Participantes.FindAsync(id);

                if (participante == null)
                {
                    return NotFound(new { error = "Participante no encontrado" });
                }

                return participante;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al obtener participante", detalle = ex.Message });
            }
        }

        // POST: api/Participantes/registro
        [HttpPost("registro")]
        public async Task<ActionResult<Participante>> PostParticipante(Participante participante)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrEmpty(participante.Nombre) ||
                    string.IsNullOrEmpty(participante.Apellidos) ||
                    string.IsNullOrEmpty(participante.Email) ||
                    string.IsNullOrEmpty(participante.Twitter) ||
                    string.IsNullOrEmpty(participante.Ocupacion))
                {
                    return BadRequest(new { error = "Faltan datos requeridos" });
                }

                participante.FechaRegistro = DateTime.Now;

                _context.Participantes.Add(participante);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetParticipante), new { id = participante.Id }, participante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al registrar participante", detalle = ex.Message });
            }
        }
    }
}