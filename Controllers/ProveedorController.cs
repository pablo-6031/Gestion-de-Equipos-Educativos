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

        public void AgregarProveedor(Proveedor proveedor)
        {
            proveedorDao.AgregarProveedor(proveedor);
        }

        public void EditarProveedor(Proveedor proveedor)
        {
            proveedorDao.EditarProveedor(proveedor);
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
