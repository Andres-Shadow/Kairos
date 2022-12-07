using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudbasesdedatos.logica
{
    internal class Administrador
    {
        private string usuario
        {
            get;
            set;
        }
        private string password
        {
            get;
            set;
        }

        public Administrador(string usuario, string password)
        {
            this.usuario = usuario;
            this.password = password;
        }


    }
}
