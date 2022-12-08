using crudbasesdedatos.logica;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudbasesdedatos.dao
{
    internal class TipoPresentacionDao
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

        public TipoPresentacion obtenerPresentacionPorId(int id)
        {
            string consulta = "select * from tipo_producto where id=" + id;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection= conectar();
            MySqlDataReader reader;

            TipoPresentacion encontrado = null;

            try
            {
                reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        int idd = reader.GetInt32(0);
                        string tipo = reader.GetString(1);

                        encontrado = new TipoPresentacion(id, tipo);
                    }
                }
            }catch(Exception ex)
            {

            }


            return encontrado;
        }

        public List<TipoPresentacion> listarTipoPresentaciones()
        {
            string consulta = "select * from tipo_producto";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;

            List<TipoPresentacion> lista= new List<TipoPresentacion>();

            try
            {
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int idd = reader.GetInt32(0);
                        string tipo = reader.GetString(1);

                        lista.Add(new TipoPresentacion(idd, tipo));
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lista;
        }

        public bool agregarTipoPresentaciono(TipoPresentacion nuevo)
        {
            string consulta = "insert into tipo_producto values (" + nuevo.id + ", \'" + nuevo.tipo + "\')";
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

        public bool eliminarTipoPresentacion(int id)
        {
            string consulta = "delete from tipo_producto where id=" + id;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return true;
        }


        public bool actualizarTipoProducto(int idViedjo, TipoPresentacion nuevo)
        {
            string consulta = "update tipo_producto set tipo=\'" + nuevo.tipo + "\' where id=" + idViedjo;
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


        public int contarCantidadPresentaciones()
        {
            string consulta = "select count(id) from tipo_producto";
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
