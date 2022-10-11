using System;
namespace iapex.Models
{
    public class Institution
    {
        public int Id { get; set; }

        public string Nombre_institucion { get; set; }

        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Codigo_postal { get; set; }
        public int Id_user { get; set; }
        public DateTime Creado_en { get; set; }
        public DateTime Modificado_en { get; set; }
        public bool Borrado_en { get; set; }
    }
}
