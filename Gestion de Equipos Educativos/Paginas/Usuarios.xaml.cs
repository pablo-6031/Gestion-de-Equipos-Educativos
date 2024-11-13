using Controllers;
using Entities;
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
using Models;

namespace Gestion_de_Equipos_Educativos.Paginas
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : Page
    {
        UsuarioController usuarioController = new UsuarioController();
        MemoryStream memoryStream;
        MemoryStream stream;
        private string imageSource = null;
        byte[] FotoUsuario;
        UsuarioDao usuarioDao = new UsuarioDao();
        //Mensaje mensaje = new Mensaje();
        //private static Configuracion _instancia = null;

        public Usuarios()
        {
            InitializeComponent();
            ListarUsuarios();
        }



        private void ListarUsuarios()
        {
            var Dtusuarios = usuarioDao.ListarUsuarios();
            TraerUsuarios(Dtusuarios);
        }

        private void TraerUsuarios(DataTable dtusuarios)
        {
            List<Usuario> usuariosList = new List<Usuario>();
            usuariosList = (from DataRow dr in dtusuarios.Rows
                            select new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Apellidos = dr["Apellidos"].ToString(),
                                Nombres = dr["Nombres"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Cuil = dr["Cuil"].ToString(),
                                Password = dr["Password"].ToString(),
                                Rol = dr["Rol"].ToString(),
                                LoginName = dr["LoginName"].ToString(),
                                Foto = dr["Foto"] != DBNull.Value ? (byte[])dr["Foto"] : null
                            }).ToList();
            this.DGUsuarios.ItemsSource = usuariosList;
        }
        private void limpiarCampos()
        {
            this.txtNombre.Clear();
            this.txtApellido.Clear();
            this.txtDireccion.Clear();
            this.txtCorreo.Clear();
            this.txtIdUsuario.Clear();
            this.txtDni.Clear();
            this.txtPassword.Clear();
            this.txtIdUsuario.Clear();
            this.cmbRol.Items.Clear();

            ImageBrush fotoUsuario = new ImageBrush();
            fotoUsuario.ImageSource = new BitmapImage(new Uri(@"hombre.png", UriKind.RelativeOrAbsolute));
            this.eFoto.Fill = fotoUsuario;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCargarImagen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
