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

        public DataTable ObtenerDetalleEquipo(int idEquipo)
        {
            return equipoDao.ObtenerDetalleEquipo(idEquipo);
        }

        public DataTable listarEquiposPorActa(int idActa)
        {
            return equipoDao.ListarEquiposPorActa(idActa);
        }

        public DataTable ListarEquiposporNumSerie(string valor)
        {
            return equipoDao.ListarEquiposporNumSerie(valor);
        }

        public void agregarEquipo(Equipo equipo)
        {
            equipoDao.AgregarEquipo(equipo);
        }

        public void agregarListaEquipo(List<Equipo> listEquipo, int idActa)
        {
            equipoDao.AgregarListaEquipo(listEquipo, idActa);
        }


        public void editarEquipo(Equipo equipo)
        {
            equipoDao.EditarEquipo(equipo);
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
