using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GestorEventos.Application.DTOs;
using GestorEventos.Domain.Entities;

namespace GestorEventos.Application.Mappers
{
    public class AutoMapping : Profile
    {

        public AutoMapping()
        {

            CreateMap<UsuarioDTO, Usuario>(); 
            CreateMap<Usuario, UsuarioDTO>();

            CreateMap<SalaDTO, Sala>();
            CreateMap<Sala, SalaDTO>();

            CreateMap<ReservaDTO, Reserva>();
            CreateMap<Reserva, ReservaDTO>();

            CreateMap<ParticipanteDTO, Participante>();
            CreateMap<Participante, ParticipanteDTO>();

            CreateMap<NotificacionDTO, Notificacion>();
            CreateMap<Notificacion, NotificacionDTO>();


            CreateMap<HorarioDTO, Horario>();
            CreateMap<Horario, HorarioDTO>();


            CreateMap<EventoDTO, Evento>();
            CreateMap<Evento, EventoDTO>();



        }
    }
}