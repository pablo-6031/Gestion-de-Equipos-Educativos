using Controllers;
using Entities;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Gestion_de_Equipos_Educativos.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaPrestamos.xaml
    /// </summary>
    public partial class VentanaPrestamos : Window
    {
        List<Equipo> ListaEquipoTemp = new List<Equipo>();
        PrestamoController prestamoController = new PrestamoController();
        public VentanaPrestamos()
        {
            InitializeComponent();

        }

     

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtApellido.Text) && !string.IsNullOrEmpty(txtDni.Text))
                {
                    Prestamo prestamo = new Prestamo
                    {
                        Nombre = txtNombre.Text.ToUpper(),
                        Apellido = txtApellido.Text.ToUpper(),
                        FechaPrestamo = dpFechaPrestamo.SelectedDate.HasValue ? dpFechaPrestamo.SelectedDate.Value : DateTime.Now,
                        Dni = txtDni.Text.ToUpper(),
                        Funcion = txtFuncion.Text.ToUpper(),
                    };

                    prestamoController.AgregarPrestamo(prestamo,ListaEquipoTemp);
                    MessageBox.Show("Equipo agregado con éxito", "Agregar");
                    
                }
                else
                {
                    MessageBox.Show("Complete los campos obligatorios", "Advertencia");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            dpFechaPrestamo.SelectedDate = null;
            txtApellido.Text = "";
            txtDni.Text = "";
            txtFuncion.Text = "";

        }

        private void btnAgregarEquipo_Click(object sender, RoutedEventArgs e)
        {
            mostrarVentana();
            if (EquipoCache.NumSerie != null && EquipoCache.Matricula != null && EquipoCache.Estado != null && EquipoCache.Observacion != null && EquipoCache.Destino != null)
            {


                if (!ListaEquipoTemp.Any(e2 => e2.NumSerie == EquipoCache.NumSerie))
                {
                    // Crear un nuevo equipo si no existe en la lista
                    Equipo equipo = new Equipo
                    {
                        IdEquipo=(int)EquipoCache.IdEquipo,
                        NumSerie = EquipoCache.NumSerie,
                        Matricula = EquipoCache.Matricula,
                        Estado = EquipoCache.Estado,
                        Observacion = EquipoCache.Observacion,
                        Destino = EquipoCache.Destino,
                        IdTipoEquipo = EquipoCache.IdTipoEquipo,
                        IdActa = 0
                    };

                    ListaEquipoTemp.Add(equipo); // Agregar a la lista temporal
                }
                else
                {
                    MessageBox.Show("El equipo con este número de serie ya está en la lista.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }

            ListarEquipos(ListaEquipoTemp);
        }

        private void ListarEquipos(List<Equipo> list)
        {

            if (list.Count > 0)
            {
                DGEquipos.ItemsSource = null;
                DGEquipos.ItemsSource = list;
            }
        }

        private void mostrarVentana()
        {
            // Referencia a la ventana principal
            var mainWindow = Application.Current.MainWindow as MainWindow;

            // Aplica el desenfoque al contenido principal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = new BlurEffect
                {
                    Radius = 10
                };
            }

            // Crea una instancia del modal
            VentanaSeleccionEquipo selecionEquipos = new VentanaSeleccionEquipo
            {
                Owner = mainWindow // Establece el propietario
            };

            selecionEquipos.ShowDialog(); // Abre la ventana


            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private void btnEliminarFilaEquipo_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que disparó el evento
            Button boton = sender as Button;

            // Obtener la fila (elemento de la lista) asociada al botón
            Equipo equipo = boton.DataContext as Equipo;
            // Eliminar el acta de la lista local y actualizar el DataGrid
            var listaEquipos = DGEquipos.ItemsSource as List<Equipo>;
            listaEquipos.Remove(equipo);

            ListarEquipos(listaEquipos);
        }
    }
}
