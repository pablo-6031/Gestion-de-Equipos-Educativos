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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Models;
using System.Drawing.Imaging;
using Microsoft.Win32;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;



namespace Gestion_de_Equipos_Educativos.Paginas
{

    public partial class TipoEquipos : Page
    {
        TipoEquipoController tipoEquipoController = new TipoEquipoController();
        MemoryStream memoryStream;
        MemoryStream stream;
        private string imageSource = null;
        byte[] FotoTipoEquipo;



        public TipoEquipos()
        {
            InitializeComponent();
            ListarTipoEquipo();
        }
        private void TraerTipoEquipos(DataTable dtTipoEquipos)
        {
            List<TipoEquipo> tipoEquiposList = new List<TipoEquipo>();
            tipoEquiposList = (from DataRow dr in dtTipoEquipos.Rows
                               select new TipoEquipo()
                               {
                                   IdTipoEquipo = Convert.ToInt32(dr["id_tipo_equipo"]),
                                   Tipo = dr["Tipo"].ToString(),
                                   Marca = dr["Marca"].ToString(),
                                   Modelo = dr["Modelo"].ToString(),
                                   Detalle = dr["detalle_tecnico"].ToString(),
                                   Foto = dr["Foto"] != DBNull.Value ? (byte[])dr["Foto"] : null
                               }).ToList();
            this.DGTipoEquipos.ItemsSource = tipoEquiposList;
        }

        private void ListarTipoEquipo()
        {
            var DtTipoEquipo = tipoEquipoController.ListaTipoEquipos();
            TraerTipoEquipos(DtTipoEquipo);
        }
        private void limpiarCampos()
        {
            this.txtTipo.Clear();
            this.txtMarca.Clear();
            this.txtModelo.Clear();
            this.txtDetalle.Clear();
            this.txtIdTipoEquipo.Clear();

            ImageBrush fotoTipoEquipo = new ImageBrush();
            fotoTipoEquipo.ImageSource = new BitmapImage(new Uri(@"default_image.png", UriKind.RelativeOrAbsolute));
            this.eFoto.Fill = fotoTipoEquipo;
        }



        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtIdTipoEquipo.Text))
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

                    // Convertir el stream en un arreglo de bytes para almacenar la foto
                    byte[] pic = stream.ToArray();

                    // Crear una instancia de TipoEquipos y asignar valores de los campos de la interfaz
                    TipoEquipo tipoEquipo = new TipoEquipo
                    {
                        IdTipoEquipo = int.Parse(this.txtIdTipoEquipo.Text),
                        Tipo = this.txtTipo.Text,
                        Marca = this.txtMarca.Text,
                        Modelo = this.txtModelo.Text,
                        Detalle = this.txtDetalle.Text,
                        Foto = pic
                    };

                    try
                    {
                        tipoEquipoController.EditarTipoEquipo(tipoEquipo);
                        //mensaje.MensajePersonalizado("Editar", "Cambios realizados con éxito");
                        //mensaje.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        //mensaje.MensajePersonalizado("Error", "Error al editar");
                        //mensaje.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error");
            }

            ListarTipoEquipo(); // Actualizar la lista de tipo de equipos
            limpiarCampos(); // Limpiar los campos de entrada
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
                ImageBrush fotoTipoEquipo = new ImageBrush();
                fotoTipoEquipo.ImageSource = bitmapImage;
                this.eFoto.Fill = fotoTipoEquipo;
            }
        }

        private void DGTipoEquipos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!this.DGTipoEquipos.Items.IsEmpty)
            {
                TipoEquipo tipoEquipo = (TipoEquipo)this.DGTipoEquipos.CurrentItem;
                this.txtIdTipoEquipo.Text = tipoEquipo.IdTipoEquipo.ToString();
                this.txtTipo.Text = tipoEquipo.Tipo;
                this.txtMarca.Text = tipoEquipo.Marca;
                this.txtModelo.Text = tipoEquipo.Modelo;
                this.txtDetalle.Text = tipoEquipo.Detalle;
                FotoTipoEquipo = tipoEquipo.Foto;

                if (FotoTipoEquipo != null)
                {
                    MemoryStream ms = new MemoryStream(FotoTipoEquipo);
                    memoryStream = ms;
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.CacheOption = BitmapCacheOption.OnLoad; // Asegura que la imagen se cargue completamente
                    image.EndInit();

                    ImageBrush miFoto = new ImageBrush
                    {
                        ImageSource = image
                    };
                    this.eFoto.Fill = miFoto; // Asigna la imagen al control eFoto
                }
                else
                {
                    // Si no hay imagen, limpiar la imagen de eFoto
                    this.eFoto.Fill = null;
                }
            }
        }
    }
}
