using System;
namespace iapex.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public int Id_user { get; set; }

        public int Id_hospital { get; set; }
        public DateTime Creado_en { get; set; }
        public DateTime Modificado_en { get; set; }
        public bool Borrado_en { get; set; }
    }
}
