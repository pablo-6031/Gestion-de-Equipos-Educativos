using Controllers;
using Entities;
using Gestion_de_Equipos_Educativos.Paginas;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
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
    /// <summary>
    /// Lógica de interacción para VentanaTipoEquipo.xaml
    /// </summary>
    public partial class VentanaTipoEquipo : Window
    {
        TipoEquipoController tipoEquipoController = new TipoEquipoController();
        MemoryStream memoryStream;
        MemoryStream stream;
        private string imageSource = null;
        byte[] FotoTipoEquipo;
        public VentanaTipoEquipo(string opcion)
        {
            InitializeComponent();
            if (opcion == "ver")
            {
                soloLectura(true);
            }

            if (TipoEquipoCache.IdTipoEquipo == 0 || TipoEquipoCache.IdTipoEquipo == null)
            {
                limpiarCampos();


            }
            else
            {
                cargarCampos();
            }

        }

        private void soloLectura(bool opcion)
        {
            txtTipo.IsReadOnly = opcion;
            txtMarca.IsReadOnly = opcion;
            txtModelo.IsReadOnly = opcion;
            txtDetalle.IsReadOnly = opcion;
            if (opcion)
            {
                btnAgregarFoto.IsEnabled = false;
            }
            else
            {
                btnAgregarFoto.IsEnabled = true;
            }
        }

        private void cargarCampos()
        {
            this.txtTipo.Text = TipoEquipoCache.Tipo;
            this.txtMarca.Text = TipoEquipoCache.Marca;
            this.txtModelo.Text = TipoEquipoCache.Modelo;
            this.txtDetalle.Text = TipoEquipoCache.Detalle;
            eFoto.Fill = ConvertirBytesAImageBrush(TipoEquipoCache.Foto);
        }

        private void limpiarCampos()
        {
            this.txtTipo.Text="";
            this.txtMarca.Clear();
            this.txtModelo.Clear();
            this.txtDetalle.Clear();

            ImageBrush fotoTipoEquipo = new ImageBrush();
            fotoTipoEquipo.ImageSource = new BitmapImage(new Uri(@"default_image.png", UriKind.RelativeOrAbsolute));
            this.eFoto.Fill = fotoTipoEquipo;
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

        private void limpiarEquipoCache()
        {
            TipoEquipoCache.IdTipoEquipo = 0;
            TipoEquipoCache.Tipo = "";
            TipoEquipoCache.Marca = "";
            TipoEquipoCache.Modelo = "";
            TipoEquipoCache.Detalle = "";
        }




        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiarEquipoCache();
            this.Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TipoEquipoCache.IdTipoEquipo == 0 || TipoEquipoCache.IdTipoEquipo == null)
                {
                    if (!string.IsNullOrEmpty(this.txtTipo.Text) && !string.IsNullOrEmpty(txtModelo.Text))
                    {

                        // Crear una instancia de TipoEquipos y asignar valores de los campos de la interfaz
                        TipoEquipo tipoEquipo = new TipoEquipo
                        {
                            Tipo = this.txtTipo.Text.ToUpper(),
                            Marca = this.txtMarca.Text.ToUpper(),
                            Modelo = this.txtModelo.Text.ToUpper(),
                            Detalle = this.txtDetalle.Text,
                            Foto = FotoTipoEquipo != null ? FotoTipoEquipo : null
                        };

                        try
                        {
                            
                            string mensaj = tipoEquipoController.AgregarTipoEquipo(tipoEquipo);
                            mostrarVentanaMensaje(mensaj, "Agregar");

                        }
                        catch (Exception ex)
                        {
                            string mensaj = "Error al guardar el tipo de equipo";
                            mostrarVentanaError(mensaj, "Error");
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.txtTipo.Text))
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

                        // Crear una instancia de TipoEquipos y asignar valores de los campos de la interfaz

                        TipoEquipo tipoEquipo = new TipoEquipo
                        {
                            IdTipoEquipo = (int)TipoEquipoCache.IdTipoEquipo,
                            Tipo = this.txtTipo.Text.ToUpper(),
                            Marca = this.txtMarca.Text.ToUpper(),
                            Modelo = this.txtModelo.Text.ToUpper(),
                            Detalle = this.txtDetalle.Text,
                            Foto = ConvertirImageBrushABytes(imageBrush)
                        };

                        try
                        {
                            string mensaj = tipoEquipoController.EditarTipoEquipo(tipoEquipo);
                            mostrarVentanaMensaje(mensaj, "Editar");
                        }
                        catch (Exception ex)
                        {

                            string mensaj = "Error al actualizar el usuario";
                            mostrarVentanaError(mensaj, "Error");
                        }


                    }
                }


                
            }

            catch (Exception ex)
            {
                //mensaje.MensajePersonalizado("Error", "Error al editar");
                //mensaje.ShowDialog();
            }
            limpiarEquipoCache();
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
                ImageBrush fotoTipoEquipo = new ImageBrush();
                fotoTipoEquipo.ImageSource = bitmapImage;
                this.eFoto.Fill = fotoTipoEquipo;

                FotoTipoEquipo = File.ReadAllBytes(openFileDialog.FileName);
            }
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
