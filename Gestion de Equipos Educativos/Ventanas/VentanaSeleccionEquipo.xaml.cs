using Controllers;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para VentanaSeleccionEquipo.xaml
    /// </summary>
    public partial class VentanaSeleccionEquipo : Window
    {
        EquipoController equipoController = new EquipoController();
        public VentanaSeleccionEquipo()
        {
            InitializeComponent();
            ListarEquipos();
        }
        private void ListarEquipos()
        {

            DataTable dtEquipos = equipoController.listaEquipos();
            List<Equipo> ListaEquipo = ConvertirDataTableEnListaDeEquipos(dtEquipos);
            if (ListaEquipo.Count > 0)
            {
                DGEquipos.ItemsSource = null;
                DGEquipos.ItemsSource = ListaEquipo;
            }




            /*
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
            */
        }

        public List<Equipo> ConvertirDataTableEnListaDeEquipos(DataTable dataTable)
        {
            var listaEquipos = new List<Equipo>();

            foreach (DataRow fila in dataTable.Rows)
            {
                var equipo = new Equipo
                {
                    IdEquipo = Convert.ToInt32(fila["id_equipo"]),
                    NumSerie = fila["num_serie"].ToString(),
                    Matricula = fila["matricula"].ToString(),
                    Estado = fila["estado"].ToString(),
                    Observacion = fila["observacion"].ToString(),
                    Destino = fila["destino"].ToString(),
                    IdTipoEquipo = Convert.ToInt32(fila["id_tipo_equipo"]),
                    IdActa = Convert.ToInt32(fila["id_acta"])
                };

                listaEquipos.Add(equipo);
            }

            return listaEquipos;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (DGEquipos.SelectedItem is Equipo equipoSeleccionado)
            {
                EquipoCache.IdEquipo = equipoSeleccionado.IdEquipo;
                EquipoCache.Estado = equipoSeleccionado.Estado;
                EquipoCache.Observacion = equipoSeleccionado.Observacion;
                EquipoCache.NumSerie = equipoSeleccionado.NumSerie;
                EquipoCache.Matricula = equipoSeleccionado.Matricula;
                EquipoCache.Destino = equipoSeleccionado.Destino;
                EquipoCache.IdTipoEquipo = equipoSeleccionado.IdTipoEquipo;

                this.Close();
            }
        }
    }
}
