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
    public class ReservaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly GestorDBcontext _context;
        

        public ReservaController(GestorDBcontext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
           
        }

        [HttpPost]
        public async Task<ActionResult<ReservaDTO>> CrearReserva([FromBody] ReservaDTO reservaDTO)
        {
            if (reservaDTO == null)
            {
                return BadRequest("Los datos de la reserva no pueden ser nulos.");
            }

           
            var sala = await _context.Salas.FirstOrDefaultAsync(s => s.Id == reservaDTO.SalaId);
            if (sala == null)
            {
                return NotFound($"No se encontró una sala con el ID {reservaDTO.SalaId}.");
            }

           
            if (reservaDTO.Cantidad > sala.Capacidad)
            {
                return BadRequest($"La sala tiene una capacidad máxima de {sala.Capacidad} personas. No se puede reservar para {reservaDTO.Cantidad} personas.");
            }

           
            var reserva = _mapper.Map<Reserva>(reservaDTO);
            _context.Reservaciones.Add(reserva);
            await _context.SaveChangesAsync();

            
            var reservaCreadaDTO = _mapper.Map<ReservaDTO>(reserva);

            return CreatedAtAction(nameof(GetReservaById), new { id = reserva.Id }, reservaCreadaDTO);
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReservas()
        {
            var reservas = await _context.Reservaciones
                .Include(r => r.Sala) 
                .Include(r => r.Participante) 
                .ToListAsync();

            
            var reservasDTO = _mapper.Map<List<ReservaDTO>>(reservas);

            return Ok(reservasDTO);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaDTO>> GetReservaById(int id)
        {
            var reserva = await _context.Reservaciones
                .Include(r => r.Sala) 
                .Include(r => r.Participante) 
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null)
            {
                return NotFound($"No se encontró una Reserva con el ID {id}.");
            }

            
            var reservaDTO = _mapper.Map<ReservaDTO>(reserva);

            return Ok(reservaDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReserva(int id, [FromBody] ReservaDTO reservaDTO)
        {
            if (id != reservaDTO.Id)
            {
                return BadRequest("Los ID no coinciden.");
            }

           
            var reserva = await _context.Reservaciones.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            
            var sala = await _context.Salas.FirstOrDefaultAsync(s => s.Id == reservaDTO.SalaId);
            if (sala == null)
            {
                return NotFound($"No se encontró una sala con el ID {reservaDTO.SalaId}.");
            }

            
            if (reservaDTO.Cantidad > sala.Capacidad)
            {
                return BadRequest($"La sala tiene una capacidad máxima de {sala.Capacidad} personas. No se puede reservar para {reservaDTO.Cantidad} personas.");
            }

        
            _mapper.Map(reservaDTO, reserva);

            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reservaciones.FindAsync(id);

            if (reserva == null)
            {
                return NotFound($"No se encontró una Reserva con el ID {id}.");
            }

            _context.Reservaciones.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();       
        }
    }
}



