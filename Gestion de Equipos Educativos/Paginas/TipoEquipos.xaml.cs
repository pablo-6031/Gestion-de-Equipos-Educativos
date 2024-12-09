using Controllers;
using Entities;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Models;
using System.Drawing.Imaging;
using Microsoft.Win32;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Gestion_de_Equipos_Educativos.Ventanas;
using System.Windows.Media.Effects;



namespace Gestion_de_Equipos_Educativos.Paginas
{

    public partial class TipoEquipos : Page
    {
        TipoEquipoController tipoEquipoController = new TipoEquipoController();
        MemoryStream memoryStream;
        MemoryStream stream;
        private string imageSource = null;
        byte[] FotoTipoEquipo;



        public TipoEquipos()
        {
            InitializeComponent();
            ListarTipoEquipo();
        }




        private void ListarTipoEquipo()
        {
            // Obtenemos la lista de alumnos desde la base de datos
            DataTable dataTable = tipoEquipoController.ListaTipoEquipos();

            // Verificamos que el DataTable tenga datos
            if (dataTable.Rows.Count > 0)
            {
                // Asigna el DataTable como fuente de datos de un control (por ejemplo, DataGrid)
                DGTipoEquipos.ItemsSource = dataTable.DefaultView;
            }
            else
            {

            }
        }



        private void btnAgregarTipoEquipo_Click(object sender, RoutedEventArgs e)
        {
            mostrarVentana("agregar");
            ListarTipoEquipo();
        }

       

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {



            if (!DGTipoEquipos.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGTipoEquipos.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                TipoEquipoCache.IdTipoEquipo = (int)rowView["id_tipo_equipo"];
                TipoEquipoCache.Tipo = rowView["tipo"].ToString();
                TipoEquipoCache.Marca = rowView["marca"].ToString();
                TipoEquipoCache.Modelo = rowView["modelo"].ToString();
                TipoEquipoCache.Detalle = rowView["detalle_tecnico"].ToString();
                TipoEquipoCache.Foto = rowView["foto"] != DBNull.Value ? (byte[])rowView["foto"] : null;

                mostrarVentana("editar");
            }
            ListarTipoEquipo();

        }
    


        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (DGTipoEquipos.SelectedItem != null)
            {

                DataRowView rowView = (DataRowView)DGTipoEquipos.SelectedItem;
                int idTipoEquipo = Convert.ToInt32(rowView["id_tipo_equipo"]);

                try
                {

                    string mensaje = "¿Desea eliminar a " + rowView["tipo"].ToString() + " " + rowView["marca"].ToString() + " del registro?";
                    bool resp = mostrarVentanaEleccion(mensaje, "ACEPTAR", "CANCELAR");
                    if (resp)
                    {
                        tipoEquipoController.EliminarTipoEquipo(idTipoEquipo);
                        string mensaj = "Tipo de equipo eliminado con éxito";
                        mostrarVentanaMensaje(mensaj, "Eliminar");
                        ListarTipoEquipo();
                    }

                }
                catch (Exception ex)
                {
                    string mensaj = "Error al eliminar el tipo de equipo";
                    mostrarVentanaError(mensaj, "Eliminar");
                }
            }
            else
            {
                string mensaj = "Seleccione un tipo de equipo para eliminar";
                mostrarVentanaError(mensaj, "Advertencia");
            }
            ListarTipoEquipo();
        }




        private void mostrarVentana(string opcion)
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
            VentanaTipoEquipo ventanaUsuario = new VentanaTipoEquipo(opcion)
            {
                Owner = mainWindow // Establece el propietario
            };

            ventanaUsuario.ShowDialog(); // Abre la ventana



            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private bool mostrarVentanaEleccion(string mensaje, string boton1, string boton2)
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

            // Crea una instancia del modal y envía un parámetro
            VentanaMensajeEleccion ventanaMensaje = new VentanaMensajeEleccion(mensaje,boton1,boton2)
            {
                Owner = mainWindow, // Establece el propietario
            };

            // Abre la ventana
            bool? dialogResult = ventanaMensaje.ShowDialog();



            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
            return ventanaMensaje.Eleccion;
        }

        private void mostrarVentanaMensaje(string mensaje, string titulo)
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
            VentanaMensaje ventanaMensaje = new VentanaMensaje(mensaje, titulo)
            {
                Owner = mainWindow // Establece el propietario
            };

            ventanaMensaje.ShowDialog(); // Abre la ventana


            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private void mostrarVentanaError(string mensaje, string titulo)
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
            VentanaMensajeError ventanaMensaje = new VentanaMensajeError(mensaje, titulo)
            {
                Owner = mainWindow // Establece el propietario
            };

            ventanaMensaje.ShowDialog(); // Abre la ventana


            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textoBusqueda = txtBuscar.Text.Trim();
            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                // Si no hay texto, mostrar todos los docentes
                ListarTipoEquipo();
            }
            else
            {

                // Llamar al controlador para obtener los datos filtrados

                DataTable dataTable = tipoEquipoController.FiltrarTipoEquipos(textoBusqueda);

                // Actualizar el DataGrid
                DGTipoEquipos.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
