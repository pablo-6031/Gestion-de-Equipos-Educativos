using Controllers;
using Entities;
using Gestion_de_Equipos_Educativos.Paginas;
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
using System.Windows.Shapes;

namespace Gestion_de_Equipos_Educativos.Ventanas
{
    /// <summary>
    /// Lógica de interacción para NuevosEquipos.xaml
    /// </summary>
    public partial class NuevosEquipos : Window
    {
        TipoEquipoController tipoEquipoController = new TipoEquipoController();
        MemoryStream memoryStream;

        public NuevosEquipos()
        {
            InitializeComponent();
            verificarIngreso();
        }

        private void verificarIngreso()
        {
            if (EquipoCache.IdEquipo != 0)
            {
                cargarDatos();
            }
        }

        private void cargarDatos()
        {
            TipoEquipo tipoEquipo = new TipoEquipo();

            tipoEquipo = tipoEquipoController.TraerTipoEquipos(EquipoCache.IdTipoEquipo);


            this.txtNumSerie.Text = EquipoCache.NumSerie;
            this.txtMatricula.Text = EquipoCache.Matricula;
            this.cmbEstado.Text = EquipoCache.Estado;
            this.txtObservacion.Text = EquipoCache.Observacion;
            this.cmbDestino.Text = EquipoCache.Destino;
            this.cmbTipo.Text = tipoEquipo.Tipo;
            this.cmbModelo.Text = tipoEquipo.Modelo;

        }

        private void mover(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private Dictionary<int, string> tiposEquipos = new Dictionary<int, string>();

        private void LlenarComboBox(String nombre)
        {
            tiposEquipos.Clear();
            DataTable dataTable = tipoEquipoController.ListaTipoEquipos();

            if (dataTable.Rows.Count > 0)
            {

                foreach (DataRow row in dataTable.Rows)
                {
                    if (nombre == row["tipo"].ToString())
                    {
                        int id = Convert.ToInt32(row["Id_Tipo_Equipo"]);
                        string tipo = row["modelo"].ToString();
                        tiposEquipos[id] = tipo;
                    }
                    
                }

                cmbModelo.ItemsSource = tiposEquipos;
                cmbModelo.DisplayMemberPath = "Value";    // Muestra el valor (tipo)
                cmbModelo.SelectedValuePath = "Key";      // Usa el ID (IdTipoEquipo) como valor seleccionado
            }
            else
            {

            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Verifica que los campos requeridos no estén vacíos
            if (this.txtNumSerie.Text != "" && this.txtMatricula.Text != "" && this.cmbDestino.Text != "" && cmbTipo.SelectedValue != null)
            {

                EquipoCache.NumSerie = this.txtNumSerie.Text;
                EquipoCache.Matricula = this.txtMatricula.Text;
                EquipoCache.Estado = this.cmbEstado.Text;
                EquipoCache.Observacion = this.txtObservacion.Text;
                EquipoCache.Destino = this.cmbDestino.Text;
                EquipoCache.IdTipoEquipo = (int)cmbModelo.SelectedValue;


                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Complete los campos obligatorios", "Advertencia");
            }


        }


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbModelo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var FotoTipoEquipo = (byte[])tipoEquipoController.ObtenerFoto((int)cmbModelo.SelectedValue);


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

        private void cmbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbModelo.ItemsSource = null;
            cmbModelo.Items.Clear();

            ComboBoxItem selectedItem = (ComboBoxItem)cmbTipo.SelectedItem;
            string tipo = selectedItem.Content.ToString();
            LlenarComboBox(tipo);
        }

        private void txtMatricula_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
