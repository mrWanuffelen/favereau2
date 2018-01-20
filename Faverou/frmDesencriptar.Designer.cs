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
            this.btnSalida = new System.Windows.Forms.Button();
            this.btnInFTP = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDescargarFTP
            // 
            this.btnDescargarFTP.Location = new System.Drawing.Point(12, 24);
            this.btnDescargarFTP.Name = "btnDescargarFTP";
            this.btnDescargarFTP.Size = new System.Drawing.Size(75, 23);
            this.btnDescargarFTP.TabIndex = 0;
            this.btnDescargarFTP.Text = "Descargar";
            this.btnDescargarFTP.UseVisualStyleBackColor = true;
            this.btnDescargarFTP.Click += new System.EventHandler(this.btnDescargarFTP_Click);
            // 
            // btnSalida
            // 
            this.btnSalida.Location = new System.Drawing.Point(13, 92);
            this.btnSalida.Name = "btnSalida";
            this.btnSalida.Size = new System.Drawing.Size(75, 23);
            this.btnSalida.TabIndex = 1;
            this.btnSalida.Text = "Salida";
            this.btnSalida.UseVisualStyleBackColor = true;
            this.btnSalida.Click += new System.EventHandler(this.btnSalida_Click);
            // 
            // btnInFTP
            // 
            this.btnInFTP.Location = new System.Drawing.Point(128, 92);
            this.btnInFTP.Name = "btnInFTP";
            this.btnInFTP.Size = new System.Drawing.Size(75, 23);
            this.btnInFTP.TabIndex = 2;
            this.btnInFTP.Text = "Subir a FTP";
            this.btnInFTP.UseVisualStyleBackColor = true;
            this.btnInFTP.Click += new System.EventHandler(this.btnInFTP_Click);
            // 
            // btnBorrar
            // 
            this.btnBorrar.Location = new System.Drawing.Point(210, 91);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(75, 23);
            this.btnBorrar.TabIndex = 3;
            this.btnBorrar.Text = "Borrar FTP";
            this.btnBorrar.UseVisualStyleBackColor = true;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // frmDesencriptar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 204);
            this.Controls.Add(this.btnBorrar);
            this.Controls.Add(this.btnInFTP);
            this.Controls.Add(this.btnSalida);
            this.Controls.Add(this.btnDescargarFTP);
            this.Name = "frmDesencriptar";
            this.Text = "frmDesencriptar";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDescargarFTP;
        private System.Windows.Forms.Button btnSalida;
        private System.Windows.Forms.Button btnInFTP;
        private System.Windows.Forms.Button btnBorrar;
    }
}