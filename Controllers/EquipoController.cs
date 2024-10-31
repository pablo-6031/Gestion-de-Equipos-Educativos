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

        public void agregarEquipo(Equipo equipo)
        {
            equipoDao.AgregarEquipo(equipo);
        }

        public void editarEquipo(Equipo equipo)
        {
            equipoDao.EditarEquipo(equipo);
        }

        public void eliminarEquipo(int id)
        {
            equipoDao.EliminarEquipo(id);
        }

        public byte[] obtenerFoto(int id)
        {
            return equipoDao.ObtenerFoto(id);
        }
       

    }
}
