using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projeto_compactador_arquivo
{
    public partial class Form1 : Form
    {
        string ext = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdArq = new FolderBrowserDialog();
            fbdArq.Description = "Selecione uma pasta origem";
            fbdArq.ShowNewFolderButton = true;
            
            int num = (int)fbdArq.ShowDialog();
            this.txtcaminho_comp.Text = fbdArq.SelectedPath;

            this.txtdest.Text = fbdArq.SelectedPath + @"\" + System.IO.Path.GetFileName(txtcaminho_comp.Text) + ext;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdArq = new FolderBrowserDialog();
            fbdArq.Description = "Selecione uma pasta origem";
            fbdArq.ShowNewFolderButton = true;

            int num = (int)fbdArq.ShowDialog();
           
            this.txtdest.Text = fbdArq.SelectedPath + @"\" + System.IO.Path.GetFileName(txtcaminho_comp.Text) + ext;
        }

        public static void ExecutaProg(string parametro)
        {

            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\7zFM.exe");
            string  sZipPath = (string)registryKey.GetValue("Path");
            sZipPath = sZipPath.Replace("/", "\\");
            sZipPath = sZipPath + string.Format("{0}", (object)"7zG.exe");
            registryKey.Dispose();

            try
            {
                int milliseconds = 14400000;
                Process process = Process.Start(new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = sZipPath,
                    Arguments = parametro
                });
                process.WaitForInputIdle();
                process.WaitForExit(milliseconds);
                if (process.HasExited)
                    return;
                if (process.Responding)
                    process.CloseMainWindow();
                else
                    process.Kill();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Erro ao executar programa externo. Erro:" + ex.Message);
            }
        }


        public static string compactar_rar(string inputfilename, string outputfilename)
        {
            string the_rar;
            RegistryKey the_Reg;
            object the_Obj;
            string the_Info;
            ProcessStartInfo the_StartInfo;
            Process the_Process;

          //  if (File.Exists(Util.verificar_rar_instal()) == false)
          //  {
           //     return "O Programa WinRar não esta instalado no Computar ?";
           // }

            string pas = "";
            //   if (txtpes_rar.TextLength != 0)
            //   {
            // pas = "-hp" + txtpes_rar.Text;
            //   }

            try
            {
                the_Reg = Registry.ClassesRoot.OpenSubKey(@"WinRAR\shell\open\command");
                // for winrar path
                the_Obj = the_Reg.GetValue("");
                the_rar = the_Obj.ToString();
                the_Reg.Close();
                the_rar = the_rar.Substring(1, the_rar.Length - 7);
                the_Info = Convert.ToString((Convert.ToString("a -ep " + pas + "" + " ") + outputfilename) + " " + " ") + inputfilename;
                // i dare say for parameter
                the_StartInfo = new ProcessStartInfo();

                the_StartInfo.FileName = the_rar;
                the_StartInfo.Arguments = the_Info;
                the_StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                // the_StartInfo.WorkingDirectory = workingfolder
                the_Process = new Process();
                the_Process.StartInfo = the_StartInfo;
                the_Process.Start();
                // starting compress Process

                the_Process.WaitForExit();
                do
                    ;
                while (!the_Process.HasExited);
                int exitCode = the_Process.ExitCode;
                the_Process.Close();
                Application.DoEvents();

                //  MessageBox.Show(exitCode == 0 ? "OK" : "Erro MySQL.");

                return exitCode == 0 ? "compactado" : $"Erro ao compactar arquivo: {exitCode}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtcaminho_comp.TextLength == 0)
            {
                MessageBox.Show("Informe o local do arquivo para compactar ?", "Aviso !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                   button1.Focus();
                return;
            }
            if (opt7z.Checked == true)
            ExecutaProg(" a " + txtdest.Text + " " + txtcaminho_comp.Text + @"\" + "* -r -mx9 -ssw -up1q1r2x1y2z1w2 ");
          
            else

            compactar_rar(txtcaminho_comp.Text, txtdest.Text);
            MessageBox.Show("Arquivo compactado com Sucesso !", "Aviso !", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void opt7z_CheckedChanged(object sender, EventArgs e)
        {
            
            int p = txtdest.Text.IndexOf(".");
            txtdest.Text = txtdest.Text.Substring(0, p) + ".7z";
            ext = ".7z";
        }

        private void optrar_CheckedChanged(object sender, EventArgs e)
        {
            int p = txtdest.Text.IndexOf(".");
            txtdest.Text = txtdest.Text.Substring(0, p) + ".rar";
            ext = ".rar";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (opt7z.Checked == true)
                ext = ".7z";
           else
                ext = ".rar";
        }
    }
}
