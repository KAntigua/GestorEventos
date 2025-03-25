using Microsoft.EntityFrameworkCore;
using GestorEventos.Domain.Entities;

namespace GestorEventos.Infrastructure.Persistence
{
    public class GestorDBcontext : DbContext
    {
       
        public GestorDBcontext(DbContextOptions<GestorDBcontext> options) : base(options)
        {

        }


        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Reserva> Reservaciones { get; set; }

        public async Task GetUsuarioById(int id)
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Sala>().ToTable("Sala");
            modelBuilder.Entity<Participante>().ToTable("Participante");
            modelBuilder.Entity<Notificacion>().ToTable("Notificacion");
            modelBuilder.Entity<Horario>().ToTable("Horario");
            modelBuilder.Entity<Reserva>().ToTable("Reserva");
            modelBuilder.Entity<Evento>().ToTable("Evento");

        }
    }

    
        
}
