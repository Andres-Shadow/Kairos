using crudbasesdedatos.dao;
using kairos.logica;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kairos.dao
{
    internal class EmpleadoDao
    {
        private string servidor = "localhost";
        private string usr = "root";
        private string pswd = "andres_1";
        private string bbdd = "test";
        private ClienteDao clienteRepo = new ClienteDao();

        //CONEXION

        public MySqlConnection conectar()
        {
            MySqlConnection cx;
            string conexion = "Database=" + bbdd + "; Data Source=" + servidor + "; User Id=" + usr + "; Password=" + pswd + "";
            cx = new MySqlConnection(conexion);
            cx.Open();
            return cx;
        }

        public Empleado obtenerEmpleadoPorId(int idEmpleado)
        {
            string consulta = "select * from empleado where id=" + idEmpleado;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;

            Empleado encontrado = null;

            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string nombre = reader.GetString(0);
                        string cedula = reader.GetString(1);
                        string correo = reader.GetString(2);
                        int id = reader.GetInt32(3);
                        encontrado = new Empleado(nombre, cedula, correo, id);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return encontrado;
        }
    }
}
