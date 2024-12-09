using Controllers;
using Entities;
using Gestion_de_Equipos_Educativos.Ventanas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gestion_de_Equipos_Educativos.Paginas
{
    /// <summary>
    /// Lógica de interacción para Prestamos.xaml
    /// </summary>
    public partial class Prestamos : Page
    {
        PrestamoController prestamoController = new PrestamoController();
        public Prestamos()
        {
            InitializeComponent();
            listarPrestamos();

        }

        private void listarPrestamos()
        {
            DataTable dtPrestamo = prestamoController.ListarPrestamos();

            DGPrestamos.ItemsSource = dtPrestamo.DefaultView;
            /*
            List<Prestamo> ListaPrestamo = ConvertirDataTableEnLista(dtPrestamo);
            if (ListaPrestamo.Count > 0)
            {
                DGPrestamos.ItemsSource = null;
                DGPrestamos.ItemsSource = ListaPrestamo;
            }
            */
        }
        public List<Prestamo> ConvertirDataTableEnLista(DataTable dataTable)
        {
            var listaPrestamo = new List<Prestamo>();

            foreach (DataRow fila in dataTable.Rows)
            {
                var prestamo = new Prestamo
                {
                    IdPrestamo = Convert.ToInt32(fila["IdPrestamo"]),
                    Nombre = fila["NombrePrestamo"].ToString(),
                    Apellido = fila["apellido"].ToString(),
                    FechaPrestamo = DateTime.Parse(fila["fecha_prestamo"].ToString()),
                    Dni = fila["dni"].ToString(),
                    Funcion = fila["funcion"].ToString(),
                    
                };

                listaPrestamo.Add(prestamo);
            }

            return listaPrestamo;
        }

        private void btnAgregarPrestamo_Click(object sender, RoutedEventArgs e)
        {
            mostrarVentana();
        }

        private void mostrarVentana()
        {
            // Referencia a la ventana principal
            var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;

            // Aplica el desenfoque al contenido principal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = new BlurEffect
                {
                    Radius = 10
                };
            }
            // Crea una instancia del modal
            VentanaPrestamos ventanaPrestamo = new VentanaPrestamos
            {
                Owner = mainWindow // Establece el propietario
            };

            ventanaPrestamo.ShowDialog(); // Abre la ventana



            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            Reportes.FormReporteSinide fru = new Reportes.FormReporteSinide();
            fru.IdPrestamo = 9;
            fru.ShowDialog();
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textoBusqueda = txtBuscar.Text.Trim();
            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                // Si no hay texto, mostrar todos los docentes
                listarPrestamos();
            }
            else
            {

                // Llamar al controlador para obtener los datos filtrados

                DataTable dataTable = prestamoController.FiltrarPrestamos(textoBusqueda);

                // Actualizar el DataGrid
                DGPrestamos.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
