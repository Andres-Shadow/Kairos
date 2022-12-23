using crudbasesdedatos;
using crudbasesdedatos.logica;
using crudbasesdedatos.servicioImpl;
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
    public partial class DetallePedido : Form
    {
        private Cliente cliente;
        private Empleado empleado;
        private int llamado = 0;
        private AdminServicioImpl adminServicio = new AdminServicioImpl();
        private List<Presentacion> presentacions;

        //CARRITO
        private List<Presentacion> carrito = new List<Presentacion>();
        
        private Presentacion seleccionado = null;   
        public DetallePedido(string cedula, string cedulaEmpleado, int llamado)
        {
            InitializeComponent();
            cliente = adminServicio.obtenerClientePorCedula(cedula);
            empleado = adminServicio.obterEmpleadoPorCedula(cedulaEmpleado);
            label3.Text = "Cliente : " + cliente.nombre;
            presentacions = adminServicio.listarPresentaciones();
            this.llamado = llamado;
            setearListaCarrito();
        }

        private void setearListaCarrito()
        {
            dataGridView1.Rows.Clear();
            Presentacion aux = null;
            for(int i = 0; i< presentacions.Count; i++)
            {
                aux = presentacions[i];
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = aux.id;
                dataGridView1.Rows[i].Cells[1].Value = aux.producto.nombre;
                dataGridView1.Rows[i].Cells[2].Value = aux.tipo_producto.tipo;
                dataGridView1.Rows[i].Cells[3].Value = aux.existencias;
            }
        }

        private void actualizarCarrito()
        {
            dataGridView2.Rows.Clear();
            Presentacion aux = null;
            for (int i = 0; i <carrito.Count; i++)
            {
                aux = carrito[i];
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = aux.id;
                dataGridView2.Rows[i].Cells[1].Value = aux.producto.nombre;
                dataGridView2.Rows[i].Cells[2].Value = aux.tipo_producto.tipo;
                dataGridView2.Rows[i].Cells[3].Value = aux.existencias;
            }
        }


        private Presentacion obtenerPresentacionPorId(int id)
        {
            for(int i = 0; i< presentacions.Count; i++)
            {
                if (presentacions[i].id == id)
                {
                    return presentacions[i];
                }
            }
            return null;
        }

        private void eliminarPresentacionCarrito(int id)
        {
            for(int i = 0; i<carrito.Count; i++)
            {
                Presentacion aux = carrito[i];
                if(aux.id == id)
                {
                    carrito.Remove(aux);
                }
            }
            actualizarCarrito();
        }

        private void Agregar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value);
            string cantidad = "";
            cantidad = txtCantidadProducto.Text;
            seleccionado = obtenerPresentacionPorId(id);
            if(seleccionado != null && cantidad != null){
                if(cantidad == "")
                {
                    cantidad = ""+1;
                }
                else if(Int32.Parse(cantidad) >= seleccionado.existencias)
                {
                    cantidad = ""+seleccionado.existencias;
                }
                seleccionado.existencias = Int32.Parse(cantidad);
                this.carrito.Add(seleccionado);
                actualizarCarrito();
                txtCantidadProducto.Text = "";
                label4.Text = "Valor: " + calcularValorCarrito();
            }
        }

        public float calcularValorCarrito()
        {
            float valor = 0;
            Presentacion aux = null;
            for(int i = 0; i < this.carrito.Count; i++)
            {
                aux = carrito[i];
                valor += aux.precio * aux.existencias;
            }
            return valor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(this.dataGridView2.SelectedRows[0].Cells[0].Value);
                eliminarPresentacionCarrito(id);
                label4.Text = "Valor: " + calcularValorCarrito();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float valor = calcularValorCarrito();
            string cedulaCliente = this.cliente.cedula;
            int idEmpleado = empleado.id;
            bool todo = adminServicio.tramitarPedido(carrito, cedulaCliente, idEmpleado, valor);
            volverAGestion();
        }

        private void volverAGestion()
        {
            if(llamado == 1)
            {
                gestion_admin gestion = new gestion_admin(this.empleado.cedula);
                gestion.Show();
                this.Hide();
            }
            else if(llamado == 2)
            {
                EmpleadoView empleado = new EmpleadoView(this.empleado.cedula);
                empleado.Show();
                this.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(this.dataGridView2.SelectedRows[0].Cells[0].Value);
                eliminarPresentacionCarrito(id);
                string cantidad = "";
                cantidad = txtCantidadProducto.Text;
                seleccionado = obtenerPresentacionPorId(id);
                if (seleccionado != null && cantidad != null)
                {
                    if (cantidad == "")
                    {
                        cantidad = "" + 1;
                    }
                    else if (Int32.Parse(cantidad) >= seleccionado.existencias)
                    {
                        cantidad = "" + seleccionado.existencias;
                    }

                    seleccionado.existencias = Int32.Parse(cantidad);
                    this.carrito.Add(seleccionado);
                    actualizarCarrito();
                    txtCantidadProducto.Text = "";
                    label4.Text = "Valor: " + calcularValorCarrito();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            volverAGestion();
        }
    }
}
