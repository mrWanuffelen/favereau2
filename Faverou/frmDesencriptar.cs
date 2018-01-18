using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Faverou
{
    public partial class frmDesencriptar : Form
    {
        public frmDesencriptar()
        {
            InitializeComponent();
        }

        List<string> archivos = new List<string>();


        private void btnDescargarFTP_Click(object sender, EventArgs e)
        {
            archivos = listarArchivosFTP();
            foreach (string arch in archivos)
            {
                DescargarTxtFTP(arch);
            }
        }

        #region FTP
        public void DescargarTxtFTP(string _fileName)
        {
            #region Credencial
            string url = "ftp://genomica-acha.com.ar/" + _fileName;
            string user = "genoma";
            string password = "Acha2099";
            #endregion

            #region Descargo el TXT
            // Get the object used to communicate with the server.
            FtpWebRequest dirFtp = ((FtpWebRequest)FtpWebRequest.Create(url));
            string pathFolder = CreateDirectoryAndFile(_fileName);

            // Los datos del usuario (credenciales)
            NetworkCredential cr = new NetworkCredential(user, password);
            dirFtp.Credentials = cr;

            // El comando a ejecutar usando la enumeración de WebRequestMethods.Ftp
            dirFtp.Method = WebRequestMethods.Ftp.DownloadFile;

            // Obtener el resultado del comando
            StreamReader reader =
                new StreamReader(dirFtp.GetResponse().GetResponseStream());

            // Leer el stream
            string res = reader.ReadToEnd();

            // Guardarlo localmente con la extensión .txt
            string ficLocal = Path.Combine(pathFolder, Path.GetFileName(url));
            StreamWriter sw = new StreamWriter(ficLocal, false, Encoding.UTF8);
            sw.Write(res);
            sw.Close();

            // Cerrar el stream abierto.
            reader.Close();
            #endregion

            ArrayList documentos = ObtenerNombreDocumentacion(ficLocal);
            foreach (string doc in documentos)
            {
                DescargarPdfFTP(pathFolder, doc);
            }
        }

        public void DescargarPdfFTP(string path, string _fileName)
        {
            #region Credencial
            string url = "ftp://genomica-acha.com.ar/" + _fileName;
            string user = "genoma";
            string password = "Acha2099";
            #endregion

            string ResponseDescription = "";
            path += "\\" + _fileName;

            FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(url);
            req.Method = WebRequestMethods.Ftp.DownloadFile;
            req.Credentials = new NetworkCredential(user, password);
            req.UseBinary = true;
            req.Proxy = null;
            try
            {
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                Stream stream = response.GetResponseStream();
                byte[] buffer = new byte[2048];
                FileStream fs = new FileStream(path, FileMode.Create);
                int ReadCount = stream.Read(buffer, 0, buffer.Length);
                while (ReadCount > 0)
                {
                    fs.Write(buffer, 0, ReadCount);
                    ReadCount = stream.Read(buffer, 0, buffer.Length);
                }
                ResponseDescription = response.StatusDescription;
                fs.Close();
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string CreateDirectoryAndFile(string _nameFolder)
        {
            String[] substrings = _nameFolder.Split('.');
            
            string path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", " ") + "Archivos\\ArchivosEntrada\\" + substrings[0];
            
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        public ArrayList ObtenerNombreDocumentacion(string _path)
        {
            ArrayList documentos = new ArrayList();

            string texto = System.IO.File.ReadAllText(_path);

            String[] posicionDoc = texto.Split(';');

            if (!string.IsNullOrEmpty(posicionDoc[38]))
                documentos.Add(posicionDoc[38].ToString());

            if (!string.IsNullOrEmpty(posicionDoc[39]))
                documentos.Add(posicionDoc[39].ToString());

            if (!string.IsNullOrEmpty(posicionDoc[40]))
                documentos.Add(posicionDoc[40].ToString());

            if (!string.IsNullOrEmpty(posicionDoc[41]))
                documentos.Add(posicionDoc[41].ToString());

            if (!string.IsNullOrEmpty(posicionDoc[42]))
                documentos.Add(posicionDoc[42].ToString());

            return documentos;
        }
                
        public List<string> listarArchivosFTP()
        {
            #region Credencial
            string url = "ftp://genomica-acha.com.ar/";
            string user = "genoma";
            string pass = "Acha2099";
            #endregion

            FtpWebRequest dirFtp = ((FtpWebRequest)FtpWebRequest.Create(url));

            // Los datos del usuario (credenciales)
            NetworkCredential cr = new NetworkCredential(user, pass);
            dirFtp.Credentials = cr;

            // El comando a ejecutar
            dirFtp.Method = "LIST";

            // También usando la enumeración de WebRequestMethods.Ftp
            dirFtp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            // Obtener el resultado del comando

            List<string> archivos_ftp = new List<string>();
            

            StreamReader reader = new StreamReader(dirFtp.GetResponse().GetResponseStream());


            while (!reader.EndOfStream)
            {
                //Application.DoEvents();
                //archivos_ftp.Add(reader.ReadLine());
                String[] caracter = reader.ReadLine().Split(' ');
                Int32 cant = caracter.Count() - 1;
                String[] extension = caracter[cant].Split('.');
                Int32 cant1 = extension.Count();
                if (extension.Count() > 1)
                {
                    if (extension[1].ToString() == "txt")
                        archivos_ftp.Add(caracter[cant].ToString());
                }

                //01-17-18  05:42PM               121560 0500026170.pdf
            }
            reader.Close();

            return archivos_ftp;
        }
        #endregion

        private void btnDecodificar_Click(object sender, EventArgs e)
        {
            DataTable data = new DataTable();
            string path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", " ") + "Archivos\\ArchivosEntrada\\";
            foreach (string arch in archivos)
            {
                String[] substrings = arch.Split('.');
                DataTable newData = LecturaArchivoEntrada(path + substrings[0] + "\\" + arch);
                data.Merge(newData);
            }
        }

        public DataTable LecturaArchivoEntrada(string path)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            string line;

            #region Columnas
            dt.Columns.Add(new DataColumn("Campo1"));
            dt.Columns.Add(new DataColumn("Campo2"));
            dt.Columns.Add(new DataColumn("Campo3"));
            dt.Columns.Add(new DataColumn("Campo4"));
            dt.Columns.Add(new DataColumn("Campo5"));
            dt.Columns.Add(new DataColumn("Campo6"));

            dt.Columns.Add(new DataColumn("Campo7"));
            dt.Columns.Add(new DataColumn("Campo8"));
            dt.Columns.Add(new DataColumn("Campo9"));
            dt.Columns.Add(new DataColumn("Campo10"));
            dt.Columns.Add(new DataColumn("Campo11"));
            dt.Columns.Add(new DataColumn("Campo12"));

            dt.Columns.Add(new DataColumn("Campo13"));
            dt.Columns.Add(new DataColumn("Campo14"));
            dt.Columns.Add(new DataColumn("Campo15"));
            dt.Columns.Add(new DataColumn("Campo16"));
            dt.Columns.Add(new DataColumn("Campo17"));
            dt.Columns.Add(new DataColumn("Campo18"));

            dt.Columns.Add(new DataColumn("Campo19"));
            dt.Columns.Add(new DataColumn("Campo20"));
            dt.Columns.Add(new DataColumn("Campo21"));
            dt.Columns.Add(new DataColumn("Campo22"));
            dt.Columns.Add(new DataColumn("Campo23"));
            dt.Columns.Add(new DataColumn("Campo24"));

            dt.Columns.Add(new DataColumn("Campo25"));
            dt.Columns.Add(new DataColumn("Campo26"));
            dt.Columns.Add(new DataColumn("Campo27"));
            dt.Columns.Add(new DataColumn("Campo28"));
            dt.Columns.Add(new DataColumn("Campo29"));
            dt.Columns.Add(new DataColumn("Campo30"));

            dt.Columns.Add(new DataColumn("Campo31"));
            dt.Columns.Add(new DataColumn("Campo32"));
            dt.Columns.Add(new DataColumn("Campo33"));
            dt.Columns.Add(new DataColumn("Campo34"));
            dt.Columns.Add(new DataColumn("Campo35"));
            dt.Columns.Add(new DataColumn("Campo36"));

            dt.Columns.Add(new DataColumn("Campo37"));
            dt.Columns.Add(new DataColumn("Campo38"));
            dt.Columns.Add(new DataColumn("Campo39"));
            dt.Columns.Add(new DataColumn("Campo40"));
            dt.Columns.Add(new DataColumn("Campo41"));
            dt.Columns.Add(new DataColumn("Campo42"));

            dt.Columns.Add(new DataColumn("Campo43"));
            dt.Columns.Add(new DataColumn("Campo44"));
            dt.Columns.Add(new DataColumn("Campo45"));
            dt.Columns.Add(new DataColumn("Campo46"));
            dt.Columns.Add(new DataColumn("Campo47"));
            dt.Columns.Add(new DataColumn("Campo48"));

            dt.Columns.Add(new DataColumn("Campo49"));
            dt.Columns.Add(new DataColumn("Campo50"));
            dt.Columns.Add(new DataColumn("Campo51"));

            dt.Columns.Add(new DataColumn("CampoEstado"));
            #endregion

            // Read the file and display it line by line.  
            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                dr = dt.NewRow();

                String[] campos = line.Split(';');

                dr["Campo1"] = campos[1].ToString();
                dr["Campo2"] = campos[2].ToString();
                dr["Campo3"] = campos[3].ToString();
                dr["Campo4"] = campos[4].ToString();
                dr["Campo5"] = campos[5].ToString();
                dr["Campo6"] = campos[6].ToString();

                dr["Campo7"] = campos[7].ToString();
                dr["Campo8"] = campos[8].ToString();
                dr["Campo9"] = campos[9].ToString();
                dr["Campo10"] = campos[10].ToString();
                dr["Campo11"] = campos[11].ToString();
                dr["Campo12"] = campos[12].ToString();

                dr["Campo13"] = campos[13].ToString();
                dr["Campo14"] = campos[14].ToString();
                dr["Campo15"] = campos[15].ToString();
                dr["Campo16"] = campos[16].ToString();
                dr["Campo17"] = campos[17].ToString();
                dr["Campo18"] = campos[18].ToString();

                dr["Campo19"] = campos[19].ToString();
                dr["Campo20"] = campos[20].ToString();
                dr["Campo21"] = campos[21].ToString();
                dr["Campo22"] = campos[22].ToString();
                dr["Campo23"] = campos[23].ToString();
                dr["Campo24"] = campos[24].ToString();

                dr["Campo25"] = campos[25].ToString();
                dr["Campo26"] = campos[26].ToString();
                dr["Campo27"] = campos[27].ToString();
                dr["Campo28"] = campos[28].ToString();
                dr["Campo29"] = campos[29].ToString();
                dr["Campo30"] = campos[30].ToString();

                dr["Campo31"] = campos[31].ToString();
                dr["Campo32"] = campos[32].ToString();
                dr["Campo33"] = campos[33].ToString();
                dr["Campo34"] = campos[34].ToString();
                dr["Campo35"] = campos[35].ToString();
                dr["Campo36"] = campos[36].ToString();

                dr["Campo37"] = campos[37].ToString();
                dr["Campo38"] = campos[38].ToString();
                dr["Campo39"] = campos[39].ToString();
                dr["Campo40"] = campos[40].ToString();
                dr["Campo41"] = campos[41].ToString();
                dr["Campo42"] = campos[42].ToString();

                dr["Campo43"] = campos[43].ToString();
                dr["Campo44"] = campos[44].ToString();
                dr["Campo45"] = campos[45].ToString();
                dr["Campo46"] = campos[46].ToString();
                dr["Campo47"] = campos[47].ToString();
                dr["Campo48"] = campos[48].ToString();

                dr["Campo49"] = campos[49].ToString();
                dr["Campo50"] = campos[50].ToString();
                dr["Campo51"] = campos[51].ToString();

                dr["CampoEstado"] = false;                
                dt.Rows.Add(dr);
            }

            file.Close();

            return dt;
        }

        
    }
}
