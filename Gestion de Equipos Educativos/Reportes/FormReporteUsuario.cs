using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_de_Equipos_Educativos.Reportes
{
    public partial class FormReporteUsuario : Form
    {
        public int IdEquipo {  get; set; }
        public FormReporteUsuario()
        {
            InitializeComponent();
        }

        private void FormReporteUsuario_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dsReporteUsuario.sp_ListarEquipos' Puede moverla o quitarla según sea necesario.
            this.sp_ListarEquiposTableAdapter.Fill(this.dsReporteUsuario.sp_ListarEquipos,IdEquipo);
            this.reportViewer1.RefreshReport();
        }
    }
}
