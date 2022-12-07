using crudbasesdedatos.logica;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudbasesdedatos.dao
{
    internal class ProductoDao
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

        public List<Producto> listar()
        {
            
            string consulta = "select * from producto";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;

            List<Producto> lista= new List<Producto>();

            try
            {
                reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        string nombre, estado;
                        int id;
                        nombre = reader.GetString(0);
                        estado = reader.GetString(1);
                        id = reader.GetInt32(2);
                        lista.Add(new Producto(nombre, id, estado));
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show("No existen productos");
            }

     

            return lista;
        }


       

        public bool agregarProducto(string nombre, string estado, int id)
        {

            string consulta = "insert into producto values ( \'" + nombre + "\',\'" + estado + "\'," + id + ")";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar(); 
            MySqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }

        public Producto obtenerProductoPorId(int id)
        {
            Producto p = null;
            string consulta = "select * from producto where id="+id;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar(); 
            MySqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    
                    while (reader.Read())
                    {
                        string nombre, estado, tipo;
                        int idd;
                        float precio;
                        nombre = reader.GetString(0);
                        estado = reader.GetString(1);
                        idd = reader.GetInt32(2);
                        p = new Producto(nombre, id, estado);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            return p;
        }


        public bool eliminarProducto(int id)
        {
            string consulta = "delete from producto where id=" + id;
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

        public int contarProductosExistentes()
        {
            string consulta = "select count(id) from producto";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            int total = 0;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        total = reader.GetInt32(0);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return total;
        }
    }
}
