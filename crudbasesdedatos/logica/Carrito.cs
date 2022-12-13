using crudbasesdedatos.logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kairos.logica
{
    internal class Carrito
    {
        public Pedido pedido { get; set; }
        public int id{ get; set; }
        public List<Canasta> canastas { get; set; }

        public Carrito(Pedido pedido, int id) 
        {
            this.pedido = pedido;
            this.id = id;
        }

        public bool agregarProductoCanasta(Presentacion presentacion, int cantidad)
        {
            Canasta nuevo = new Canasta(this, presentacion, cantidad);
            this.canastas.Add(nuevo);
            return true;
        }

    }
}
