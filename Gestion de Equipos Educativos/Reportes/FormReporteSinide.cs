using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Gestion_de_Equipos_Educativos.Reportes
{
    public partial class FormReporteSinide : Form
    {
        public int IdPrestamo {  get; set; } 
        public FormReporteSinide()
        {
            InitializeComponent();
        }

        private void FormReporteSinide_Load(object sender, EventArgs e)
        {


            this.sp_ReporteSinideTableAdapter.Connection.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename= C:\\Users\\pablo\\source\\repos\\Gestion de Equipos Educativos\\DataBase\\DB_GestionEquipos.mdf;Integrated Security=True;Connect Timeout=30";

            // Llena el adaptador
            this.sp_ReporteSinideTableAdapter.Fill(this.dsReporteSinide.sp_ReporteSinide, IdPrestamo);

            
            this.reportViewer1.RefreshReport();
            
        }
    }
}
