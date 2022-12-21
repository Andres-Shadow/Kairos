using crudbasesdedatos.dao;
using crudbasesdedatos.logica;
using crudbasesdedatos.servicios;
using kairos.dao;
using kairos.logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace crudbasesdedatos.servicioImpl
{
    internal class AdminServicioImpl : AdminServicio
    {
        private ClienteDao clienteRepo = new ClienteDao();
        private ProductoDao productoRepo = new ProductoDao();
        private PresentacionDao presentacionRepo = new PresentacionDao();
        private TipoPresentacionDao tipoPresentacionRepo = new TipoPresentacionDao();
        private PedidoDao pedidoRepo = new PedidoDao();
        private CarritoDao carritRepo = new CarritoDao();
        private FacturaDao facturaRepo = new FacturaDao();
        private EmpleadoDao empleadoRepo = new EmpleadoDao();

        public bool actualizarCliente(string cedulaViejo, Cliente nuevo)
        {
            bool result = false;


            result = clienteRepo.actualizarCliente(cedulaViejo, nuevo);
            
            if(!result)
            {
                throw new Exception("No se ha podido actualizar el cliente");
            }
            
            return result;
        }

        public bool actualizarEmpleado(Empleado empleado)
        {
            bool result = empleadoRepo.actualizarEmpleado(empleado);
            if (result)
            {
                throw new Exception("No se ha podido actualizar el empleado");
            }
            return result;
        }

        public bool actualizarPresetnacion(int idViejo, Presentacion nuevo)
        {
            bool result = false;
            result = presentacionRepo.actualizarPresentacion(idViejo, nuevo);
            if(!result)
            {
                throw new Exception("No se ha podido actualizar la presentacion");
            }

            return result;
        }

        public bool actualizarProducto(int idViejo, Producto nuevo)
        {
            
            bool result = false;
            result = productoRepo.actualizarProducto(idViejo, nuevo);
            if (!result)
            {
                throw new Exception("No se ha podido actualizar el producto");
            }
            return result;
        }

        public bool actualizarTipoPresentacion(int idVIejo, TipoPresentacion nuevo)
        {
            bool result = false;
            result = tipoPresentacionRepo.actualizarTipoProducto(idVIejo, nuevo);
            if (!result)
            {
                throw new Exception("No se ha podido actualizar el tipo de presentacion");
            }
            return result;
        }

        public bool agregarCliente(Cliente nuevo)
        {
            bool result = clienteRepo.agrearCliente(nuevo);
            if(!result)
            {
                throw new Exception("No se ha podido agregar el cliente");
            }
            return result;
        }

        public bool agregarEmpleado(Empleado empleado)
        {
            bool result = empleadoRepo.agregarEmpleado(empleado);
            if (!result)
            {
                throw new Exception("No se ha podido agregar el empleado");
            }
            return result;
        }

        public bool agregarPresentacion(Presentacion presentacion)
        {
            bool result = presentacionRepo.agregarPresentacion(presentacion);
            if (!result)
            {
                throw new Exception("No se ha podido agregar esta presentacion");
            }
            return result;
        }

        public bool agregarProducto(Producto nuevo)
        {
            string nombre = nuevo.nombre;
            int id = nuevo.id;
            string estado = nuevo.estado;

            bool result = productoRepo.agregarProducto(nombre, estado, id);

            if(!result)
            {
                throw new Exception("No se ha podido agregar el producto");
            }

            return result;
        }

        public bool agregarTipoPresentacion(TipoPresentacion nuevo)
        {
            bool result = tipoPresentacionRepo.agregarTipoPresentaciono(nuevo);
            if (!result)
            {
                throw new Exception("No se ha podido agregar el tipo de presentacion");
            }
            return result;
        }

        public int contarFacturas()
        {
            int cantidad = facturaRepo.contarFacturas();
            return cantidad;
        }

        public int contarPresentaciones()
        {
            int total = 0;
            total = presentacionRepo.contarPresentaciones();
            return total;
        }

        public int contarProductos()
        {
            int total = 0;
            total = productoRepo.contarProductosExistentes();
            return total;
        }

        public int contarTipoPresentacion()
        {
            int total = 0;
            total = tipoPresentacionRepo.contarCantidadPresentaciones();
            return total;
        }

        public bool eliminarCliente(string cedula)
        {
            bool result = clienteRepo.eliminarCliente(cedula);
            if (!result)
            {
                throw new Exception("No se ha podido eliminar el cliente");
            }

            return result;
        }

        public bool eliminarEmpleado(int id)
        {
            bool result = empleadoRepo.eliminarEmpleado(id);
            if(!result)
            {
                throw new Exception("No se ha podido eliminar el empleado");
            }
            return result;
        }

        public bool eliminarPresetnacion(int id)
        {
            bool result = presentacionRepo.eliminarPresentacion(id);
            if (!result)
            {
                throw new Exception("No se ha podido eliminar esta presentacion");
            }

            return result;
        }

        public bool eliminarProducto(int id)
        {
            bool result = productoRepo.eliminarProducto(id);
            if (!result)
            {
                throw new Exception("No se ha podido eliminar el producto");
            }
            return result; 
        }

        public bool eliminarTipoPresentacion(int idViejo)
        {
            bool result = tipoPresentacionRepo.eliminarTipoPresentacion(idViejo);
            if (!result)
            {
                throw new Exception("No se ha podido eliminar este tipo de presentacion");
            }
            return result;
        }

        public bool facturar(Pedido pedido)
        {
            Canasta cAux = null;
            int idPresentacion;
            int catidadActual;
            int cantidadResultado;
            bool continuar = true;
            bool finalizado = true;

            string date = DateTime.UtcNow.ToString("dd-MM-yyyy");
            string[] partes = date.Split("-");
            int dia = Int32.Parse(partes[0]) + 1;
            int mes = Int32.Parse(partes[1]);
            int anio = Int32.Parse(partes[2]);


            string fecha = anio + "/" + mes + "/" + dia;
            int idFactura = contarFacturas() + 1;

            Factura f = new Factura(idFactura, pedido, fecha);

            if (pedido.estado.Equals("creado"))
            {
                try
                {
                    bool primerPaso = facturaRepo.agregarFactura(f);
                    if (primerPaso)
                    {
                        Carrito aux = carritRepo.obtenerCarritoSegunPedido(f.pedido.id);
                        if (aux != null)
                        {
                            MessageBox.Show("se ha encontrado el carrito pa");
                            aux.canastas = obtenerCanastaSegunCarrito(aux);
                            for (int i = 0; i < aux.canastas.Count && continuar == true; i++)
                            {
                                cAux = aux.canastas[i];
                                idPresentacion = cAux.presentacion.id;
                                catidadActual = cAux.presentacion.existencias;
                                cantidadResultado = catidadActual - cAux.cantidad;
                                continuar = presentacionRepo.reducirExistencias(idPresentacion, cantidadResultado);
                            }
                        }
                        finalizado = pedidoRepo.actualizarEstadoPedidoAFacturado(aux.pedido.id);
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Este pedido ya ha sido facturado");
            }


            return finalizado == true;
        }

        public List<Cliente> listarClientes()
        {
            List<Cliente> listaClientes = clienteRepo.obtenerClientes();
            return listaClientes;
        }

        public List<Empleado> listarEmpleado()
        {
            List<Empleado> listaEmpleado = empleadoRepo.listarEmpleado();
            return listaEmpleado;
        }

        public List<Pedido> listarPedidos()
        {
            List<Pedido> lista = pedidoRepo.listarPedidos();
            return lista;
        }

        public List<Presentacion> listarPresentaciones()
        {
            List<Presentacion> listaPresentaciones = presentacionRepo.listarPresentaciones();
            return listaPresentaciones;
        }

        public List<Producto> listarProductos()
        {
            List<Producto> listaProductos = productoRepo.listar();
            return listaProductos;
        }

        public List<TipoPresentacion> listarTipoPresentaciones()
        {
            List<TipoPresentacion> listaTipoPresentaciones = tipoPresentacionRepo.listarTipoPresentaciones();
            return listaTipoPresentaciones;
        }

        public List<Canasta> obtenerCanastaSegunCarrito(Carrito carrito)
        {
            List<Canasta> listaCanasta = carritRepo.obtenerCanastaDePedido(carrito);
            return listaCanasta;
        }

        public Carrito obtenerCarritoSegunPedido(Pedido pedido)
        {
            Carrito encontrado = carritRepo.obtenerCarritoSegunPedido(pedido);
            if(encontrado == null)
            {
                throw new Exception("No se ha podido encontrar el carrito");
            }
            return encontrado;
        }

        public Cliente obtenerClientePorCedula(string cedula)
        {
            Cliente encontrado = clienteRepo.obtenerClientePorId(cedula);
            if (encontrado == null)
            {
                throw new Exception("Cliente no encontrado");
            }

            return encontrado;
        }

        public Factura obtenerFacturaPorIdPedido(int id)
        {
            Factura encontrado = facturaRepo.obtenerFacturaPorIdPedido(id);
            if(encontrado == null)
            {
                throw new Exception("No se ha podido encontrar la factura");
            }
            return encontrado;
        }

        public Pedido obtenerPedidoPorId(int idPedido)
        {
            Pedido encontrado = pedidoRepo.obtenerPedidoSegunId(idPedido);
            if(encontrado == null)
            {
                throw new Exception("No se ha podido encontrar el pedido");
            }
            return encontrado;
        }

        public List<Pedido> obtenerPedidosSegunCliente(Cliente cliente)
        {
            List<Pedido> lista = pedidoRepo.obtenerPedidosSegunCliente(cliente);
            if (lista == null)
            {
                throw new Exception("No se ha encontrado ese cliente en los pedidos");
            }
            return lista;
        }

        public Presentacion obtenerPresentacionPorId(int id)
        {
            Presentacion encontrado = presentacionRepo.obtenerPresentacionPorId(id);
            if (encontrado == null)
            {
                throw new Exception("Presentacion no encontrada");
            }

            return encontrado;
        }

        public Producto obtenerProductoPorId(int id)
        {
            Producto encontrao = productoRepo.obtenerProductoPorId(id);

            if (encontrao == null)
            {
                throw new Exception("Producto no encontrado");
            }

            return encontrao;
        }

        public TipoPresentacion obtenerTipoPresentacionPorId(int id)
        {
            TipoPresentacion encontrado = tipoPresentacionRepo.obtenerPresentacionPorId(id);
            if (encontrado == null)
            {
                throw new Exception("Tipo presentacion no encontrado");
            }
            return encontrado;
        }

        public float obtenervalorCarritoSegunCarrito(Pedido pedido)
        {
            float valor = pedidoRepo.calcularValorCarrito(pedido);
            return valor;
        }

        public Empleado obterEmpleadoPorCedula(string cedula)
        {
            Empleado encontrado = empleadoRepo.obtenerEmpleadoPorCedula(cedula);
            if(encontrado == null)
            {
                throw new Exception("No se ha encontrado el empleado");
            }
            return encontrado;
        }

        public bool reincorporar(Factura factura)
        {
            Canasta cAux = null;
            int idPresentacion;
            int catidadActual;
            int cantidadResultado;
            bool continuar = true;
            bool finalizado = true;
            if (factura.pedido.estado.Equals("facturado"))
            {
                try
                {
                    bool primerPaso = facturaRepo.eliminarFactura(factura);
                    if (primerPaso)
                    {
                        Carrito aux = carritRepo.obtenerCarritoSegunPedido(factura.pedido.id);
                        if (aux != null)
                        {
                            aux.canastas = obtenerCanastaSegunCarrito(aux);
                            for (int i = 0; i < aux.canastas.Count && continuar == true; i++)
                            {
                                cAux = aux.canastas[i];
                                idPresentacion = cAux.presentacion.id;
                                catidadActual = cAux.presentacion.existencias;
                                cantidadResultado = catidadActual + cAux.cantidad;
                                continuar = presentacionRepo.reducirExistencias(idPresentacion, cantidadResultado);
                            }
                        }
                        //finalizado = pedidoRepo.actualizarEstadoPedidoACreado(aux.pedido.id);
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }


            return finalizado == true;
        }

        public bool tramitarPedido(List<Presentacion> lista, string cedulaCliente, int idEmpleado, float valor)
        {
            bool pedidoCreado = pedidoRepo.tramitarPedido(cedulaCliente, idEmpleado, valor);
            int idPedido = pedidoRepo.contarPedidos();
            MessageBox.Show(""+idPedido);
            Pedido creado = obtenerPedidoPorId(idPedido);
            Carrito aux = new Carrito(creado, idPedido);
            bool carritoCreado = carritRepo.agregarCarrito(aux);

            if(pedidoCreado == true &&  carritoCreado==true)
            {
                bool terminar = carritRepo.agregarProductosCarrito(aux, lista);
                if (terminar)
                {
                    MessageBox.Show("que viva el vicio");
                }
            }
            

            return true;
        }
    }
}
