using System.Data;
using Models;
using Entities;

namespace Controllers
{
    public class ActaController
    {
        private ActaDao actaDao = new ActaDao();

        public DataTable ListarActas()
        {
            return actaDao.ListarActas();
        }

        public void AgregarActa(Acta acta)
        {
            actaDao.AgregarActa(acta);
        }

        public void EditarActa(Acta acta)
        {
            actaDao.EditarActa(acta);
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
