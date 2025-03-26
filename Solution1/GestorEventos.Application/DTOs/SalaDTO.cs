using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Application.DTOs
{
    public class SalaDTO
    {
        public int Id { get; set; }

        
        public string Nombre { get; set; }

        
        public int Capacidad { get; set; }

        public int Precio { get; set; }
    }
}
