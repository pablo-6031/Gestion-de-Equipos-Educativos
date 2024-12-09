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
    public class PrestamoController
    {
        private PrestamoDao prestamoDao = new PrestamoDao();

        // Método para listar todos los préstamos
        public DataTable ListarPrestamos()
        {
            return prestamoDao.ListarPrestamos();
        }

        public DataTable FiltrarPrestamos(string texto)
        {
            return prestamoDao.FiltrarPrestamos(texto);
        }

        

        // Método para agregar un nuevo préstamo
        public void AgregarPrestamo(Prestamo prestamo, List<Equipo> equipos)
        {
            prestamoDao.AgregarPrestamo(prestamo, equipos);
        }

        // Método para editar un préstamo existente
        public void EditarPrestamo(Prestamo prestamo)
        {
            prestamoDao.EditarPrestamo(prestamo);
        }


        // Método para obtener detalles de un préstamo específico
        public Prestamo ObtenerPrestamo(int idPrestamo)
        {
            return prestamoDao.ObtenerPrestamo(idPrestamo);
        }
    }
}
