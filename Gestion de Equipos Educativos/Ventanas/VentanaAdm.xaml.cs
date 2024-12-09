using Controllers;
using Entities;
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
using System.Windows.Shapes;

namespace Gestion_de_Equipos_Educativos.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaAdm.xaml
    /// </summary>
    public partial class VentanaAdm : Window
    {
        int idAdm = 0;  
        EquipoController equipoController = new EquipoController();
        public VentanaAdm()
        {
            InitializeComponent();
            cargarCampos();
           

        }

        private void cargarCampos()
        {
            Titulo.Content = EquipoCache.Modelo;
            txtMatricula.Text = EquipoCache.Matricula;
            txtNumeroActa.Text = EquipoCache.NumSerie;
            txtObservacion.Text = EquipoCache.Observacion;
            txtEstado.Text = EquipoCache.Estado;
            if (EquipoCache.IdEquipo != 0 && EquipoCache.IdEquipo != null)
            {
                idAdm = (int)EquipoCache.IdEquipo;
            }
            ListarEquipos(idAdm);

        }

        private void btnAgregarActa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAgregarEquipo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarEquipoCache();
            NuevosEquipos nuevosEquipos = new NuevosEquipos("agregarEnAdm");
            nuevosEquipos.ShowDialog(); // Abre la ventana
            //mostrarVentana();

            cargarEnDB();
        }

        private void cargarEnDB()
        {
            if (EquipoCache.IdTipoEquipo != 0 && EquipoCache.Destino != null)
            {
                Equipo equipo = new Equipo();
                equipo.NumSerie = EquipoCache.NumSerie;
                equipo.Matricula = EquipoCache.Matricula;
                equipo.Estado = EquipoCache.Estado;
                equipo.Observacion = EquipoCache.Observacion;
                equipo.Destino = EquipoCache.Destino;
                equipo.IdTipoEquipo = EquipoCache.IdTipoEquipo;
                equipo.TipoEquipo = EquipoCache.TipoEquipo;
                equipo.IdActa = ActaCache.IdActa;

                equipoController.AgregarEquipo(equipo, idAdm);

                string mensaj = "Equipo agregado con éxito";
                mostrarVentana(mensaj, "AGREGAR");

                ListarEquipos(idAdm);



            }

            LimpiarEquipoCache();
        }


        private void ListarEquipos(int id)
        {
            // Obtenemos la lista de alumnos desde la base de datos
            DataTable dataTable = equipoController.ListarEquiposPorADMovil(id);

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

        private void LimpiarEquipoCache()
        {
            EquipoCache.IdEquipo = 0;
            EquipoCache.NumSerie = "";
            EquipoCache.Matricula = "";
            EquipoCache.Estado = "";
            EquipoCache.Observacion = "";
            EquipoCache.Destino = "";
            EquipoCache.Modelo = "";
            EquipoCache.IdTipoEquipo = 0;
        }

        private void mostrarVentana(string opcion)
        {
            // Referencia a la ventana principal
            var mainWindow = Application.Current.MainWindow as MainWindow;

            // Crea una instancia del modal
            NuevosEquipos nuevosEquipos = new NuevosEquipos(opcion)
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

        private void btnVerrFila_Click(object sender, RoutedEventArgs e)
        {
            if (!DGEquipos.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGEquipos.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                EquipoCache.IdEquipo = (int)rowView["id_equipo"];

                mostrarDetalleEquipo();
            }
            ListarEquipos(idAdm);
        }

        private void btnEditarFila_Click(object sender, RoutedEventArgs e)
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


                mostrarVentana("editar");
            }
            ListarEquipos(idAdm);
        }

        private void btnEliminarrFila_Click(object sender, RoutedEventArgs e)
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
                        ListarEquipos(idAdm);
                    }


                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error al eliminar el equipo: " + ex.Message, "Error");
                }



                ListarEquipos(idAdm);



            }
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
    }
}
