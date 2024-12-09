using Controllers;
using Entities;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Gestion_de_Equipos_Educativos.Ventanas
{

    public partial class VentanaUsuarios : Window
    {
        InstitucionController institucionController = new InstitucionController();
        UsuarioController usuarioController = new UsuarioController();
        private string imageSource = null;
        byte[] FotoUsuario;
        MemoryStream memoryStream;
        MemoryStream stream;
        public VentanaUsuarios(string opcion)
        {
            InitializeComponent();
            if (opcion == "ver")
            {
                soloLectura(true);
            }

            if (UsuarioCache2.IdUsuario == 0 || UsuarioCache2.IdUsuario == null)
            {
                LimpiarCampos();


            }
            else
            {
                cargarCampos();
                
            }

        }

        private void cargarCampos()
        {
            txtNombre.Text = UsuarioCache2.Nombre;
            txtApellido.Text = UsuarioCache2.Apellido;
            txtLoginName.Text = UsuarioCache2.LoginName;
            txtCuil.Text = UsuarioCache2.Cuil;
            txtPassword.Text = UsuarioCache2.Password;
            txtCorreo.Text = UsuarioCache2.Correo;
            this.cmbRol.Text = UsuarioCache2.Rol;

            eFoto.Fill = ConvertirBytesAImageBrush(UsuarioCache2.Foto);
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


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

                FotoUsuario = File.ReadAllBytes(openFileDialog.FileName);
            }
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (UsuarioCache2.IdUsuario == 0 || UsuarioCache2.IdUsuario == null)
                {
                    // Verifica que los campos requeridos no estén vacíos
                    if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtApellido.Text))
                    {
                        // Crea un nuevo objeto de tipo Alumno y asigna valores a sus propiedades
                        Usuario usuario = new Usuario
                        {
                            Nombres = txtNombre.Text.ToUpper(),
                            Apellidos = txtApellido.Text.ToUpper(),
                            Cuil = txtCuil.Text.ToUpper(),
                            Correo = txtCorreo.Text.ToUpper(),
                            LoginName = txtLoginName.Text.ToUpper(),
                            Password = txtPassword.Text,
                            Rol = cmbRol.Text.ToUpper(),
                            IdInstitucion = 1,
                            Foto = FotoUsuario != null ? FotoUsuario : null
                        };

                        try
                        {
                            // Guarda el alumno en la base de datos
                            string mensaj = usuarioController.AgregarUsuario(usuario);
                            mostrarVentanaMensaje(mensaj, "Agregar");
                        }
                        catch (Exception)
                        {
                            string mensaj = "Error al guardar el usuario";
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
                    Usuario usuario = new Usuario
                    {
                        IdUsuario = (int)UsuarioCache2.IdUsuario,
                        Nombres = txtNombre.Text.ToUpper(),
                        Apellidos = txtApellido.Text.ToUpper(),
                        Cuil = txtCuil.Text.ToUpper(),
                        Correo = txtCorreo.Text.ToUpper(),
                        LoginName = txtLoginName.Text.ToUpper(),
                        Password = txtPassword.Text,
                        Rol = cmbRol.Text.ToUpper(),
                        IdInstitucion = 1,
                        Foto = ConvertirImageBrushABytes(imageBrush)
                    };

                    try
                    {
                        string mensaj = usuarioController.EditarUsuario(usuario);
                        mostrarVentanaMensaje(mensaj, "Editar");

                    }
                    catch (Exception ex)
                    {
                        string mensaj = "Error al actualizar el usuario";
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
            txtCuil.Text = "";
            txtCorreo.Text = "";
            txtLoginName.Text = "";
            txtPassword.Text = "";
            cmbInstitucion.Text = "";
            cmbRol.Text = "";

            // Limpia la imagen
            ImageBrush fotoUsuario = new ImageBrush();
            fotoUsuario.ImageSource = new BitmapImage(new Uri(@"default_image.png", UriKind.RelativeOrAbsolute));
            eFoto.Fill = fotoUsuario;
            FotoUsuario = null;
            UsuarioCache2.IdUsuario = 0;
        }

        private void soloLectura(bool opcion)
        {
            txtNombre.IsReadOnly = opcion;
            txtApellido.IsReadOnly = opcion;
            txtCorreo.IsReadOnly = opcion;
            txtCuil.IsReadOnly = opcion;
            txtLoginName.IsReadOnly = opcion;
            txtPassword.IsReadOnly = opcion;
            txtLoginName.IsReadOnly = opcion;
            cmbRol.IsReadOnly = opcion;
            if (opcion)
            {
                btnAgregarFoto.IsEnabled = false;
            }
            else
            {
                btnAgregarFoto.IsEnabled = true;
            }

        }


        private void cmbInstitucion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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


    }
}
