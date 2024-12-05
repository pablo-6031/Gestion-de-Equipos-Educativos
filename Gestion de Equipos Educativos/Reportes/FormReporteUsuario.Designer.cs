namespace Gestion_de_Equipos_Educativos.Reportes
{
    partial class FormReporteUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dsReporteUsuario = new Gestion_de_Equipos_Educativos.Reportes.dsReporteUsuario();
            this.dsReporteUsuarioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_ListarEquiposBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.spListarEquiposBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_ListarEquiposTableAdapter = new Gestion_de_Equipos_Educativos.Reportes.dsReporteUsuarioTableAdapters.sp_ListarEquiposTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteUsuarioBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sp_ListarEquiposBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spListarEquiposBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.spListarEquiposBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Gestion_de_Equipos_Educativos.Reportes.rptReporteUsuario.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(37, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1122, 687);
            this.reportViewer1.TabIndex = 0;
            // 
            // dsReporteUsuario
            // 
            this.dsReporteUsuario.DataSetName = "dsReporteUsuario";
            this.dsReporteUsuario.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsReporteUsuarioBindingSource
            // 
            this.dsReporteUsuarioBindingSource.DataSource = this.dsReporteUsuario;
            this.dsReporteUsuarioBindingSource.Position = 0;
            // 
            // sp_ListarEquiposBindingSource
            // 
            this.sp_ListarEquiposBindingSource.DataMember = "sp_ListarEquipos";
            this.sp_ListarEquiposBindingSource.DataSource = this.dsReporteUsuario;
            // 
            // spListarEquiposBindingSource
            // 
            this.spListarEquiposBindingSource.DataMember = "sp_ListarEquipos";
            this.spListarEquiposBindingSource.DataSource = this.dsReporteUsuarioBindingSource;
            // 
            // sp_ListarEquiposTableAdapter
            // 
            this.sp_ListarEquiposTableAdapter.ClearBeforeFill = true;
            // 
            // FormReporteUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 787);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormReporteUsuario";
            this.Text = "FormReporteUsuario";
            this.Load += new System.EventHandler(this.FormReporteUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteUsuarioBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sp_ListarEquiposBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spListarEquiposBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dsReporteUsuarioBindingSource;
        private dsReporteUsuario dsReporteUsuario;
        private System.Windows.Forms.BindingSource sp_ListarEquiposBindingSource;
        private System.Windows.Forms.BindingSource spListarEquiposBindingSource;
        private dsReporteUsuarioTableAdapters.sp_ListarEquiposTableAdapter sp_ListarEquiposTableAdapter;
    }
}