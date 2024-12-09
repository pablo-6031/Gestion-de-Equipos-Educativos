using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Entities;
using Gestion_de_Equipos_Educativos.Paginas;
using ServicioTecnicoPage = Gestion_de_Equipos_Educativos.Paginas.ServicioTecnico;    // Alias para la página

namespace Gestion_de_Equipos_Educativos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string rolIngreso = "";
        public MainWindow(string rol)
        {
            InitializeComponent();
            cargarDatos();
            rolIngreso = rol;
            permisosSegunRol(rol);

        }

        private void permisosSegunRol(string rol)
        {
            if (rol == "DOCENTE")
            {
                btnTipoEquipo.Visibility = Visibility.Collapsed;
                btnActa.Visibility = Visibility.Collapsed;
                btnInstitucion.Visibility = Visibility.Collapsed;
                btnProveedor.Visibility = Visibility.Collapsed;
                btnTipoEquipo.Visibility = Visibility.Collapsed;
            }
            else if(rol == "DIRECTOR")
            {
                btnTipoEquipo.Visibility = Visibility.Collapsed;
                btnInstitucion.Visibility = Visibility.Collapsed;
                btnProveedor.Visibility = Visibility.Collapsed;
                btnTipoEquipo.Visibility = Visibility.Collapsed;
            }else if (rol == "TECNICO")
            {
                btnProveedor.Visibility = Visibility.Collapsed;
                btnTipoEquipo.Visibility = Visibility.Collapsed;
            }
        }

        private void cargarDatos()
        {

            this.txtApellido.Text=  UsuarioCache.Apellido;
            this.txtNombre.Text = UsuarioCache.Nombre;
            this.FotoUsuario.Fill = ConvertirBytesAImageBrush(UsuarioCache.Foto);
        }

        public ImageBrush ConvertirBytesAImageBrush(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            // Convertir los bytes a un BitmapImage
            BitmapImage bitmap = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = ms;
                bitmap.EndInit();
            }

            // Crear un ImageBrush con el BitmapImage
            return new ImageBrush { ImageSource = bitmap };
        }

        private void mover(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new Usuarios(rolIngreso));
        }

        private void btnInventario_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new Inventario(rolIngreso));
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
            FramePrincipal.Navigate(new Alumnos(rolIngreso));
        }

        private void btnProveedor_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new Proveedores());
        }

        private void btnActa_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new Actas(rolIngreso));
        }

        private void btnServicioTecnico_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new ServicioTecnicoPage());
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

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnPrestamo_Click(object sender, RoutedEventArgs e)
        {
            FramePrincipal.Navigate(new Prestamos());
        }
    }
}
