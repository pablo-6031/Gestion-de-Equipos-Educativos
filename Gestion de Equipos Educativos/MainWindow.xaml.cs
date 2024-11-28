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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gestion_de_Equipos_Educativos.Paginas;

namespace Gestion_de_Equipos_Educativos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void mover(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new Usuarios());
        }

        private void btnInventario_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new Inventario());
        }

 

        private void btnTipoEquipo_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new TipoEquipos());
        }

        private void btnInstitucion_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new Instituciones());
        }

        private void btnAlumnos_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new Alumnos());
        }

        private void btnProveedor_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new Proveedores());
        }

        private void btnActa_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new Actas());
        }

        private void btnServicioTecnico_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new ServicioTecnico());
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }



        private void btnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
            
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
