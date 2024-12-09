using Controllers;
using Entities;
using Gestion_de_Equipos_Educativos.Paginas;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Gestion_de_Equipos_Educativos.Paginas
{
    /// <summary>
    /// Lógica de interacción para Instituciones.xaml
    /// </summary>
    public partial class Instituciones : Page
    {
        InstitucionController institucionController = new InstitucionController();
        int IdInstitucion = 0;
        public Instituciones()
        {
            InitializeComponent();
            ListarInstituciones();
            ColumnaDatos.Width = new GridLength(0);
        }



        private void ListarInstituciones()
        {
            // Obtenemos la lista de instituciones desde la base de datos
            DataTable dataTable = institucionController.ListaInstituciones();

            // Verificamos que el DataTable tenga datos
            if (dataTable.Rows.Count > 0)
            {
                // Asigna el DataTable como fuente de datos de un control (por ejemplo, DataGrid)
                DGInstituciones.ItemsSource = dataTable.DefaultView;
            }

        }

        private void LimpiarCampos()
        {
            // Limpia los TextBox
            txtNombre.Text = "";
            txtCue.Text = "";
            txtTurno.Text = "";
            txtDirector.Text = "";
            txtCalle.Text = "";
            txtNumeroCalle.Text = "";
            txtBarrio.Text = "";
            txtLocalidad.Text = "";
            txtProvincia.Text = "";
            txtCP.Text = "";
            txtTelefono.Text = "";

            // Limpia ComboBox
            cmbNivel.SelectedIndex = -1;
            cmbRegion.SelectedIndex = -1;


            ColumnaDatos.Width = new GridLength(0);
        }

       
        

        private void DGInstituciones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!this.DGInstituciones.Items.IsEmpty )
            {
                DataRowView rowView = (DataRowView)this.DGInstituciones.SelectedItem;
                IdInstitucion = (int)rowView["id_institucion"];
                this.txtNombre.Text = rowView["nombre"].ToString();
                this.txtCue.Text = rowView["cue"].ToString();
                this.txtTurno.Text = rowView["turno"].ToString();
                this.txtDirector.Text = rowView["director"].ToString();
                this.cmbNivel.Text = rowView["nivel"].ToString();
                this.txtCalle.Text = rowView["calle"].ToString();
                this.txtNumeroCalle.Text = rowView["numerodecalle"].ToString();
                this.txtBarrio.Text = rowView["barrio"].ToString();
                this.txtLocalidad.Text = rowView["localidad"].ToString();
                this.txtProvincia.Text = rowView["provincia"].ToString();
                this.txtCP.Text = rowView["codigopostal"].ToString();
                this.cmbRegion.Text = rowView["region"].ToString();
                this.txtTelefono.Text = rowView["telefono"].ToString();

            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (IdInstitucion == 0)
                {
                    // Verifica que los campos requeridos no estén vacíos
                    if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtCue.Text))
                    {
                        // Crea un nuevo objeto de tipo Institucion y asigna valores a sus propiedades
                        Institucion institucion = new Institucion
                        {
                            Nombre = txtNombre.Text.ToUpper(),
                            Cue = txtCue.Text.ToUpper(),
                            Turno = txtTurno.Text.ToUpper(),
                            Director = txtDirector.Text.ToUpper(),
                            Nivel = cmbNivel.Text.ToUpper(),
                            Calle = txtCalle.Text.ToUpper(),
                            NumeroCalle = txtNumeroCalle.Text.ToUpper(),
                            Barrio = txtBarrio.Text.ToUpper(),
                            Localidad = txtLocalidad.Text.ToUpper(),
                            Provincia = txtProvincia.Text.ToUpper(),
                            CodigoPostal = txtCP.Text.ToUpper(),
                            region = cmbRegion.Text.ToUpper(),
                            Telefono = txtTelefono.Text.ToUpper()
                        };

                        try
                        {
                            // Guarda la institución en la base de datos
                            institucionController.AgregarInstitucion(institucion);
                            string mensaj = "Institución " + institucion.Nombre + " agregada con éxito";
                            mostrarVentana(mensaj, "Agregar");
                        }
                        catch (Exception)
                        {
                            string mensaj = "Error al guardar la institución";
                            mostrarVentanaError(mensaj, "Error");
                        }
                    }
                    else
                    {
                        string mensaj = "Complete los campos obligatorios";
                        mostrarVentanaError(mensaj, "Advertencia");
                    }
                }
                else
                {
                    Institucion institucion = new Institucion
                    {
                        IdInstitucion = IdInstitucion,
                        Nombre = txtNombre.Text.ToUpper(),
                        Cue = txtCue.Text.ToUpper(),
                        Turno = txtTurno.Text.ToUpper(),
                        Director = txtDirector.Text.ToUpper(),
                        Nivel = cmbNivel.Text.ToUpper(),
                        Calle = txtCalle.Text.ToUpper(),
                        NumeroCalle = txtNumeroCalle.Text.ToUpper(),
                        Barrio = txtBarrio.Text.ToUpper(),
                        Localidad = txtLocalidad.Text.ToUpper(),
                        Provincia = txtProvincia.Text.ToUpper(),
                        CodigoPostal = txtCP.Text.ToUpper(),
                        region = cmbRegion.Text.ToUpper(),
                        Telefono = txtTelefono.Text.ToUpper()
                    };

                    try
                    {
                        institucionController.EditarInstitucion(institucion);
                        string mensaj = "Institución actualizada con éxito";
                        mostrarVentana(mensaj, "Editar");
                        ListarInstituciones();
                    }
                    catch (Exception ex)
                    {
                        string mensaj = "Error al actualizar la institución";
                        mostrarVentanaError(mensaj, "Error");
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            // Actualiza la lista de instituciones y limpia los campos
            ListarInstituciones();
            LimpiarCampos();
            soloLectura(false);
            IdInstitucion = 0;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            soloLectura(false);
            LimpiarCampos();
            IdInstitucion = 0;
        }


        private void btnVerrFila_Click(object sender, RoutedEventArgs e)
        {
            if (!this.DGInstituciones.Items.IsEmpty)
            {
                ColumnaDatos.Width = new GridLength(400, GridUnitType.Pixel);

                DataRowView rowView = (DataRowView)this.DGInstituciones.SelectedItem;

                this.txtNombre.Text = rowView["nombre"].ToString();
                this.txtCue.Text = rowView["cue"].ToString();
                this.txtTurno.Text = rowView["turno"].ToString();
                this.txtDirector.Text = rowView["director"].ToString();
                this.cmbNivel.Text = rowView["nivel"].ToString();
                this.txtCalle.Text = rowView["calle"].ToString();
                this.txtNumeroCalle.Text = rowView["numerodecalle"].ToString();
                this.txtBarrio.Text = rowView["barrio"].ToString();
                this.txtLocalidad.Text = rowView["localidad"].ToString();
                this.txtProvincia.Text = rowView["provincia"].ToString();
                this.txtCP.Text = rowView["codigopostal"].ToString();
                this.cmbRegion.Text = rowView["region"].ToString();
                this.txtTelefono.Text = rowView["telefono"].ToString();

                soloLectura(true);
            }
        }

        private void soloLectura(bool opcion)
        {
            txtNombre.IsReadOnly = opcion;
            txtCue.IsReadOnly = opcion;
            txtTurno.IsReadOnly = opcion;
            txtDirector.IsReadOnly = opcion;
            cmbNivel.IsReadOnly = opcion;
            txtCalle.IsReadOnly = opcion;
            txtNumeroCalle.IsReadOnly = opcion;
            txtBarrio.IsReadOnly = opcion;
            txtLocalidad.IsReadOnly = opcion;
            txtProvincia.IsReadOnly = opcion;
            txtCP.IsReadOnly = opcion;
            txtTelefono.IsReadOnly = opcion;

        }

        private void btnEditarFila_Click(object sender, RoutedEventArgs e)
        {
            if (!this.DGInstituciones.Items.IsEmpty)
            {
                ColumnaDatos.Width = new GridLength(400, GridUnitType.Pixel);

                DataRowView rowView = (DataRowView)this.DGInstituciones.SelectedItem;
                IdInstitucion = (int)rowView["id_institucion"];
                this.txtNombre.Text = rowView["nombre"].ToString();
                this.txtCue.Text = rowView["cue"].ToString();
                this.txtTurno.Text = rowView["turno"].ToString();
                this.txtDirector.Text = rowView["director"].ToString();
                this.cmbNivel.Text = rowView["nivel"].ToString();
                this.txtCalle.Text = rowView["calle"].ToString();
                this.txtNumeroCalle.Text = rowView["numerodecalle"].ToString();
                this.txtBarrio.Text = rowView["barrio"].ToString();
                this.txtLocalidad.Text = rowView["localidad"].ToString();
                this.txtProvincia.Text = rowView["provincia"].ToString();
                this.txtCP.Text = rowView["codigopostal"].ToString();
                this.cmbRegion.Text = rowView["region"].ToString();
                this.txtTelefono.Text = rowView["telefono"].ToString();

            }
        }

        private void btnEliminarrFila_Click(object sender, RoutedEventArgs e)
        {
            if (DGInstituciones.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)DGInstituciones.SelectedItem;
                int idInstitucion = Convert.ToInt32(rowView["id_institucion"]);
                this.txtNombre.Text = rowView["nombre"].ToString();

                try
                {
                    string mensaje = "¿Desea eliminar a "+ rowView["nombre"].ToString() + " del registro?" ;
                    bool resp = mostrarVentanaEleccion(mensaje, "ACEPTAR", "CANCELAR");
                    if (resp)
                    {
                        institucionController.EliminarInstitucion(idInstitucion);
                        string mensaj = "Institución eliminada con éxito";
                        mostrarVentana(mensaj, "Eliminar");
                    }

                    
                }
                catch (Exception ex)
                {
                    string mensaj = "Error al eliminar la institución";
                    mostrarVentanaError(mensaj, "Error");
                }
            }
            else
            {
                string mensaj = "Seleccione una institución para eliminar";
                mostrarVentanaError(mensaj, "Advertencia");
            }
            ListarInstituciones();
        }

        private bool mostrarVentanaEleccion(string mensaje, string boton1, string boton2)
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
            VentanaMensaje ventanaMensaje = new VentanaMensaje(mensaje,titulo)
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

        private void btnAgregarInstitucion_Click(object sender, RoutedEventArgs e)
        {
            ColumnaDatos.Width = new GridLength(400, GridUnitType.Pixel);
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textoBusqueda = txtBuscar.Text.Trim();
            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                // Si no hay texto, mostrar todos los docentes
                ListarInstituciones();
            }
            else
            {

                // Llamar al controlador para obtener los datos filtrados

                DataTable dataTable = institucionController.FiltrarInstituciones(textoBusqueda);

                // Actualizar el DataGrid
                DGInstituciones.ItemsSource = dataTable.DefaultView;
            }

        }
    }
}
