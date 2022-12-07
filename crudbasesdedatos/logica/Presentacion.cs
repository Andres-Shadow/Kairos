using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudbasesdedatos.logica
{
    internal class Presentacion
    {

        //SETEADOS A MANO
        public Producto producto
        {
            get; set;
        }

        public TipoPresentacion tipo_producto
        {
            get; set;
        }


        public int id
        {
            get; set;
        }

        public int existencias
        {
            get; set;
        }

        public float precio
        {
            get; set;
        }

        public Presentacion(Producto prodducto, TipoPresentacion tipo, int existencias, float precio, int id)
        {
            this.producto = prodducto;
            this.tipo_producto = tipo;
            this.existencias = existencias;
            this.precio = precio;
            this.id = id;
        }

    }
}
