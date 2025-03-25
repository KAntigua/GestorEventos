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

    public class NotifiacionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly GestorDBcontext _context;

        public NotifiacionController(IMapper mapper, GestorDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearNotificacion([FromBody] NotificacionDTO notificacionDTO)
        {
            if (notificacionDTO == null)
            {
                return BadRequest("Los datos de la notificación no pueden ser nulos.");
            }

            try
            {
                
                var participante = await _context.Participantes
                    .FirstOrDefaultAsync(p => p.Id == notificacionDTO.ParticipanteId);

                if (participante == null)
                {
                    return NotFound($"No se encontró el participante con el ID {notificacionDTO.ParticipanteId}.");
                }

                
                var notificacion = new Notificacion
                {
                    Mensaje = notificacionDTO.Mensaje,
                    FechaEnvio = notificacionDTO.FechaEnvio,
                    ParticipanteId = notificacionDTO.ParticipanteId
                };

                _context.Notificaciones.Add(notificacion);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetNotificacionById), new { id = notificacion.Id }, notificacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la notificación: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificacionById(int id)
        {
            var notificacion = await _context.Notificaciones
                .FirstOrDefaultAsync(n => n.Id == id);

            if (notificacion == null)
            {
                return NotFound($"No se encontró la notificación con el ID {id}.");
            }

            var notificacionDTO = _mapper.Map<NotificacionDTO>(notificacion);

            return Ok(notificacionDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotificaciones()
        {
            var notificaciones = await _context.Notificaciones.ToListAsync();
            var notificacionesDTO = _mapper.Map<List<NotificacionDTO>>(notificaciones);

            return Ok(notificacionesDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificacion(int id, [FromBody] NotificacionDTO notificacionDTO)
        {
            if (notificacionDTO == null)
            {
                return BadRequest("Los datos de la notificación no pueden ser nulos.");
            }

            try
            {
                
                var notificacion = await _context.Notificaciones.FirstOrDefaultAsync(n => n.Id == id);
                if (notificacion == null)
                {
                    return NotFound($"No se encontró la notificación con el ID {id}.");
                }

                var participante = await _context.Participantes
                    .FirstOrDefaultAsync(p => p.Id == notificacionDTO.ParticipanteId);

                if (participante == null)
                {
                    return NotFound($"No se encontró un participante con el ID {notificacionDTO.ParticipanteId}.");
                }

                
                notificacion.Mensaje = notificacionDTO.Mensaje;
                notificacion.FechaEnvio = notificacionDTO.FechaEnvio;
                notificacion.ParticipanteId = notificacionDTO.ParticipanteId;

               
                _context.Notificaciones.Update(notificacion);
                await _context.SaveChangesAsync();

                
                var notificacionActualizadaDTO = _mapper.Map<NotificacionDTO>(notificacion);

                return Ok(notificacionActualizadaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la notificación: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacion(int id)
        {
            try
            {
                
                var notificacion = await _context.Notificaciones.FirstOrDefaultAsync(n => n.Id == id);
                if (notificacion == null)
                {
                    return NotFound($"No se encontró la notificación con el ID {id}.");
                }

                
                _context.Notificaciones.Remove(notificacion);
                await _context.SaveChangesAsync();

                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la notificación: {ex.Message}");
            }
        }
    }
}
