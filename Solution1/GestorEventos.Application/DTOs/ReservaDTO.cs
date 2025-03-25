﻿using GestorEventos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Application.DTOs
{
    public class ReservaDTO
    {
        public int Id { get; set; }

        
        public string NombreReservacion { get; set; }

        
        
        public string Cedula { get; set; }

       
        public DateTime FechaReserva { get; set; }

       
        public int Cantidad { get; set; }


        public int SalaId { get; set; }

        public int ParticipanteId { get; set; }




    }


}

