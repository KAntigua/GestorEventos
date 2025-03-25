using GestorEventos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Application.DTOs
{
    public class NotificacionDTO
    {
        
            public int Id { get; set; }

           
            public string Mensaje { get; set; }

           
            public DateTime FechaEnvio { get; set; }

           
            public int ParticipanteId { get; set; }

       
    }
}
