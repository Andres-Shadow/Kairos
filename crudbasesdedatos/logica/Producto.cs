using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudbasesdedatos.logica
{
    internal class Producto
    {

        public String nombre
        {
            get;
            set;
        }
        public int id
        {
            get;
            set;
        }
        public string estado
        {
            get;
            set;
        }
        

        public Producto(string nombre, int id, string estado)
        {
            this.nombre = nombre;
            this.id = id;
            this.estado = estado;  
        }

        public override String ToString()
        {
            return nombre;
        }

    }
}
