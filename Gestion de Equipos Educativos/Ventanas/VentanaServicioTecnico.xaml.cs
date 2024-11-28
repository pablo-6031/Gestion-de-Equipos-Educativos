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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
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
        private string imageSource = null;
        byte[] FotoServTec;
        public VentanaServicioTecnico()
        {
            InitializeComponent();
        }

        private void txtBuscarEquipo_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textoBusqueda = txtBuscarEquipo.Text.Trim();
            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                // Si no hay texto, mostrar todos los docentes
                
            }


            // Llamar al controlador para obtener los datos filtrados

            var dtEquipos = equipoController.ListarEquiposporNumSerie(textoBusqueda);

            this.DGEquipos.ItemsSource = dtEquipos.DefaultView;
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
    }
}
