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
    internal class PedidoDao
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


        public List<Pedido> obtenerPedidosSegunCliente(Cliente cliente)
        {
            string consulta = "select * from pedido where cedula_cliente=\'" + cliente.cedula + "\' and estado = \'creado\'";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            List<Pedido> lista = new List<Pedido>();
            Pedido pAux = null;
            Cliente cAux = null;
            Empleado eAux = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string estado = reader.GetString(1);
                        cAux =  clienteRepo.obtenerClientePorId(reader.GetString(2));
                        eAux = empleadoRepo.obtenerEmpleadoPorId(reader.GetInt32(3));
                        float valor = reader.GetFloat(4);
                        pAux = new Pedido(id, estado, cAux, eAux, valor);
                        lista.Add(pAux);
                    }
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            return lista;
        }

        public List<Pedido> listarPedidos()
        {
            string consulta = "select * from pedido";
            Pedido pedido = null;
            Cliente cliente = null;
            Empleado eaux = null;
            List<Pedido> lista = new List<Pedido>();  
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string estado = reader.GetString(1);
                        cliente = clienteRepo.obtenerClientePorId(reader.GetString(2));
                        eaux = empleadoRepo.obtenerEmpleadoPorId(reader.GetInt32(3));
                        float valor = reader.GetFloat(4);   
                        pedido = new Pedido(id, estado, cliente, eaux, valor);
                        lista.Add(pedido);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return lista;
        }

        public Pedido obtenerPedidoSegunId(int id)
        {
            string consulta = "select * from pedido where id=" + id;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            Pedido pedido = null;
            try
            {   
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int idd = reader.GetInt32(0);
                        string estado = reader.GetString(1);
                        Cliente aux = clienteRepo.obtenerClientePorId(reader.GetString(2));
                        Empleado empleado = empleadoRepo.obtenerEmpleadoPorId(reader.GetInt32(3));
                        float valor = reader.GetFloat(4);
                        pedido = new Pedido(idd, estado, aux, empleado, valor);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return pedido;
        }

        public bool actualizarEstadoPedidoAFacturado(int idPedido) 
        {

            string consulta = "update pedido set estado = \'facturado\' where id = " + idPedido;
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

        public bool tramitarPedido( string cedulaCliente, int idEmpleado, float valor)
        {
            int id = contarPedidos()+1;
            string consulta = "insert into pedido values ("+id + ", \'creado\' , \'" + cedulaCliente + "\', " + idEmpleado + ", " + valor + ")";
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

        public int contarPedidos()
        {
            string consula = "select count(id) from pedido";
            MySqlCommand cmd = new MySqlCommand(consula);
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


        public bool actualizarEstadoPedidoACreado(int idPedido)
        {

            string consulta = "update pedido set estado = \'creado\' where id = " + idPedido;
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

        public float calcularValorCarrito(Pedido pedido)
        {
            string consulta = "select sum(cp.cantidad*pre.precio) from pedido p join carrito c on c.id_pedido = p.id join carrito_presentacion cp on cp.id_carrito = c.id join presentacion pre on pre.id = cp.id_presentacion where p.id = "+pedido.id;
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Connection = conectar();
            MySqlDataReader reader;
            float total = 0;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        total = reader.GetInt32(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return total;
        }





    }
}
