using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_FTP
{
    
    public partial class Form1 : Form
    {
        funcoes funcoes = new funcoes();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            funcoes.config(txthost.Text, txtuser.Text, txtpessword.Text, txtdir.Text);
        }

        private void ler_diretorioftp()
        {

            DataTable dataTable = new DataTable();

            dataTable = funcoes.diretorio_arquivoFTP();
            dgvarquivo.Rows.Clear();

            foreach (DataRow row in dataTable.Rows)
            {
              dgvarquivo.Rows.Add(row["nome"].ToString(),row["size"].ToString());
            }

            
        }


        public void EnviarArquivoFTP(string fileName)
        {
            try
            {

                toolStripProgressBar1.Visible = true;

                FileInfo arquivoInfo = new FileInfo(fileName);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(txthost.Text + "/" + txtdir.Text + "/" + arquivoInfo.Name);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(txtuser.Text, txtpessword.Text);
                request.UseBinary = true;
                request.KeepAlive = true;
                request.UsePassive = false;
                //   request.Timeout = 3000;
                request.ContentLength = arquivoInfo.Length;

                ServicePointManager.DefaultConnectionLimit = 10;


                using (FileStream fs = arquivoInfo.OpenRead())
                {
                    byte[] buffer = new byte[2048];
                    int bytesSent = 0;
                    int bytes = 0;

                    using (Stream stream = request.GetRequestStream())
                    {
                        while (bytesSent < arquivoInfo.Length)
                        {
                            bytes = fs.Read(buffer, 0, buffer.Length);

                            stream.Write(buffer, 0, bytes);
                            bytesSent += bytes;



                            toolStripStatusLabel1.Text = "Aguarde, Enviando arquivo FTP... " + funcoes.GetFileSize(arquivoInfo.Length) + " / " + funcoes.GetFileSize(bytesSent);

                            toolStripProgressBar1.Value = System.Convert.ToInt32(bytesSent / (double)arquivoInfo.Length * 100);

                            //lblper.Text = toolStripProgressBar1.Value + "%";


                            Application.DoEvents();
                        }
                    }
                }

                toolStripProgressBar1.Visible = false;

                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();

               if (string.IsNullOrEmpty(result))
                {
                  toolStripStatusLabel1.Text = "Arquivo enviado com Sucesso em: " + DateTime.Now;
                  MessageBox.Show("Arquivo enviado com Sucesso !", "Aviso !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                  
               else
                    toolStripStatusLabel1.Text = result;



                //   lblper.Visible = false;



                //txtUploadFile.Clear();
            }
            catch (WebException e)
            {
                string status = ((FtpWebResponse)e.Response).StatusDescription;
               // Interaction.MsgBox(status, MsgBoxStyle.Critical, "Aviso !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            //define as propriedades do controle 
            //OpenFileDialog
            ofd1.Multiselect = false;
            ofd1.Title = "Selecionar Arquivos";
            ofd1.InitialDirectory = @"C:\Dados\";
            //filtra para exibir todos arquivos
            ofd1.Filter = "All files (*.*)|*.*";
            ofd1.CheckFileExists = true;
            ofd1.CheckPathExists = true;
            ofd1.RestoreDirectory = true;

          
            if (ofd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    txtArquivoUpload.Text = ofd1.FileName;
                }
                catch (SecurityException ex)
                {
                    // O usuário  não possui permissão para ler arquivos
                    MessageBox.Show("Erro de segurança Contate o administrador de segurança da rede.\n\n" +
                                                "Mensagem : " + ex.Message + "\n\n" +
                                                "Detalhes (enviar ao suporte):\n\n" + ex.StackTrace);
                }
                catch (Exception ex)
                {
                    // Não pode carregar a imagem (problemas de permissão)
                    MessageBox.Show("Você pode não ter permissão para ler o arquivo , ou " +
                                               " ele pode estar corrompido.\n\nErro reportado : " + ex.Message);
                }
            }
        }

        private void bntenviar_Click(object sender, EventArgs e)
        {
            bntenviar.Enabled = false;
            EnviarArquivoFTP(txtArquivoUpload.Text);
            ler_diretorioftp();
            bntenviar.Enabled = true;
        }

       
        private void toolStripdeletar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente deletar o arquivo ?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) 
            {

              if (funcoes.DeleteFileFTP(dgvarquivo.CurrentRow.Cells[0].Value.ToString()))
                {
                    MessageBox.Show("Arquivo Excluirdo com Sucesso !", "Aviso !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvarquivo.Rows.Remove(dgvarquivo.CurrentRow);
                }
                else
                {
                    MessageBox.Show("Ocorreu um erro, arquivo não excluido !", "Aviso !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                
            }

        }

        private void dgvarquivo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                // saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = dgvarquivo.CurrentRow.Cells[0].Value.ToString();

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //  dest_down = saveFileDialog1.FileName;
                    dgvarquivo.Enabled = false;
                    Download(saveFileDialog1.FileName);
                }
            }
        }

        private void Download(string dest)
        {
            try
            {
                string url = txthost.Text + "/" + txtdir.Text + "/" + dgvarquivo.CurrentRow.Cells[0].Value.ToString();
                NetworkCredential credentials = new NetworkCredential(txtuser.Text, txtpessword.Text);
              //  MessageBox.Show(url);
                // Query size of the file to be downloaded
                WebRequest sizeRequest = WebRequest.Create(url);
                sizeRequest.Credentials = credentials;
                sizeRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                int size = (int)sizeRequest.GetResponse().ContentLength;

                toolStripProgressBar1.Visible = true;
                // Download the file
                WebRequest request = WebRequest.Create(url);
                request.Credentials = credentials;
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                using (Stream ftpStream = request.GetResponse().GetResponseStream())
                using (Stream fileStream = File.Create(dest))
                {
                    byte[] buffer = new byte[2048];
                    int read;
                    int bytesSent = 0;
                    while ((read = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, read);
                        int position = (int)fileStream.Position;
                        bytesSent += read;

                        toolStripStatusLabel1.Text = "Aguarde, baixando arquivo FTP... " + funcoes.GetFileSize(size) + " / " + funcoes.GetFileSize(bytesSent);

                        toolStripProgressBar1.Value = System.Convert.ToInt32(bytesSent / (double)size * 100);

                        //lblper.Text = toolStripProgressBar1.Value + "%";


                        Application.DoEvents();

                    }
                }

                toolStripStatusLabel1.Text = "Arquivo baixado com Sucesso em; " + DateTime.Now;
                toolStripProgressBar1.Visible = false;
                dgvarquivo.Enabled = true;
                MessageBox.Show("Arquivo baixado com Sucesso !", "Aviso !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (txthost.TextLength != 0)
            {
               ler_diretorioftp();
            }
           
            timer1.Dispose();
        }
    }
}
