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
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Gestion_de_Equipos_Educativos.Ventanas;
using System.Windows.Media.Effects;

namespace Gestion_de_Equipos_Educativos.Paginas
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : Page
    {
        UsuarioController usuarioController = new UsuarioController();
        MemoryStream memoryStream;
        MemoryStream stream;
        private string imageSource = null;
        byte[] FotoUsuario;
        UsuarioDao usuarioDao = new UsuarioDao();
        //Mensaje mensaje = new Mensaje();
        //private static Configuracion _instancia = null;

        public Usuarios(string rol)
        {
            InitializeComponent();
            ListarUsuarios();
        }

        public Usuarios()
        {
            InitializeComponent();
            ListarUsuarios();
        }



        private void ListarUsuarios()
        {
            // Obtenemos la lista de alumnos desde la base de datos
            DataTable dataTable = usuarioController.listaUsuarios();

            // Verificamos que el DataTable tenga datos
            if (dataTable.Rows.Count > 0)
            {
                // Asigna el DataTable como fuente de datos de un control (por ejemplo, DataGrid)
                DGUsuarios.ItemsSource = dataTable.DefaultView;
            }
            else
            {

            }
        }






        private void btnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            mostrarVentana("agregar");
            ListarUsuarios();
        }



        private void btnVerrFila_Click(object sender, RoutedEventArgs e)
        {
            if (!DGUsuarios.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGUsuarios.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                UsuarioCache2.IdUsuario = (int)rowView["id_usuario"];
                UsuarioCache2.Nombre = rowView["nombres"].ToString();
                UsuarioCache2.Apellido = rowView["apellidos"].ToString();
                UsuarioCache2.Cuil = rowView["cuil"].ToString();
                UsuarioCache2.LoginName = rowView["loginName"].ToString();
                UsuarioCache2.Password = rowView["password"].ToString();
                UsuarioCache2.Correo = rowView["correo"].ToString();
                UsuarioCache2.Rol = rowView["rol"].ToString();
                UsuarioCache2.Foto = rowView["foto"] != DBNull.Value ? (byte[])rowView["foto"] : null;

                mostrarVentana("ver");
            }
            ListarUsuarios();
        }

        private void btnEditarFila_Click(object sender, RoutedEventArgs e)
        {
            if (!DGUsuarios.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGUsuarios.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                UsuarioCache2.IdUsuario = (int)rowView["id_usuario"];
                UsuarioCache2.Nombre = rowView["nombres"].ToString();
                UsuarioCache2.Apellido = rowView["apellidos"].ToString();
                UsuarioCache2.Cuil = rowView["cuil"].ToString();
                UsuarioCache2.LoginName = rowView["loginName"].ToString();
                UsuarioCache2.Password = rowView["password"].ToString();
                UsuarioCache2.Correo = rowView["correo"].ToString();
                UsuarioCache2.Rol = rowView["rol"].ToString();
                UsuarioCache2.Foto = rowView["foto"] != DBNull.Value ? (byte[])rowView["foto"] : null;

                mostrarVentana("editar");
            }
            ListarUsuarios();
        }

        private void btnEliminarrFila_Click(object sender, RoutedEventArgs e)
        {
            if (DGUsuarios.SelectedItem != null)
            {

                DataRowView rowView = (DataRowView)DGUsuarios.SelectedItem;
                int idUsuario = Convert.ToInt32(rowView["id_usuario"]);

                try
                {

                    string mensaje = "¿Desea eliminar a " + rowView["nombres"].ToString() + " " + rowView["apellidos"].ToString() + " del registro?";
                    bool resp = mostrarVentanaEleccion(mensaje, "ACEPTAR", "CANCELAR");
                    if (resp)
                    {
                        usuarioController.eliminarUsuario(idUsuario);
                        string mensaj = "Usuario eliminado con éxito";
                        mostrarVentana(mensaj, "Eliminar");
                        ListarUsuarios();
                    }

                }
                catch (Exception ex)
                {
                    string mensaj = "Error al eliminar el usuario";
                    mostrarVentanaError(mensaj, "Eliminar");
                }
            }
            else
            {
                string mensaj = "Seleccione un usuario para eliminar";
                mostrarVentanaError(mensaj, "Advertencia");
            }
            ListarUsuarios();
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
            VentanaUsuarios ventanaUsuario = new VentanaUsuarios(opcion)
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
                ListarUsuarios();
            }
            else
            {

                // Llamar al controlador para obtener los datos filtrados

                DataTable dataTable = usuarioController.FiltrarUsuarios(textoBusqueda);

                // Actualizar el DataGrid
                DGUsuarios.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
