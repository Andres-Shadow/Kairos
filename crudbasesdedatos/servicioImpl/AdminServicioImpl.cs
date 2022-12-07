using crudbasesdedatos.dao;
using crudbasesdedatos.logica;
using crudbasesdedatos.servicios;
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

        public bool actualizarCliente(string cedulaViejo, Cliente nuevo)
        {
            bool eliminacion = clienteRepo.eliminarCliente(cedulaViejo);
            bool result = false;

            if(eliminacion)
            {
              result = clienteRepo.agrearCliente(nuevo);
            }
            else
            {
                throw new Exception("NO SE HA ENCONTRADO EL CLIENTE");
            }
            return result;
        }

        public bool actualizarPresetnacion(int idViejo, Presentacion nuevo)
        {
            bool eliminacion = presentacionRepo.eliminarPresentacion(idViejo);
            bool result = false;
            if (eliminacion)
            {
                result = presentacionRepo.agregarPresentacion(nuevo);
            }
            else
            {
                throw new Exception("No se ha encontrado esa presentacion");
            }

            return result;
        }

        public bool actualizarProducto(int idViejo, Producto nuevo)
        {
            bool eliminacion = productoRepo.eliminarProducto(idViejo);
            bool result = false;
            if (eliminacion)
            {
                string nombre = nuevo.nombre;
                int id = nuevo.id;
                string estado = nuevo.estado;

                result =  productoRepo.agregarProducto(nombre, estado, id);
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

    }
}
