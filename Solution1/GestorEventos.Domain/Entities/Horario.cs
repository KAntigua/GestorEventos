using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Domain.Entities
{
    public class Horario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }


        public int EventoId { get; set; }
        [ForeignKey("EventoId")]
        public virtual Evento Evento { get; set; }


        public int SalaId { get; set; }

        [ForeignKey("SalaId")]
        public virtual Sala Sala { get; set; }



    }
}
