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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
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
        public Alumnos()
        {
            InitializeComponent();
            ListarAlumnos();
            LlenarComboBox();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verifica que los campos requeridos no estén vacíos
                if (!string.IsNullOrEmpty(txtNombres.Text) && !string.IsNullOrEmpty(txtApellidos.Text))
                {
                    // Crea un nuevo objeto de tipo Alumno y asigna valores a sus propiedades
                    Alumno alumno = new Alumno
                    {
                        Nombres = txtNombres.Text,
                        Apellidos = txtApellidos.Text,
                        Curso = txtCurso.Text,
                        Cuil = txtCuil.Text,
                        Telefono = txtTelefono.Text,
                        IdInstitucion = Convert.ToInt32(cmbInstitucion.SelectedValue),
                        Foto = FotoAlumno != null ? FotoAlumno : null
                    };

                    try
                    {
                        // Guarda el alumno en la base de datos
                        alumnoController.agregarAlumno(alumno);
                        System.Windows.MessageBox.Show("Alumno " + alumno.Nombres + " " + alumno.Apellidos + " agregado con éxito", "Agregar");
                    }
                    catch (Exception)
                    {
                        System.Windows.MessageBox.Show("Error al guardar el alumno", "Error");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Complete los campos obligatorios", "Advertencia");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error");
            }

            // Actualiza la lista de alumnos y limpia los campos
            ListarAlumnos();
            LimpiarCampos();
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

        private void LimpiarCampos()
        {
            // Limpia los TextBox
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtCurso.Text = "";
            txtCuil.Text = "";
            txtTelefono.Text = "";
            cmbInstitucion.SelectedIndex = -1;

            // Limpia la imagen
            ImageBrush fotoAlumno = new ImageBrush();
            fotoAlumno.ImageSource = new BitmapImage(new Uri(@"default_image.png", UriKind.RelativeOrAbsolute));
            eFoto.Fill = fotoAlumno;
            FotoAlumno = null;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (DGAlumnos.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)DGAlumnos.SelectedItem;
                int idAlumno = Convert.ToInt32(rowView["id_alumno"]);

                try
                {
                    alumnoController.eliminarAlumno(idAlumno);
                    System.Windows.MessageBox.Show("Alumno eliminado con éxito", "Eliminar");
                    ListarAlumnos();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error al eliminar el alumno: " + ex.Message, "Error");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Seleccione un alumno para eliminar", "Advertencia");
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (DGAlumnos.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)DGAlumnos.SelectedItem;
                int idAlumno = Convert.ToInt32(rowView["id_alumno"]);

                Alumno alumno = new Alumno
                {
                    IdAlumno = idAlumno,
                    Nombres = txtNombres.Text,
                    Apellidos = txtApellidos.Text,
                    Curso = txtCurso.Text,
                    Cuil = txtCuil.Text,
                    Telefono = txtTelefono.Text,
                    IdInstitucion = Convert.ToInt32(cmbInstitucion.SelectedValue),
                    Foto = FotoAlumno != null ? FotoAlumno : null
                };

                try
                {
                    alumnoController.editarAlumno(alumno);
                    System.Windows.MessageBox.Show("Alumno actualizado con éxito", "Editar");
                    ListarAlumnos();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error al actualizar el alumno: " + ex.Message, "Error");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Seleccione un alumno para editar", "Advertencia");
            }
        }

        private void DGAlumnos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!DGAlumnos.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGAlumnos.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                txtIdAlumno.Text = rowView["id_alumno"].ToString();
                txtNombres.Text = rowView["nombres"].ToString();
                txtApellidos.Text = rowView["apellidos"].ToString();
                txtCurso.Text = rowView["curso"].ToString();
                txtCuil.Text = rowView["cuil"].ToString();
                txtTelefono.Text = rowView["telefono"].ToString();
                cmbInstitucion.SelectedValue = rowView["id_institucion"];

                // Cargar la imagen si está disponible
                FotoAlumno = rowView["foto"] != DBNull.Value ? (byte[])rowView["foto"] : null;
                if (FotoAlumno != null)
                {
                    MemoryStream ms = new MemoryStream(FotoAlumno);
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


        private void btnCargarImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (.jpg)|*.jpg|PNG(*.png)|*.png|All files(*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;//selecciona mas de un archivo

            bool? revisarOK = openFileDialog.ShowDialog();
            if (revisarOK == true)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(openFileDialog.FileName));
                imageSource = openFileDialog.FileName.ToString();
                ImageBrush fotoAlumno = new ImageBrush();
                fotoAlumno.ImageSource = bitmapImage;
                this.eFoto.Fill = fotoAlumno;

                FotoAlumno = File.ReadAllBytes(openFileDialog.FileName);
            }
        }


       

        private void LlenarComboBox()
        {
            DataTable dataTable = institucionController.ListaInstituciones();

            if (dataTable.Rows.Count > 0)
            {

                foreach (DataRow row in dataTable.Rows)
                {
                    int id = Convert.ToInt32(row["Id_institucion"]);
                    string nombre = row["nombre"].ToString();
                    institucion[id] = nombre;
                }

                cmbInstitucion.ItemsSource = institucion;
                cmbInstitucion.DisplayMemberPath = "Value";    // Muestra el valor (tipo)
                cmbInstitucion.SelectedValuePath = "Key";      // Usa el ID (IdTipoEquipo) como valor seleccionado
            }
            else
            {
                System.Windows.MessageBox.Show("No hay datos en la tabla");
            }
        }
    }
}
