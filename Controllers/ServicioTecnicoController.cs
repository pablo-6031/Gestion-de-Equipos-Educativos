using System.Data;
using Models;
using Entities;

namespace Controllers
{
    public class ServicioTecnicoController
    {
        private ServicioTecnicoDao servicioTecnicoDao = new ServicioTecnicoDao();

        public DataTable ListarServiciosTecnicos()
        {
            return servicioTecnicoDao.ListarServiciosTecnicos();
        }

        public DataTable FiltrarServiciosTecnicos(string texto)
        {
            return servicioTecnicoDao.FiltrarServiciosTecnicos(texto);
        }


        

        public DataTable ListarServiciosTecnicosConEquipos()
        {
            return servicioTecnicoDao.ListarServiciosTecnicosConEquipos();
        }

        public void AgregarServicioTecnico(ServicioTecnico servicio)
        {
            servicioTecnicoDao.AgregarServicioTecnico(servicio);
        }

        public void EditarServicioTecnico(ServicioTecnico servicio)
        {
            servicioTecnicoDao.EditarServicioTecnico(servicio);
        }

        public void EliminarServicioTecnico(int idServicioTecnico)
        {
            servicioTecnicoDao.EliminarServicioTecnico(idServicioTecnico);
        }

        public byte[] ObtenerFoto(int idServicioTecnico)
        {
            return servicioTecnicoDao.ObtenerFoto(idServicioTecnico);
        }
    }
}
