using Controllers;
using Entities;
using Gestion_de_Equipos_Educativos.Paginas;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
using System.Windows.Shapes;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Gestion_de_Equipos_Educativos.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaAlumnos.xaml
    /// </summary>
    public partial class VentanaAlumnos : Window
    {
        InstitucionController institucionController = new InstitucionController();
        AlumnoController alumnoController = new AlumnoController();
        private Dictionary<int, string> institucion = new Dictionary<int, string>();
        private string imageSource = null;
        byte[] FotoAlumno;
        MemoryStream memoryStream;
        MemoryStream stream;
        string opcionAlumno ="";
        public VentanaAlumnos(string opcion)
        {
            opcionAlumno =opcion;   
            InitializeComponent();
            if (opcion == "ver")
            {
                soloLectura(true);
            }

            if (AlumnoCache.IdAlumno == 0 || AlumnoCache.IdAlumno == null)
            {
                LimpiarCampos();
               

            }
            else
            {
                cargarCampos();
            }

        }

        public ImageBrush ConvertirBytesAImageBrush(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            // Convertir los bytes a un BitmapImage
            BitmapImage bitmap = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = ms;
                bitmap.EndInit();
            }

            // Crear un ImageBrush con el BitmapImage
            return new ImageBrush { ImageSource = bitmap };
        }


        public byte[] ConvertirImageBrushABytes(ImageBrush imageBrush)
        {
            if (imageBrush == null || imageBrush.ImageSource == null)
                return null;

            try
            {
                BitmapSource bitmapSource = imageBrush.ImageSource as BitmapSource;

                if (bitmapSource == null)
                    return null;

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    return ms.ToArray(); // Retorna los bytes de la imagen
                }
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }



        private void cargarCampos()
        {
            txtNombre.Text = AlumnoCache.Nombres;
            txtApellido.Text = AlumnoCache.Apellidos;
            txtCurso.Text = AlumnoCache.Curso;
            txtCuil.Text = AlumnoCache.Cuil;
            txtTelefono.Text = AlumnoCache.Telefono;
            eFoto.Fill = ConvertirBytesAImageBrush(AlumnoCache.Foto);
        }

        private void soloLectura(bool opcion)
        {
            txtNombre.IsReadOnly = opcion;
            txtApellido.IsReadOnly = opcion;
            txtCurso.IsReadOnly = opcion;
            txtCuil.IsReadOnly = opcion;
            txtTelefono.IsReadOnly = opcion;
            if (opcion)
            {
                btnAgregarFoto.IsEnabled = false;
            }
            else
            {
                btnAgregarFoto.IsEnabled = true;
            }

        }

        

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (opcionAlumno == "agregar")
                {
                    // Verifica que los campos requeridos no estén vacíos
                    if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtApellido.Text))
                    {
                        // Crea un nuevo objeto de tipo Alumno y asigna valores a sus propiedades
                        Alumno alumno = new Alumno
                        {
                            Nombres = txtNombre.Text.ToUpper(),
                            Apellidos = txtApellido.Text.ToUpper(),
                            Curso = txtCurso.Text.ToUpper(),
                            Cuil = txtCuil.Text.ToUpper(),
                            Telefono = txtTelefono.Text.ToUpper(),
                            IdInstitucion = 1,
                            Foto = FotoAlumno != null ? FotoAlumno : null
                        };

                        try
                        {
                            int idEquipo = (int)EquipoCache.IdEquipo;
                            // Guarda el alumno en la base de datos
                            alumnoController.agregarAlumno(alumno, idEquipo);
                            string mensaj = "Alumno " + alumno.Nombres + " " + alumno.Apellidos + " agregado con éxito";
                            mostrarVentanaMensaje(mensaj, "Agregar");
                        }
                        catch (Exception)
                        {
                            string mensaj = "Error al guardar el alumno";
                            mostrarVentanaError(mensaj, "Error");
                        }
                    }
                    else
                    {
                        string mensaj = "Complete los campos obligatorios";
                        mostrarVentanaError(mensaj, "Advertencia"); ;
                    }
                }
                else
                {
                    // Verificar si se ha seleccionado una nueva imagen
                    if (imageSource == null)
                    {
                        stream = this.memoryStream;
                    }
                    else
                    {
                        stream = new MemoryStream(File.ReadAllBytes(this.imageSource));
                    }


                    ImageBrush imageBrush = eFoto.Fill as ImageBrush;
                    Alumno alumno = new Alumno
                    {
                        IdAlumno = (int)AlumnoCache.IdAlumno,
                        Nombres = txtNombre.Text,
                        Apellidos = txtApellido.Text,
                        Curso = txtCurso.Text,
                        Cuil = txtCuil.Text,
                        Telefono = txtTelefono.Text,
                        IdInstitucion = 1,
                        Foto = ConvertirImageBrushABytes(imageBrush)
                    };
                    try
                    {
                        alumnoController.editarAlumno(alumno);
                        string mensaj = "Alumno actualizado con éxito";
                        mostrarVentanaMensaje(mensaj, "Editar");

                    }
                    catch (Exception ex)
                    {
                        string mensaj = "Error al actualizar el alumno";
                        mostrarVentanaError(mensaj, "Error");
                    }
                }
                
            }
            catch (Exception ex)
            {
                string mensaj = ex.Message;
                mostrarVentanaError(mensaj, "Error");
            }

            LimpiarCampos();
            soloLectura(false);
            this.Close();
        }

        private void LimpiarCampos()
        {
            // Limpia los TextBox
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCurso.Text = "";
            txtCuil.Text = "";
            txtTelefono.Text = "";

            // Limpia la imagen
            ImageBrush fotoAlumno = new ImageBrush();
            fotoAlumno.ImageSource = new BitmapImage(new Uri(@"default_image.png", UriKind.RelativeOrAbsolute));
            eFoto.Fill = fotoAlumno;
            FotoAlumno = null;
            AlumnoCache.IdAlumno = 0;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            soloLectura(false);
            LimpiarCampos();
            this.Close();
        }




        private void mostrarVentanaMensaje(string mensaje, string titulo)
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

        

        private void btnAgregarFoto_Click(object sender, RoutedEventArgs e)
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
    }
}
