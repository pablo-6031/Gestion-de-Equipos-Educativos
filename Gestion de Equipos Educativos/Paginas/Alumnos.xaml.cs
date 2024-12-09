using Controllers;
using Entities;
using Gestion_de_Equipos_Educativos.Ventanas;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;


namespace Gestion_de_Equipos_Educativos.Paginas
{
    /// <summary>
    /// Lógica de interacción para Alumnos.xaml
    /// </summary>
    public partial class Alumnos : Page
    {
        InstitucionController institucionController = new InstitucionController();
        AlumnoController alumnoController = new AlumnoController();
        private Dictionary<int, string> institucion = new Dictionary<int, string>();
        private string imageSource = null;
        byte[] FotoAlumno;

        public Alumnos(string rol)
        {
            InitializeComponent();
            ListarAlumnos();
           // LlenarComboBox();
        }


        private void btnAgregarAlumno_Click(object sender, RoutedEventArgs e)
        {
            mostrarVentana("agregar");
            ListarAlumnos();

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
            VentanaAlumnos ventanaAlumno = new VentanaAlumnos(opcion)
            {
                Owner = mainWindow // Establece el propietario
            };

            ventanaAlumno.ShowDialog(); // Abre la ventana

           

            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }



        private void ListarAlumnos()
        {
            // Obtenemos la lista de alumnos desde la base de datos
            DataTable dataTable = alumnoController.listarAlumnos();

            // Verificamos que el DataTable tenga datos
            if (dataTable.Rows.Count > 0)
            {
                // Asigna el DataTable como fuente de datos de un control (por ejemplo, DataGrid)
                DGAlumnos.ItemsSource = dataTable.DefaultView;
            }
            else
            {

            }
        }






        private void DGAlumnos_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (!DGAlumnos.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGAlumnos.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                AlumnoCache.IdAlumno = (int)rowView["id_alumno"];
                AlumnoCache.Nombres = rowView["nombres"].ToString();
                AlumnoCache.Apellidos = rowView["apellidos"].ToString();
                AlumnoCache.Curso = rowView["curso"].ToString();
                AlumnoCache.Cuil = rowView["cuil"].ToString();
                AlumnoCache.Telefono = rowView["telefono"].ToString();
                AlumnoCache.IdInstitucion = (int)rowView["id_institucion"];
                AlumnoCache.Foto =  rowView["foto"] != DBNull.Value ? (byte[])rowView["foto"] : null;

                mostrarVentana("editar");
            }
        }

        private void btnVerrFila_Click(object sender, RoutedEventArgs e)
        {
            if (!DGAlumnos.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGAlumnos.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                AlumnoCache.IdAlumno = (int)rowView["id_alumno"];
                AlumnoCache.Nombres = rowView["nombres"].ToString();
                AlumnoCache.Apellidos = rowView["apellidos"].ToString();
                AlumnoCache.Curso = rowView["curso"].ToString();
                AlumnoCache.Cuil = rowView["cuil"].ToString();
                AlumnoCache.Telefono = rowView["telefono"].ToString();
                AlumnoCache.IdInstitucion = (int)rowView["id_institucion"];
                AlumnoCache.Foto = rowView["foto"] != DBNull.Value ? (byte[])rowView["foto"] : null;

                mostrarVentana("ver");
            }
            ListarAlumnos();
        }


        

        private void btnEditarFila_Click(object sender, RoutedEventArgs e)
        {
            if (!DGAlumnos.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGAlumnos.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                AlumnoCache.IdAlumno = (int)rowView["id_alumno"];
                AlumnoCache.Nombres = rowView["nombres"].ToString();
                AlumnoCache.Apellidos = rowView["apellidos"].ToString();
                AlumnoCache.Curso = rowView["curso"].ToString();
                AlumnoCache.Cuil = rowView["cuil"].ToString();
                AlumnoCache.Telefono = rowView["telefono"].ToString();
                AlumnoCache.IdInstitucion = (int)rowView["id_institucion"];
                AlumnoCache.Foto = rowView["foto"] != DBNull.Value ? (byte[])rowView["foto"] : null;

                mostrarVentana("editar");
            }
            ListarAlumnos();
        }

        private void btnEliminarrFila_Click(object sender, RoutedEventArgs e)
        {
            if (DGAlumnos.SelectedItem != null)
            {

                DataRowView rowView = (DataRowView)DGAlumnos.SelectedItem;
                int idAlumno = Convert.ToInt32(rowView["id_alumno"]);

                try
                {

                    string mensaje = "¿Desea eliminar a " + rowView["nombres"].ToString() + " " + rowView["apellidos"].ToString() + " del registro?";
                    bool resp = mostrarVentanaEleccion(mensaje, "ACEPTAR", "CANCELAR");
                    if (resp)
                    {
                        alumnoController.eliminarAlumno(idAlumno);
                        string mensaj = "Alumno eliminado con éxito";
                        mostrarVentana(mensaj, "Eliminar");
                        ListarAlumnos();
                    }
                    
                }
                catch (Exception ex)
                {
                    string mensaj = "Error al eliminar el alumno";
                    mostrarVentanaError(mensaj, "Eliminar");
                }
            }
            else
            {
                string mensaj = "Seleccione un alumno para eliminar";
                mostrarVentanaError(mensaj, "Advertencia");
            }
            ListarAlumnos();
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
            VentanaMensajeEleccion ventanaMensaje = new VentanaMensajeEleccion(mensaje, boton1, boton2)
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

        private void mostrarVentana(string mensaje, string titulo)
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
                ListarAlumnos();
            }
            else
            {

                // Llamar al controlador para obtener los datos filtrados

                DataTable dataTable = alumnoController.FiltrarAlumnos(textoBusqueda);

                // Actualizar el DataGrid
                DGAlumnos.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
