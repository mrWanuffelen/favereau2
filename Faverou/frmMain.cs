using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Faverou
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnPagoTasadores_Click(object sender, EventArgs e)
        {
            frmPagoTasadores fm = new frmPagoTasadores();
            fm.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmFacturacionClientes fm = new frmFacturacionClientes();
            fm.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no implementada","Aviso del Sistema");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDesencriptar fm = new frmDesencriptar();
            fm.ShowDialog(this);
        }
    }
}
