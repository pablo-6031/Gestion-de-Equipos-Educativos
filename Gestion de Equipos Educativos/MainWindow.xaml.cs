﻿using System;
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
    }
}
