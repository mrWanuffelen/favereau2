using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Faverou
{
    public partial class frmFacturacionClientesFinder : Form
    {
        public string cliente = string.Empty;

        public frmFacturacionClientesFinder()
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

                string Query = "select distinct(rf_tas.view_value) ";
                Query += "from sdmform fo ";
                Query += "inner join SDMWORKAREACONCRETE wa on fo.id = wa.id_form ";
                Query += "inner join SDMREFERENCEFORM rf_tas on wa.id = rf_tas.id_workarea and rf_tas.id_refformtype = 222 ";
                Query += "where fo.id_formtype = 21 and len(ltrim(rtrim(rf_tas.view_value))) > 0 and rf_tas.view_value like '%" + txtNombre.Text.Trim() + "%'";

                connection.Open();
                DataTable dt = new DataTable();
                dt.Clear();

                dt.Columns.Add("Nombre");

                try
                {
                    SqlCommand cmd = new SqlCommand(Query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dtClientes = new DataTable();
                    dtClientes.Clear();
                    dtClientes.Load(reader);

                    DataRow dr;

                    foreach (DataRow row in dtClientes.Rows)
                    {
                        dr = dt.NewRow();
                        dr[0] = row[0].ToString();
                        dt.Rows.Add(dr);
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("Se produjo un error al cargar los datos.", "Error");
                }

                dataGridView1.DataSource = dt;

                dataGridView1.Columns[0].Width = 490;

            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.cliente = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                this.cliente = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                this.Close();
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                fillGrid();
            }
        }
    }
}
