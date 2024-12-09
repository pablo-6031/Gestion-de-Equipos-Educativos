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
    /// Lógica de interacción para VentanaMensajeEleccion.xaml
    /// </summary>
    public partial class VentanaMensajeEleccion : Window
    {



        public bool Eleccion { get; private set; }

        public VentanaMensajeEleccion(string mensaje,string boton1, string boton2)
        {
            InitializeComponent();
            btnAceptar.Content = boton1;
            btnCancelar.Content = boton2;
            // Configura el mensaje en la ventana
            txtMensaje.Text = mensaje; // Suponiendo que tienes un TextBlock llamado lblMensaje
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Eleccion = true;
            this.Close();
        }


        private void btnCancelar_Click_1(object sender, RoutedEventArgs e)
        {
            Eleccion = false;
            this.Close();
        }
    }
}
