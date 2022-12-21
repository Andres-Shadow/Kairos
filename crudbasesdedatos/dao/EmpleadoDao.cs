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
                        int id = reader.GetInt32(2);
                        string password = reader.GetString(3);
                        string tipo = reader.GetString(4);
                        encontrado = new Empleado(nombre, cedula, id, password, tipo);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return encontrado;
        }

        public Empleado obtenerEmpleadoPorCedula(string cedula)
        {
            string consulta = "select * from empleado where cedula = " + cedula;
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
                        string cedula2 = reader.GetString(1);
                        int id = reader.GetInt32(2);
                        string password = reader.GetString(3);
                        string tipo = reader.GetString(4);
                        encontrado = new Empleado(nombre, cedula2, id, password, tipo);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return encontrado;
        }

        public bool agregarEmpleado(Empleado empleado)
        {
            string consulta = "insert into empleado values (\'" + empleado.nombre + "\', \'" + empleado.cedula + "\', " + empleado.id + ",\'" + empleado.password + "\', \'" + empleado.tipo + "\')";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
            }catch(Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }

            return true;
        }

        public bool actualizarEmpleado(Empleado empleado)
        {
            string nombre, cedula, password, tipo;
            nombre = empleado.nombre;
            cedula = empleado.cedula;
            password = empleado.password;
            tipo = empleado.tipo;
            string consulta = "update empleado set nombre = \'" + nombre + "\' ,cedula = \'" + cedula + "\', password = \'" + password + "\', tipo = \'" + tipo + "\' where id = " + empleado.id;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }

        public bool eliminarEmpleado(int id)
        {
            string consulta = "delete from empleado where id = " + id;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }

        public List<Empleado> listarEmpleado()
        {
            string consulta = "select * from empleado";
            List<Empleado> lista = new List<Empleado>();
            Empleado aux = null;
            string nombre, cedula, password, tipo;
            int id;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        nombre = reader.GetString(0);
                        cedula = reader.GetString(1);
                        id = reader.GetInt32(2);
                        password = reader.GetString(3);
                        tipo = reader.GetString(4);
                        aux = new Empleado(nombre, cedula, id, password, tipo);
                        lista.Add(aux);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return lista;
        }
    }
}
