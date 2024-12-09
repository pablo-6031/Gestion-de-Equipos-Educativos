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
    /// Lógica de interacción para Proveedores.xaml
    /// </summary>
    public partial class Proveedores : Page
    {
        ProveedorController proveedorController = new ProveedorController();
        int IdProveedor = 0;
        public Proveedores()
        {
            InitializeComponent();
            ListarProveedores();
            ColumnaDatos.Width = new GridLength(0);
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verifica que los campos requeridos no estén vacíos
                if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtCorreo.Text))
                {
                    // Crea un nuevo objeto de tipo Proveedor y asigna valores a sus propiedades
                    Proveedor proveedor = new Proveedor
                    {
                        Nombre = txtNombre.Text,
                        Telefono = txtTelefono.Text,
                        Correo = txtCorreo.Text,
                        Direccion = txtDireccion.Text,

                    };

                    try
                    {
                        // Guarda el proveedor en la base de datos
                        proveedorController.AgregarProveedor(proveedor);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error al guardar el proveedor", "Error");
                    }
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

            // Actualiza la lista de proveedores y limpia los campos
            ListarProveedores();
            LimpiarCampos();
        }
        private void ListarProveedores()
        {
            // Obtenemos la lista de proveedores desde la base de datos
            DataTable dataTable = proveedorController.listarProveedores();

            // Verificamos que el DataTable tenga datos
            if (dataTable.Rows.Count > 0)
            {
                // Asigna el DataTable como fuente de datos de un control (por ejemplo, DataGrid)
                DGProveedores.ItemsSource = dataTable.DefaultView;
            }

        }

        private void LimpiarCampos()
        {
            // Limpia los TextBox
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtDireccion.Text = "";
            txtJurisdiccion.Text = "";

            ColumnaDatos.Width = new GridLength(0);

        }



        private void DGProveedores_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!DGProveedores.Items.IsEmpty)
            {
                ColumnaDatos.Width = new GridLength(700, GridUnitType.Pixel);
                DataRowView rowView = (DataRowView)DGProveedores.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz

                txtNombre.Text = rowView["nombre"].ToString();
                txtTelefono.Text = rowView["telefono"].ToString();
                txtCorreo.Text = rowView["correo"].ToString();
                txtDireccion.Text = rowView["direccion"].ToString();

            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (IdProveedor == 0)
                {
                    // Verifica que los campos requeridos no estén vacíos
                    if (!string.IsNullOrEmpty(txtNombre.Text) )
                    {
                        // Crea un nuevo objeto de tipo Institucion y asigna valores a sus propiedades
                        Proveedor proveedor = new Proveedor
                        {
                            Nombre = txtNombre.Text,
                            Jurisdiccion = txtJurisdiccion.Text,
                            Telefono = txtTelefono.Text,
                            Correo = txtCorreo.Text,
                            Direccion = txtDireccion.Text,
                            
                        };

                        try
                        {
                            // Guarda la institución en la base de datos
                            string mensaj = proveedorController.AgregarProveedor(proveedor);
                            mostrarVentana(mensaj, "Agregar");
                        }
                        catch (Exception)
                        {
                            string mensaj = "Error al guardar el proveedor";
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
                    Proveedor Proveedor = new Proveedor
                    {
                        IdProveedor = IdProveedor,
                        Nombre = txtNombre.Text,
                        Jurisdiccion = txtJurisdiccion.Text,
                        Telefono = txtTelefono.Text,
                        Correo = txtCorreo.Text,
                        Direccion = txtDireccion.Text,
                        
                    };

                    try
                    {
                        string mensaj = proveedorController.EditarProveedor(Proveedor);
                        mostrarVentana(mensaj, "Editar");
                        ListarProveedores();
                    }
                    catch (Exception ex)
                    {
                        string mensaj = "Error al actualizar el proveedor";
                        mostrarVentanaError(mensaj, "Error");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            // Actualiza la lista de instituciones y limpia los campos
            ListarProveedores();
            LimpiarCampos();
            soloLectura(false);
            IdProveedor = 0;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            soloLectura(false);
            LimpiarCampos();
            IdProveedor = 0;
        }

        private void btnEditar_Click_1(object sender, RoutedEventArgs e)
        {
            if (!this.DGProveedores.Items.IsEmpty)
            {
                ColumnaDatos.Width = new GridLength(400, GridUnitType.Pixel);

                DataRowView rowView = (DataRowView)this.DGProveedores.SelectedItem;
                IdProveedor = (int)rowView["id_proveedor"];
                this.txtNombre.Text = rowView["nombre"].ToString();
                this.txtJurisdiccion.Text = rowView["jurisdiccion"].ToString();
                this.txtTelefono.Text = rowView["Telefono"].ToString();
                this.txtCorreo.Text = rowView["Correo"].ToString();
                this.txtDireccion.Text = rowView["Direccion"].ToString();
                

            }
        }

        private void btnEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            if (DGProveedores.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)DGProveedores.SelectedItem;
                int idProveedor = Convert.ToInt32(rowView["id_proveedor"]);

                try
                {
                    string mensaje = "¿Desea eliminar a " + rowView["nombre"].ToString() + " del registro?";
                    bool resp = mostrarVentanaEleccion(mensaje, "ACEPTAR", "CANCELAR");
                    if (resp)
                    {
                        proveedorController.EliminarProveedor(idProveedor);
                        string mensaj = "El proveedor fue eliminado con éxito";
                        mostrarVentana(mensaj, "Eliminar");

                    }


                }
                catch (Exception ex)
                {
                    string mensaj = "Error al eliminar el proveedor";
                    mostrarVentanaError(mensaj, "Error");
                }
            }
            else
            {
                string mensaj = "Seleccione un proveedor para eliminar";
                mostrarVentanaError(mensaj, "Advertencia");
            }
            ListarProveedores();
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

        private void btnVerrFila_Click(object sender, RoutedEventArgs e)
        {
            if (!this.DGProveedores.Items.IsEmpty)
            {
                ColumnaDatos.Width = new GridLength(400, GridUnitType.Pixel);

                DataRowView rowView = (DataRowView)this.DGProveedores.SelectedItem;

                this.txtNombre.Text = rowView["nombre"].ToString();
                this.txtJurisdiccion.Text = rowView["jurisdiccion"].ToString();
                this.txtTelefono.Text = rowView["Telefono"].ToString();
                this.txtCorreo.Text = rowView["Correo"].ToString();
                this.txtDireccion.Text = rowView["Direccion"].ToString();

                soloLectura(true);
            }
        }

        private void soloLectura(bool opcion)
        {
            txtNombre.IsReadOnly = opcion;
            txtJurisdiccion.IsReadOnly = opcion;
            txtTelefono.IsReadOnly = opcion;
            txtCorreo.IsReadOnly = opcion;
            txtDireccion.IsReadOnly = opcion;
            

        }

        private void btnAgregarProveedor_Click(object sender, RoutedEventArgs e)
        {
            ColumnaDatos.Width = new GridLength(400, GridUnitType.Pixel);
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textoBusqueda = txtBuscar.Text.Trim();
            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                // Si no hay texto, mostrar todos los docentes
                ListarProveedores();
            }
            else
            {

                // Llamar al controlador para obtener los datos filtrados

                DataTable dataTable = proveedorController.FiltrarProveedores(textoBusqueda);

                // Actualizar el DataGrid
                DGProveedores.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
