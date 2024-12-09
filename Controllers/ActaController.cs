using System.Data;
using Models;
using Entities;
using System.Collections.Generic;
using System;

namespace Controllers
{
    public class ActaController
    {
        private ActaDao actaDao = new ActaDao();

        public DataTable ListarActas()
        {
            return actaDao.ListarActas();
        }

        public DataTable FiltrarActasPorFecha(string fecha)
        {
            return actaDao.FiltrarActasPorFecha(fecha);
        }


        public DataTable ListarActasPorFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            return actaDao.ListarActasPorFechas(fechaDesde, fechaHasta);
        }


        public void AgregarActa(Acta acta)
        {
            actaDao.AgregarActa(acta);
        }

        public void AgregarActaConEquipos(Acta acta, List<Equipo> equipos, List<Equipo> adm)
        {
            actaDao.AgregarActaConEquipos(acta, equipos, adm);
        }



        
        public void EditarActaConEquipos(Acta acta, List<Equipo> equipos)
        {
            actaDao.EditarActaConEquipos(acta, equipos);
        }

        public void EliminarActa(int idActa)
        {
            actaDao.EliminarActa(idActa);
        }

        public Acta ObtenerActa(int idActa)
        {
            return actaDao.ObtenerActa(idActa);
        }
    }
}
