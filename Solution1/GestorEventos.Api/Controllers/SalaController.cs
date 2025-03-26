using Microsoft.EntityFrameworkCore;
using GestorEventos.Application.DTOs;
using AutoMapper;
using GestorEventos.Domain.Entities;
using GestorEventos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.Api.Controllers
   
{

    [Route("api/[Controller]")]
    [ApiController]

    public class SalaCotroller : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly GestorDBcontext _context;

        public SalaCotroller(IMapper mapper, GestorDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSala([FromBody] SalaDTO salaDTO)
        {
            if (salaDTO == null)
            {
                return BadRequest("Los datos de la sala no pueden ser nulos.");
            }

            try
            {
                var sala = _mapper.Map<Sala>(salaDTO);


                await _context.Salas.AddAsync(sala);
                await _context.SaveChangesAsync();

                var salaCreadaDTO = _mapper.Map<SalaDTO>(sala);
                return CreatedAtAction(nameof(GetSalaById), new { id = sala.Id }, salaCreadaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la sala: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalas()
        {
            try
            {
                var salas = await _context.Salas.ToListAsync();

                if (salas == null || !salas.Any())
                {
                    return NotFound("No se encontraron salas.");
                }

                var salasDTO = _mapper.Map<List<SalaDTO>>(salas);
                return Ok(salasDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las salas: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalaById(int id)
        {
            try
            {
                var sala = await _context.Salas
                                         .FirstOrDefaultAsync(s => s.Id == id);

                if (sala == null)
                    return NotFound($"No se encontró una sala con el ID {id}.");

                var salaDTO = _mapper.Map<SalaDTO>(sala);
                return Ok(salaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> PutSala(int id, [FromBody] SalaDTO salaDTO)
        {
            if (salaDTO == null)
            {
                return BadRequest("Los datos de la sala no pueden ser nulos.");
            }

            var salaExistente = await _context.Salas.FindAsync(id);
            if (salaExistente == null)
            {
                return NotFound($"No se encontró una sala con el ID {id}.");
            }

            try
            {
                salaExistente.Nombre = salaDTO.Nombre;
                salaExistente.Capacidad = salaDTO.Capacidad;
                salaExistente.Precio = salaDTO.Precio;

              
                await _context.SaveChangesAsync();

                var salaActualizadaDTO = _mapper.Map<SalaDTO>(salaExistente);
                return Ok(salaActualizadaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la sala: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
            {
                return NotFound($"No se encontró una sala con el ID {id}.");
            }

            try
            {
                _context.Salas.Remove(sala);
                await _context.SaveChangesAsync();

                return Ok($"Sala con ID {id} eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la sala: {ex.Message}");
            }
        }
    }


}
