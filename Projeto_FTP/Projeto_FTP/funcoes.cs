using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

namespace Projeto_FTP
{
    class funcoes
    {
        string host;
        string user;
        string password;
        string diretorio;

        public void config(string host, string user, string password, string diretorio) 
        {
            this.host = host;
            this.user = user;
            this.password = password;
            this.diretorio = diretorio;
        }

        public string GetFileSize(long FileInBytes)
        {
            string strSize;
           
            if (FileInBytes < 1024)
                strSize = FileInBytes + " by"; // Byte
            else if (FileInBytes < 1048576)
                strSize = Math.Round((FileInBytes / (double)1024), 2) + " KB"; // Kilobyte
            else if (FileInBytes < 1047527424)
                strSize = Math.Round((FileInBytes / (double)1024) / 1024, 2) + " MB"; // Megabyte
            else if (FileInBytes < 1099511627776)
                strSize = Math.Round(((FileInBytes / (double)1024) / 1024) / 1024, 2) + " GB"; // Gigabyte
            else
                strSize = Math.Round((((FileInBytes / (double)1024) / 1024) / 1024) / 1024, 2) + " TB";// Terabyte

            return strSize;
        }


        public string  File_List_Remote_Computer()
        {
            try
            {
                // I set the Ip address of the remote computer
                string IpAddress = "";
                IpAddress =this.host;

                // I set the credentials of the remote computer.
                string UserName = this.user;
                string UserPassword = this.password;

                // I set the Ftp object.
                System.Net.FtpWebRequest FtpWebRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(IpAddress);
                FtpWebRequest.Credentials = new System.Net.NetworkCredential(UserName, UserPassword);
                FtpWebRequest.Method = System.Net.WebRequestMethods.Ftp.ListDirectory;
                FtpWebRequest.Proxy = null;
                System.Net.FtpWebResponse FtpWebResponse = (System.Net.FtpWebResponse)FtpWebRequest.GetResponse();
                
                // I set the StreamReader object.
                // It will contain the file list of the remote computer
                System.IO.StreamReader StreamReader = new System.IO.StreamReader
                    (FtpWebResponse.GetResponseStream(), System.Text.Encoding.ASCII);

                // I set the DataFound variable.
                bool DataFound = false;

                // I set FileName variable.
                string FileName = "";


                // I loop on the StreamReader object.
                while (StreamReader.EndOfStream == false)
                {
                    System.IO.FileStream sr = new System.IO.FileStream(host + "/" + FileName, System.IO.FileMode.Open);

                    DataFound = true;

                    FileName = StreamReader.ReadLine().ToString();

                   return FileName + "|" + sr.Length;
                }

                // If the value of the DataFound variable is
                // False then no data was found.
                if (DataFound == false)
                   return "No data found.";

                // I close the objects.
                StreamReader.Close();
                FtpWebResponse.Close();
                FtpWebRequest = null;

                return "";
            }
            catch (Exception ex)
            {
                // I show an error message if the sub generates an error.
                if (ex.Message.Contains("(530)"))
                  return "User name or password is wrong.";
                else
                    return "Error message: " + ex.Message;
            }
        }

        public DataTable diretorio_arquivoFTP()
        {
            
            DataTable ret_table = new DataTable();
            ret_table.Columns.Add("nome", typeof(string));
            ret_table.Columns.Add("size", typeof(string));


            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(this.host + "/" + this.diretorio + "/");
            request.Credentials = new NetworkCredential(this.user, this.password);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream());
            string pattern = @"^(\d+-\d+-\d+\s+\d+:\d+(?:AM|PM))\s+(<DIR>|\d+)\s+(.+)$";
            // Dim regex As Regex = New Regex(pattern)
            IFormatProvider culture = CultureInfo.GetCultureInfo("en-us");
          
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string regex = "^" + @"(?<dir>[\-ld])" + @"(?<permission>[\-rwx]{9})" + @"\s+" + @"(?<filecode>\d+)" + @"\s+" + @"(?<owner>\w+)" + @"\s+" + @"(?<group>\w+)" + @"\s+" + @"(?<size>\d+)" + @"\s+" + @"(?<month>\w{3})" + @"\s+" + @"(?<day>\d{1,2})" + @"\s+" + @"(?<timeyear>[\d:]{4,5})" + @"\s+" + "(?<filename>(.*))" + "$";
                var split = new Regex(regex).Match(line);
                string dir = split.Groups["dir"].ToString();
                string filename = split.Groups["filename"].ToString();

                if (split.Groups["size"].ToString() != "0")
                    
                    ret_table.Rows.Add(split.Groups["filename"].ToString(), GetFileSize(Convert.ToInt64(split.Groups["size"].ToString())));
                
            }

            return ret_table;
        }

        public bool DeleteFileFTP(string nome_arq)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(this.host + "/" + this.diretorio + "/" + nome_arq);
                request.Credentials = new NetworkCredential(this.user, this.password);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

     

    }
}
