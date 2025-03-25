using Microsoft.EntityFrameworkCore;
using GestorEventos.Application.DTOs;
using AutoMapper;
using GestorEventos.Domain.Entities;
using GestorEventos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;


namespace GestorEventos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HorarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly GestorDBcontext _context;

        public HorarioController(IMapper mapper, GestorDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearHorario(HorarioDTO horarioDTO)
        {
            var eventoExiste = await _context.Eventos.AnyAsync(e => e.Id == horarioDTO.EventoId);
            if (!eventoExiste)
            {
                return BadRequest("El evento especificado no existe.");
            }

            var salaExiste = await _context.Salas.AnyAsync(s => s.Id == horarioDTO.SalaId);
            if (!salaExiste)
            {
                return BadRequest("La sala especificada no existe.");
            }

            var horario = new Horario
            {
                FechaHoraInicio = horarioDTO.FechaHoraInicio,
                FechaHoraFin = horarioDTO.FechaHoraFin,
                EventoId = horarioDTO.EventoId,
                SalaId = horarioDTO.SalaId
            };

            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CrearHorario), new { id = horario.Id }, horario);
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioDTO>>> GetHorarios()
        {
            var horarios = await _context.Horarios.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<HorarioDTO>>(horarios));
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioDTO>> GetHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);

            if (horario == null)
            {
                return NotFound($"No se encontró un horario con ID {id}.");
            }

            return Ok(_mapper.Map<HorarioDTO>(horario));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, HorarioDTO horarioDTO)
        {
            if (horarioDTO == null || id != horarioDTO.Id)
            {
                return BadRequest("El ID proporcionado no coincide con el ID del horario o los datos son inválidos.");
            }

            var horarioExistente = await _context.Horarios.FindAsync(id);
            if (horarioExistente == null)
            {
                return NotFound($"No se encontró un horario con ID {id}.");
            }

            
            _mapper.Map(horarioDTO, horarioExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Otro usuario ha modificado este horario. Refresque los datos e intente nuevamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }

            return NoContent(); 
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);

            if (horario == null)
            {
                return NotFound($"No se encontró un horario con ID {id}.");
            }

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }


    }
}
