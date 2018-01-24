using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        #region Credenciales FTP
        /*static string url = "ftp://genomica-acha.com.ar/";
        static string user = "genoma";
        static string pass = "Acha2099";*/

        //static string url = "ftp://190.210.219.36";
        static string urlDown = "ftp://190.210.219.36/IN/";
        static string urlUp = "ftp://190.210.219.36/OUT/";
        static string user = "prueba";
        static string pass = "feriado";
        #endregion

        List<string> archivos = new List<string>();

        private void btnDescargarFTP_Click(object sender, EventArgs e)
        {
            #region Ftp
            archivos = listarArchivosFTP();
            foreach (string arch in archivos)
            {
                DescargarTxtFTP(arch);
            }
            #endregion
            
            #region DataTable
            DataTable data = new DataTable();
            string path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "") + "Archivos\\ArchivosEntrada\\";
            foreach (string arch in archivos)
            {
                String[] substrings = arch.Split('.');

                Decrypt(path + substrings[0], substrings[0]);
                                
                DataTable newData = LecturaArchivoEntrada(path + substrings[0] + "\\" + arch);
                data.Merge(newData);

                CrearLogEntrada(arch);
            }
            #endregion
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            string nameFolder = "\\VUELTA0000" + DateTime.Today.Year + DateTime.Today.Month + DateTime.Today.Day + DateTime.Today.Hour + DateTime.Today.Minute;
            string nameFile = nameFolder + ".txt";

            string path = CreateDirectoryAndFileEnd(nameFolder);
            string pathFile = path + "\\" + nameFile;

            #region DataTable
            DataTable data = new DataTable();
            string path1 = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "") + "Archivos\\ArchivosEntrada\\";
            foreach (string arch in archivos)
            {
                String[] substrings = arch.Split('.');
                DataTable newData = LecturaArchivoEntrada(path1 + substrings[0] + "\\" + arch);
                data.Merge(newData);
            }
            #endregion

            //Genera el txt
            EscribirArchivoSalida(pathFile, data);

            //Se crea el registro del archivo de salida
            CrearLogSalida(nameFile);

            //Encriptar archivo
            Encrypt(pathFile);            
        }

        private void btnInFTP_Click(object sender, EventArgs e)
        {
            string nameFolder = "\\VUELTA0000" + DateTime.Today.Year + DateTime.Today.Month + DateTime.Today.Day + DateTime.Today.Hour + DateTime.Today.Minute;
            string nameFile = nameFolder + ".txt";

            string pathFolder = CreateDirectoryAndFileEnd(nameFolder);
            
            string path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "") + "Archivos\\ArchivosSalida\\" + nameFolder;

            //Carga los archivos en el FTP
            FileInfo[] files = listAllArchivosDirectory(pathFolder);
            foreach(FileInfo f in files)
            {
                if(f.Extension.ToString() != ".txt")
                    UploadFile(pathFolder + "\\" + f.Name, f.Name);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            DeleteFileFTP();
        }

        #region Métodos Auxiliares FTP
        #region Descarga
        public void DescargarTxtFTP(string _fileName)
        {
            string url = urlDown + _fileName;

            #region Descargo el TXT
            // Get the object used to communicate with the server.
            FtpWebRequest dirFtp = ((FtpWebRequest)FtpWebRequest.Create(url));
            string pathFolder = CreateDirectoryAndFile(_fileName);

            // Los datos del usuario (credenciales)
            NetworkCredential cr = new NetworkCredential(user, pass);
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
            string url = urlDown + _fileName;

            string ResponseDescription = "";
            path += "\\" + _fileName;

            FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(url);
            req.Method = WebRequestMethods.Ftp.DownloadFile;
            req.Credentials = new NetworkCredential(user, pass);
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
            
            string path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "") + "Archivos\\ArchivosEntrada\\" + substrings[0];
            
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        public string CreateDirectoryAndFileEnd(string _nameFolder)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "") + "Archivos\\ArchivosSalida\\" + _nameFolder;

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
            FtpWebRequest dirFtp = ((FtpWebRequest)FtpWebRequest.Create(urlDown));

            // Los datos del usuario (credenciales)
            NetworkCredential cr = new NetworkCredential(user, pass);
            dirFtp.Credentials = cr;

            //dirFtp.UsePassive = true;

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
                    if (extension[1].ToString() == "asc")
                        archivos_ftp.Add(caracter[cant].ToString());
                }
            }
            reader.Close();

            return archivos_ftp;
        }

        public List<string> listAllArchivosFTP()
        {
            FtpWebRequest dirFtp = ((FtpWebRequest)FtpWebRequest.Create(urlDown));

            // Los datos del usuario (credenciales)
            NetworkCredential cr = new NetworkCredential(user, pass);
            dirFtp.Credentials = cr;

            //dirFtp.UsePassive = true;

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

                archivos_ftp.Add(caracter[cant].ToString());                
            }
            reader.Close();

            return archivos_ftp;
        }

        public FileInfo[] listAllArchivosDirectory(string _path)
        {
            DirectoryInfo directory = new DirectoryInfo(_path);

            FileInfo[] files = directory.GetFiles("*.*");

            DirectoryInfo[] directories = directory.GetDirectories();

            return files;
        }

        public void DeleteFileFTP()
        {
            List<string> archivosDelete = listAllArchivosFTP();

            foreach (string arch in archivosDelete)
            {
                string url = urlDown + arch;
                FtpWebRequest requestFileDelete = (FtpWebRequest)WebRequest.Create(url);
                requestFileDelete.Credentials = new NetworkCredential(user, pass);
                requestFileDelete.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse responseFileDelete = (FtpWebResponse)requestFileDelete.GetResponse();
            }
        }
        #endregion

        public static void UploadFile(string path, string _fileName)
        {
            string newUri = urlUp + _fileName;

            FtpWebRequest ftpRequest;
            FtpWebResponse ftpResponse;
            try
            {
                //define os requesitos para se conectar com o servidor
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(newUri));
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.Proxy = null;
                ftpRequest.UseBinary = true;
                ftpRequest.Credentials = new NetworkCredential(user, pass);

                //Seleção do arquivo a ser enviado
                FileInfo archivo = new FileInfo(path);
                byte[] fileContents = new byte[archivo.Length];

                using (FileStream fr = archivo.OpenRead())
                {
                    fr.Read(fileContents, 0, Convert.ToInt32(archivo.Length));
                }

                using (Stream writer = ftpRequest.GetRequestStream())
                {
                    writer.Write(fileContents, 0, fileContents.Length);
                }

                //obtem o FtpWebResponse da operação de upload
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            }
            catch (WebException webex)
            {
                MessageBox.Show(webex.ToString());
            }
        }
        #endregion

        #region Métodos Auxiliares DataTable
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

        public void EscribirArchivoSalida(string path, DataTable data)
        {
            StreamWriter WriteReportFile = File.AppendText(path);

            foreach (DataRow r in data.Rows)
            {
                string nuevaLinea = r[1].ToString() + ";" + r[2].ToString() + ";" + r[3].ToString() + ";" + r[4].ToString() + ";" + r[5].ToString() + ";" + r[6].ToString() + ";" + r[7].ToString() + ";" + r[8].ToString() + ";" + r[9].ToString();
                nuevaLinea += ";" + r[10].ToString() + ";" + r[11].ToString() + ";" + r[12].ToString() + ";" + r[13].ToString() + ";" + r[14].ToString() + ";" + r[15].ToString() + ";" + r[16].ToString() + ";" + r[17].ToString() + ";" + r[18].ToString();
                nuevaLinea += ";" + r[19].ToString() + ";" + r[20].ToString() + ";" + r[21].ToString() + ";" + r[22].ToString() + ";" + r[23].ToString() + ";" + r[24].ToString() + ";" + r[25].ToString() + ";" + r[26].ToString() + ";" + r[27].ToString();
                nuevaLinea += ";" + r[28].ToString() + ";" + r[29].ToString() + ";" + r[30].ToString() + ";" + r[31].ToString() + ";" + r[32].ToString() + ";" + r[33].ToString() + ";" + r[34].ToString() + ";" + r[35].ToString() + ";" + r[36].ToString();
                nuevaLinea += ";" + r[37].ToString() + ";" + r[38].ToString() + ";" + r[39].ToString() + ";" + r[40].ToString() + ";" + r[41].ToString() + ";" + r[42].ToString() + ";" + r[43].ToString() + ";" + r[44].ToString() + ";" + r[45].ToString();
                nuevaLinea += ";" + r[46].ToString() + ";" + r[47].ToString() + ";" + r[48].ToString() + ";" + r[49].ToString() + ";" + r[50].ToString() + ";" + r[51].ToString();

                WriteReportFile.WriteLine(nuevaLinea, true);
            }

            WriteReportFile.Close();
        }
        #endregion

        #region Métodos Log
        public void CrearLogEntrada(string _file)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "") + "Archivos\\Log\\";
                string nameFile = "LogEntrada.txt";
                string fullPath = path + nameFile;
                
                if (!File.Exists(fullPath))
                {
                    FileStream fs = File.Create(fullPath);
                    fs.Close();
                }

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(fullPath, true);

                //Write a line of text
                sw.WriteLine(String.Format("{0:d/M/yyyy HH:mm:ss}", DateTime.Now) + "," + _file + "," + -1);

                //Close the file
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CrearLogSalida(string _file)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "") + "Archivos\\Log\\";
                string nameFile = "LogSalida.txt";
                string fullPath = path + nameFile;
                
                if (!File.Exists(fullPath))
                {
                    FileStream fs = File.Create(fullPath);
                    fs.Close();
                }

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(fullPath, true);

                //Write a line of text
                sw.WriteLine(String.Format("{0:d/M/yyyy HH:mm:ss}", DateTime.Now) + "," + _file + "," + -1);

                //Close the file
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Encriptar
        public void Encrypt(string _pathFile)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "") + "Archivos\\Cmd_Faverau_Encrypt.bat";
                if (File.Exists(path))
                {
                    File.Delete(path);
                    GenerateFileEncryptBat(path, _pathFile);
                }
                else
                {
                    GenerateFileEncryptBat(path, _pathFile);
                }

                Process.Start(path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GenerateFileEncryptBat(string path, string _pathFile)
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("@echo off");

                string command = "cd C:\\";
                string folder = "\"Program Files (x86)\\gnu\\gnupg\"";

                string commandEnd = command + folder;

                sw.WriteLine(commandEnd);
                sw.WriteLine("start gpg --default-key jperez@empresadejuanperez.com.ar -se -r diegoh.gomez@bancofrances.com.ar --batch --yes --passphrase 123 --armor " + _pathFile);
                sw.WriteLine("exit");
            }
        }


        public void Decrypt(string _pathFile, string _nameFile)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "") + "Archivos\\Cmd_Faverau_Decrypt.bat";
                if (File.Exists(path))
                {
                    File.Delete(path);
                    GenerateFileDecryptBat(path, _pathFile, _nameFile);
                }
                else
                {
                    GenerateFileDecryptBat(path, _pathFile, _nameFile);
                }

                Process.Start(path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GenerateFileDecryptBat(string path, string _pathFile, string _nameFile)
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("@echo off");

                string command = "cd C:\\";
                string folder = "\"Program Files (x86)\\gnu\\gnupg\"";

                string commandEnd = command + folder;

                sw.WriteLine(commandEnd);
                sw.WriteLine("start gpg --batch --yes --passphrase 123 -o salida.txt -d " + _pathFile + "\\" + _nameFile + ".asc");
                sw.WriteLine("exit");
            }
        }
        #endregion


    }
}
