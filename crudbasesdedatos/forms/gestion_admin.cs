using crudbasesdedatos.dao;
using crudbasesdedatos.forms;
using crudbasesdedatos.logica;
using crudbasesdedatos.servicioImpl;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System.Data;
using System.Windows.Forms;

namespace crudbasesdedatos
{
    public partial class gestion_admin : Form
    {

        private AdminServicioImpl adminServicio = new AdminServicioImpl();
        public gestion_admin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //LLENADO DE LA TABLA DE PRODUCTOS
            listar();

            //LLENADO DE LA TABLA DE CLIENTES
            listarClientes();

            //LLENADO DE LA TABLA PRODUCTOS EXISTENTES PARA PRESENTACION
            listarProductosPresentacio();

            //LLENADO DE LA TABLA DE TIPO DE PRESENTACION PARA PRESENTACION
            listarTipoPresentacionPresentacion();


            //LLENADO DE LA TABLA PRESENTACIONES
            listarPresentaciones();

            string[] estados = {"CREADO", "ACTUALIZADO", "TRAMITADO"};

            comboEstado.Items.Clear();
            comboEstado.Items.AddRange(estados);

        }

        

        private void gestion_admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
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

        //PRODUCTO
        private void listar()
        {
            List<Producto> lista = adminServicio.listarProductos();
            dataGridView1.Rows.Clear();

            for (int i = 0; i< lista.Count; i++)
            {
                Producto p = lista[i];
                dataGridView1.Rows.Add();  
                dataGridView1.Rows[i].Cells[0].Value = p.id;
                dataGridView1.Rows[i].Cells[1].Value = p.nombre;
                dataGridView1.Rows[i].Cells[2].Value = p.estado;
            }
        }

        //CLIENTE
        private void listarClientes()
        {
            List<Cliente> lista = adminServicio.listarClientes();
            dataGridView2.Rows.Clear();

            for(int i = 0; i<lista.Count; i++)
            {
                Cliente c = lista[i];
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = c.nombre;
                dataGridView2.Rows[i].Cells[1].Value = c.cedula;
                dataGridView2.Rows[i].Cells[2].Value = c.telefono;
                dataGridView2.Rows[i].Cells[3].Value = c.direccion;
                dataGridView2.Rows[i].Cells[4].Value = c.nit;
                
            }
        }

        //PRODUCTOS EXISNTETES
        private void listarProductosPresentacio()
        {
            List<Producto> productosExistentes = adminServicio.listarProductos();
            dataGridProductosExistentes.Rows.Clear();

            for (int i = 0; i < productosExistentes.Count; i++)
            {
                Producto aux = productosExistentes[i];
                dataGridProductosExistentes.Rows.Add();
                dataGridProductosExistentes.Rows[i].Cells[0].Value = aux.id;
                dataGridProductosExistentes.Rows[i].Cells[1].Value = aux.nombre;
            }
        }

        //TIPO PRESENTACION EXISTENTE
        private void listarTipoPresentacionPresentacion()
        {
            List<TipoPresentacion> listaTiposPresentacionExistentes = adminServicio.listarTipoPresentaciones();
            dataGridTipoPresentacion.Rows.Clear();
            for (int i = 0; i < listaTiposPresentacionExistentes.Count; i++)
            {
                TipoPresentacion aux = listaTiposPresentacionExistentes[i];
                dataGridTipoPresentacion.Rows.Add();
                dataGridTipoPresentacion.Rows[i].Cells[0].Value = aux.id;
                dataGridTipoPresentacion.Rows[i].Cells[1].Value = aux.tipo;
            }

        }

        //PRESENTACIONES
        private void listarPresentaciones()
        {
            List<Presentacion> presentaciones = adminServicio.listarPresentaciones();
            dataGridView3.Rows.Clear();
            for(int i = 0; i<presentaciones.Count; i++)
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

        private Producto obtenerProductoSeleccionado()
        {
            int id = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value);
            Producto aux = adminServicio.obtenerProductoPorId(id);
            return aux;
        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Producto aux = obtenerProductoSeleccionado();

                if(aux != null)
                {
                    txtNombreProd.Text = aux.nombre;
                    txtIdProducto.Text = ""+aux.id;
                    comboEstado.SelectedItem = aux.estado;
                }
                else
                {
                    txtNombreProd.Text = "";
                    txtIdProducto.Text = "";
                }
                
            }catch(Exception ex)
            {

            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cliente aux = obtenerClienteSeleccionado();

                if(aux != null )
                {
                    txtNombreCliente.Text = aux.nombre;
                    txtTelefonoCliente.Text = aux.telefono;
                    txtCedulaCliente.Text = aux.cedula;
                    txtDireccionCliente.Text = aux.direccion;
                    txtNitCliente.Text = aux.nit;
                }
            }catch(Exception ex)
            {

            }
        }

        //AGREGAR PRODUCTO
        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreProd.Text;

            int id = adminServicio.contarProductos()+1;
            string estado = "Creado";


            Producto nuevo = new Producto(nombre, id, estado);

            bool aproved = adminServicio.agregarProducto(nuevo);

            if (aproved)
            {
                MessageBox.Show("Producto agregado");
                limpiar();
                listar();
            }
            else
            {
                MessageBox.Show("Fallo al agregar");
            }
        }

        //ELIMINAR PRODUCTO
        private void button3_Click(object sender, EventArgs e)
        {
            Producto eliminar = obtenerProductoSeleccionado();
            if (MessageBox.Show("Quieres eliminar a este producto?", "Alerta", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                adminServicio.eliminarProducto(eliminar.id);   
            }
            limpiar();
            listar();
        }

        //ACTUALIZAR PRDOCUTO
        private void button2_Click(object sender, EventArgs e)
        {
            Producto actualizar = obtenerProductoSeleccionado();
            string nombre = txtNombreProd.Text;
            string estado = "CREADO";
            int id = Int32.Parse(txtIdProducto.Text);
            

            if (actualizar != null && verifyDupe(id, actualizar.id))
            {
                Producto aux = new Producto(nombre, id, estado);
                adminServicio.actualizarProducto(actualizar.id, aux);
                listar();
                limpiar();
            }
        }



        private bool verifyDupe(int idNuevo, int idViejo)
        {
            return idNuevo == idViejo;
        }


        private void limpiar()
        {
            txtIdProducto.Text = "";
            txtNombreProd.Text = "";
            comboEstado.SelectedText = "";
        }

        //BOTON LIMPIAR PRODUCTO
        private void button4_Click_1(object sender, EventArgs e)
        {
            limpiar();
        }

        //REGRESO A LOGIN
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }


        //AGREGAR CLIENTE

        private void button6_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreCliente.Text;
            string cedula = txtCedulaCliente.Text;
            string telefono = txtTelefonoCliente.Text;
            string direccion = txtDireccionCliente.Text;
            string nit = txtNitCliente.Text;

            bool result = false;

            if(nit.Equals(""))
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

        //ACTUALIZAR CLIENTE
        private void button7_Click(object sender, EventArgs e)
        {
            Cliente actualizar = obtenerClienteSeleccionado();
            string nombre = txtNombreCliente.Text;
            string cedula = txtCedulaCliente.Text;
            string telefono = txtTelefonoCliente.Text;
            string direccion = txtDireccionCliente.Text;
            string nit = txtNitCliente.Text;
            bool result = false;

            if(actualizar != null && verifyDupeCliente(actualizar.cedula, cedula))
            {
                if (nit.Equals(""))
                {
                    Cliente cliente = new Cliente(nombre, cedula, telefono, direccion, "-");
                    result = adminServicio.actualizarCliente(actualizar.cedula, cliente);
                    limpiarCliente();
                }
                else
                {
                    Cliente cliente = new Cliente(nombre, cedula, telefono, direccion, nit);
                    result = adminServicio.actualizarCliente(actualizar.cedula, cliente);
                    limpiarCliente();
                }

                if (result)
                {
                    MessageBox.Show("Cliente actualizado");
                    listarClientes();
                }
            }

        }

        //ELIMINAR CLIENTE
        private void button8_Click(object sender, EventArgs e)
        {
            Cliente cliente = obtenerClienteSeleccionado();

            if(cliente != null)
            {
                if (MessageBox.Show("Quieres eliminar a este cliente?", "Alerta", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    adminServicio.eliminarCliente(cliente.cedula);
                    limpiarCliente();
                    listarClientes();
                }
            }
        }

        private bool verifyDupeCliente(string cedulaOld,string cedulaNew )
        {
            return (cedulaOld.Equals(cedulaNew)) ? true : false;
        }


        private Cliente obtenerClienteSeleccionado()
        {
            string cedula = (string) this.dataGridView2.SelectedRows[0].Cells[1].Value;
            Cliente aux = adminServicio.obtenerClientePorCedula(cedula);
            return aux;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            limpiarCliente();
        }

        private void limpiarCliente()
        {
            txtCedulaCliente.Text = "";
            txtNombreCliente.Text = "";
            txtTelefonoCliente.Text = "";
            txtDireccionCliente.Text = "";
            txtNitCliente.Text = "";
        }


        //AGREGAR PRESENTACION
        private void button10_Click(object sender, EventArgs e)
        {
            Producto seleccionado = adminServicio.obtenerProductoPorId(Convert.ToInt32(this.dataGridProductosExistentes.SelectedRows[0].Cells[0].Value));
            TipoPresentacion tipo = adminServicio.obtenerTipoPresentacionPorId(Convert.ToInt32(this.dataGridTipoPresentacion.SelectedRows[0].Cells[0].Value));

            if (seleccionado != null)
            {
                if(tipo != null)
                {
                    int existencias = Int32.Parse(txtExistenciasPresentacion.Text);
                    float precio = float.Parse(txtPrecioPresentacion.Text);
                    int id = adminServicio.contarPresentaciones() + 1;

                    Presentacion nuevo = new Presentacion(seleccionado, tipo, existencias, precio, id);

                    adminServicio.agregarPresentacion(nuevo);

                    listarPresentaciones();
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado un tipo de presentacion");
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un producto existente");
            }
        }
    }



}