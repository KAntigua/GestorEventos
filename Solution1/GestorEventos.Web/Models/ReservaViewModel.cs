namespace GestorEventos.Web.Models
{
    public class ReservaViewModel
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
