using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class TipoEquipoController
    {
        private TipoEquipoDao tipoEquipoDao = new TipoEquipoDao();

        public DataTable ListaTipoEquipos()
        {
            return tipoEquipoDao.ListarTipoEquipos();
        }

        public void AgregarTipoEquipo(TipoEquipo tipoEquipo)
        {
            tipoEquipoDao.AgregarTipoEquipo(tipoEquipo);
        }

        public void EditarTipoEquipo(TipoEquipo tipoEquipo)
        {
            tipoEquipoDao.EditarTipoEquipo(tipoEquipo);
        }

        public void EliminarTipoEquipo(int id)
        {
            tipoEquipoDao.EliminarTipoEquipo(id);
        }

        public byte[] ObtenerFoto(int id)
        {
            return tipoEquipoDao.ObtenerFoto(id);
        }
    }
}
