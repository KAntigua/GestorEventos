namespace GestorEventos.Web.Models
{
    public class NotificacionViewModel
    {
        public int Id { get; set; }


        public string Mensaje { get; set; }


        public DateTime FechaEnvio { get; set; }


        public int ParticipanteId { get; set; }
    }
}
