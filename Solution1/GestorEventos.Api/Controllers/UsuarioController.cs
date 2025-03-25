using AutoMapper;
using GestorEventos.Domain.Entities;
using GestorEventos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestorEventos.Application.DTOs;



namespace GestorEventos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly GestorDBcontext _context;

        public UsuarioController(IMapper mapper, GestorDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Post([FromBody] UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            try
            {
                
                var existeCorreo = await _context.Usuarios
                                                  .AnyAsync(u => u.Correo == usuarioDTO.Correo);
                if (existeCorreo)
                {
                    return BadRequest("El correo electrónico ya está en uso.");
                }

                var usuario = _mapper.Map<Usuario>(usuarioDTO);

             
                _context.Usuarios.Add(usuario); 
                await _context.SaveChangesAsync(); 

                var usuarioCreadoDTO = _mapper.Map<UsuarioDTO>(usuario);

                
                return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuarioCreadoDTO);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"Error al crear el usuario: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarioById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound($"No se encontró un usuario con el ID {id}.");
            }

            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(usuarioDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();

                if (usuarios == null || !usuarios.Any())
                {
                    return NotFound("No se encontraron usuarios.");
                }

                var usuariosDTO = _mapper.Map<List<UsuarioDTO>>(usuarios);
                return Ok(usuariosDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los usuarios: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                return BadRequest("Los datos del usuario no pueden ser nulos.");
            }

            var usuarioExistente = await _context.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound($"No se encontró un usuario con el ID {id}.");
            }

            try
            {
                
                usuarioExistente.Nombre = usuarioDTO.Nombre;
                usuarioExistente.Contrasena = usuarioDTO.Contrasena;
                usuarioExistente.Correo = usuarioDTO.Correo;
                usuarioExistente.Rol = usuarioDTO.Rol;
                usuarioExistente.FechaRegistro = usuarioDTO.FechaRegistro;

               
                await _context.SaveChangesAsync();

                
                var usuarioActualizadoDTO = _mapper.Map<UsuarioDTO>(usuarioExistente);
                return Ok(usuarioActualizadoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el usuario: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    return NotFound($"No se encontró un usuario con el ID {id}.");
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                return Ok($"Sala con ID {id} eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el usuario: {ex.Message}");
            }
        }

    }
}
