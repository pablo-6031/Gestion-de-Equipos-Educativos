using Controllers;
using Entities;
using Gestion_de_Equipos_Educativos.Ventanas;
using Microsoft.Extensions.Options;
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
        ServicioTecnicoController servicioTecnicoController = new ServicioTecnicoController();
        public ServicioTecnico()
        {
            InitializeComponent();
            listarEquipos();
        }

        private void mostrarVentana(string opcion)
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
            VentanaServicioTecnico servTec = new VentanaServicioTecnico(opcion)
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

        }

        private void listarEquipos()
        {
            
            var dtServicio = servicioTecnicoController.ListarServiciosTecnicosConEquipos();

            this.DGServiciosTecnicos.ItemsSource = dtServicio.DefaultView;
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textoBusqueda = txtBuscar.Text.Trim();
            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                // Si no hay texto, mostrar todos los docentes
                listarEquipos();
            }
            else
            {

                // Llamar al controlador para obtener los datos filtrados

                DataTable dataTable = servicioTecnicoController.FiltrarServiciosTecnicos(textoBusqueda);

                // Actualizar el DataGrid
                DGServiciosTecnicos.ItemsSource = dataTable.DefaultView;
            }
        }

        private void btnVerrFila_Click(object sender, RoutedEventArgs e)
        {
            if (!DGServiciosTecnicos.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGServiciosTecnicos.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                ServicioTecnicoCache.IdServicioTecnico = (int)rowView["Id_Servicio_Tecnico"];
                ServicioTecnicoCache.FechaEnvio = Convert.ToDateTime(rowView["Fecha_Envio"]);
                ServicioTecnicoCache.Responsable = rowView["Responsable"].ToString();
                ServicioTecnicoCache.NumSerie = rowView["Num_serie"].ToString();
                ServicioTecnicoCache.Falla = rowView["Falla"].ToString();
                ServicioTecnicoCache.Foto = rowView["Foto"] != DBNull.Value ? (byte[])rowView["Foto"] : null;

                mostrarVentana("ver");
            }
            listarEquipos();
        }

        


        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (!DGServiciosTecnicos.Items.IsEmpty)
            {
                DataRowView rowView = (DataRowView)DGServiciosTecnicos.SelectedItem;

                // Asigna los valores seleccionados en el DataGrid a los campos de la interfaz
                ServicioTecnicoCache.IdServicioTecnico = (int)rowView["Id_Servicio_Tecnico"];
                ServicioTecnicoCache.FechaEnvio = Convert.ToDateTime(rowView["Fecha_Envio"]);
                ServicioTecnicoCache.Responsable = rowView["Responsable"].ToString();
                ServicioTecnicoCache.NumSerie = rowView["Num_serie"].ToString();
                ServicioTecnicoCache.Falla = rowView["Falla"].ToString();
                ServicioTecnicoCache.Foto = rowView["Foto"] != DBNull.Value ? (byte[])rowView["Foto"] : null;

                mostrarVentana("editar");
            }
            listarEquipos();
        }
    }
}
