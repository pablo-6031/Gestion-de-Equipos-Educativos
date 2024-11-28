using Controllers;
using Entities;
using Gestion_de_Equipos_Educativos.Ventanas;
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
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;


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
        int idActaSeleccionada=0;
        private Dictionary<int, string> institucion = new Dictionary<int, string>();
        private Dictionary<int, string> proveedor = new Dictionary<int, string>();
        private Dictionary<int, string> unidad = new Dictionary<int, string>();

        private string imageSource = null;
        byte[] FotoActa;

        public Actas()
        {
            InitializeComponent();
            ListarActas();
            LlenarComboBox();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNumeroActa.Text) && !string.IsNullOrEmpty(txtResponsable.Text))
                {
                    Acta acta = new Acta
                    {
                        NumeroActa = txtNumeroActa.Text,
                        Estado = "Completa",
                        FechaEntrega = dpFechaEntrega.SelectedDate.HasValue ? dpFechaEntrega.SelectedDate.Value : DateTime.Now,
                        Responsable = txtResponsable.Text,
                        IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue),
                        IdInstitucion = Convert.ToInt32(cmbInstitucion.SelectedValue),
                        Foto = FotoActa != null ? FotoActa : null
                    };

                    try
                    {
                        if (idActaSeleccionada == 0)
                        {
                            actaController.agregarActaConEquipos(acta, ListaEquipoTemp);
                            //actaController.AgregarActa(acta);
                            MessageBox.Show("Acta agregada con éxito", "Agregar");
                        }
                        else
                        {
                            equipoController.agregarListaEquipo(ListaEquipoTemp, idActaSeleccionada);
                            //actaController.AgregarActa(acta);
                            MessageBox.Show("Equipo agregado con éxito", "Agregar");
                        }
                        
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error al guardar el acta", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Complete los campos obligatorios", "Advertencia");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            ListarActas();
            LimpiarCampos();



            /*


            if (DGActas.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)DGActas.SelectedItem;
                int idActa = Convert.ToInt32(rowView["id_acta"]);

                Acta acta = new Acta
                {
                    IdActa = idActa,
                    NumeroActa = txtNumeroActa.Text,
                    Estado = "",
                    FechaEntrega = dpFechaEntrega.SelectedDate.HasValue ? dpFechaEntrega.SelectedDate.Value : DateTime.Now,
                    Responsable = txtResponsable.Text,
                    IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue),
                    IdInstitucion = Convert.ToInt32(cmbInstitucion.SelectedValue),
                    Foto = FotoActa != null ? FotoActa : null
                };

                try
                {
                    actaController.EditarActa(acta);
                    MessageBox.Show("Acta actualizada con éxito", "Editar");
                    ListarActas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el acta: " + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un acta para editar", "Advertencia");
            }

            */


        }

        private void ListarActas()
        {
            //DataTable dataTable = actaController.ListarActas();
            /*
                        if (dataTable.Rows.Count > 0)
                        {
                            DGActas.ItemsSource = dataTable.DefaultView;
                        }
            */
            var dtActas = actaController.ListarActas();
            TraerActas(dtActas);
        }

        private void TraerActas(DataTable dtActas)
        {
            List<Acta> actasList = (from DataRow dr in dtActas.Rows
                                                select new Acta()
                                                {
                                                    IdActa = Convert.ToInt32(dr["id_acta"]),
                                                    NumeroActa = dr["num_acta"].ToString(),
                                                    Estado = dr["estado"].ToString(),
                                                    FechaEntrega = DateTime.Parse(dr["fecha_entrega"].ToString()),
                                                    Responsable = dr["responsable"].ToString(),
                                                    IdProveedor = Convert.ToInt32(dr["id_proveedor"].ToString()),
                                                    IdInstitucion = Convert.ToInt32(dr["id_institucion"].ToString()),
                                                    Foto = dr["Foto"] == DBNull.Value ? null : (byte[])dr["Foto"]
                                                }).ToList();
            this.DGActas.ItemsSource = actasList;
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
            // Verificar que haya un elemento seleccionado
            if (DGActas.SelectedItem is Entities.Acta actaSeleccionada)
            {
                // Asignar los valores del objeto a los controles
                txtNumeroActa.Text = actaSeleccionada.NumeroActa;
                dpFechaEntrega.SelectedDate = actaSeleccionada.FechaEntrega;
                txtResponsable.Text = actaSeleccionada.Responsable;
                cmbProveedor.SelectedValue = actaSeleccionada.IdProveedor;
                cmbInstitucion.SelectedValue = actaSeleccionada.IdInstitucion;
                // Cargar la imagen asociada
                CargarImagen(actaSeleccionada.Foto, eFoto);

                idActaSeleccionada = actaSeleccionada.IdActa;
                DataTable dtEquipos = equipoController.listarEquiposPorActa(idActaSeleccionada);
                ListaEquipoGuardado = ConvertirDataTableEnListaDeEquipos(dtEquipos);
                ListarEquipos(ListaEquipoGuardado);


            }

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
                    Destino = fila["destino"].ToString(),
                    IdTipoEquipo = Convert.ToInt32(fila["id_tipo_equipo"]),
                    IdActa = Convert.ToInt32(fila["id_acta"])
                };

                listaEquipos.Add(equipo);
            }

            return listaEquipos;
        }

        private void CargarImagen(object foto, Ellipse destino)
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
            mostrarVentana();


            if (EquipoCache.NumSerie != null && EquipoCache.Matricula != null && EquipoCache.Estado != null && EquipoCache.Observacion != null && EquipoCache.Destino != null)
            {
                Equipo equipo = new Equipo();
                equipo.NumSerie = EquipoCache.NumSerie;
                equipo.Matricula = EquipoCache.Matricula;
                equipo.Estado = EquipoCache.Estado;
                equipo.Observacion = EquipoCache.Observacion;
                equipo.Destino = EquipoCache.Destino;
                equipo.IdTipoEquipo = EquipoCache.IdTipoEquipo;
                equipo.IdActa = 0;

                ListaEquipoTemp.Add(equipo);

            }
            
            ListarEquipos(ListaEquipoTemp);
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
            NuevosEquipos nuevosEquipos = new NuevosEquipos
            {
                Owner = mainWindow // Establece el propietario
            };

            nuevosEquipos.ShowDialog(); // Abre la ventana


            // Quita el desenfoque al cerrar el modal
            if (mainWindow != null)
            {
                mainWindow.Principal.Effect = null;
            }
        }

        private void btnBorrador_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNumeroActa.Text) && !string.IsNullOrEmpty(txtResponsable.Text))
                {
                    Acta acta = new Acta
                    {
                        NumeroActa = txtNumeroActa.Text,
                        Estado = "Borrador",
                        FechaEntrega = dpFechaEntrega.SelectedDate.HasValue ? dpFechaEntrega.SelectedDate.Value : DateTime.Now,
                        Responsable = txtResponsable.Text,
                        IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue),
                        IdInstitucion = Convert.ToInt32(cmbInstitucion.SelectedValue),
                        Foto = FotoActa != null ? FotoActa : null
                    };

                    try
                    {
                        if (idActaSeleccionada == 0)
                        {
                            actaController.agregarActaConEquipos(acta, ListaEquipoTemp);
                            //actaController.AgregarActa(acta);
                            MessageBox.Show("Acta agregada con éxito", "Agregar");
                        }
                        else
                        {
                            equipoController.agregarListaEquipo(ListaEquipoTemp, idActaSeleccionada);
                            //actaController.AgregarActa(acta);
                            MessageBox.Show("Equipo agregado con éxito", "Agregar");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error al guardar el acta", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Complete los campos obligatorios", "Advertencia");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            ListarActas();
            LimpiarCampos();
        }

        private void btnEliminarFila_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que disparó el evento
            Button boton = sender as Button;

            // Obtener la fila (elemento de la lista) asociada al botón
            Acta acta = boton.DataContext as Acta;

            if (acta != null)
            {

                // Llamar al controlador para eliminar el acta de la base de datos
                try
                {
                    actaController.EliminarActa(acta.IdActa);
                    MessageBox.Show("Acta eliminada con éxito", "Eliminar");


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el acta: " + ex.Message, "Error");
                }

                // Eliminar el acta de la lista local y actualizar el DataGrid
                var listaActas = DGActas.ItemsSource as List<Acta>;
                listaActas.Remove(acta);

                DGActas.ItemsSource = null; // Resetear ItemsSource para actualizar
                DGActas.ItemsSource = listaActas;


            }

        }

        private void btnEditarFila_Click(object sender, RoutedEventArgs e)
        {

            // Obtener el botón que se clickeó
            if (sender is Button button && button.DataContext is Entities.Acta actaSeleccionada)
            {
                // Cargar los datos del acta seleccionada en los controles
                txtNumeroActa.Text = actaSeleccionada.NumeroActa;
                dpFechaEntrega.SelectedDate = actaSeleccionada.FechaEntrega;
                txtResponsable.Text = actaSeleccionada.Responsable;
                cmbProveedor.SelectedValue = actaSeleccionada.IdProveedor;
                cmbInstitucion.SelectedValue = actaSeleccionada.IdInstitucion;

                // Cargar la imagen asociada
                CargarImagen(actaSeleccionada.Foto, eFoto);

                idActaSeleccionada = actaSeleccionada.IdActa;
                DataTable dtEquipos = equipoController.listarEquiposPorActa(idActaSeleccionada);
                ListaEquipoGuardado = ConvertirDataTableEnListaDeEquipos(dtEquipos);
                ListarEquipos(ListaEquipoGuardado);
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
                        string mensajes = equipoController.eliminarEquipo(equipo.IdEquipo);
                        MessageBox.Show(mensajes, "Eliminar");

                    }
                    else
                    {
                        //ListaEquipoTemp.Remove();
                        ListarEquipos(ListaEquipoTemp);
                    }

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el equipo: " + ex.Message, "Error");
                }

                // Eliminar el acta de la lista local y actualizar el DataGrid
                var listaEquipos = DGEquipos.ItemsSource as List<Equipo>;
                listaEquipos.Remove(equipo);

                ListarEquipos(listaEquipos);



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

                mostrarVentana();

            }

        }

        private void DGActas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
