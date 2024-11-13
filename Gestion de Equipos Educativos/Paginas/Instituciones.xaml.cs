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
    /// Lógica de interacción para Instituciones.xaml
    /// </summary>
    public partial class Instituciones : Page
    {
        InstitucionController institucionController = new InstitucionController();
        public Instituciones()
        {
            InitializeComponent();
            ListarInstituciones();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verifica que los campos requeridos no estén vacíos
                if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtCue.Text))
                {
                    // Crea un nuevo objeto de tipo Institucion y asigna valores a sus propiedades
                    Institucion institucion = new Institucion
                    {
                        Nombre = txtNombre.Text,
                        Cue = txtCue.Text,
                        Turno = txtTurno.Text,
                        Director = txtDirector.Text,
                        Nivel = cmbNivel.Text,
                        Calle = txtCalle.Text,
                        NumeroCalle = txtNumeroCalle.Text,
                        Barrio = txtBarrio.Text,
                        Localidad = txtLocalidad.Text,
                        Provincia = txtProvincia.Text,
                        CodigoPostal = txtCP.Text,
                        region = cmbRegion.Text,
                        Telefono = txtTelefono.Text
                    };

                    try
                    {
                        // Guarda la institución en la base de datos
                        institucionController.AgregarInstitucion(institucion);
                        MessageBox.Show("Institución " + institucion.Nombre + " agregada con éxito", "Agregar");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error al guardar la institución", "Error");
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

            // Actualiza la lista de instituciones y limpia los campos
            ListarInstituciones();
            LimpiarCampos();
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
            else
            {

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
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (DGInstituciones.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)DGInstituciones.SelectedItem;
                int idInstitucion = Convert.ToInt32(rowView["id_institucion"]);

                try
                {
                    institucionController.EliminarInstitucion(idInstitucion);
                    MessageBox.Show("Institución eliminada con éxito", "Eliminar");
                    ListarInstituciones();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la institución: " + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Seleccione una institución para eliminar", "Advertencia");
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (DGInstituciones.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)DGInstituciones.SelectedItem;
                int idInstitucion = Convert.ToInt32(rowView["id_institucion"]);

                Institucion institucion = new Institucion
                {
                    IdInstitucion = idInstitucion,
                    Nombre = txtNombre.Text,
                    Cue = txtCue.Text,
                    Turno = txtTurno.Text,
                    Director = txtDirector.Text,
                    Nivel = cmbNivel.Text,
                    Calle = txtCalle.Text,
                    NumeroCalle = txtNumeroCalle.Text,
                    Barrio = txtBarrio.Text,
                    Localidad = txtLocalidad.Text,
                    Provincia = txtProvincia.Text,
                    CodigoPostal = txtCP.Text,
                    region = cmbRegion.Text,
                    Telefono = txtTelefono.Text
                };

                try
                {
                    institucionController.EditarInstitucion(institucion);
                    MessageBox.Show("Institución actualizada con éxito", "Editar");
                    ListarInstituciones();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la institución: " + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Seleccione una institución para editar", "Advertencia");
            }
        }

        private void DGInstituciones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!this.DGInstituciones.Items.IsEmpty )
            {
                DataRowView rowView = (DataRowView)this.DGInstituciones.SelectedItem;

                this.txtIdInstitucion.Text = rowView["id_institucion"].ToString();
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
    }
}
