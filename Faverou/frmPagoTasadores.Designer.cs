namespace Faverou
{
    partial class frmPagoTasadores
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPagoTasadores));
            this.btnBuscar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblNombreTasador = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtViaticosFacturados = new System.Windows.Forms.TextBox();
            this.txtTotalFacturado = new System.Windows.Forms.TextBox();
            this.txtImporteTasador = new System.Windows.Forms.TextBox();
            this.txtImporteFacturado = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnReimprimir = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPago = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtNumeroRecibo = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblTasaciones = new System.Windows.Forms.Label();
            this.lblSeleccionadas = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(15, 9);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(127, 23);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar Tasador";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.lblNombreTasador);
            this.panel2.Location = new System.Drawing.Point(1, -2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1056, 40);
            this.panel2.TabIndex = 17;
            // 
            // lblNombreTasador
            // 
            this.lblNombreTasador.BackColor = System.Drawing.SystemColors.Window;
            this.lblNombreTasador.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreTasador.Location = new System.Drawing.Point(158, 11);
            this.lblNombreTasador.Name = "lblNombreTasador";
            this.lblNombreTasador.Size = new System.Drawing.Size(809, 23);
            this.lblNombreTasador.TabIndex = 20;
            this.lblNombreTasador.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 39);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(989, 358);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.lblSeleccionadas);
            this.panel1.Controls.Add(this.lblTasaciones);
            this.panel1.Controls.Add(this.txtViaticosFacturados);
            this.panel1.Controls.Add(this.txtTotalFacturado);
            this.panel1.Controls.Add(this.txtImporteTasador);
            this.panel1.Controls.Add(this.txtImporteFacturado);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.txtNumeroRecibo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Location = new System.Drawing.Point(-36, 398);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1162, 300);
            this.panel1.TabIndex = 18;
            // 
            // txtViaticosFacturados
            // 
            this.txtViaticosFacturados.Location = new System.Drawing.Point(869, 32);
            this.txtViaticosFacturados.Name = "txtViaticosFacturados";
            this.txtViaticosFacturados.ReadOnly = true;
            this.txtViaticosFacturados.Size = new System.Drawing.Size(150, 20);
            this.txtViaticosFacturados.TabIndex = 29;
            this.txtViaticosFacturados.Text = "0";
            this.txtViaticosFacturados.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalFacturado
            // 
            this.txtTotalFacturado.Location = new System.Drawing.Point(869, 125);
            this.txtTotalFacturado.Name = "txtTotalFacturado";
            this.txtTotalFacturado.ReadOnly = true;
            this.txtTotalFacturado.Size = new System.Drawing.Size(150, 20);
            this.txtTotalFacturado.TabIndex = 28;
            this.txtTotalFacturado.Text = "0";
            this.txtTotalFacturado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtImporteTasador
            // 
            this.txtImporteTasador.Location = new System.Drawing.Point(692, 125);
            this.txtImporteTasador.Name = "txtImporteTasador";
            this.txtImporteTasador.ReadOnly = true;
            this.txtImporteTasador.Size = new System.Drawing.Size(150, 20);
            this.txtImporteTasador.TabIndex = 27;
            this.txtImporteTasador.Text = "0";
            this.txtImporteTasador.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtImporteFacturado
            // 
            this.txtImporteFacturado.Location = new System.Drawing.Point(869, 78);
            this.txtImporteFacturado.Name = "txtImporteFacturado";
            this.txtImporteFacturado.ReadOnly = true;
            this.txtImporteFacturado.Size = new System.Drawing.Size(150, 20);
            this.txtImporteFacturado.TabIndex = 26;
            this.txtImporteFacturado.Text = "0";
            this.txtImporteFacturado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(689, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Total Importe Tasador";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(866, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Total Facturado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(866, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Subtotal Viáticos Facturados";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(866, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Subtotal Importe Facturado";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel3.Controls.Add(this.btnReimprimir);
            this.panel3.Controls.Add(this.btnImprimir);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnPago);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Location = new System.Drawing.Point(-7, 157);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1206, 55);
            this.panel3.TabIndex = 21;
            // 
            // btnReimprimir
            // 
            this.btnReimprimir.BackColor = System.Drawing.Color.SteelBlue;
            this.btnReimprimir.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReimprimir.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnReimprimir.Location = new System.Drawing.Point(87, 5);
            this.btnReimprimir.Name = "btnReimprimir";
            this.btnReimprimir.Size = new System.Drawing.Size(137, 29);
            this.btnReimprimir.TabIndex = 33;
            this.btnReimprimir.Text = "Reimprimir";
            this.btnReimprimir.UseVisualStyleBackColor = false;
            this.btnReimprimir.Click += new System.EventHandler(this.btnReimprimir_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(380, 8);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(150, 23);
            this.btnImprimir.TabIndex = 32;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(546, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 23);
            this.btnCancel.TabIndex = 31;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPago
            // 
            this.btnPago.Location = new System.Drawing.Point(711, 8);
            this.btnPago.Name = "btnPago";
            this.btnPago.Size = new System.Drawing.Size(150, 23);
            this.btnPago.TabIndex = 30;
            this.btnPago.Text = "Realizar Pago";
            this.btnPago.UseVisualStyleBackColor = true;
            this.btnPago.Click += new System.EventHandler(this.btnPago_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(876, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Salir";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtNumeroRecibo
            // 
            this.txtNumeroRecibo.Location = new System.Drawing.Point(45, 93);
            this.txtNumeroRecibo.Mask = "99999999";
            this.txtNumeroRecibo.Name = "txtNumeroRecibo";
            this.txtNumeroRecibo.PromptChar = ' ';
            this.txtNumeroRecibo.Size = new System.Drawing.Size(87, 20);
            this.txtNumeroRecibo.TabIndex = 20;
            this.txtNumeroRecibo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNumeroRecibo.Enter += new System.EventHandler(this.txtNumeroRecibo_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "N° de Recibo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Seleccionar Fecha de Pago";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(45, 32);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(87, 20);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // lblTasaciones
            // 
            this.lblTasaciones.BackColor = System.Drawing.Color.LightGray;
            this.lblTasaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTasaciones.Location = new System.Drawing.Point(406, 12);
            this.lblTasaciones.Name = "lblTasaciones";
            this.lblTasaciones.Size = new System.Drawing.Size(206, 25);
            this.lblTasaciones.TabIndex = 30;
            this.lblTasaciones.Text = "Tasaciones: 0";
            this.lblTasaciones.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSeleccionadas
            // 
            this.lblSeleccionadas.BackColor = System.Drawing.Color.LightGray;
            this.lblSeleccionadas.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeleccionadas.Location = new System.Drawing.Point(406, 46);
            this.lblSeleccionadas.Name = "lblSeleccionadas";
            this.lblSeleccionadas.Size = new System.Drawing.Size(206, 25);
            this.lblSeleccionadas.TabIndex = 31;
            this.lblSeleccionadas.Text = "Seleccionadas: 0";
            this.lblSeleccionadas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmPagoTasadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 592);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1006, 631);
            this.MinimumSize = new System.Drawing.Size(1006, 631);
            this.Name = "frmPagoTasadores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pago a Tasadores";
            this.Load += new System.EventHandler(this.frmPagoTasadores_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNombreTasador;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtNumeroRecibo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtViaticosFacturados;
        private System.Windows.Forms.TextBox txtTotalFacturado;
        private System.Windows.Forms.TextBox txtImporteTasador;
        private System.Windows.Forms.TextBox txtImporteFacturado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPago;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnReimprimir;
        private System.Windows.Forms.Label lblTasaciones;
        private System.Windows.Forms.Label lblSeleccionadas;
    }
}