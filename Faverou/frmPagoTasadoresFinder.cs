using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Faverou
{
    public partial class frmPagoTasadoresFinder : Form
    {
        private int idTasador = -1;
        private string nombreTasador = "";

        public int id
        {
            get
            {
                return idTasador;
            }
        }

        public string tasador {
            get
            {
                return nombreTasador;
            }
        }

        public frmPagoTasadoresFinder()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            fillGrid();
        }

        private void fillGrid()
        {

            String connectionString = ConfigurationManager.ConnectionStrings["FaverauConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string Query = "Select us.id, us.firstname + ' ' + us.lastname as nombre ";
                Query += "from SDMUSER us ";
                Query += "inner ";
                Query += "join SDMUSERCOMPANYPROFILE up on us.id = up.id_usercompany ";
                Query += "where up.id_profile = 31 and us.firstname like '%" + txtNombre.Text.Trim() + "%' or us.lastname like '%" + txtNombre.Text.Trim() + "%' ";
                Query += "order by us.firstname ";

                connection.Open();
                DataTable dt = new DataTable();
                dt.Clear();

                dt.Columns.Add("id");
                dt.Columns.Add("Nombre");

                try
                {
                    SqlCommand cmd = new SqlCommand(Query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dtTasadores = new DataTable();
                    dtTasadores.Clear();
                    dtTasadores.Load(reader);

                    DataRow dr;

                    foreach (DataRow row in dtTasadores.Rows)
                    {
                        dr = dt.NewRow();
                        dr[0] = row[0].ToString();
                        dr[1] = row[1].ToString();
                        dt.Rows.Add(dr);
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("Se produjo un error al cargar los datos.", "Error");
                }

                dataGridView1.DataSource = dt;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 490;

            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.nombreTasador = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.Close();
            }
            catch (Exception ex) {
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                this.nombreTasador = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                this.idTasador = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                this.Close();
            }
        }

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            btnFind.PerformClick();
        }
    }
}
