using crudbasesdedatos.dao;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kairos.dao
{
    internal class CarritoDao
    {
        private string servidor = "localhost";
        private string usr = "root";
        private string pswd = "andres_1";
        private string bbdd = "test";
        private ClienteDao clienteRepo = new ClienteDao();
        private EmpleadoDao empleadoRepo = new EmpleadoDao();

        //CONEXION

        public MySqlConnection conectar()
        {
            MySqlConnection cx;
            string conexion = "Database=" + bbdd + "; Data Source=" + servidor + "; User Id=" + usr + "; Password=" + pswd + "";
            cx = new MySqlConnection(conexion);
            cx.Open();
            return cx;
        }

        public bool agregarCarrito()
        {

            return false;
        }

    }
}
