using crudbasesdedatos.logica;
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

    }
}
