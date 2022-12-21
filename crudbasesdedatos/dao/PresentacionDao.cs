using crudbasesdedatos.logica;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudbasesdedatos.dao
{
    internal class PresentacionDao
    {
        private string servidor = "localhost";
        private string usr = "root";
        private string pswd = "andres_1";
        private string bbdd = "test";
        private ProductoDao productoRepo = new ProductoDao();
        private TipoPresentacionDao tipoPresentacionRepo = new TipoPresentacionDao();

        //CONEXION

        public MySqlConnection conectar()
        {
            MySqlConnection cx;
            string conexion = "Database=" + bbdd + "; Data Source=" + servidor + "; User Id=" + usr + "; Password=" + pswd + "";
            cx = new MySqlConnection(conexion);
            cx.Open();
            return cx;
        }


        public bool agregarPresentacion(Presentacion presentacion)
        {
            int id_producto = presentacion.producto.id;
            int id_tipo_presentacion = presentacion.tipo_producto.id;
            int existencias = presentacion.existencias;
            int id_presentacion = presentacion.id;
            float precio = presentacion.precio;

            string consulta = "insert into presentacion values (" + existencias + "," + precio + "," + id_producto + "," + id_tipo_presentacion + "," + id_presentacion + ")";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection= conectar();
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

        public List<Presentacion> listarPresentaciones()
        {
            List<Presentacion> lista = new List<Presentacion>();

            string consulta = "select * from presentacion";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection= conectar();
            MySqlDataReader reader;

            try
            {
                reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        int existencias = reader.GetInt32(0);
                        float precio = reader.GetFloat(1);
                        Producto producto = this.productoRepo.obtenerProductoPorId(reader.GetInt32(2));
                        TipoPresentacion presentacion = this.tipoPresentacionRepo.obtenerPresentacionPorId(reader.GetInt32(3));
                        int id = reader.GetInt32(4);

                        lista.Add(new Presentacion(producto, presentacion, existencias, precio, id));
                    }
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return lista;
        }

        public bool eliminarPresentacion(int id)
        {
            string consulta = "delete from presentacion where id=" + id;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection= conectar();
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


        public Presentacion obtenerPresentacionPorId(int id)
        {
            string consulta = "select * from presentacion where id=" + id;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection= conectar();
            MySqlDataReader reader;

            Presentacion encontrado = null;

            try
            {
                reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        int existencias = reader.GetInt32(0);
                        float precio = reader.GetFloat(1);
                        Producto producto = this.productoRepo.obtenerProductoPorId(reader.GetInt32(2));
                        TipoPresentacion presentacion = this.tipoPresentacionRepo.obtenerPresentacionPorId(reader.GetInt32(3));
                        int idd = reader.GetInt32(4);

                        encontrado = new Presentacion(producto, presentacion, existencias, precio, idd);
                    }
                }

            }catch(Exception e) 
            {
                MessageBox.Show(e.Message);
            }


            return encontrado;
        }

        public int contarPresentaciones()
        {
            string consulta = "select count(id) from presentacion";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            int resultado = 0;
            try
            {
                reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        resultado = reader.GetInt32(0);
                    }
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return resultado;
        }

        public bool actualizarPresentacion(int idViejo, Presentacion nuevo)
        {

            string consulta = "update presentacion set existencias=" + nuevo.existencias + ", precio=" + nuevo.precio + ", id_producto = " + nuevo.producto.id + ", id_tipo_producto=" + nuevo.tipo_producto.id +" where id="+idViejo;
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

        public bool reducirExistencias(int id, int existencias)
        {
            string consulta = "update presentacion set existencias=" + existencias + " where id=" + id;
            MessageBox.Show(consulta);
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            cmd.CommandTimeout = 1000;
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

    }
}
