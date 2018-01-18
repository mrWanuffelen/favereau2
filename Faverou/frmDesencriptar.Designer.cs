namespace Faverou
{
    partial class frmDesencriptar
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
            this.btnDescargarFTP = new System.Windows.Forms.Button();
            this.btnDecodificar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDescargarFTP
            // 
            this.btnDescargarFTP.Location = new System.Drawing.Point(200, 25);
            this.btnDescargarFTP.Name = "btnDescargarFTP";
            this.btnDescargarFTP.Size = new System.Drawing.Size(75, 23);
            this.btnDescargarFTP.TabIndex = 0;
            this.btnDescargarFTP.Text = "Descargar";
            this.btnDescargarFTP.UseVisualStyleBackColor = true;
            this.btnDescargarFTP.Click += new System.EventHandler(this.btnDescargarFTP_Click);
            // 
            // btnDecodificar
            // 
            this.btnDecodificar.Location = new System.Drawing.Point(225, 122);
            this.btnDecodificar.Name = "btnDecodificar";
            this.btnDecodificar.Size = new System.Drawing.Size(75, 23);
            this.btnDecodificar.TabIndex = 1;
            this.btnDecodificar.Text = "Decodificar";
            this.btnDecodificar.UseVisualStyleBackColor = true;
            this.btnDecodificar.Click += new System.EventHandler(this.btnDecodificar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "1- Descargue los archivos del FTP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "2- Decodifque los archivos descargados";
            // 
            // frmDesencriptar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 505);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDecodificar);
            this.Controls.Add(this.btnDescargarFTP);
            this.Name = "frmDesencriptar";
            this.Text = "frmDesencriptar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDescargarFTP;
        private System.Windows.Forms.Button btnDecodificar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}