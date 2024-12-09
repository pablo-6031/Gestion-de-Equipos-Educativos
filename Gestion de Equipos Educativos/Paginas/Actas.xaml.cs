using Controllers;
using Entities;
using Gestion_de_Equipos_Educativos.Ventanas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Rectangle = System.Windows.Shapes.Rectangle;


namespace Gestion_de_Equipos_Educativos.Paginas
{
    /// <summary>
    /// Lógica de interacción para Actas.xaml
    /// </summary>
    public partial class Actas : Page
    {
        InstitucionController institucionController = new InstitucionController();
        ProveedorController proveedorController = new ProveedorController();
        ActaController actaController = new ActaController();
        EquipoController equipoController = new EquipoController();
        List<Equipo> ListaEquipoTemp = new List<Equipo>();
        List<Equipo> ListaEquipoGuardado = new List<Equipo>();
        List<Equipo> ListaEquipoCombinada = new List<Equipo>();
        List<Equipo> ListaEquiposNormal = new List<Equipo>();
        List<Equipo> ListaAdms = new List<Equipo>();
        int idActaSeleccionada=0;
        private Dictionary<int, string> institucion = new Dictionary<int, string>();
        private Dictionary<int, string> proveedor = new Dictionary<int, string>();
        private Dictionary<int, string> unidad = new Dictionary<int, string>();

        private string imageSource = null;
        byte[] FotoActa;
        string estado = "";


        public Actas(string rol)
        {
            InitializeComponent();
            ListarActas();
            LlenarComboBox();
            ColumnaActa.Width = new GridLength(0);
            permisoSegunRol(rol);
        }

        private void permisoSegunRol(string rol)
        {
            if (rol == "DIRECTOR")
            {
                this.btnAgregar.Visibility = Visibility.Collapsed;
                OcultarBotonesEnGrid();
            }

        }
        private void OcultarBotonesEnGrid()
        {
            foreach (DataGridRow row in DGActas.Items.OfType<object>()
                     .Select(item => DGActas.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow)
                     .Where(row => row != null))
            {
                // Ocultar botón Eliminar
                Button btnEliminarFila = DGActas.Columns[0] // Suponiendo que está en la primera columna
                    .GetCellContent(row)?
                    .FindName("btnEliminarFila") as Button;

                if (btnEliminarFila != null)
                {
                    btnEliminarFila.Visibility = Visibility.Collapsed;
                }

                // Ocultar botón Editar
                Button btnEditarFila = DGActas.Columns[1] // Suponiendo que está en la segunda columna
                    .GetCellContent(row)?
                    .FindName("btnEditarFila") as Button;

                if (btnEditarFila != null)
                {
                    btnEditarFila.Visibility = Visibility.Collapsed;
                }
            }
        }


        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dividirLista();
                string estadoActa = "";
                string mensaje = "¿Desea finalizar el acta o guardarla como borrador para editarla después?";
                bool resp = mostrarVentanaEleccion(mensaje, "FINALIZAR", "BORRADOR");
                if (resp)
                {
                    estadoActa = "COMPLETA";
                }
                else
                {
                    estadoActa = "BORRADOR";
                }
                if (!string.IsNullOrEmpty(txtNumeroActa.Text) && !string.IsNullOrEmpty(txtResponsable.Text))
                {
                    Acta acta = new Acta
                    {
                        IdActa = idActaSeleccionada,
                        NumeroActa = txtNumeroActa.Text.ToUpper(),
                        Estado = estadoActa,
                        FechaEntrega = dpFechaEntrega.SelectedDate.HasValue ? dpFechaEntrega.SelectedDate.Value : DateTime.Now,
                        Responsable = txtResponsable.Text.ToUpper(),
                        IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue),
                        IdInstitucion = Convert.ToInt32(cmbInstitucion.SelectedValue),
                        Foto = FotoActa != null ? FotoActa : null
                    };

                    try
                    {
                        if (idActaSeleccionada == 0)
                        {
                            //AGREGA UNA NUEVA ACTA CON EQUIPOS
                            actaController.AgregarActaConEquipos(acta, ListaEquiposNormal, ListaAdms);
                            //actaController.AgregarActa(acta);
                            string mensaj = "Acta agregada con éxito";
                            mostrarVentana(mensaj, "AGREGAR");
                        }
                        else
                        {
                            //EDITA LA ACTA SELECCIONDA AGREGANDO NUEVOS EQUIPOS
                            actaController.EditarActaConEquipos(acta, ListaEquipoTemp);
                            //actaController.EditarActaConAdm(acta, ListaAdms);
                            //actaController.AgregarActa(acta);
                            string mensaj = "Acta editada con éxito";
                            mostrarVentana(mensaj, "EDITAR");
                        }

                    }
                    catch (Exception)
                    {
                        string mensaj = "Error al guardar el acta";
                        mostrarVentanaError(mensaj, "ERROR");
                    }
                }
                else
                {
                    string mensaj = "Complete los campos obligatorios";
                    mostrarVentanaError(mensaj, "ADVERTENCIA");
                }


            }
            catch (Exception ex)
            {
                string mensaj = ex.Message;
                mostrarVentanaError(mensaj, "ERROR");
            }

            ListarActas();
            LimpiarCampos();
            estado = "";




        }


        private void dividirLista()
        {
            foreach (Equipo item in ListaEquipoTemp)
            {
                if (item.TipoEquipo == "GABINETE MOVIL") 
                {
                    ListaAdms.Add(item);
                }
                else
                {
                    ListaEquiposNormal.Add(item);
                }
            }
        }


        private void ListarActas()
        {
            DataTable dataTable = actaController.ListarActas();
            
            if (dataTable.Rows.Count > 0)
             {
                DGActas.ItemsSource = dataTable.DefaultView;
              }
        }

       

        private void ListarEquipos(List<Equipo> list)
        {

            if (list.Count > 0)
            {
                DGEquipos.ItemsSource = null;
                DGEquipos.ItemsSource = list;
            }
        }

        private void LimpiarCampos()
        {
            txtNumeroActa.Text = "";
            dpFechaEntrega.SelectedDate = null;
            txtResponsable.Text = "";
            cmbProveedor.SelectedIndex = -1;
            cmbInstitucion.SelectedIndex = -1;

            ImageBrush fotoActa = new ImageBrush();
            fotoActa.ImageSource = new BitmapImage(new Uri(@"default_image.png", UriKind.RelativeOrAbsolute));

            FotoActa = null;

            ColumnaActa.Width = new GridLength(0);
        }

        
        private void btnCargarImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (.jpg)|*.jpg|PNG(*.png)|*.png|All files(*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            bool? revisarOK = openFileDialog.ShowDialog();
            if (revisarOK == true)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(openFileDialog.FileName));
                imageSource = openFileDialog.FileName.ToString();
                ImageBrush fotoActa = new ImageBrush();
                fotoActa.ImageSource = bitmapImage;
                this.eFoto.Fill = fotoActa;

                FotoActa = File.ReadAllBytes(openFileDialog.FileName);
            }
        }

        private void DGActas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {


        }

        public List<Equipo> ConvertirDataTableEnListaDeEquipos(DataTable dataTable)
        {
            var listaEquipos = new List<Equipo>();

            foreach (DataRow fila in dataTable.Rows)
            {
                var equipo = new Equipo
                {
                    IdEquipo = Convert.ToInt32(fila["id_equipo"]),
                    NumSerie = fila["num_serie"].ToString(),
                    Matricula = fila["matricula"].ToString(),
                    Estado = fila["estado"].ToString(),
                    Observacion = fila["observacion"].ToString(),
                    IdTipoEquipo = Convert.ToInt32(fila["id_tipo_equipo"]),
                    IdActa = Convert.ToInt32(fila["id_acta"]),
                    TipoEquipo = fila["tipo_equipo"].ToString(),
                    Modelo = fila["modelo_equipo"].ToString()

                };

                listaEquipos.Add(equipo);
            }

            return listaEquipos;
        }

        private void CargarImagen(object foto, Rectangle destino)
        {
            ImageBrush miFoto = new ImageBrush();

            if (foto is byte[] fotoBytes && fotoBytes.Length > 0)
            {
                // Convertir byte[] a BitmapImage
                using (MemoryStream ms = new MemoryStream(fotoBytes))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.CacheOption = BitmapCacheOption.OnLoad; // Asegura que se cargue completamente
                    image.EndInit();

                    miFoto.ImageSource = image;
                }
            }
            else
            {
                // Cargar imagen predeterminada
                BitmapImage defaultImage = new BitmapImage(new Uri("C:\\Users\\pablo\\source\\repos\\TP2-Proyecto_WPF_2024\\Views\\Imagenes\\hombre.png", UriKind.RelativeOrAbsolute));
                miFoto.ImageSource = defaultImage;
            }

            // Asignar el ImageBrush al Ellipse
            destino.Fill = miFoto;
        }


        private void LlenarComboBox()
        {
            DataTable institucionTable = institucionController.ListaInstituciones();
            DataTable proveedorTable = proveedorController.listarProveedores();


            if (institucionTable.Rows.Count > 0)
            {
                foreach (DataRow row in institucionTable.Rows)
                {
                    int id = Convert.ToInt32(row["Id_institucion"]);
                    string nombre = row["nombre"].ToString();
                    institucion[id] = nombre;
                }
                cmbInstitucion.ItemsSource = institucion;
                cmbInstitucion.DisplayMemberPath = "Value";
                cmbInstitucion.SelectedValuePath = "Key";
            }

            if (proveedorTable.Rows.Count > 0)
            {
                foreach (DataRow row in proveedorTable.Rows)
                {
                    int id = Convert.ToInt32(row["id_proveedor"]);
                    string nombre = row["nombre"].ToString();
                    proveedor[id] = nombre;
                }
                cmbProveedor.ItemsSource = proveedor;
                cmbProveedor.DisplayMemberPath = "Value";
                cmbProveedor.SelectedValuePath = "Key";
            }


        }

        private void btnAgregarEquipo_Click(object sender, RoutedEventArgs e)
        {
            mostrarVentana("agregar");


            if (EquipoCache.IdTipoEquipo != 0 && EquipoCache.Destino != null )
            {
                Equipo equipo = new Equipo();
                equipo.NumSerie = EquipoCache.NumSerie;
                equipo.Matricula = EquipoCache.Matricula;
                equipo.Estado = EquipoCache.Estado;
                equipo.Observacion = EquipoCache.Observacion;
                equipo.Destino = EquipoCache.Destino;
                equipo.IdTipoEquipo = EquipoCache.IdTipoEquipo;
                equipo.TipoEquipo = EquipoCache.TipoEquipo;
                equipo.IdActa = 0;

                ListaEquipoTemp.Add(equipo);
                ListaEquipoCombinada.Add(equipo);

            }
            
            ListarEquipos(ListaEquipoCombinada);
            LimpiarEquipoCache();
        }

        private void LimpiarEquipoCache()
        {
            EquipoCache.IdEquipo = 0;
            EquipoCache.NumSerie = "";
            EquipoCache.Matricula = "";
            EquipoCache.Estado = "";
            EquipoCache.Observacion = "";
            EquipoCache.Destino = "";
            EquipoCache.TipoEquipo = "";
            EquipoCache.Modelo = "";
            EquipoCache.IdTipoEquipo = 0;
        }

        private void mostrarVentana(string op)
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

            if (EquipoCache.TipoEquipo == "GABINETE MOVIL" )
            {
                // Crea una instancia del modal
                VentanaAdm ventanaAdm = new VentanaAdm
                {
                    Owner = mainWindow // Establece el propietario
                };

                ventanaAdm.ShowDialog(); // Abre la ventana
            }
            else 
            {
                // Crea una instancia del modal
                NuevosEquipos nuevosEquipos = new NuevosEquipos(op)
                {
                    Owner = mainWindow // Establece el propietario
                };

                nuevosEquipos.ShowDialog(); // Abre la ventana

            }




            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }


        private void btnEliminarFila_Click(object sender, RoutedEventArgs e)
        {


            if (DGActas.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)DGActas.SelectedItem;
                int idActa = Convert.ToInt32(rowView["id_acta"]);

                try
                {
                    string mensaje = "¿Desea eliminar el acta N° " + rowView["id_acta"].ToString() + " del registro?";
                    bool resp = mostrarVentanaEleccion(mensaje, "ACEPTAR", "CANCELAR");
                    if (resp)
                    {
                        actaController.EliminarActa(idActa);
                        string mensaj = "El acta fue eliminada con éxito";
                        mostrarVentana(mensaj, "Eliminar");

                    }


                }
                catch (Exception ex)
                {
                    string mensaj = "Error al eliminar el proveedor";
                    mostrarVentanaError(mensaj, "Error");
                }
            }
            else
            {
                string mensaj = "Seleccione un proveedor para eliminar";
                mostrarVentanaError(mensaj, "Advertencia");
            }
            ListarActas();

        }

        private void btnEditarFila_Click(object sender, RoutedEventArgs e)
        {
            if (estado != "editando")
            {
                estado = "editando";
                ColumnaActa.Width = new GridLength(500, GridUnitType.Pixel);

                DataRowView rowView = (DataRowView)this.DGActas.SelectedItem;

                this.txtNumeroActa.Text = rowView["num_acta"].ToString();
                this.dpFechaEntrega.Text = rowView["fecha_entrega"].ToString();
                this.txtResponsable.Text = rowView["responsable"].ToString();
                this.cmbProveedor.SelectedValue = rowView["id_proveedor"].ToString();
                this.cmbInstitucion.SelectedValue = rowView["id_institucion"].ToString();


                soloLectura(false);



                // Cargar la imagen asociada
                CargarImagen(rowView["foto"] != DBNull.Value ? (byte[])rowView["foto"] : null, eFoto);

                idActaSeleccionada = (int)rowView["id_acta"];
                ActaCache.IdActa = idActaSeleccionada;

                DataTable dtEquipos = equipoController.ListarEquiposAdmPorActa(idActaSeleccionada);
                ListaEquipoGuardado = ConvertirDataTableEnListaDeEquipos(dtEquipos);
                ListarEquipos(ListaEquipoGuardado);
                ListaEquipoCombinada = ListaEquipoGuardado;
            }
            else
            {
                string mensaje = "¿Primero guarde el acta seleccionada?";
                mostrarVentanaError(mensaje, "GUARDAR");
            }
            

        }


        private void btnEliminarFilaEquipo_Click(object sender, RoutedEventArgs e)
        {


            // Obtener el botón que disparó el evento
            Button boton = sender as Button;

            // Obtener la fila (elemento de la lista) asociada al botón
            Equipo equipo = boton.DataContext as Equipo;

            if (equipo != null)
            {

                // Llamar al controlador para eliminar el acta de la base de datos
                try
                {
                    // equipoController.eliminarEquipo(equipo.IdEquipo);
                    if (equipo.IdActa != 0)
                    {
                        string mensaje = "¿Desea eliminar al equipo " + equipo.NumSerie + " del registro?";
                        bool resp = mostrarVentanaEleccion(mensaje, "ACEPTAR", "CANCELAR");
                        if (resp)
                        {
                            equipoController.eliminarEquipo(equipo.IdEquipo);
                            string mensaj = "Equipo eliminado con éxito";
                            mostrarVentana(mensaj, "ELIMINAR");
                            ListaEquipoCombinada.Remove(equipo);
                        }

                    }
                    else
                    {

                        ListaEquipoTemp.Remove(equipo);
                        ListaEquipoCombinada.Remove(equipo);
                    }

                    
                }
                catch (Exception ex)
                {
                    string mensaj = "Error al eliminar el equipo";
                    mostrarVentanaError(mensaj, "Eliminar");
                }

                
                

                ListarEquipos(ListaEquipoCombinada);



            }


        }


        private bool mostrarVentanaEleccion(string mensaje,string boton1, string boton2)
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

            // Crea una instancia del modal y envía un parámetro
            VentanaMensajeEleccion ventanaMensaje = new VentanaMensajeEleccion(mensaje, boton1, boton2)
            {
                Owner = mainWindow, // Establece el propietario
            };

            // Abre la ventana
            bool? dialogResult = ventanaMensaje.ShowDialog();



            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
            return ventanaMensaje.Eleccion;
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

        private void mostrarVentana(string mensaje, string titulo)
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

        private void btnEditarFilaEquipo_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que se clickeó
            if (sender is Button button && button.DataContext is Entities.Equipo equipoSeleccionado)
            {

                // Cargar los datos del acta seleccionada en los controles
                EquipoCache.IdEquipo = equipoSeleccionado.IdEquipo;
                EquipoCache.NumSerie = equipoSeleccionado.NumSerie;
                EquipoCache.Matricula = equipoSeleccionado.Matricula;
                EquipoCache.Estado = equipoSeleccionado.Estado;
                EquipoCache.Observacion = equipoSeleccionado.Observacion;
                EquipoCache.Destino = equipoSeleccionado.Destino;
                EquipoCache.IdTipoEquipo = equipoSeleccionado.IdTipoEquipo;
                EquipoCache.TipoEquipo = equipoSeleccionado.TipoEquipo;
                EquipoCache.Modelo = equipoSeleccionado.Modelo;

                mostrarVentana("editar");

                LimpiarEquipoCache();
            }

        }



        private void DGActas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAgregarActa_Click(object sender, RoutedEventArgs e)
        {
            // Mostrar columna (por ejemplo, restableciendo su ancho original)
            LimpiarCampos();
            LimpiarEquipoCache();
            estado = "";
            ListaEquipoCombinada.Clear();


            DGEquipos.ItemsSource = null;
            DGEquipos.ItemsSource = ListaEquipoCombinada;





            ColumnaActa.Width = new GridLength(500, GridUnitType.Pixel);
        }

        private void txtBuscarActa_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = txtBuscarActa.Text.Trim();
            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                // Si no hay texto, mostrar todos los docentes
                ListarActas();
            }
            else
            {

                // Llamar al controlador para obtener los datos filtrados

                DataTable dataTable = actaController.FiltrarActasPorFecha(textoBusqueda);

                // Actualizar el DataGrid
                DGActas.ItemsSource = dataTable.DefaultView;
            }
        }

        private void btnVerrFila_Click(object sender, RoutedEventArgs e)
        {
            if (estado != "editando")
            {
                if (!this.DGActas.Items.IsEmpty)
                {
                    ColumnaActa.Width = new GridLength(500, GridUnitType.Pixel);

                    DataRowView rowView = (DataRowView)this.DGActas.SelectedItem;

                    this.txtNumeroActa.Text = rowView["num_acta"].ToString();
                    this.dpFechaEntrega.Text = rowView["fecha_entrega"].ToString();
                    this.txtResponsable.Text = rowView["responsable"].ToString();
                    this.cmbProveedor.SelectedValue = rowView["id_proveedor"].ToString();
                    this.cmbInstitucion.SelectedValue = rowView["id_institucion"].ToString();


                    soloLectura(true);





                    // Cargar la imagen asociada
                    CargarImagen(rowView["foto"] != DBNull.Value ? (byte[])rowView["foto"] : null, eFoto);

                    idActaSeleccionada = (int)rowView["id_acta"];
                    DataTable dtEquipos = equipoController.ListarEquiposAdmPorActa(idActaSeleccionada);
                    ListaEquipoGuardado = ConvertirDataTableEnListaDeEquipos(dtEquipos);
                    ListarEquipos(ListaEquipoGuardado);

                }
            }
            else
            {
                string mensaje = "¿Primero guarde el acta seleccionada?";
                 mostrarVentanaError(mensaje, "GUARDAR");
                
            }

            
            
        }
        private void soloLectura(bool opcion)
        {
            txtNumeroActa.IsReadOnly = opcion;
            dpFechaEntrega.IsEnabled = !opcion;
            txtResponsable.IsReadOnly = opcion;
            cmbProveedor.IsEnabled = !opcion;
            cmbInstitucion.IsEnabled = !opcion;

            btnAgregarEquipo.IsEnabled = !opcion;
            btnCargarImagen.IsEnabled = !opcion;
            if (opcion) 
            {
                DGEquipos.Columns[9].Visibility = Visibility.Hidden;
            }
            else
            {
                DGEquipos.Columns[9].Visibility = Visibility.Visible;
            }


        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarEquipoCache();
            ColumnaActa.Width = new GridLength(0);
            estado = "";
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            DateTime? fechaInicio = dpFechaDesde.SelectedDate;
            DateTime? fechaFin = dpFechaHasta.SelectedDate;

            if (fechaInicio == null || fechaFin == null)
            {

                string mensaje = "Por favor seleccione ambas fechas.";
                mostrarVentanaError(mensaje, "ADVERTENCIA");
                return;
            }

            // Validar que la fecha de inicio sea menor o igual a la fecha final
            if (fechaInicio > fechaFin)
            {
                string mensaj = "La fecha de inicio no puede ser mayor que la fecha final.";
                mostrarVentanaError(mensaj, "ERROR");
                return;
            }
            

            // Lógica para buscar entre fechas
            BuscarEntreFechas(fechaInicio.Value, fechaFin.Value);
        }

        private void BuscarEntreFechas(DateTime desde, DateTime hasta)
        {
            DataTable dataTable = actaController.ListarActasPorFechas(desde,hasta);

            if (dataTable.Rows.Count > 0)
            {
                DGActas.ItemsSource = dataTable.DefaultView;
            }
        }


        private void btnImprimirActa_Click(object sender, RoutedEventArgs e)
        {
            int idActaFila = obtenerIdActa();
            Reportes.FormReporteTablaActa fru = new Reportes.FormReporteTablaActa();
            fru.id_acta = Convert.ToInt32(idActaFila);
            fru.ShowDialog();
        }

        private int obtenerIdActa()
        {

            DataRowView rowView = (DataRowView)this.DGActas.SelectedItem;

            int numero = (int)rowView["id_acta"];

            return numero;

        }
    }
}
