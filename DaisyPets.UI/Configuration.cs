using DaisyPets.Core.Application.Enums;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI
{
    /// <summary>
    /// Configuration
    /// </summary>
    public partial class Configuration : MetroForm
    {
        FolderBrowser folderBrowserDialog1 = new FolderBrowser();
        /// <summary>
        /// BackUp/Restore da base de dados
        /// </summary>
        public Configuration()
        {
            InitializeComponent();
        }

        private void Configuration_Load(object sender, EventArgs e)
        {
            InitScreenData();
            // Verifica se há forms abertos (sem serem os iniciais e este) e alerta utilizador
            //CheckOpenForms();
        }

        private void InitScreenData()
        {
            btnBackup.Enabled = true;
        }


        private void btnBackup_Click(object sender, EventArgs e)
        {
            DoBackup();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// Author : Himasagar Kutikuppala 
        ///A utility method that runs the batch file with supplied arguments. /// 
        ///</summary> 
        /// <param name="batchFileName">Name of the batch file that should be run</param> 
        /// <param name="argumentsToBatchFile">;Arguments to the batch file</param> 
        /// <returns>Status of running the batch file</returns>
        protected bool ExecuteBatchFile(string batchFileName, string[] argumentsToBatchFile)
        {
            string argumentsString = string.Empty;
            try
            { //Add up all arguments as string with space separator between the arguments if (argumentsToBatchFile != null) 
                {
                    for (int count = 0; count < argumentsToBatchFile.Length; count++)
                    {
                        argumentsString += " ";
                        argumentsString += argumentsToBatchFile[count]; //argumentsString += "\""; 
                    }
                } //Create process start information 
                System.Diagnostics.ProcessStartInfo DBProcessStartInfo = new System.Diagnostics.ProcessStartInfo(batchFileName, argumentsString); //Redirect the output to standard window 
                DBProcessStartInfo.RedirectStandardOutput = true; //The output display window need not be falshed onto the front. 
                DBProcessStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                DBProcessStartInfo.UseShellExecute = false; //Create the process and run it 
                System.Diagnostics.Process dbProcess;
                dbProcess = System.Diagnostics.Process.Start(DBProcessStartInfo); //Catch the output text from the console so that if error happens, the output text can be logged. 
                System.IO.StreamReader standardOutput = dbProcess.StandardOutput; /* Wait as long as the DB Backup or Restore or Repair is going on. Ping once in every 2 seconds to check whether process is completed. */
                while (!dbProcess.HasExited)
                    dbProcess.WaitForExit(2000);
                if (dbProcess.HasExited)
                {
                    string consoleOutputText = standardOutput.ReadToEnd(); //TODO - log consoleOutputText to the log file. 
                }
                return true;
            } // Catch the SQL exception and throw the customized exception made out of that 
            catch (SqlException)
            {
                throw;
            } // Catch all general exceptions 
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual void DoDump()
        {
            /* The purpose of this function is to call mysqldump from the shell 
            * and capture it's output (from stdout) and redirect it to a filestream 
            * this has been tested and in production at my company, but caveat empteur 
            */
            string Path = @"c:\\MYSQL_Repositorio";
            string Filename = "Clinicas";
            string Argumentos = string.Format("--database {0} --result-file={1} --single-transaction --routines --triggers --user={2} --password={3}", "FichasClinicas", "root", "filename", "warmupkieunv");


            string fullfile = string.Format("{0}\\{1}", Path, Filename);
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            FileStream ostrm = new FileStream(fullfile, FileMode.Create, FileAccess.Write);

            using (StreamWriter swriter = new StreamWriter(ostrm))
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = "mysqldump"; // uses mysqldump, but can be anything really
                p.StartInfo.Arguments = Argumentos; //Command line arguments minus the < <file>.sql p.Start();
                swriter.Write(p.StandardOutput.ReadToEnd());
                p.WaitForExit();

                if (p.ExitCode != 0)
                {
                    throw new Exception(string.Format("MySQL Dump exited with code #{0}", p.ExitCode));
                }
                p.Close();
                swriter.Close();
                ostrm.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bSearchFolder_Click(object sender, EventArgs e)
        {
            fbBackup.SelectedPath = @"c:\newprojects\daisypets";
            DialogResult dr = fbBackup.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtPasta.Text = fbBackup.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            //DoRestore();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void bSearchFile_Click(object sender, EventArgs e)
        {
            string connectString = "xxx.db";
            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectString);
            var sDataFile = sqlConStrBuilder.InitialCatalog;

            ofdRestore.InitialDirectory = @"c:\newprojects\daisypets";
            DialogResult dr = ofdRestore.ShowDialog();
            if (dr == DialogResult.OK)
            {
                var sFile = ofdRestore.FileName;
                if (!sFile.Contains(sDataFile))
                {
                    string sFileName = Path.GetFileNameWithoutExtension(sFile);

                    MessageBox.Show("Escolhido: " + sFileName +
                        "\r\nPrevisto: " + sDataFile +
                        "\r\n\r\nFicheiro não parece apropriado\r\nTente de novo, p.f.",
                        "Reposição da base de dados", MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
                else
                    txtBackupFile.Text = sFile;
            }
        }

        private void DoBackup()
        {
            string sProvider = "sqlite";
            try
            {
                if (!Directory.Exists(txtPasta.Text) && !string.IsNullOrEmpty(txtPasta.Text))
                {
                    MessageBox.Show("Pasta escolhida não existe\r\n\r\nVerifique, p.f.", "Backup - Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Cursor = Cursors.Default;
                    return;
                }
                DialogResult dr = MessageBoxAdv.Show("Confirme operação, p.f.",
                    "Backup da base de daos",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    BackupSqLite();
                    Cursor = Cursors.Default;
                    MessageBoxAdv.Show("Backup concluído com sucesso.",
                        "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                txtPasta.Clear();

            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                throw new ApplicationException(ex.Message);
            }
        }

        //private void DoRestore()
        //{
        //    if (!string.IsNullOrEmpty(txtBackupFile.Text))
        //    {
        //        DialogResult dr = MessageBoxAdv.Show("Confirme operação, p.f.\r\n\r\nTodos os dados atuais serão perdidos!",
        //            "Reposição da base de dados",
        //            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning,
        //            MessageBoxDefaultButton.Button2);
        //        if (dr == DialogResult.OK)
        //        {
        //            Cursor = Cursors.WaitCursor;
        //            Utilitarios.RestoreBD_SqlServer(txtBackupFile.Text);
        //            Cursor = Cursors.Default;
        //            MessageBoxAdv.Show("Restore concluído com sucesso", "Reposição da base de dados",
        //                MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        txtBackupFile.Clear();

        //    }
        //    else
        //        MessageBoxAdv.Show("Escolha ficheiro, p.f.", "Reposição de base de dados",
        //            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //}
        //    private static void BackupDatabase()
        //    {
        //        var srv = new Server(@".\SQLEXPRESS");
        //        var db = default(Database);
        //        db = srv.Databases["ClinicaSoft2007SQL"];
        //        //Define a Backup object variable.
        //        var backup = new Backup();
        //        // Describes the backup operation
        //        backup.Action = BackupActionType.Database;
        //        backup.BackupSetDescription = "Full backup - " + DateTime.Today.ToLongDateString();
        //        backup.BackupSetName = "Backup de ClinicaSoft2007SQL";
        //        backup.Database = "ClinicaSoft2007SQL";
        //        // Set the backup device to 'file'
        //        var bdi = default(BackupDeviceItem);
        //        bdi = new BackupDeviceItem("ClinicaSoft2007SQL_Bak", DeviceType.File);
        //        backup.Devices.Add(bdi);
        //        // If 'false', the backup will be full
        //        backup.Incremental = false;
        //        // Backup expiration date
        //        var backupdate = new DateTime();
        //        backupdate = new System.DateTime(2013, 7, 12);
        //        backup.ExpirationDate = backupdate;
        //        // The log will be truncated after backup completion
        //        backup.LogTruncation = BackupTruncateLogType.Truncate;
        //        // Execute the backup process
        //        backup.SqlBackup(srv);
        //        //Remove the backup device from the Backup object.
        //        backup.Devices.Remove(bdi);
        //    }

        //    private static void RestoreDatabase()
        //    {
        //        var srv = new Server(@".\SQLEXPRESS");
        //        var db = default(Database);
        //        db = srv.Databases["ClinicaSoft2007SQL"];
        //        // Drop the database before to restore it
        //        db.Drop();
        //        //Define a Restore object variable.
        //        Restore restore = new Restore();
        //        restore.NoRecovery = true;
        //        // Set the backup device to 'file'
        //        var bdi = default(BackupDeviceItem);
        //        bdi = new BackupDeviceItem("ClinicaSoft2007SQL_Bak", DeviceType.File);
        //        //Add the device that contains the full database backup
        //        restore.Devices.Add(bdi);
        //        //Specify the database name.
        //        restore.Database = "ClinicaSoft2007SQL";
        //        //Restore the full database.
        //        restore.SqlRestore(srv);}


        private string ChooseFolder(TipoBackup Tipo)
        {
            string folderName = "";
            string sInitialDirectory;
            switch (Tipo)
            {
                case TipoBackup.SqLiteBackup:
                    sInitialDirectory = @"c:\newprojects\daisypets";
                    break;
                default:
                    sInitialDirectory = "";
                    break;
            }

            if (!string.IsNullOrEmpty(sInitialDirectory) && !Directory.Exists(sInitialDirectory))
                Directory.CreateDirectory(sInitialDirectory);

            folderBrowserDialog1.Description = "Escolha o local onde pretende fazer o backup";
            folderBrowserDialog1.SelectLocation = sInitialDirectory;
            //SendKeys.Send("{TAB}{TAB}{RIGHT}");

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                folderName = folderBrowserDialog1.SelectLocation;
                return folderName;
            }
            else
                return "";
        }
        private void BackupSqLite()
        {
            string selectedPath = txtPasta.Text; // ChooseFolder(TipoBackup.SqLiteBackup); // folderBrowse.SelectedPath;

            if (selectedPath == string.Empty)
            {
                MessageBoxAdv.Show("Não indicou caminho...");
                return;
            }

            string sSqLiteFile = @"c:\";
            string sPathSqLiteBackup = @"c:\temp";
            string sPathFixed = selectedPath.EndsWith(@"\") ? selectedPath : selectedPath + @"\";
            string sourceFileName = sSqLiteFile;
            string targetFileName = sPathFixed + Path.GetFileName(sourceFileName);
            if (sourceFileName == targetFileName)
            {
                MessageBox.Show("Escolha outro local, p.f.", "Origem e destino do ficheiro são os mesmos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!File.Exists(sourceFileName))
            {
                MessageBox.Show("Defina local onde reside base de dados\r\nnas Opções do programa", "Definições para backup", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                if (File.Exists(targetFileName))
                    File.SetAttributes(targetFileName, FileAttributes.Normal);

                File.Copy(sourceFileName, selectedPath + @"\" + Path.GetFileName(sourceFileName), true);
                //Logger.WriteTrace(this.Name, "Executado backup");
                Cursor = Cursors.Default;

                MessageBox.Show("Operação concluída com sucesso", "Backup - SqLite",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                throw new ApplicationException(ex.StackTrace);
            }

        }
    }
}
