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
    /// Lógica de interacción para VentanaDetalleEquipo.xaml
    /// </summary>
    public partial class VentanaDetalleEquipo : Window
    {
        EquipoController equipoController = new EquipoController();
        public VentanaDetalleEquipo()
        {
            InitializeComponent();
            mostrarEquipo();
        }

        private void mostrarEquipo()
        {
            var dtEquipo = equipoController.ObtenerDetalleEquipo((int)EquipoCache.IdEquipo);

            if (dtEquipo.Rows.Count > 0)
            {
                DataRow fila = dtEquipo.Rows[0];

                // Asignar directamente a los TextBox
                
                txtNumSerie.Text = fila["Num_serie"].ToString();
                txtMatricula.Text = fila["Matricula"].ToString();
                txtModelo.Text = fila["TipoEquipo"].ToString() +" "+fila["ModeloEquipo"].ToString();
                txtEstado.Text = fila["Estado"].ToString();
                txtObservacion.Text = fila["Observacion"].ToString();
                txtDestino.Text = fila["Destino"].ToString();
                txtFechaEntrega.Text = fila["Fecha_Entrega"].ToString();
                txtGarantia.Text = "Quedan " + fila["DiasGarantiaRestantes"].ToString() + " días";
                
            }
        }


        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReporte_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
