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

    public class ParticipanteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly GestorDBcontext _context;

        public ParticipanteController(IMapper mapper, GestorDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearParticipante([FromBody] ParticipanteDTO participanteDTO)
        {
            if (participanteDTO == null)
            {
                return BadRequest("Los datos del participante no pueden ser nulos.");
            }

            try
            {
                var participante = _mapper.Map<Participante>(participanteDTO);

                await _context.Participantes.AddAsync(participante);
                await _context.SaveChangesAsync();

                var participanteCreadoDTO = _mapper.Map<ParticipanteDTO>(participante);

                return CreatedAtAction(nameof(GetParticipanteById), new { id = participante.Id }, participanteCreadoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el participante: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipanteById(int id)
        {
            try
            {
                var participante = await _context.Participantes
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (participante == null)
                {
                    return NotFound($"No se encontró un participante con el ID {id}.");
                }

                var participanteDTO = _mapper.Map<ParticipanteDTO>(participante);

                return Ok(participanteDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el participante: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllParticipantes()
        {
            try
            {
                var participantes = await _context.Participantes.ToListAsync();

                var participantesDTO = _mapper.Map<List<ParticipanteDTO>>(participantes);

                return Ok(participantesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los participantes: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipante(int id, [FromBody] ParticipanteDTO participanteDTO)
        {
            if (participanteDTO == null || id != participanteDTO.Id)
            {
                return BadRequest("Los datos del participante son incorrectos.");
            }

            try
            {
                var participante = await _context.Participantes
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (participante == null)
                {
                    return NotFound($"No se encontró un participante con el ID {id}.");
                }

                
                participante.Nombre = participanteDTO.Nombre;
                participante.Correo = participanteDTO.Correo;

                await _context.SaveChangesAsync();

                var participanteActualizadoDTO = _mapper.Map<ParticipanteDTO>(participante);

                return Ok(participanteActualizadoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el participante: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipante(int id)
        {
            try
            {
                var participante = await _context.Participantes
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (participante == null)
                {
                    return NotFound($"No se encontró un participante con el ID {id}.");
                }

                _context.Participantes.Remove(participante);
                await _context.SaveChangesAsync();

                return Ok($"El Participante con ID {id} ha sido eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el participante: {ex.Message}");
            }
        }


    }
}