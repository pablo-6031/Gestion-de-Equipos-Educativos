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
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Gestion_de_Equipos_Educativos.Paginas
{
    /// <summary>
    /// Lógica de interacción para Actas.xaml
    /// </summary>
    public partial class Actas : Page
    {
        InstitucionController institucionController = new InstitucionController();
        ProveedorController proveedorController = new ProveedorController();
        ActaController actaController = new ActaController();

        private Dictionary<int, string> institucion = new Dictionary<int, string>();
        private Dictionary<int, string> proveedor = new Dictionary<int, string>();
        private Dictionary<int, string> unidad = new Dictionary<int, string>();

        private string imageSource = null;
        byte[] FotoActa;

        public Actas()
        {
            InitializeComponent();
            ListarActas();
            LlenarComboBox();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNumeroActa.Text) && !string.IsNullOrEmpty(txtResponsable.Text))
                {
                    Acta acta = new Acta
                    {
                        NumeroActa = txtNumeroActa.Text,
                        NumeroExpediente = txtNumeroExpediente.Text,
                        FechaEntrega = dpFechaEntrega.SelectedDate.HasValue ? dpFechaEntrega.SelectedDate.Value : DateTime.Now,
                        Responsable = txtResponsable.Text,
                        IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue),
                        IdInstitucion = Convert.ToInt32(cmbInstitucion.SelectedValue),
                        Foto = FotoActa != null ? FotoActa : null
                    };

                    try
                    {
                        actaController.AgregarActa(acta);
                        MessageBox.Show("Acta agregada con éxito", "Agregar");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error al guardar el acta", "Error");
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

            ListarActas();
            LimpiarCampos();
        }

        private void ListarActas()
        {
            DataTable dataTable = actaController.ListarActas();

            if (dataTable.Rows.Count > 0)
            {
                DGActas.ItemsSource = dataTable.DefaultView;
            }
        }

        private void LimpiarCampos()
        {
            txtNumeroActa.Text = "";
            txtNumeroExpediente.Text = "";
            dpFechaEntrega.SelectedDate = null;
            txtResponsable.Text = "";
            cmbProveedor.SelectedIndex = -1;
            cmbInstitucion.SelectedIndex = -1;

            ImageBrush fotoActa = new ImageBrush();
            fotoActa.ImageSource = new BitmapImage(new Uri(@"default_image.png", UriKind.RelativeOrAbsolute));
            eFoto.Fill = fotoActa;
            FotoActa = null;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (DGActas.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)DGActas.SelectedItem;
                int idActa = Convert.ToInt32(rowView["id_acta"]);

                try
                {
                    actaController.EliminarActa(idActa);
                    MessageBox.Show("Acta eliminada con éxito", "Eliminar");
                    ListarActas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el acta: " + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un acta para eliminar", "Advertencia");
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (DGActas.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)DGActas.SelectedItem;
                int idActa = Convert.ToInt32(rowView["id_acta"]);

                Acta acta = new Acta
                {
                    IdActa = idActa,
                    NumeroActa = txtNumeroActa.Text,
                    NumeroExpediente = txtNumeroExpediente.Text,
                    FechaEntrega = dpFechaEntrega.SelectedDate.HasValue ? dpFechaEntrega.SelectedDate.Value : DateTime.Now,
                    Responsable = txtResponsable.Text,
                    IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue),
                    IdInstitucion = Convert.ToInt32(cmbInstitucion.SelectedValue),
                    Foto = FotoActa != null ? FotoActa : null
                };

                try
                {
                    actaController.EditarActa(acta);
                    MessageBox.Show("Acta actualizada con éxito", "Editar");
                    ListarActas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el acta: " + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un acta para editar", "Advertencia");
            }
        }

        private void btnCargarImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (.jpg)|*.jpg|PNG(*.png)|*.png|All files(*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            bool? revisarOK = openFileDialog.ShowDialog();
            if (revisarOK == true)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(openFileDialog.FileName));
                imageSource = openFileDialog.FileName.ToString();
                ImageBrush fotoActa = new ImageBrush();
                fotoActa.ImageSource = bitmapImage;
                this.eFoto.Fill = fotoActa;

                FotoActa = File.ReadAllBytes(openFileDialog.FileName);
            }
        }

        private void DGActas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!DGActas.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGActas.SelectedItem;

                txtNumeroActa.Text = rowView["num_acta"].ToString();
                txtNumeroExpediente.Text = rowView["num_expediente"].ToString();
                dpFechaEntrega.SelectedDate = Convert.ToDateTime(rowView["fecha_entrega"]);
                txtResponsable.Text = rowView["responsable"].ToString();
                cmbProveedor.SelectedValue = rowView["id_proveedor"];
                cmbInstitucion.SelectedValue = rowView["id_institucion"];

                FotoActa = rowView["foto"] != DBNull.Value ? (byte[])rowView["foto"] : null;
                if (FotoActa != null)
                {
                    MemoryStream ms = new MemoryStream(FotoActa);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();

                    ImageBrush miFoto = new ImageBrush { ImageSource = image };
                    eFoto.Fill = miFoto;
                }
                else
                {
                    eFoto.Fill = null;
                }
            }
        }

       

        private void LlenarComboBox()
        {
            DataTable institucionTable = institucionController.ListaInstituciones();
            DataTable proveedorTable = proveedorController.listarProveedores();


            if (institucionTable.Rows.Count > 0)
            {
                foreach (DataRow row in institucionTable.Rows)
                {
                    int id = Convert.ToInt32(row["Id_institucion"]);
                    string nombre = row["nombre"].ToString();
                    institucion[id] = nombre;
                }
                cmbInstitucion.ItemsSource = institucion;
                cmbInstitucion.DisplayMemberPath = "Value";
                cmbInstitucion.SelectedValuePath = "Key";
            }

            if (proveedorTable.Rows.Count > 0)
            {
                foreach (DataRow row in proveedorTable.Rows)
                {
                    int id = Convert.ToInt32(row["Id_proveedor"]);
                    string nombre = row["nombre"].ToString();
                    proveedor[id] = nombre;
                }
                cmbProveedor.ItemsSource = proveedor;
                cmbProveedor.DisplayMemberPath = "Value";
                cmbProveedor.SelectedValuePath = "Key";
            }


        }
    }
}
