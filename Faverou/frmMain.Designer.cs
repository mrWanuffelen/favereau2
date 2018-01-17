namespace Faverou
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnPagoTasadores = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Highlight;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(674, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnPagoTasadores
            // 
            this.btnPagoTasadores.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnPagoTasadores.FlatAppearance.BorderSize = 2;
            this.btnPagoTasadores.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnPagoTasadores.Location = new System.Drawing.Point(23, 52);
            this.btnPagoTasadores.Name = "btnPagoTasadores";
            this.btnPagoTasadores.Size = new System.Drawing.Size(171, 80);
            this.btnPagoTasadores.TabIndex = 2;
            this.btnPagoTasadores.Text = "Pago a Tasadores";
            this.btnPagoTasadores.UseVisualStyleBackColor = true;
            this.btnPagoTasadores.Click += new System.EventHandler(this.btnPagoTasadores_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(221, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(171, 80);
            this.button2.TabIndex = 3;
            this.button2.Text = "Facturación a Clientes";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(416, 52);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(171, 80);
            this.button3.TabIndex = 4;
            this.button3.Text = "ABM de Incidencias";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 361);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnPagoTasadores);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Loyal - Faverou";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button btnPagoTasadores;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}