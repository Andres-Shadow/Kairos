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
    internal class FacturaDao
    {
        private string servidor = "localhost";
        private string usr = "root";
        private string pswd = "andres_1";
        private string bbdd = "test";
        private PedidoDao pedidoRepo = new PedidoDao();
        private PresentacionDao presentacionRepo = new PresentacionDao();
        private CarritoDao carritoRepo = new CarritoDao();
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

        public bool agregarFactura(Factura factura)
        {
            Carrito encontrado = carritoRepo.obtenerCarritoSegunPedido(factura.pedido.id);

            string consulta = "insert into factura values (" + factura.id + ", " + factura.pedido.id + ", \'" + factura.fecha + "\')";
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


        public bool eliminarFactura(Factura factura)
        {
            Carrito encontrado = carritoRepo.obtenerCarritoSegunPedido(factura.pedido.id);

            string consulta = "delete from factura where id=" + factura.id;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            cmd.CommandTimeout= 1000;
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

        public int contarFacturas()
        {
            string consulta = "select count(id) from factura";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            int cantidad = 0;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        cantidad = reader.GetInt32(0);
                    }
                }
            }catch(Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
            return cantidad;
        }

        public Factura obtenerFacturaPorIdPedido(int id)
        {
            string consulta = "select * from factura where id_pedido=" + id;
            MessageBox.Show(consulta);
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            Factura encontrado = null;
            int idFactura = 0;
            Cliente cliente = null;
            Pedido pedido = null;
            float valor = 0;
            string fecha = "";
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        idFactura = reader.GetInt32(0);
                        
                        pedido = pedidoRepo.obtenerPedidoSegunId(reader.GetInt32(1));
                        
                        fecha = reader.GetString(2);
                        encontrado = new Factura(idFactura,pedido, fecha);
                    }
                }
            }catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
            }


            return encontrado;
        }




    }
}
