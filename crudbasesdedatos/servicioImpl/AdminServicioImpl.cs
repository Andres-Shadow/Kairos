using crudbasesdedatos.dao;
using crudbasesdedatos.logica;
using crudbasesdedatos.servicios;
using kairos.dao;
using kairos.logica;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Cliente> listarClientes()
        {
            List<Cliente> listaClientes = clienteRepo.obtenerClientes();
            return listaClientes;
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

        public Cliente obtenerClientePorCedula(string cedula)
        {
            Cliente encontrado = clienteRepo.obtenerClientePorId(cedula);
            if (encontrado == null)
            {
                throw new Exception("Cliente no encontrado");
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
    }
}
