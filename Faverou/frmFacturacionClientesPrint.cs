using CrystalDecisions.CrystalReports.Engine;
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
    public partial class frmFacturacionClientesPrint : Form
    {

        public frmFacturacionClientesPrint()
        {
            InitializeComponent();
        }

        private void frmFacturacionClientesPrint_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtFacturaFind;
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            fillForm();
        }


        private void fillForm()
        {

            if (string.IsNullOrEmpty(txtFacturaFind.Text.Trim()) || txtFacturaFind.Text.Trim() == "0")
                return;

            String connectionString = ConfigurationManager.ConnectionStrings["FaverauConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string Query = "EXEC dbo.FacturacionClientesPrint " + txtFacturaFind.Text.Trim();

                connection.Open();
                DataTable dt = new DataTable();
                dt.Clear();

                dt.Columns.Add("id");
                dt.Columns.Add("Identificación Crédito");
                dt.Columns.Add("Nombre Deudor");
                dt.Columns.Add("Tipo Tasación");
                dt.Columns.Add("Numero Tasación");
                dt.Columns.Add("Estado");
                dt.Columns.Add("Importe Tasador");
                dt.Columns.Add("Importe Facturado");
                dt.Columns.Add("Domicilio");
                dt.Columns.Add("Localidad");
                dt.Columns.Add("Provincia");
                dt.Columns.Add("Fecha Borrador");
                dt.Columns.Add("Fecha Pedida");
                dt.Columns.Add("Fecha Asignada");
                dt.Columns.Add("Fecha Realizada");
                dt.Columns.Add("Fecha a Supervisar");
                dt.Columns.Add("Fecha Supervisada");
                dt.Columns.Add("Fecha Entregada");
                dt.Columns.Add("Fecha Procesada");
                dt.Columns.Add("WorkAreaFPT");

                try
                {
                    SqlCommand cmd = new SqlCommand(Query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dtTasadores = new DataTable();
                    dtTasadores.Clear();
                    dtTasadores.Load(reader);

                    DataRow dr;

                    if (dtTasadores.Rows.Count > 0)
                    {

                        foreach (DataRow row in dtTasadores.Rows)
                        {
                            dr = dt.NewRow();
                            dr[0] = row[0].ToString();
                            dr[1] = row[1].ToString();
                            dr[2] = row[2].ToString();
                            dr[3] = row[3].ToString();
                            dr[4] = row[4].ToString();
                            dr[5] = row[5].ToString();
                            dr[6] = row[12].ToString();
                            dr[7] = row[13].ToString();
                            dr[8] = row[6].ToString();
                            dr[9] = row[7].ToString();
                            dr[10] = row[8].ToString();
                            dr[11] = row[15].ToString();
                            dr[12] = row[19].ToString();
                            dr[13] = row[23].ToString();
                            dr[14] = row[27].ToString();
                            dr[15] = row[31].ToString();
                            dr[16] = row[35].ToString();
                            dr[17] = row[39].ToString();
                            dr[18] = row[43].ToString();
                            dr[19] = row[48].ToString();

                            dt.Rows.Add(dr);
                        }

                        dataGridView1.DataSource = null;
                        dataGridView1.Columns.Clear();

                        dataGridView1.DataSource = dt;

                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.Columns[3].ReadOnly = true;
                        dataGridView1.Columns[4].ReadOnly = true;
                        dataGridView1.Columns[5].ReadOnly = true;
                        dataGridView1.Columns[6].ReadOnly = true;
                        dataGridView1.Columns[7].ReadOnly = true;
                        dataGridView1.Columns[8].ReadOnly = true;
                        dataGridView1.Columns[9].ReadOnly = true;
                        dataGridView1.Columns[10].ReadOnly = true;
                        dataGridView1.Columns[11].ReadOnly = true;
                        dataGridView1.Columns[12].ReadOnly = true;
                        dataGridView1.Columns[13].ReadOnly = true;
                        dataGridView1.Columns[14].ReadOnly = true;
                        dataGridView1.Columns[15].ReadOnly = true;
                        dataGridView1.Columns[16].ReadOnly = true;
                        dataGridView1.Columns[17].ReadOnly = true;
                        dataGridView1.Columns[18].ReadOnly = true;
                        dataGridView1.Columns[19].ReadOnly = true;

                        fillHeader(dataGridView1.Rows[0].Cells[0].Value.ToString());
                        txtFechaPago.Text = dtTasadores.Rows[0].ItemArray[11].ToString();
                        txtNumeroRecibo.Text = txtFacturaFind.Text;

                        fillControls(dtTasadores);

                    }
                    else
                    {
                        clearForm();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Se produjo un error al cargar los datos.", "Error");
                }

            }

        }


        private void fillControls(DataTable dt)
        {

            float totalTasador = 0;
            float totalFacturado = 0;
            float flot = 0;

            foreach (DataRow row in dt.Rows)
            {

                flot = 0;
                if (float.TryParse(row[12].ToString(), out flot))
                    totalTasador += flot;

                flot = 0;
                if (float.TryParse(row[13].ToString(), out flot))
                    totalFacturado += flot;

            }

            txtImporteTasador.Text = totalTasador.ToString();
            txtTotalFacturado.Text = totalFacturado.ToString();

        }

        private void clearForm()
        {

            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

            this.txtImporteTasador.Text = "0";
            this.txtTotalFacturado.Text = "0";
            this.txtNumeroRecibo.Text = "0";
            this.txtFechaPago.Text = "";
            lblNombreTasador.Text = "";

        }

        private void fillHeader(string idForm)
        {

            String connectionString = ConfigurationManager.ConnectionStrings["FaverauConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string Query = "select distinct(rf_tas.view_value) ";
                Query += "from sdmform fo ";
                Query += "inner join SDMWORKAREACONCRETE wa on fo.id = wa.id_form ";
                Query += "inner join SDMREFERENCEFORM rf_tas on wa.id = rf_tas.id_workarea and rf_tas.id_refformtype = 222 ";
                Query += "where fo.id = " + idForm ;

                connection.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand(Query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dtClientes = new DataTable();
                    dtClientes.Clear();
                    dtClientes.Load(reader);

                    if (dtClientes.Rows.Count > 0) 
                        lblNombreTasador.Text = dtClientes.Rows[0][0].ToString();

                }
                catch (Exception e)
                {
                    MessageBox.Show("Se produjo un error al cargar los datos.", "Error");
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("identificacionCredito"));
            dt.Columns.Add(new DataColumn("nombreDeudor"));
            dt.Columns.Add(new DataColumn("tipoTasacion"));
            dt.Columns.Add(new DataColumn("numeroTasacion"));
            dt.Columns.Add(new DataColumn("estado"));
            dt.Columns.Add(new DataColumn("importeTasador"));
            dt.Columns.Add(new DataColumn("importeFacturado"));
            dt.Columns.Add(new DataColumn("domicilio"));
            dt.Columns.Add(new DataColumn("localidad"));
            dt.Columns.Add(new DataColumn("provincia"));

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["identificacionCredito"] = row.Cells[1].Value.ToString();
                dr["nombreDeudor"] = row.Cells[2].Value.ToString();
                dr["tipoTasacion"] = row.Cells[3].Value.ToString();
                dr["numeroTasacion"] = row.Cells[4].Value.ToString();
                dr["estado"] = row.Cells[5].Value.ToString();
                dr["importeTasador"] = row.Cells[6].Value.ToString();
                dr["importeFacturado"] = row.Cells[7].Value.ToString();
                dr["domicilio"] = row.Cells[8].Value.ToString();
                dr["localidad"] = row.Cells[9].Value.ToString();
                dr["provincia"] = row.Cells[10].Value.ToString();

                dt.Rows.Add(dr);
            }


            ReportDocument report = new ReportDocument();

            string fullPath = AppDomain.CurrentDomain.BaseDirectory + "Reports\\rptFacturacionClientes.rpt";

            report.Load(fullPath);

            //       report.Load(@"C:\Users\marce\Documents\Visual Studio 2015\Projects\Faverou\Faverou\Reports\rptFacturacionClientes.rpt");

            report.SetDataSource(dt);

            report.SetParameterValue("tasador", lblNombreTasador.Text);
            report.SetParameterValue("registros", dt.Rows.Count.ToString());
            report.SetParameterValue("fecha", DateTime.Now.ToShortDateString());
            report.SetParameterValue("totalFacturado", txtTotalFacturado.Text);
            report.SetParameterValue("numeroRecibo", txtNumeroRecibo.Text);
            report.SetParameterValue("fechaPago", txtFechaPago.Text);

            string filename = AppDomain.CurrentDomain.BaseDirectory + "Reports\\ListadoFacturacionClientes.pdf";

            report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);

            System.Diagnostics.Process.Start(filename);

              /*string rutaURL = @"C:\Users\marce\Documents\Visual Studio 2015\Projects\Faverou\Faverou\Reports\";
              string filename = "ListadoFacturacionClientes.pdf";

              report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaURL + filename);

              System.Diagnostics.Process.Start(rutaURL + filename);*/

        }

        private void txtFacturaFind_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fillForm();
            }
        }
    }
}
