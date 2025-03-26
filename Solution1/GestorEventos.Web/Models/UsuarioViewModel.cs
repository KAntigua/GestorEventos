namespace GestorEventos.Web.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public string Correo { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string Rol { get; set; }
    }
}
