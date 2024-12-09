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
using Button = System.Windows.Controls.Button;



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
        public Inventario(string rol)
        {
            InitializeComponent();

            ListarEquipos();
        }
        private Dictionary<int, string> tiposEquipos = new Dictionary<int, string>();



        private void ListarEquipos()
        {
            // Obtenemos la lista de alumnos desde la base de datos
            DataTable dataTable = equipoController.ListarEquiposConTipo();

            // Verificamos que el DataTable tenga datos
            if (dataTable.Rows.Count > 0)
            {
                // Asigna el DataTable como fuente de datos de un control (por ejemplo, DataGrid)
                DGEquipos.ItemsSource = dataTable.DefaultView;
            }
            else
            {

            }
        }



        /*
        private void ListarEquipos()
        {

            DataTable dtEquipos = equipoController.listaEquipos();
            List<Equipo> ListaEquipo = ConvertirDataTableEnListaDeEquipos(dtEquipos);
            if (ListaEquipo.Count > 0)
            {
                DGEquipos.ItemsSource = null;
                DGEquipos.ItemsSource = ListaEquipo;
            }


        }


        public List<Equipo> ConvertirDataTableEnListaDeEquipos(DataTable dataTable)
        {
            var listaEquipos = new List<Equipo>();

            foreach (DataRow fila in dataTable.Rows)
            {
                var equipo = new Equipo
                {
                    IdEquipo = Convert.ToInt32(fila["id_equipo"]),
                    NumSerie = fila["num_serie"].ToString(),
                    Matricula = fila["matricula"].ToString(),
                    Estado = fila["estado"].ToString(),
                    Observacion = fila["observacion"].ToString(),
                    Destino = fila["destino"].ToString(),
                    IdTipoEquipo = Convert.ToInt32(fila["id_tipo_equipo"]),
                    IdActa = Convert.ToInt32(fila["id_acta"])
                };

                listaEquipos.Add(equipo);
            }

            return listaEquipos;
        }
        */

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (!DGEquipos.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGEquipos.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                EquipoCache.IdUsuario = (int)rowView["id_usuario"];
                EquipoCache.Nombre = rowView["nombres"].ToString();
                EquipoCache.Apellido = rowView["apellidos"].ToString();
                EquipoCache.Cuil = rowView["cuil"].ToString();
                EquipoCache.LoginName = rowView["loginName"].ToString();
                EquipoCache.Password = rowView["password"].ToString();
                EquipoCache.Correo = rowView["correo"].ToString();
                EquipoCache.Rol = rowView["rol"].ToString();
                EquipoCache.Foto = rowView["foto"] != DBNull.Value ? (byte[])rowView["foto"] : null;

                mostrarVentana("editar");
            }
            ListarUsuarios();

            */
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
            NuevosEquipos nuevosEquipos = new NuevosEquipos("ver")
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
            NuevosEquipos nuevosEquipos = new NuevosEquipos("ver")
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
            if (DGEquipos.SelectedItem is Entities.Equipo equipoSeleccionado)
            {
                // Cargar los datos del acta seleccionada en los controles
                EquipoCache.IdEquipo = equipoSeleccionado.IdEquipo;
                EquipoCache.NumSerie = equipoSeleccionado.NumSerie;
                EquipoCache.Matricula = equipoSeleccionado.Matricula;
                EquipoCache.Estado = equipoSeleccionado.Estado;
                EquipoCache.Observacion = equipoSeleccionado.Observacion;
                EquipoCache.Destino = equipoSeleccionado.Destino;
                EquipoCache.IdTipoEquipo = equipoSeleccionado.IdTipoEquipo;

            }

            mostrarDetalleEquipo();
        }

        private void btnVerEquipo_Click(object sender, RoutedEventArgs e)
        {
            if (!DGEquipos.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGEquipos.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                EquipoCache.IdEquipo = (int)rowView["id_equipo"];

                mostrarDetalleEquipo();
            }
            ListarEquipos();

        }



        private void mostrarDetalleEquipo()
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
            VentanaDetalleEquipo detalleEquipo = new VentanaDetalleEquipo
            {
                Owner = mainWindow // Establece el propietario
            };

            detalleEquipo.ShowDialog(); // Abre la ventana


            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private void btnEditarEquipo_Click(object sender, RoutedEventArgs e)
        {

            if (!DGEquipos.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGEquipos.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                EquipoCache.IdEquipo = (int)rowView["id_equipo"];
                EquipoCache.NumSerie = rowView["num_serie"].ToString();
                EquipoCache.Matricula = rowView["matricula"].ToString();
                EquipoCache.Estado = rowView["estado"].ToString();
                EquipoCache.Observacion = rowView["observacion"].ToString();
                EquipoCache.Destino = rowView["destino"].ToString();
                EquipoCache.IdTipoEquipo = (int)rowView["id_tipo_equipo"];


                mostrarVentana();
            }
            ListarEquipos();



        }

        private void btnEliminarEquipo_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que disparó el evento
            Button boton = sender as Button;

            // Obtener la fila (elemento de la lista) asociada al botón
            Equipo equipo = boton.DataContext as Equipo;

            if (equipo != null)
            {

                // Llamar al controlador para eliminar el acta de la base de datos
                try
                {
                    // equipoController.eliminarEquipo(equipo.IdEquipo);
                    if (equipo.IdActa != 0)
                    {
                        //string mensajes = equipoController.eliminarEquipo(equipo.IdEquipo);
                        //System.Windows.MessageBox.Show(mensajes, "Eliminar");

                    }
                    else
                    {
                        //ListaEquipoTemp.Remove();
                        ListarEquipos();
                    }


                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error al eliminar el equipo: " + ex.Message, "Error");
                }



                ListarEquipos();



            }

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
                ListarEquipos();
            }
            else
            {

                // Llamar al controlador para obtener los datos filtrados

                DataTable dataTable = equipoController.ListarEquiposporNumSerieCompleto(textoBusqueda);

                // Actualizar el DataGrid
                DGEquipos.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
