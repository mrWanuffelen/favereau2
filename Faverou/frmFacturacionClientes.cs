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
    public partial class frmFacturacionClientes : Form
    {
        private enum formState { clear, editing, final };
        private double importeFacturado = 0;
        private double importeTasador = 0;
        private double totalFacturado = 0;
        private bool gridBinded = false;
        private formState estado = formState.clear;
        private string clienteSelected = string.Empty;

        public frmFacturacionClientes()
        {
            InitializeComponent();
        }


        private void frmFacturacionClientes_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            syncButtons();
        }


        private void syncButtons()
        {
            if (estado == formState.clear)
            {
                btnCancel.Enabled = false;
                btnPago.Enabled = false;
                btnImprimir.Enabled = false;
            }
            else if (estado == formState.editing)
            {
                btnCancel.Enabled = true;
                btnPago.Enabled = true;
                btnImprimir.Enabled = false;
            }
            else if (estado == formState.final)
            {
                btnCancel.Enabled = true;
                btnPago.Enabled = false;
                btnImprimir.Enabled = true;
            }

        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var form = new frmFacturacionClientesFinder())
            {
                form.ShowDialog();
                string cliente = form.cliente.Trim();

                if (!string.IsNullOrEmpty(cliente))
                {
                    clearForm();

                    if (cliente != this.clienteSelected)
                    {
                        this.clienteSelected = cliente;

                        gridBinded = false;

                        fillHeader(cliente);
                        fillGrid(cliente);

                        this.txtImporteFacturado.Text = "0";
                        this.txtImporteTasador.Text = "0";
                        this.txtTotalFacturado.Text = "0";
                        this.txtViaticosFacturados.Text = "0";
                        this.txtNumeroFactura.Text = "0";
                        importeFacturado = 0;
                        importeTasador = 0;
                        totalFacturado = 0; 
                    }

                }

            }
        }


        private void fillHeader(string cliente)
        {
            lblNombreTasador.Text = cliente;
        }


        private void fillGrid(string cliente)
        {

            String connectionString = ConfigurationManager.ConnectionStrings["FaverauConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string Query = "EXEC dbo.FacturacionClientes '" + cliente + "'";

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

                        DataGridViewCheckBoxColumn checkBox = new DataGridViewCheckBoxColumn(false);
                        checkBox.HeaderText = "Selec";
                        dataGridView1.Columns.Add(checkBox);

                        dataGridView1.DataSource = dt;

                        dataGridView1.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dataGridView1.Columns[2].HeaderCell.Style.BackColor = Color.Aquamarine;
                        dataGridView1.Columns[3].HeaderCell.Style.BackColor = Color.Aquamarine;
                        dataGridView1.Columns[7].HeaderCell.Style.BackColor = Color.Aquamarine;
                        dataGridView1.Columns[8].HeaderCell.Style.BackColor = Color.Aquamarine;
                        dataGridView1.Columns[10].HeaderCell.Style.BackColor = Color.Aquamarine;

                        dataGridView1.EnableHeadersVisualStyles = false;
                        dataGridView1.Columns[0].Width = 43;
                        dataGridView1.Columns[1].Visible = false;

                        dataGridView1.Columns[2].DefaultCellStyle.BackColor = Color.Aquamarine;
                        dataGridView1.Columns[3].DefaultCellStyle.BackColor = Color.Aquamarine;
                        dataGridView1.Columns[7].DefaultCellStyle.BackColor = Color.Aquamarine;
                        dataGridView1.Columns[8].DefaultCellStyle.BackColor = Color.Aquamarine;
                        dataGridView1.Columns[10].DefaultCellStyle.BackColor = Color.Aquamarine;

                        dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dataGridView1.Columns[4].ReadOnly = true;
                        dataGridView1.Columns[5].ReadOnly = true;
                        dataGridView1.Columns[6].ReadOnly = true;
                        dataGridView1.Columns[9].ReadOnly = true;
                        dataGridView1.Columns[11].ReadOnly = true;
                        dataGridView1.Columns[12].ReadOnly = true;
                        dataGridView1.Columns[13].ReadOnly = true;
                        dataGridView1.Columns[14].ReadOnly = true;
                        dataGridView1.Columns[15].ReadOnly = true;
                        dataGridView1.Columns[16].ReadOnly = true;
                        dataGridView1.Columns[17].ReadOnly = true;
                        dataGridView1.Columns[18].ReadOnly = true;
                        dataGridView1.Columns[19].ReadOnly = true;

                        gridBinded = true;

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                            chk.Value = false;
                        }

                        estado = formState.editing;
                        syncButtons();
                    }
                    else
                    {
                     //   clearForm();
                    }

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                DataGridViewCheckBoxCell chk = dataGridView1.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell;

                changeValue(Convert.ToBoolean(chk.Value), e.RowIndex);

                if (Convert.ToBoolean(chk.Value))
                    chk.Value = CheckState.Unchecked;// false;
                else
                    chk.Value = CheckState.Checked;

                dataGridView1.EndEdit();
            }
        }


        private void changeValue(bool valor, int pos)
        {

            if (gridBinded)
            {

                if (!valor)
                {
                    importeFacturado += Convert.ToDouble(dataGridView1.Rows[pos].Cells[8].Value);
                    txtImporteFacturado.Text = Convert.ToString(importeFacturado);

                    importeTasador += Convert.ToDouble(dataGridView1.Rows[pos].Cells[7].Value);
                    txtImporteTasador.Text = Convert.ToString(importeTasador);

                    totalFacturado = importeFacturado;
                    txtTotalFacturado.Text = Convert.ToString(totalFacturado);
                }
                else
                {
                    importeFacturado -= Convert.ToDouble(dataGridView1.Rows[pos].Cells[8].Value);
                    txtImporteFacturado.Text = Convert.ToString(importeFacturado);

                    importeTasador -= Convert.ToDouble(dataGridView1.Rows[pos].Cells[7].Value);
                    txtImporteTasador.Text = Convert.ToString(importeTasador);

                    totalFacturado = importeFacturado;
                    txtTotalFacturado.Text = Convert.ToString(totalFacturado);
                }

            }

        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (gridBinded)
            {

                // Importe Tasador
                if (e.ColumnIndex == 7)
                {
                    DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[7] as DataGridViewCell;

                    float newFloat;

                    if (!float.TryParse(e.FormattedValue.ToString(),
                                        out newFloat) || newFloat < 0)
                    {
                        e.Cancel = true;
                    }
                    else
                    {

                        DataGridViewCheckBoxCell chk = dataGridView1.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell;

                        if (Convert.ToBoolean(chk.Value))
                        {

                            // valor anterior
                            importeTasador -= Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                            // valor nuevo
                            importeTasador += Convert.ToDouble(e.FormattedValue);

                            txtImporteTasador.Text = Convert.ToString(importeTasador);

                        }

                    }

                }


                // Importe Facturado
                if (e.ColumnIndex == 8)
                {
                    DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[8] as DataGridViewCell;

                    float newFloat;

                    if (!float.TryParse(e.FormattedValue.ToString(),
                                        out newFloat) || newFloat < 0)
                    {
                        e.Cancel = true;
                    }
                    else
                    {

                        DataGridViewCheckBoxCell chk = dataGridView1.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell;

                        if (Convert.ToBoolean(chk.Value))
                        {

                            // valor anterior
                            importeFacturado -= Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                            // valor nuevo
                            importeFacturado += Convert.ToDouble(e.FormattedValue);
                            txtImporteFacturado.Text = Convert.ToString(importeFacturado);

                            // total facturado
                            totalFacturado -= Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                            totalFacturado += Convert.ToDouble(e.FormattedValue);
                            txtTotalFacturado.Text = Convert.ToString(totalFacturado);

                        }

                    }

                }

            }
        }


        private void btnFacturar_Click(object sender, EventArgs e)
        {
            string errMsg = validateFactura();

            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg, "Atención");
            }
            else
            {

                String connectionString = ConfigurationManager.ConnectionStrings["FaverauConnectionString"].ConnectionString;
                bool isOk = true;

                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    List<DataGridViewRow> lst = dataGridView1.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => (r.Cells[0] as DataGridViewCheckBoxCell).Value.Equals(CheckState.Checked))
                    .ToList();

                    con.Open();

                    // Start a local transaction.
                    SqlTransaction sqlTran = con.BeginTransaction();

                    // Enlist a command in the current transaction.
                    SqlCommand command = con.CreateCommand();
                    command.Transaction = sqlTran;

                    try
                    {

                        foreach (DataGridViewRow row in lst)
                        {

                            // Inserto Nro. de Factura
                            string cmd = "update SDMREFERENCEFORM ";
                            cmd += "set view_value = '" + txtNumeroFactura.Text + "' ";
                            cmd += "where id_workarea = " + row.Cells[20].Value.ToString() + " and id_refformtype = 10137 ";

                            command.CommandText = cmd;
                            command.ExecuteNonQuery();

                            // Inserto Fecha de Factura
                            cmd = "update SDMREFERENCEFORM ";
                            cmd += "set view_value = '" + dateTimePicker1.Text + "' ";
                            cmd += "where id_workarea = " + row.Cells[20].Value.ToString() + " and id_refformtype = 10136 ";

                            command.CommandText = cmd;
                            command.ExecuteNonQuery();

                            // Inserto Nro. de Factura en tabla StringValues
                            cmd = "insert into SDMEDITREFSTRINGVALUES ";
                            cmd += "(value, id_reference) ";
                            cmd += "values(" + txtNumeroFactura.Text + ",(select id from SDMREFERENCEFORM where id_workarea = " + row.Cells[20].Value.ToString() + " and id_refformtype = 10137))";

                            command.CommandText = cmd;
                            command.ExecuteNonQuery();

                            estado = formState.final;
                            syncButtons();

                        }

                        sqlTran.Commit();

                    }
                    catch (Exception ex)
                    {
                        isOk = false;

                        try
                        {
                            // Attempt to roll back the transaction.
                            sqlTran.Rollback();
                        }
                        catch (Exception exRollback)
                        {

                        }

                    }
                    finally
                    {
                        con.Close();
                    }

                    if (isOk)
                        MessageBox.Show("El pago se registro exitosamente.", "Mensaje del Sistema");
                    else
                        MessageBox.Show("No es posible registrar el pago.", "Se produjo un error");

                }

            }

        }

        private string validateFactura()
        {

            string mensaje = string.Empty;

            // Valido nro de factura
            int nroRecibo;
            if (!int.TryParse(txtNumeroFactura.Text, out nroRecibo) || nroRecibo < 1)
                mensaje += "Debe ingresar un Nro. de Factura válido.\n";

            // Vaido que se seleccione al menos una tasacion
            int cant = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(r => (r.Cells[0] as DataGridViewCheckBoxCell).Value.Equals(CheckState.Checked))
                .ToList().Count;

            if (cant < 1)
                mensaje += "Debe seleccionar al menos una tasación.\n";

            // valido que exista un importe facturado
            if (totalFacturado <= 0)
                mensaje += "El importe de la factura no puedo ser cero.\n";

            return mensaje;
        }
        

        private void clearForm()
        {

            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

            this.txtImporteFacturado.Text = "0";
            this.txtImporteTasador.Text = "0";
            this.txtTotalFacturado.Text = "0";
            this.txtViaticosFacturados.Text = "0";
            this.txtNumeroFactura.Text = "0";
            importeFacturado = 0;
            importeTasador = 0;
            totalFacturado = 0;

            gridBinded = false;
            estado = formState.clear;
            clienteSelected = string.Empty;

            syncButtons();
            lblNombreTasador.Text = "";

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void txtNumeroFactura_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                txtNumeroFactura.SelectAll();
            });
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

            List<DataGridViewRow> lst = dataGridView1.Rows
            .Cast<DataGridViewRow>()
            .Where(r => (r.Cells[0] as DataGridViewCheckBoxCell).Value.Equals(CheckState.Checked))
            .ToList();

            foreach (DataGridViewRow row in lst)
            {
                DataRow dr = dt.NewRow();
                dr["identificacionCredito"] = row.Cells[2].Value.ToString();
                dr["nombreDeudor"] = row.Cells[3].Value.ToString();
                dr["tipoTasacion"] = row.Cells[4].Value.ToString();
                dr["numeroTasacion"] = row.Cells[5].Value.ToString();
                dr["estado"] = row.Cells[6].Value.ToString();
                dr["importeTasador"] = row.Cells[7].Value.ToString();
                dr["importeFacturado"] = row.Cells[8].Value.ToString();
                dr["domicilio"] = row.Cells[9].Value.ToString();
                dr["localidad"] = row.Cells[10].Value.ToString();
                dr["provincia"] = row.Cells[11].Value.ToString();

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
            report.SetParameterValue("numeroRecibo", txtNumeroFactura.Text);
            report.SetParameterValue("fechaPago", dateTimePicker1.Text);

            string filename = AppDomain.CurrentDomain.BaseDirectory + "Reports\\ListadoFacturacionClientes.pdf";

            report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);

            System.Diagnostics.Process.Start(filename);

          /*  string rutaURL = @"C:\Users\marce\Documents\Visual Studio 2015\Projects\Faverou\Faverou\Reports\";
            string filename = "ListadoFacturacionClientes.pdf";

            report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaURL + filename);

            System.Diagnostics.Process.Start(rutaURL + filename);*/
        }

        private void btnReimprimir_Click(object sender, EventArgs e)
        {
            using (var form = new frmFacturacionClientesPrint())
            {
                form.ShowDialog();
            }
        }
    }
}
