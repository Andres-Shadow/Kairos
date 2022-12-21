using crudbasesdedatos.logica;
using kairos.logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudbasesdedatos.servicios
{
    internal interface AdminServicio
    {

        //PRODUCTOS

        public bool agregarProducto(Producto nuevo);
        public bool eliminarProducto(int id);
        public bool actualizarProducto(int idViejo, Producto nuevo);
        public List<Producto> listarProductos();
        public Producto obtenerProductoPorId(int id);
        public int contarProductos();



        //CLIENTES
        public bool agregarCliente(Cliente nuevo);
        public bool eliminarCliente(string cedula);
        public bool actualizarCliente(string cedulaViejo, Cliente nuevo);
        public List<Cliente> listarClientes();
        public Cliente obtenerClientePorCedula(string cedula);

        //PRESENTACIONES
        public bool agregarPresentacion(Presentacion presentacion);
        public bool eliminarPresetnacion(int id);
        public bool actualizarPresetnacion(int idViejo, Presentacion nuevo);
        public List<Presentacion> listarPresentaciones();
        public Presentacion obtenerPresentacionPorId(int id);
        public int contarPresentaciones();

        //TIPO PRESENTACIONES

        public List<TipoPresentacion> listarTipoPresentaciones();
        public TipoPresentacion obtenerTipoPresentacionPorId(int id);
        public bool agregarTipoPresentacion(TipoPresentacion nuevo);
        public bool eliminarTipoPresentacion(int idViejo);
        public bool actualizarTipoPresentacion(int idVIejo, TipoPresentacion nuevo);
        public int contarTipoPresentacion();

        //PEDIDOS
        public List<Pedido> obtenerPedidosSegunCliente(Cliente cliente);
        public Pedido obtenerPedidoPorId(int idPedido);
        public List<Pedido> listarPedidos();

        public bool tramitarPedido(List<Presentacion> lista, string cedulaCliente, int idEmpleado, float valor);

        //CARRITO
        public Carrito obtenerCarritoSegunPedido(Pedido pedido);
        public List<Canasta> obtenerCanastaSegunCarrito(Carrito carrito);
        public float obtenervalorCarritoSegunCarrito(Pedido pedido);

        //FACTURA
        public bool facturar(Pedido pedido);
        public Factura obtenerFacturaPorIdPedido(int id);
        public int contarFacturas();
        public bool reincorporar(Factura factura);


        //EMPLEADO
        public Empleado obterEmpleadoPorCedula(string cedula);
        public bool agregarEmpleado(Empleado empleado);
        public bool eliminarEmpleado(int id);
        public bool actualizarEmpleado(Empleado empleado);
        public List<Empleado> listarEmpleado();

        public int contarEmpleados();

    }
}
