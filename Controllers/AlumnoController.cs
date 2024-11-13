using System.Data;
using Models;
using Entities;

namespace Controllers
{
    public class AlumnoController
    {
        private AlumnoDao alumnoDao = new AlumnoDao();

        public DataTable listarAlumnos()
        {
            return alumnoDao.ListarAlumnos();
        }

        public void agregarAlumno(Alumno alumno)
        {
            alumnoDao.AgregarAlumno(alumno);
        }

        public void editarAlumno(Alumno alumno)
        {
            alumnoDao.EditarAlumno(alumno);
        }

        public void eliminarAlumno(int idAlumno)
        {
            alumnoDao.EliminarAlumno(idAlumno);
        }

        public byte[] obtenerFoto(int idAlumno)
        {
            return alumnoDao.ObtenerFoto(idAlumno);
        }
    }
}
