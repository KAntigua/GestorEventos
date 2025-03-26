using GestorEventos.Domain.Entities;


namespace GestorEventos.Web.Models
{
    public class SalaViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        
        public int Capacidad { get; set; }
       
        public int Precio { get; set; }
    }
}
