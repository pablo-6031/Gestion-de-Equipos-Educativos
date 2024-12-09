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

        public DataTable FiltrarTipoEquipos(string texto)
        {
            return tipoEquipoDao.FiltrarTipoEquipos(texto);
        }

        public List<string> ObtenerTiposEquiposUnicos()
        {
            return tipoEquipoDao.ObtenerTiposEquiposUnicos();
        }


        public TipoEquipo TraerTipoEquipos(int id)
        {
            return tipoEquipoDao.TraerTipoEquipos(id);
        }


        public string AgregarTipoEquipo(TipoEquipo tipoEquipo)
        {
            return tipoEquipoDao.AgregarTipoEquipo(tipoEquipo);
        }

        public string EditarTipoEquipo(TipoEquipo tipoEquipo)
        {
            return tipoEquipoDao.EditarTipoEquipo(tipoEquipo);
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
