using System.Data;
using Models;
using Entities;

namespace Controllers
{
    public class ProveedorController
    {
        private ProveedorDao proveedorDao = new ProveedorDao();

        public DataTable listarProveedores()
        {
            return proveedorDao.ListarProveedores();
        }

        public DataTable FiltrarProveedores(string texto)
        {
            return proveedorDao.FiltrarProveedores(texto);
        }


        

        public string AgregarProveedor(Proveedor proveedor)
        {
            return proveedorDao.AgregarProveedor(proveedor);
        }

        public string EditarProveedor(Proveedor proveedor)
        {
            return proveedorDao.EditarProveedor(proveedor);
        }

        public void EliminarProveedor(int idProveedor)
        {
            proveedorDao.EliminarProveedor(idProveedor);
        }

        public Proveedor ObtenerProveedor(int idProveedor)
        {
            return proveedorDao.ObtenerProveedor(idProveedor);
        }
    }
}
