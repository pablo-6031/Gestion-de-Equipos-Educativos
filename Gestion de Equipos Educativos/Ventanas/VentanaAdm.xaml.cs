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
    /// Lógica de interacción para VentanaAdm.xaml
    /// </summary>
    public partial class VentanaAdm : Window
    {
        public VentanaAdm()
        {
            InitializeComponent();
        }

        private void btnAgregarActa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
