using Controllers;
using Entities;
using Microsoft.Extensions.Options;
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
    /// Lógica de interacción para VentanaServicioTecnico.xaml
    /// </summary>
    public partial class VentanaServicioTecnico : Window
    {
        EquipoController equipoController = new EquipoController();
        ServicioTecnicoController servicioTecnicoController = new ServicioTecnicoController();
        private string imageSource = null;
        byte[] FotoServTec;
        MemoryStream memoryStream;
        MemoryStream stream;
        int idEquipo;
        string opcionEntrada = "";
        public VentanaServicioTecnico(string opcion)
        {
            InitializeComponent();
            cargarEquipo();
            opcionEntrada = opcion;
            if (opcion == "ver")
            {
                soloLectura(true);
            }
            cargarEquipo();

        }

        private void LimpiarCampos()
        {
            // Limpia los TextBox
            txtNumSerie.Text = "";
            txtFalla.Text = "";
            txtResponsable.Text = "";


            // Limpia la imagen
            ImageBrush fotoSer = new ImageBrush();
            fotoSer.ImageSource = new BitmapImage(new Uri(@"default_image.png", UriKind.RelativeOrAbsolute));
            eFoto.Fill = fotoSer;
            FotoServTec = null;
            AlumnoCache.IdAlumno = 0;
        }




        private void cargarEquipo()
        {
            if (ServicioTecnicoCache.IdServicioTecnico == 0 || ServicioTecnicoCache.IdServicioTecnico == null)
            {
                txtNumSerie.Text = EquipoCache.NumSerie.ToString();
                txtNumSerie.IsReadOnly = true;
            }
            else
            {
                txtNumSerie.Text = ServicioTecnicoCache.NumSerie;
                txtFalla.Text = ServicioTecnicoCache.Falla.ToString();
                txtResponsable.Text = ServicioTecnicoCache.Responsable.ToString();
                eFoto.Fill = ConvertirBytesAImageBrush(ServicioTecnicoCache.Foto);

                

            }



            
        }
        private void soloLectura(bool opcion)
        {
            txtNumSerie.IsReadOnly = opcion;
            txtFalla.IsReadOnly = opcion;
            txtResponsable.IsReadOnly = opcion;

            if (opcion)
            {
                btnAgregarFoto.IsEnabled = false;
            }
            else
            {
                btnAgregarFoto.IsEnabled = true;
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

        private void CargarImagen(object foto, Ellipse destino)
        {
            ImageBrush miFoto = new ImageBrush();

            if (foto is byte[] fotoBytes && fotoBytes.Length > 0)
            {
                // Convertir byte[] a BitmapImage
                using (MemoryStream ms = new MemoryStream(fotoBytes))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.CacheOption = BitmapCacheOption.OnLoad; // Asegura que se cargue completamente
                    image.EndInit();

                    miFoto.ImageSource = image;
                }
            }
            else
            {
                // Cargar imagen predeterminada
                BitmapImage defaultImage = new BitmapImage(new Uri("C:\\Users\\pablo\\source\\repos\\TP2-Proyecto_WPF_2024\\Views\\Imagenes\\hombre.png", UriKind.RelativeOrAbsolute));
                miFoto.ImageSource = defaultImage;
            }

            // Asignar el ImageBrush al Ellipse
            destino.Fill = miFoto;
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

                FotoServTec = File.ReadAllBytes(openFileDialog.FileName);
            }
        }


        private void btnCancelar_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (opcionEntrada == "editar")
                {

                    if (imageSource == null)
                    {
                        stream = this.memoryStream;
                    }
                    else
                    {
                        stream = new MemoryStream(File.ReadAllBytes(this.imageSource));
                    }


                    ImageBrush imageBrush = eFoto.Fill as ImageBrush; 
                    ServicioTecnico servicio = new ServicioTecnico();
                    servicio.IdServicioTecnico = (int)ServicioTecnicoCache.IdServicioTecnico;
                    servicio.Falla = this.txtFalla.Text;
                    servicio.Foto = ConvertirImageBrushABytes(imageBrush);


                    servicioTecnicoController.EditarServicioTecnico(servicio);

                    string mensaj = "Servicio tecnico editado con éxito";
                    mostrarVentana(mensaj, "EDITAR");
                }
                else
                {
                    ServicioTecnico servicio = new ServicioTecnico();
                    servicio.IdEquipo = (int)EquipoCache.IdEquipo;
                    servicio.FechaEnvio = DateTime.Now;
                    servicio.Responsable = this.txtResponsable.Text;
                    servicio.Falla = this.txtFalla.Text;
                    servicio.Foto = FotoServTec != null ? FotoServTec : null;


                    servicioTecnicoController.AgregarServicioTecnico(servicio);

                    string mensaj = "Servicio tecnico registrado con éxito";
                    mostrarVentana(mensaj, "AGREGAR");
                }

            }
            catch (Exception ex)
            {
                string mensaj = "Error al eliminar el equipo";
                mostrarVentanaError(mensaj + ex, "Error");
            }
            this.Close();

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
                ImageBrush fotoServicio = new ImageBrush();
                fotoServicio.ImageSource = bitmapImage;
                this.eFoto.Fill = fotoServicio;

                FotoServTec = File.ReadAllBytes(openFileDialog.FileName);
            }
        }
    }
}
