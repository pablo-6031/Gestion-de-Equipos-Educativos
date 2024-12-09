namespace Gestion_de_Equipos_Educativos.Reportes
{
    partial class FormReporteSinide
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
            this.dsReporteSinide = new Gestion_de_Equipos_Educativos.Reportes.dsReporteSinide();
            this.dsReporteSinideBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.spReporteSinideBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_ReporteSinideTableAdapter = new Gestion_de_Equipos_Educativos.Reportes.dsReporteSinideTableAdapters.sp_ReporteSinideTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteSinide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteSinideBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteSinideBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.spReporteSinideBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Gestion_de_Equipos_Educativos.Reportes.rptReporteSinide.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(796, 449);
            this.reportViewer1.TabIndex = 0;
            // 
            // dsReporteSinide
            // 
            this.dsReporteSinide.DataSetName = "dsReporteSinide";
            this.dsReporteSinide.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsReporteSinideBindingSource
            // 
            this.dsReporteSinideBindingSource.DataSource = this.dsReporteSinide;
            this.dsReporteSinideBindingSource.Position = 0;
            // 
            // spReporteSinideBindingSource
            // 
            this.spReporteSinideBindingSource.DataMember = "sp_ReporteSinide";
            this.spReporteSinideBindingSource.DataSource = this.dsReporteSinideBindingSource;
            // 
            // sp_ReporteSinideTableAdapter
            // 
            this.sp_ReporteSinideTableAdapter.ClearBeforeFill = true;
            // 
            // FormReporteSinide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormReporteSinide";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormReporteSinide_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteSinide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsReporteSinideBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteSinideBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dsReporteSinideBindingSource;
        private dsReporteSinide dsReporteSinide;
        private System.Windows.Forms.BindingSource spReporteSinideBindingSource;
        private dsReporteSinideTableAdapters.sp_ReporteSinideTableAdapter sp_ReporteSinideTableAdapter;
    }
}