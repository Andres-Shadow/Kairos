using crudbasesdedatos.logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kairos.logica
{
    internal class Canasta
    {
        public Carrito carrito
        {
            get; set;
        }

        public Presentacion presentacion
        {
            get; set;
        }

        public int cantidad
        {
            get; set;
        }

        public Canasta(Carrito carrito, Presentacion presentacion, int cantidad)
        {
            this.carrito = carrito;
            this.presentacion = presentacion;
            this.cantidad = cantidad;
        }
    }
}
