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
    public partial class FormReporteTablaActa : Form
    {
        public int id_acta { get; set; } 
        public FormReporteTablaActa()
        {
            InitializeComponent();
        }

        private void FormReporteTablaActa_Load(object sender, EventArgs e)
        {

            // Ajusta la conexión antes de llenar el adaptador
            this.sp_ReporteTablaActaTableAdapter.Connection.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename= C:\\Users\\pablo\\source\\repos\\Gestion de Equipos Educativos\\DataBase\\DB_GestionEquipos.mdf;Integrated Security=True;Connect Timeout=30";

            // Llena el adaptador
            this.sp_ReporteTablaActaTableAdapter.Fill(this.dsReporteTablaActa.sp_ReporteTablaActa, 47);

            this.reportViewer1.RefreshReport();

            /*
            this.sp_ReporteTablaActaTableAdapter.Fill(this.dsReporteTablaActa.sp_ReporteTablaActa,IdActa);
            this.reportViewer1.RefreshReport();
            */
        }
    }
}
