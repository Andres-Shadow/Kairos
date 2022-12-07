using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudbasesdedatos.logica
{
    internal class TipoPresentacion
    {
        public int id
        {
            get; set;
        }

        public string tipo
        {
            get; set;
        }

        public TipoPresentacion(int id, string tipo)
        {
            this.id = id;
            this.tipo = tipo;
        }
    }
}
