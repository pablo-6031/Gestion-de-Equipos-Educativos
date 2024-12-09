using Controllers;
using Entities;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Gestion_de_Equipos_Educativos
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private UsuarioController usuarioController = new UsuarioController();
        public Login()
        {
            InitializeComponent();
        }
        private void mover(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) // Verificar si el botón izquierdo está presionado
            {
                this.DragMove();
            }

        }

        private void txtLoginName_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtLoginName.Text == "Usuario")
            {
                txtLoginName.Text = string.Empty;
            }
        }

        private void txtLoginName_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtLoginName.Text == "")
            {
                txtLoginName.Text = "Usuario";
            }
        }

        private void txtPassword_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtPassword.Password == "Contraseña")
            {
                txtPassword.Password = string.Empty;
            }
        }

        private void txtPassword_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtPassword.Password == "")
            {
                txtPassword.Password = "Constraseña";
            }
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtLoginName.Text != "Usuario")
                {
                    if (txtPassword.Password != "Contraseña")
                    {
                        var validarLogin = usuarioController.login(txtLoginName.Text, txtPassword.Password);
                        if (validarLogin)
                        {
                            MainWindow ventanaPrincipal = new MainWindow(UsuarioCache.Rol);
                            txtLoginName.Clear();
                            txtPassword.Clear();
                            
                            ventanaPrincipal.ShowDialog();
                            ventanaPrincipal.Closed += Logout;
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error", "Error: ingrese la contraseña");
                    }
                }
                else
                {
                    MessageBox.Show("Error", "Error: ingrese el usuario");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Logout(object sender, EventArgs e)
        {
            try
            {
                txtLoginName.Text = "Usuario";
                txtPassword.Password = "Contraseña";
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(@"user.jpg", UriKind.Relative));
                this.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error", "Error: " + ex.Message);
            }
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
