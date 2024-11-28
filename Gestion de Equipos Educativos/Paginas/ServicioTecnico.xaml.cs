using Gestion_de_Equipos_Educativos.Ventanas;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gestion_de_Equipos_Educativos.Paginas
{
    /// <summary>
    /// Lógica de interacción para ServiciosTecnicos.xaml
    /// </summary>
    public partial class ServicioTecnico : Page
    {
        public ServicioTecnico()
        {
            InitializeComponent();
        }

        private void mostrarVentana()
        {
            // Referencia a la ventana principal
            var mainWindow = Application.Current.MainWindow as MainWindow;

            // Aplica el desenfoque al contenido principal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = new BlurEffect
                {
                    Radius = 10
                };
            }

            // Crea una instancia del modal
            VentanaServicioTecnico servTec = new VentanaServicioTecnico
            {
                Owner = mainWindow // Establece el propietario
            };

            servTec.ShowDialog(); // Abre la ventana


            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            mostrarVentana();
        }
    }
}
