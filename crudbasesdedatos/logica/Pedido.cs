using crudbasesdedatos.logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kairos.logica
{
    internal class Pedido
    {
        public int id
        {
            get; set;
        }

        public string estado
        {
            get; set;
        }

        public Cliente cliente
        {
            get; set;
        }

        public Empleado empleado
        {
            get; set;
        }

        public Pedido(int id, string estado, Cliente cliente, Empleado empleado)
        {
            this.id = id;
            this.estado = estado;
            this.cliente = cliente;
            this.empleado = empleado;
        }
    }
}
