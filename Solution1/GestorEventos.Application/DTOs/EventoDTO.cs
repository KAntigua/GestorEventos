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
    public class EventoDTO
    {
        public int Id { get; set; }

   
        public string Nombre { get; set; }

   
        public int TotalParticipante { get; set; }


        public DateTime FechaHora { get; set; }

        
        public int SalaId { get; set; }

      
    }
}
