using crudbasesdedatos.logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kairos.logica
{
    internal class Factura
    {
        public int id
        {
            get; set;
        }

        public Pedido pedido
        {
            get; set;
        }

        public string fecha
        {
            get; set;
        }

        public Factura(int id, Pedido pedido,  string fecha)
        {
            this.id = id;
            this.pedido = pedido;
            this.fecha = fecha;
        }




    }
}
