using Controllers;
using Entities;
using Gestion_de_Equipos_Educativos.Paginas;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
using System.Windows.Shapes;

namespace Gestion_de_Equipos_Educativos.Ventanas
{
    /// <summary>
    /// Lógica de interacción para NuevosEquipos.xaml
    /// </summary>
    public partial class NuevosEquipos : Window
    {
        TipoEquipoController tipoEquipoController = new TipoEquipoController();
        EquipoController equipoController = new EquipoController();
        MemoryStream memoryStream;
        string opcion = "";

        public NuevosEquipos(string op)
        {
            InitializeComponent();
            verificarIngreso();
            opcion = op;
        }

        private void verificarIngreso()
        {
            if (EquipoCache.IdEquipo == 0 || EquipoCache.IdEquipo == null)
            {
                LimpiarCampos();
                LlenarComboBoxTipo();
            }
            else
            {
                cargarDatos();
            }
        }

        private void cargarDatos()
        {
            TipoEquipo tipoEquipo = new TipoEquipo();

            tipoEquipo = tipoEquipoController.TraerTipoEquipos(EquipoCache.IdTipoEquipo);


            this.txtNumSerie.Text = EquipoCache.NumSerie;
            this.txtMatricula.Text = EquipoCache.Matricula;
            //this.cmbEstado.Text = EquipoCache.Estado;
            this.txtObservacion.Text = EquipoCache.Observacion;
            this.cmbDestino.Text = EquipoCache.Destino;
            LlenarComboBoxTipo();
            // CARGAR EL CMBTIPO
            string tipo = tipoEquipo.Tipo;
            if (!cmbTipo.Items.Contains(tipo))
            {
                cmbTipo.Items.Add(tipo);
            }
            cmbTipo.SelectedItem = tipo;
            //cargar cmbmodelo
            LlenarComboBox(tipo);

            this.cmbModelo.Text = tipoEquipo.Modelo;


            string modelo = EquipoCache.Estado;
            if (!cmbEstado.Items.Contains(modelo))
            {
                cmbEstado.Items.Add(modelo);
            }
            cmbEstado.SelectedItem = modelo;




        }

        private void mover(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private Dictionary<int, string> tiposEquipos = new Dictionary<int, string>();

        private void LlenarComboBoxTipo()
        {
            if (EquipoCache.TipoEquipo == "GABINETE MOVIL")
            {
                List<string> tiposEquiposNombre = tipoEquipoController.ObtenerTiposEquiposUnicos();
                tiposEquiposNombre.Remove("GABINETE MOVIL");
                cmbTipo.ItemsSource = tiposEquiposNombre;
            }
            else
            {
                List<string> tiposEquipos = tipoEquipoController.ObtenerTiposEquiposUnicos();
                cmbTipo.ItemsSource = tiposEquipos;
            }
            
        }




        private void LlenarComboBox(String nombre)
        {
            tiposEquipos.Clear();
            DataTable dataTable = tipoEquipoController.ListaTipoEquipos();
            string nom = "";
            if (dataTable.Rows.Count > 0)
            {

                foreach (DataRow row in dataTable.Rows)
                {
                    nom = row["tipo"].ToString();
                    if (nombre.ToLower() == nom.ToLower())
                    {
                        int id = Convert.ToInt32(row["Id_Tipo_Equipo"]);
                        string tipo = row["modelo"].ToString();
                        tiposEquipos[id] = tipo;
                    }
                    
                }

                cmbModelo.ItemsSource = tiposEquipos;
                cmbModelo.DisplayMemberPath = "Value";    // Muestra el valor (tipo)
                cmbModelo.SelectedValuePath = "Key";      // Usa el ID (IdTipoEquipo) como valor seleccionado
            }
            else
            {

            }
        }









        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                if (opcion == "editar")
                {
                    Equipo equipo = new Equipo
                    {
                        IdEquipo = (int)EquipoCache.IdEquipo,
                        NumSerie = txtNumSerie.Text.ToUpper(),
                        Matricula = txtMatricula.Text.ToUpper(),
                        Estado = cmbEstado.Text.ToUpper(),
                        Observacion = txtObservacion.Text,
                        Destino = cmbDestino.Text.ToUpper(),
                        IdTipoEquipo = EquipoCache.IdTipoEquipo

                    };

                    try
                    {
                        string mensaj = equipoController.EditarEquipo(equipo);
                        mostrarVentanaMensaje(mensaj, "Editar");

                    }
                    catch (Exception ex)
                    {
                        string mensaj = "Error al actualizar el alumno";
                        mostrarVentanaError(mensaj, "Error");
                    }
                }
                else
                {
                    bool resp = equipoController.ComprobarEquipo(txtNumSerie.Text, txtMatricula.Text);
                    if (resp)
                    {
                        string mensaj = "Existe un equipo con el mismo número de serie o la misma matricula";
                        mostrarVentanaError(mensaj, "Error");
                    }
                    else
                    {
                        EquipoCache.NumSerie = txtNumSerie.Text.ToUpper();
                        EquipoCache.Matricula = txtMatricula.Text.ToUpper();
                        EquipoCache.Estado = cmbEstado.Text.ToUpper();
                        EquipoCache.Observacion = txtObservacion.Text;
                        EquipoCache.Destino = cmbDestino.Text.ToUpper();
                        EquipoCache.IdTipoEquipo = (int)cmbModelo.SelectedValue;
                        EquipoCache.TipoEquipo = cmbTipo.Text;

                        //this.Close();
                        /*
                        var selectedItem = cmbTipo.SelectedItem as ComboBoxItem;
                        if (selectedItem != null)
                        {
                            string selectedValue = selectedItem.Content.ToString();
                        }
                        */
                    }

                    
                }
                


            }
            catch (Exception ex)
            {
                string mensaj = ex.Message;
                mostrarVentanaError(mensaj, "Error");
            }

            this.Close();


        }

        private void LimpiarCampos()
        {

            this.txtNumSerie.Text = "";
            this.txtMatricula.Text = "";
            this.cmbEstado.Text = "";
            this.txtObservacion.Text = "";
            this.cmbDestino.Text = "";
            this.cmbTipo.Text = "";
            this.cmbModelo.Text = "";


        }
        
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /*
        private void cmbModelo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var FotoTipoEquipo = (byte[])tipoEquipoController.ObtenerFoto((int)cmbModelo.SelectedValue);


            if (FotoTipoEquipo != null)
            {
                MemoryStream ms = new MemoryStream(FotoTipoEquipo);
                memoryStream = ms;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = ms;
                image.CacheOption = BitmapCacheOption.OnLoad; // Asegura que la imagen se cargue completamente
                image.EndInit();

                ImageBrush miFoto = new ImageBrush
                {
                    ImageSource = image
                };
                this.eFoto.Fill = miFoto; // Asigna la imagen al control eFoto
            }
            else
            {
                // Si no hay imagen, limpiar la imagen de eFoto
                this.eFoto.Fill = null;
            }



        }

        */




        private void cmbModelo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verificar si hay un valor seleccionado
            if (cmbModelo.SelectedValue == null)
            {
                // Si no hay un valor seleccionado, limpiar la imagen de eFoto
                this.eFoto.Fill = null;
                return;
            }

            try
            {
                // Obtener la foto del tipo de equipo
                var FotoTipoEquipo = tipoEquipoController.ObtenerFoto((int)cmbModelo.SelectedValue);

                if (FotoTipoEquipo != null && FotoTipoEquipo.Length > 0)
                {
                    // Convertir los bytes en una imagen y mostrarla
                    using (MemoryStream ms = new MemoryStream(FotoTipoEquipo))
                    {
                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.StreamSource = ms;
                        image.CacheOption = BitmapCacheOption.OnLoad; // Asegura que la imagen se cargue completamente
                        image.EndInit();

                        ImageBrush miFoto = new ImageBrush
                        {
                            ImageSource = image
                        };
                        this.eFoto.Fill = miFoto; // Asigna la imagen al control eFoto
                    }
                }
                else
                {
                    // Si no hay imagen disponible, limpiar la imagen de eFoto
                    this.eFoto.Fill = null;
                }
            }
            catch (Exception ex)
            {

                string mensaj = "Ocurrió un error al cargar la imagen";
                mostrarVentanaError(mensaj, "ERROR");
            }
        }




        private void cmbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbModelo.ItemsSource = null;
            cmbModelo.Items.Clear();

            //ComboBoxItem selectedItem = (ComboBoxItem)cmbTipo.SelectedItem;
            //string tipo = selectedItem.Content.ToString();
            string tipo = cmbTipo.SelectedItem?.ToString();
            LlenarComboBox(tipo);
        }

        private void txtMatricula_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void mostrarVentanaMensaje(string mensaje, string titulo)
        {
            // Referencia a la ventana principal
            var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;

            // Aplica el desenfoque al contenido principal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = new BlurEffect
                {
                    Radius = 10
                };
            }

            // Crea una instancia del modal
            VentanaMensaje ventanaMensaje = new VentanaMensaje(mensaje, titulo)
            {
                Owner = mainWindow // Establece el propietario
            };

            ventanaMensaje.ShowDialog(); // Abre la ventana


            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private void mostrarVentanaError(string mensaje, string titulo)
        {
            // Referencia a la ventana principal
            var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;

            // Aplica el desenfoque al contenido principal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = new BlurEffect
                {
                    Radius = 10
                };
            }

            // Crea una instancia del modal
            VentanaMensajeError ventanaMensaje = new VentanaMensajeError(mensaje, titulo)
            {
                Owner = mainWindow // Establece el propietario
            };

            ventanaMensaje.ShowDialog(); // Abre la ventana


            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private void btnTecnico_Click(object sender, RoutedEventArgs e)
        {
            if (EquipoCache.Estado == "ENTREGADO" || EquipoCache.Estado == "EN DEPOSITO" || EquipoCache.Estado != "RECIBIDA EN MIGRACION" || EquipoCache.Estado != "EN PRESTAMO")
            {
                bool resp = equipoController.VerificarGarantiaEquipo((int)EquipoCache.IdEquipo);
                if (resp)
                {
                    mostrarVentanaServicio("agregar");
                }
                else
                {
                    string mensaj = "La garantía está vencida";
                    mostrarVentanaError(mensaj, "ERROR");
                }
                
            }
            else
            {
                string mensaj = "La garantía está vencida";
                mostrarVentanaError(mensaj, "ERROR");
            }
            
        }


        private void mostrarVentanaServicio(string op)
        {
            // Referencia a la ventana principal
            var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;

            // Aplica el desenfoque al contenido principal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = new BlurEffect
                {
                    Radius = 10
                };
            }

            // Crea una instancia del modal
            VentanaServicioTecnico ventanaServTec = new VentanaServicioTecnico(op)
            {
                Owner = mainWindow // Establece el propietario
            };

            ventanaServTec.ShowDialog(); // Abre la ventana


            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private void mostrarVentanaAlumno(string op)
        {
            // Referencia a la ventana principal
            var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;

            // Aplica el desenfoque al contenido principal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = new BlurEffect
                {
                    Radius = 10
                };
            }

            // Crea una instancia del modal
            VentanaAlumnos ventanaAlumno = new VentanaAlumnos(op)
            {
                Owner = mainWindow // Establece el propietario
            };

            ventanaAlumno.ShowDialog(); // Abre la ventana


            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private void btnAlumno_Click(object sender, RoutedEventArgs e)
        {
            if ((EquipoCache.Estado == "EN DEPOSITO" || EquipoCache.Estado == "RECIBIDA EN MIGRACION") && EquipoCache.Estado == "RECIBIDA EN MIGRACION")
            {
                if (EquipoCache.Destino == "SIN ASIGNAR" || EquipoCache.Destino == "ALUMNO")
                {
                    mostrarVentanaAlumno("agregar");
                }
                else
                {
                    string mensaj = "No se puede asignar este equipo";
                    mostrarVentanaError(mensaj, "ERROR");
                }
                
            }
            else
            {
                string mensaj = "No se puede asignar este equipo";
                mostrarVentanaError(mensaj, "ERROR");
            }
        }



    }
}
