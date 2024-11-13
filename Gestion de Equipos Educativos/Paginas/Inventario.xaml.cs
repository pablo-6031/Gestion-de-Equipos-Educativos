using Controllers;
using Entities;
using Models;
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


namespace Gestion_de_Equipos_Educativos.Paginas
{
    /// <summary>
    /// Lógica de interacción para Inventario.xaml
    /// </summary>
    public partial class Inventario : Page
    {
        private string imageSource = null;
        private int idTipoEquipoSeleccionado ;
       TipoEquipoController tipoEquipoController = new TipoEquipoController();
       EquipoController equipoController = new EquipoController();
        public Inventario()
        {
            InitializeComponent();
            LlenarComboBox();
            ListarEquipos();
        }
        private Dictionary<int, string> tiposEquipos = new Dictionary<int, string>();

        private void LlenarComboBox()
        {
            DataTable dataTable = tipoEquipoController.ListaTipoEquipos();

            if (dataTable.Rows.Count > 0)
            {

                foreach (DataRow row in dataTable.Rows)
                {
                    int id = Convert.ToInt32(row["Id_Tipo_Equipo"]);
                    string tipo = row["tipo"].ToString();
                    tiposEquipos[id] = tipo;
                }

                cmbTipo.ItemsSource = tiposEquipos;
                cmbTipo.DisplayMemberPath = "Value";    // Muestra el valor (tipo)
                cmbTipo.SelectedValuePath = "Key";      // Usa el ID (IdTipoEquipo) como valor seleccionado
            }
            else
            {

            }
        }


        private void btnCargarImagen_Click(object sender, RoutedEventArgs e)
        {

        }



        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verifica que los campos requeridos no estén vacíos
                if (this.txtNumSerie.Text != "" && this.txtMatricula.Text != "")
                {
                    // Crea un nuevo objeto de tipo Equipo y asigna valores a sus propiedades
                    Equipo equipo = new Equipo();
                    equipo.NumSerie = this.txtNumSerie.Text;
                    equipo.Matricula = this.txtMatricula.Text;
                    equipo.Estado = this.txtEstado.Text;
                    equipo.Observacion = this.txtObservacion.Text;
                    equipo.FechaIngreso = this.dtpFechaIngreso.SelectedDate.HasValue ? this.dtpFechaIngreso.SelectedDate.Value : DateTime.Now;
                    equipo.Destino = this.cmbDestino.Text;

                    // Convertimos los valores de IdTipoEquipo e IdActa a enteros, si existen
                    equipo.IdTipoEquipo = (int)cmbTipo.SelectedValue;
                    // equipo.IdActa = int.TryParse(this.txtIdActa.Text, out int idActa) ? idActa : 0;
                    equipo.IdActa = 0;

                    try
                    {
                        // Guarda el equipo en la base de datos
                        equipoController.agregarEquipo(equipo);
                        System.Windows.MessageBox.Show("Agregar", "Equipo " + equipo.NumSerie + " agregado con éxito");
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Error", "Error al guardar equipo");
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

            // Actualiza la lista de equipos y limpia los campos
            ListarEquipos();
            LimpiarCampos();


        }
        private void LimpiarCampos()
        {
            // Limpia los TextBox
            txtNumSerie.Text = "";
            txtMatricula.Text = "";
            txtObservacion.Text = "";
            txtEstado.Text = "";


            // Limpia ComboBox

            cmbDestino.SelectedIndex = -1;
            cmbTipo.SelectedIndex = -1;

            // Limpia DatePicker
            dtpFechaIngreso.SelectedDate = null;

            // Limpia cualquier imagen o referencia adicional
            this.eFoto = null;
            this.imageSource = null;

        }

        private void ListarEquipos()
        {
            // Obtenemos la lista de equipos desde la base de datos
            DataTable dataTable = equipoController.listaEquipos();

            // Verificamos que el DataTable tenga datos
            if (dataTable.Rows.Count > 0)
            {
                // Asigna el DataTable como fuente de datos de un control (por ejemplo, DataGrid o ListBox)
                DGEquipos.ItemsSource = dataTable.DefaultView; // Suponiendo que tienes un DataGrid llamado 'dataGridEquipos'
            }
            else
            {

            }
        }


        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
