using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEventos.Domain.Entities
{
    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TotalParticipante { get; set; }
        public DateTime FechaHora { get; set; }  

        public int SalaId { get; set; }
        
        [ForeignKey("SalaId")]
        public virtual Sala Sala { get; set; }
       

    
    }
}
