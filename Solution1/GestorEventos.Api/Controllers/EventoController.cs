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
    public class EventoController : ControllerBase
    {
        
        
            private readonly IMapper _mapper;
            private readonly GestorDBcontext _context;

            public EventoController(IMapper mapper, GestorDBcontext context)
            {
                _mapper = mapper;
                _context = context;
            }

        [HttpPost]
        public IActionResult CrearEvento(EventoDTO eventoDTO)
        {

            var sala = _context.Salas.Find(eventoDTO.SalaId);
            if (sala == null)
            {
                return NotFound($"No se encontró una sala con el ID {eventoDTO.SalaId}.");
            }

            var evento = new Evento
            {
                Nombre = eventoDTO.Nombre,
                TotalParticipante = eventoDTO.TotalParticipante,
                FechaHora = eventoDTO.FechaHora,
                SalaId = eventoDTO.SalaId
            };

            _context.Eventos.Add(evento);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetById), 
                new { id = evento.Id },
                evento); 
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoDTO>>> GetEventos()
        {
            var eventos = await _context.Eventos.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<EventoDTO>>(eventos));
        }

        


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var evento = _context.Eventos.Find(id);
            if (evento == null)
            {
                return NotFound($"No se encontró el Evento con ID {id}.");
            }
            return Ok(evento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, EventoDTO eventoDTO)
        {
            if (id != eventoDTO.Id)
            {
                return BadRequest();
            }

            var evento = _mapper.Map<Evento>(eventoDTO);
            _context.Entry(evento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound($"No se encontró el evento con ID {id}.");
            }

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }

    }
}
