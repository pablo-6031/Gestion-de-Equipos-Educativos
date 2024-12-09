namespace Gestion_de_Equipos_Educativos.Reportes
{
    partial class FormReporteTablaActa
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dsReporteTablaActa = new Gestion_de_Equipos_Educativos.Reportes.dsReporteTablaActa();
            this.dsReporteTablaActaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsReporteTablaInstitucion = new Gestion_de_Equipos_Educativos.Reportes.dsReporteTablaInstitucion();
            this.dsReporteTablaInstitucionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.spReporteTablaActaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_ReporteTablaActaTableAdapter = new Gestion_de_Equipos_Educativos.Reportes.dsReporteTablaActaTableAdapters.sp_ReporteTablaActaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteTablaActa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteTablaActaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteTablaInstitucion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteTablaInstitucionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteTablaActaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "TablaActa";
            reportDataSource1.Value = this.spReporteTablaActaBindingSource;
            reportDataSource2.Name = "Institucion";
            reportDataSource2.Value = this.dsReporteTablaInstitucionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Gestion_de_Equipos_Educativos.Reportes.rptReporteTablaActa.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 525);
            this.reportViewer1.TabIndex = 0;
            // 
            // dsReporteTablaActa
            // 
            this.dsReporteTablaActa.DataSetName = "dsReporteTablaActa";
            this.dsReporteTablaActa.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsReporteTablaActaBindingSource
            // 
            this.dsReporteTablaActaBindingSource.DataSource = this.dsReporteTablaActa;
            this.dsReporteTablaActaBindingSource.Position = 0;
            // 
            // dsReporteTablaInstitucion
            // 
            this.dsReporteTablaInstitucion.DataSetName = "dsReporteTablaInstitucion";
            this.dsReporteTablaInstitucion.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsReporteTablaInstitucionBindingSource
            // 
            this.dsReporteTablaInstitucionBindingSource.DataSource = this.dsReporteTablaInstitucion;
            this.dsReporteTablaInstitucionBindingSource.Position = 0;
            // 
            // spReporteTablaActaBindingSource
            // 
            this.spReporteTablaActaBindingSource.DataMember = "sp_ReporteTablaActa";
            this.spReporteTablaActaBindingSource.DataSource = this.dsReporteTablaActaBindingSource;
            // 
            // sp_ReporteTablaActaTableAdapter
            // 
            this.sp_ReporteTablaActaTableAdapter.ClearBeforeFill = true;
            // 
            // FormReporteTablaActa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 525);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormReporteTablaActa";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormReporteTablaActa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteTablaActa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteTablaActaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteTablaInstitucion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteTablaInstitucionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteTablaActaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource spReporteTablaActaBindingSource;
        private System.Windows.Forms.BindingSource dsReporteTablaActaBindingSource;
        private dsReporteTablaActa dsReporteTablaActa;
        private System.Windows.Forms.BindingSource dsReporteTablaInstitucionBindingSource;
        private dsReporteTablaInstitucion dsReporteTablaInstitucion;
        private dsReporteTablaActaTableAdapters.sp_ReporteTablaActaTableAdapter sp_ReporteTablaActaTableAdapter;
    }
}