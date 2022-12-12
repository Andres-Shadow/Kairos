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
            string consulta = "select * from pedido where cedula_cliente=\'" + cliente.cedula + "\'";
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
                        pAux = new Pedido(id, estado, cAux, eAux);
                        lista.Add(pAux);
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
