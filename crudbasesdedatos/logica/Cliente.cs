using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudbasesdedatos.logica
{
    internal class Cliente
    {
        public string nombre
        {
            get; set;
        }
        public string cedula
        {
            get; set;
        }
        public string telefono
        {
            get; set;
        }
        public string direccion
        {
            get; set;
        }

        public string nit
        {
            get; set;
        }

        

        public Cliente(string nombre, string cedula, string telefono, string direccion, string nit)
        {
            this.nombre = nombre;
            this.cedula = cedula;
            this.telefono = telefono;
            this.direccion = direccion;
            this.nit = nit;
        }

        public Cliente (string nombre, string cedula, string telefono, string direccion)
        {
            this.nombre = nombre;
            this.cedula = cedula;
            this.telefono = telefono;
            this.direccion = direccion;
        }

    }
}
