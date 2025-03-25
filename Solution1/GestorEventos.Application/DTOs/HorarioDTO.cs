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
    
        public class HorarioDTO
        {
            public int Id { get; set; }

            
            public DateTime FechaHoraInicio { get; set; }

   
            public DateTime FechaHoraFin { get; set; }

        public int EventoId { get; set; }
       

        
        public int SalaId { get; set; }
        

        }



}
