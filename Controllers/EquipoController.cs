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
    public class EquipoController
    {
        private EquipoDao equipoDao = new EquipoDao();

        public DataTable listaEquipos()
        {
            return equipoDao.ListarEquipos();
        }

        public DataTable ListarEquiposporNumSerieCompleto(string texto)
        {
            return equipoDao.ListarEquiposporNumSerieCompleto(texto);
        }

        public bool ComprobarEquipo(string numSerie, string matricula)
        {
            return equipoDao.ComprobarEquipo(numSerie, matricula);
        }

        public bool VerificarGarantiaEquipo(int idEquipo)
        {
            return equipoDao.VerificarGarantiaEquipo(idEquipo);
        }


        




        public DataTable ListarEquiposConTipo()
        {
            return equipoDao.ListarEquiposConTipo();
        }

        public DataTable ListarEquiposPorADMovil(int idAdm)
        {
            return equipoDao.ListarEquiposPorADMovil(idAdm);
        }


        




        public DataTable ObtenerDetalleEquipo(int idEquipo)
        {
            return equipoDao.ObtenerDetalleEquipo(idEquipo);
        }

        public DataTable ListarEquiposAdmPorActa(int idActa)
        {
            return equipoDao.ListarEquiposAdmPorActa(idActa);
        }


        

        public DataTable ListarEquiposporNumSerie(string valor)
        {
            return equipoDao.ListarEquiposporNumSerie(valor);
        }

        public void AgregarEquipo(Equipo equipo, int idAdm)
        {
            equipoDao.AgregarEquipo(equipo, idAdm);
        }

        public void agregarListaEquipo(List<Equipo> listEquipo, int idActa)
        {
            equipoDao.AgregarListaEquipo(listEquipo, idActa);
        }


        public string EditarEquipo(Equipo equipo)
        {
            return equipoDao.EditarEquipo(equipo);
        }
        /*
        public void eliminarEquipo(int id)
        {
            equipoDao.EliminarEquipo(id);
        }
        */
        public string eliminarEquipo(int idEquipo) => equipoDao.EliminarEquipo(idEquipo);

        public byte[] obtenerFoto(int id)
        {
            return equipoDao.ObtenerFoto(id);
        }
       

    }
}
