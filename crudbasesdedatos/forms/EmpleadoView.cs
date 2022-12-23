using crudbasesdedatos.forms;
using crudbasesdedatos.logica;
using crudbasesdedatos.servicioImpl;
using crudbasesdedatos.servicios;
using kairos.logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kairos.forms
{
    public partial class EmpleadoView : Form
    {
        private AdminServicioImpl adminServicio = new AdminServicioImpl();
        private Empleado empleado = null;
        public EmpleadoView(string cedula)
        {
            InitializeComponent();
            listarClientes();
            listarPedidosFactura();
            listarPresentaciones();

            empleado = adminServicio.obterEmpleadoPorCedula(cedula);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[2];
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[3];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreCliente.Text;
            string cedula = txtCedulaCliente.Text;
            string telefono = txtTelefonoCliente.Text;
            string direccion = txtDireccionCliente.Text;
            string nit = txtNitCliente.Text;

            bool result = false;

            if (nit.Equals(""))
            {
                Cliente cliente = new Cliente(nombre, cedula, telefono, direccion, "-");
                result = adminServicio.agregarCliente(cliente);
            }
            else
            {
                Cliente cliente = new Cliente(nombre, cedula, telefono, direccion, nit);
                result = adminServicio.agregarCliente(cliente);
            }

            if (result)
            {
                MessageBox.Show("Cliente agregado");
                limpiarCliente();
                listarClientes();
            }
        }

        private void listarClientes()
        {
            List<Cliente> lista = adminServicio.listarClientes();
            dataGridView1.Rows.Clear();

            for (int i = 0; i < lista.Count; i++)
            {
                Cliente c = lista[i];
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = c.nombre;
                dataGridView1.Rows[i].Cells[1].Value = c.cedula;
                dataGridView1.Rows[i].Cells[2].Value = c.telefono;
                dataGridView1.Rows[i].Cells[3].Value = c.direccion;
                dataGridView1.Rows[i].Cells[4].Value = c.nit;

                dataGridClientesPedidos.Rows.Add();
                dataGridClientesPedidos.Rows[i].Cells[0].Value = c.cedula;
                dataGridClientesPedidos.Rows[i].Cells[1].Value = c.nombre;
            }
        }

        private void limpiarCliente()
        {
            txtNombreCliente.Text = "";
            txtCedulaCliente.Text = "";
            txtTelefonoCliente.Text = "";
            txtDireccionCliente.Text = "";
            txtNitCliente.Text = "";
        }

        private void dataGridClientesPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string cedula = (string)this.dataGridClientesPedidos.SelectedRows[0].Cells[0].Value;
            Cliente seleccionado = adminServicio.obtenerClientePorCedula(cedula);

            List<Pedido> lista = adminServicio.obtenerPedidosSegunCliente(seleccionado);



            dataGridProductosCarrito.Rows.Clear();
            dataGridPedidosCliente.Rows.Clear();
            for (int i = 0; i < lista.Count; i++)
            {
                Pedido aux = lista[i];
                dataGridPedidosCliente.Rows.Add();
                dataGridPedidosCliente.Rows[i].Cells[0].Value = aux.id;
                dataGridPedidosCliente.Rows[i].Cells[1].Value = aux.estado;
            }
        }

        private void dataGridPedidosCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(this.dataGridPedidosCliente.SelectedRows[0].Cells[0].Value);
            Pedido pedidoSeleccionado = adminServicio.obtenerPedidoPorId(id);
            Carrito carro = adminServicio.obtenerCarritoSegunPedido(pedidoSeleccionado);
            carro.canastas = adminServicio.obtenerCanastaSegunCarrito(carro);

            dataGridProductosCarrito.Rows.Clear();
            for (int i = 0; i < carro.canastas.Count; i++)
            {
                Canasta aux = carro.canastas[i];
                dataGridProductosCarrito.Rows.Add();
                //id nombre cantidad
                dataGridProductosCarrito.Rows[i].Cells[0].Value = aux.presentacion.id;
                dataGridProductosCarrito.Rows[i].Cells[1].Value = aux.presentacion.producto.nombre;
                dataGridProductosCarrito.Rows[i].Cells[2].Value = aux.presentacion.tipo_producto.tipo;
                dataGridProductosCarrito.Rows[i].Cells[3].Value = aux.cantidad;

            }
        }

        private void dataGridClientesPedidos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string cedula = (string)this.dataGridClientesPedidos.SelectedRows[0].Cells[0].Value;
            Cliente seleccionado = adminServicio.obtenerClientePorCedula(cedula);

            List<Pedido> lista = adminServicio.obtenerPedidosSegunCliente(seleccionado);



            dataGridProductosCarrito.Rows.Clear();
            dataGridPedidosCliente.Rows.Clear();
            for (int i = 0; i < lista.Count; i++)
            {
                Pedido aux = lista[i];
                dataGridPedidosCliente.Rows.Add();
                dataGridPedidosCliente.Rows[i].Cells[0].Value = aux.id;
                dataGridPedidosCliente.Rows[i].Cells[1].Value = aux.estado;
            }
        }

        private void dataGridPedidosCliente_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(this.dataGridPedidosCliente.SelectedRows[0].Cells[0].Value);
            Pedido pedidoSeleccionado = adminServicio.obtenerPedidoPorId(id);
            Carrito carro = adminServicio.obtenerCarritoSegunPedido(pedidoSeleccionado);
            carro.canastas = adminServicio.obtenerCanastaSegunCarrito(carro);

            dataGridProductosCarrito.Rows.Clear();
            for (int i = 0; i < carro.canastas.Count; i++)
            {
                Canasta aux = carro.canastas[i];
                dataGridProductosCarrito.Rows.Add();
                //id nombre cantidad
                dataGridProductosCarrito.Rows[i].Cells[0].Value = aux.presentacion.id;
                dataGridProductosCarrito.Rows[i].Cells[1].Value = aux.presentacion.producto.nombre;
                dataGridProductosCarrito.Rows[i].Cells[2].Value = aux.presentacion.tipo_producto.tipo;
                dataGridProductosCarrito.Rows[i].Cells[3].Value = aux.cantidad;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cedula = (string)this.dataGridClientesPedidos.SelectedRows[0].Cells[0].Value;
            this.Hide();
            DetallePedido d = new DetallePedido(cedula, empleado.cedula,2);
            d.Show();
        }

        private void dataGridPedidosFacturacion_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(this.dataGridPedidosFacturacion.SelectedRows[0].Cells[0].Value);
            Pedido seleccionado = adminServicio.obtenerPedidoPorId(id);
            Carrito carroPedido = adminServicio.obtenerCarritoSegunPedido(seleccionado);
            carroPedido.canastas = adminServicio.obtenerCanastaSegunCarrito(carroPedido);
            carritoPedidoFacturacion.Rows.Clear();
            for (int i = 0; i < carroPedido.canastas.Count; i++)
            {
                Canasta aux = carroPedido.canastas[i];
                carritoPedidoFacturacion.Rows.Add();
                carritoPedidoFacturacion.Rows[i].Cells[0].Value = aux.presentacion.id;
                carritoPedidoFacturacion.Rows[i].Cells[1].Value = aux.presentacion.producto.nombre;
                carritoPedidoFacturacion.Rows[i].Cells[2].Value = aux.presentacion.tipo_producto.tipo;
                carritoPedidoFacturacion.Rows[i].Cells[3].Value = aux.cantidad;
            }
        }

        private void listarPedidosFactura()
        {
            List<Pedido> lista = adminServicio.listarPedidos();
            dataGridPedidosFacturacion.Rows.Clear();
            for (int i = 0; i < lista.Count; i++)
            {
                Pedido aux = lista[i];
                dataGridPedidosFacturacion.Rows.Add();
                dataGridPedidosFacturacion.Rows[i].Cells[0].Value = aux.id;
                dataGridPedidosFacturacion.Rows[i].Cells[1].Value = aux.cliente.nombre;
                dataGridPedidosFacturacion.Rows[i].Cells[2].Value = aux.estado;
                dataGridPedidosFacturacion.Rows[i].Cells[3].Value = aux.valor;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                int id = Convert.ToInt32(this.dataGridPedidosFacturacion.SelectedRows[0].Cells[0].Value);
                Pedido seleccionado = adminServicio.obtenerPedidoPorId(id);
                adminServicio.facturar(seleccionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            listarPedidosFactura();
            listarPresentaciones();
        }

        private void listarPresentaciones()
        {
            List<Presentacion> presentaciones = adminServicio.listarPresentaciones();
            dataGridView3.Rows.Clear();
            for (int i = 0; i < presentaciones.Count; i++)
            {
                Presentacion aux = presentaciones[i];
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = aux.id;
                dataGridView3.Rows[i].Cells[1].Value = aux.producto.nombre;
                dataGridView3.Rows[i].Cells[2].Value = aux.tipo_producto.tipo;
                dataGridView3.Rows[i].Cells[3].Value = aux.precio;
                dataGridView3.Rows[i].Cells[4].Value = aux.existencias;


            }
        }

        private void EmpleadoView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                if (MessageBox.Show("Desea salir de la aplicacion?", "Alerta", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Se cancelo la salida");
                }
            }
        }
    }
}
