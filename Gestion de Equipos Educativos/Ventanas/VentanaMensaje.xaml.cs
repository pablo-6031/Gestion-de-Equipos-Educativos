using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para VentanaMensaje.xaml
    /// </summary>
    public partial class VentanaMensaje : Window
    {
        public VentanaMensaje(string mensaje, string titulo)
        {
            InitializeComponent();
            txtMensaje.Text = mensaje; // Suponiendo que tienes un TextBlock llamado lblMensaje
            txtTitulo.Content = titulo;
        }

        private void btnAceptar_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
