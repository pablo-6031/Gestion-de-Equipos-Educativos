using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Models;
using System.Data;

namespace Controllers
{
    public class InstitucionController
    {
        private InstitucionDao institucionDao = new InstitucionDao();
        
        public DataTable ListaInstituciones()
        {
            return institucionDao.ListarInstituciones();
        }

        public void AgregarInstitucion(Institucion institucion)
        {
            institucionDao.AgregarInstitucion(institucion);
        }

        public void EditarInstitucion(Institucion institucion)
        {
            institucionDao.EditarInstitucion(institucion);
        }

        public void EliminarInstitucion(int id)
        {
            institucionDao.EliminarInstitucion(id);
        }
    }
}
