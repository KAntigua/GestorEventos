﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GestorEventos.Application.DTOs
{
    public class UsuarioDTO
    {

        public int Id { get; set; }
        public string Nombre { get; set; }

        
        public string Contrasena { get; set; }


        public string Correo { get; set; }

     
        public string Rol { get; set; }

        public DateTime FechaRegistro { get; set; }


    }
}
