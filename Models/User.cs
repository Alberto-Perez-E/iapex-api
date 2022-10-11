using System;
using System.Text.Json.Serialization;
namespace iapex.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }
        public string Apellido_paterno { get; set; }
        public string Apellido_materno { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Curp { get; set; }
        public string Ine { get; set; }
        public string Fecha_nacimiento { get; set; }
        public DateTime Creado_en { get; set; }
        public DateTime Modificado_en { get; set; }
        public bool Borrado_en { get; set; }
        public string Contrasena { get; set; }
    }
}
