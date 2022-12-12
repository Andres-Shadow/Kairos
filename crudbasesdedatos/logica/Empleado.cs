using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kairos.logica
{
    internal class Empleado
    {
        public string nombre
        {
            get; set;
        }

        public string cedula
        {
            get; set;
        }

        public string correo
        {
            get; set;
        }

        public int id
        {
            get; set;
        }

        public Empleado(string nombre, string cedula, string correo, int id)
        {
            this.nombre = nombre;
            this.cedula = cedula;
            this.correo = correo;
            this.id = id;
        }   
    }
}
