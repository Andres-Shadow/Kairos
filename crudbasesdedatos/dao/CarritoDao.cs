using crudbasesdedatos.dao;
using crudbasesdedatos.logica;
using kairos.logica;
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
        private PedidoDao pedidoRepo = new PedidoDao();
        private PresentacionDao presentacionRepo = new PresentacionDao();

        //CONEXION

        public MySqlConnection conectar()
        {
            MySqlConnection cx;
            string conexion = "Database=" + bbdd + "; Data Source=" + servidor + "; User Id=" + usr + "; Password=" + pswd + "";
            cx = new MySqlConnection(conexion);
            cx.Open();
            return cx;
        }

        public bool agregarCarrito(Carrito carrito)
        {
            string consulta = "insert into carrito values (" + carrito.id + "," + carrito.pedido.id + ")";
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

            return false;
        }

        public Carrito obtenerCarritoPorId(int id)
        {
            string consulta = "select * from carrito where id=" + id;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            Carrito carrito = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int idd = reader.GetInt32(0);
                        Pedido pedido = pedidoRepo.obtenerPedidoSegunId(reader.GetInt32(1));
                        carrito = new Carrito(pedido, id);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return carrito;
        }

        public Carrito obtenerCarritoSegunPedido(Pedido pedido)
        {
            int idPedido = pedido.id;
            string consulta = "select * from carrito where id_pedido=" + idPedido;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            Carrito found = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        found = new Carrito(pedido, id);
                    }
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return found;
        }

        public List<Canasta> obtenerCanastaDePedido(Carrito carrito)
        {
            string consulta = "select * from carrito_presentacion where id_carrito = " + carrito.id;
            
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            List<Canasta> found = new List<Canasta>();
            Canasta aux = null;
            int cantidad = 0;
            Presentacion pAux = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cantidad = reader.GetInt32(0);
                        pAux = presentacionRepo.obtenerPresentacionPorId(reader.GetInt32(2));
                        aux = new Canasta(carrito, pAux, cantidad);
                        
                        found.Add(aux); 
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return found;   
        }




    }
}
