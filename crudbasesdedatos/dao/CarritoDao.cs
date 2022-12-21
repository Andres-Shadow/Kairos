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

            return true;
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

        public float calcularValorCarrito(Carrito carrito)
        {
            string consulta = "select sum(p.precio*c.cantidad) from carrito_presentacion c join presentacion p on c.id_presentacion = p.id where c.id_carrito =" + carrito.id;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            float total = 0;
            try
            {
                reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
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

        public Carrito obtenerCarritoSegunPedido(int idPedido)
        {
            Carrito aux = null;
            Pedido pedido = null;
            int id = 0;
            string consulta = "select * from carrito where id_pedido=" + idPedido;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            cmd.CommandTimeout = 1000;
            MySqlDataReader reader;
            
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        pedido = pedidoRepo.obtenerPedidoSegunId(reader.GetInt32(1));
                        aux = new Carrito(pedido, id);
                    }
                }
            }catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
            //aux.canastas = obtenerCanastaDePedido(aux);
            return aux;
        }

        public bool agregarProductosCarrito(Carrito carrito, List<Presentacion> presentaciones)
        {
            Presentacion aux = null;
            string consulta = "";
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            cmd.Connection = conectar();
            for(int i = 0; i < presentaciones.Count; i++)
            {
                aux = presentaciones[i];
                consulta = "insert into carrito_presentacion values ("+aux.existencias + "," + carrito.id + "," + aux.id + ")";
                cmd.CommandText = consulta;
                try
                {
                    reader = cmd.ExecuteReader();
                    reader.Close();
                }catch(Exception ex )
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return true;
        }



    }
}
