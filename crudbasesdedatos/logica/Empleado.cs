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

        public string password
        {
            get; set;
        }

        public string tipo
        {
            get; set;
        }

        public int id
        {
            get; set;
        }

        public Empleado(string nombre, string cedula, int id, string password, string tipo)
        {
            this.nombre = nombre;
            this.cedula = cedula;
            this.password = password;
            this.tipo = tipo;
            this.id = id;
        }   
    }
}
