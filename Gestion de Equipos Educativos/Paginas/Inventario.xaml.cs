using Controllers;
using Entities;
using Gestion_de_Equipos_Educativos.Ventanas;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Gestion_de_Equipos_Educativos.Paginas
{
    /// <summary>
    /// Lógica de interacción para Inventario.xaml
    /// </summary>
    public partial class Inventario : Page
    {
        private string imageSource = null;
        private int idTipoEquipoSeleccionado ;
       TipoEquipoController tipoEquipoController = new TipoEquipoController();
       EquipoController equipoController = new EquipoController();
        public Inventario()
        {
            InitializeComponent();

            ListarEquipos();
        }
        private Dictionary<int, string> tiposEquipos = new Dictionary<int, string>();

        


        private void btnCargarImagen_Click(object sender, RoutedEventArgs e)
        {

        }



        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mostrarVentana();
                if (EquipoCache.NumSerie != "")
                {
                    Equipo equipo = new Equipo();
                    equipo.NumSerie = EquipoCache.NumSerie;
                    equipo.Matricula = EquipoCache.Matricula;
                    equipo.Estado = EquipoCache.Estado;
                    equipo.Observacion = EquipoCache.Observacion;
                    equipo.Destino = EquipoCache.Destino;
                    equipo.IdTipoEquipo = EquipoCache.IdTipoEquipo;
                    equipo.IdActa = 1;

                    equipoController.agregarEquipo(equipo);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error");
            }

            // Actualiza la lista de equipos y limpia los campos
            ListarEquipos();
  


        }
        

        private void ListarEquipos()
        {
            // Obtenemos la lista de equipos desde la base de datos
            DataTable dataTable = equipoController.listaEquipos();

            // Verificamos que el DataTable tenga datos
            if (dataTable.Rows.Count > 0)
            {
                // Asigna el DataTable como fuente de datos de un control (por ejemplo, DataGrid o ListBox)
                DGEquipos.ItemsSource = dataTable.DefaultView; // Suponiendo que tienes un DataGrid llamado 'dataGridEquipos'
            }
            else
            {

            }
        }


        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

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
            NuevosEquipos nuevosEquipos = new NuevosEquipos
            {
                Owner = mainWindow // Establece el propietario
            };

            nuevosEquipos.ShowDialog(); // Abre la ventana


            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private void mostrarVentanaCargada(Equipo equipo)
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
            NuevosEquipos nuevosEquipos = new NuevosEquipos
            {
                Owner = mainWindow // Establece el propietario
            };

            nuevosEquipos.ShowDialog(); // Abre la ventana


            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private void DGEquipos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!this.DGEquipos.Items.IsEmpty)
            {
                Equipo equipo = (Equipo)this.DGEquipos.CurrentItem;
                mostrarVentanaCargada(equipo);

            }
        }
    }
}
