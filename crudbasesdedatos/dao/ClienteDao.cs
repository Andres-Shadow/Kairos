using crudbasesdedatos.logica;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace crudbasesdedatos.dao
{
    internal class ClienteDao
    {
        private string servidor = "localhost";  
        private string usr = "root";
        private string pswd = "andres_1";
        private string bbdd = "test";

        //CONEXION
        
        public MySqlConnection conectar()
        {
            MySqlConnection cx;
            string conexion = "Database=" + bbdd + "; Data Source=" + servidor + "; User Id=" + usr + "; Password=" + pswd + "";
            cx = new MySqlConnection(conexion);
            cx.Open();
            return cx;
        }

        public bool agrearCliente(Cliente cliente)
        {
            
            string consulta = "insert into cliente  values (\'" + cliente.nombre + "\', \'" + cliente.cedula + "\', \'" + cliente.telefono + "\', \'" + cliente.direccion + "\', \'"+cliente.nit+"\')";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;

            try
            {
                reader = cmd.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public List<Cliente> obtenerClientes()
        {
            string consulta = "select * from cliente";

            string nombre = "";
            string cedula = "";
            string telefono = "";
            string direccion = "";
            string nit = null;

            MySqlCommand cdm = new MySqlCommand(consulta);
            cdm.Connection = conectar();
            MySqlDataReader reader;

            List<Cliente> lista = new List<Cliente>();

            try
            {
                reader = cdm.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        nombre = reader.GetString(0);
                        cedula = reader.GetString(1);
                        telefono = reader.GetString(2);
                        direccion = reader.GetString(3);
                        nit = reader.GetString(4);
                        Cliente aux = new Cliente(nombre, cedula, telefono, direccion, nit);
                        lista.Add(aux);
                        
                    }
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return lista;
        }


        public Cliente obtenerClientePorId(string cedula)
        {
            string consulta = "select * from cliente where Cedula= \'" + cedula + "\'";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;

            string nombre = "";
            string _cedula = "";
            string telefono = "";
            string direccion = "";
            string nit = "";
            Cliente aux = null;

            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        nombre = reader.GetString(0);
                        _cedula= reader.GetString(1);
                        telefono = reader.GetString(2);
                        direccion = reader.GetString(3);
                        nit = reader.GetString(4);

                        aux = new Cliente(nombre, _cedula, telefono, direccion, nit);

                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return aux;
        }

        public bool eliminarCliente(string cedula)
        {
            string consulta = "delete from cliente where cedula=\'" + cedula + "\'";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;

            try
            {
                reader = cmd.ExecuteReader();
            }catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
            
            return true;
        }


        public bool actualizarCliente(string cedula, Cliente actualizar)
        {
            string consulta = "update cliente set nombre= \'"+actualizar.nombre+"\', telefono= \'"+actualizar.telefono+"\', direccion = \'"+actualizar.direccion+"\', nit=\'"+actualizar.nit+"\' where cedula =\'"+cedula+"\'";
            
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
            }catch(Exception e) 
            {
                MessageBox.Show(e.Message);
            }

            return true;
        }

    }
}
