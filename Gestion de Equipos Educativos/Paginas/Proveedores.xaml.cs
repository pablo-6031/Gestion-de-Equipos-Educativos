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
using System.Windows.Input;
using System.Windows.Media;
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
        public Proveedores()
        {
            InitializeComponent();
            ListarProveedores();
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
            else
            {

            }
        }

        private void LimpiarCampos()
        {
            // Limpia los TextBox
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtDireccion.Text = "";

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (DGProveedores.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)DGProveedores.SelectedItem;
                int idProveedor = Convert.ToInt32(rowView["id_proveedor"]);

                try
                {
                    proveedorController.EliminarProveedor(idProveedor);
                    MessageBox.Show("Proveedor eliminado con éxito", "Eliminar");
                    ListarProveedores();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el proveedor: " + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un proveedor para eliminar", "Advertencia");
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (DGProveedores.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)DGProveedores.SelectedItem;
                int idProveedor = Convert.ToInt32(rowView["id_proveedor"]);

                Proveedor proveedor = new Proveedor
                {
                    IdProveedor = idProveedor,
                    Nombre = txtNombre.Text,
                    Telefono = txtTelefono.Text,
                    Correo = txtCorreo.Text,
                    Direccion = txtDireccion.Text,
                };

                try
                {
                    proveedorController.EditarProveedor(proveedor);
                    MessageBox.Show("Proveedor actualizado con éxito", "Editar");
                    ListarProveedores();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el proveedor: " + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un proveedor para editar", "Advertencia");
            }
        }

        private void DGProveedores_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!DGProveedores.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGProveedores.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                txtIdProveedor.Text = rowView["id_proveedor"].ToString();
                txtNombre.Text = rowView["nombre"].ToString();
                txtTelefono.Text = rowView["telefono"].ToString();
                txtCorreo.Text = rowView["correo"].ToString();
                txtDireccion.Text = rowView["direccion"].ToString();

            }
        }
    }
}
