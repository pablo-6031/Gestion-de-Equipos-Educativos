using System.Data;
using Models;
using Entities;

namespace Controllers
{
    public class AdMovilController
    {
        private AdMovilDao adMovilDao = new AdMovilDao();

        public DataTable ListarAdMoviles()
        {
            return adMovilDao.ListarAdMoviles();
        }

        public void AgregarAdMovil(AdMovil adMovil)
        {
            adMovilDao.AgregarAdMovil(adMovil);
        }

        public void EditarAdMovil(AdMovil adMovil)
        {
            adMovilDao.EditarAdMovil(adMovil);
        }

        public void EliminarAdMovil(int idAdMovil)
        {
            adMovilDao.EliminarAdMovil(idAdMovil);
        }

        public byte[] ObtenerFoto(int idAdMovil)
        {
            return adMovilDao.ObtenerFoto(idAdMovil);
        }
    }
}
