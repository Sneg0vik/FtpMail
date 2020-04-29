using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using BytesRoad.Net.Ftp;
using System.IO.Compression;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public ListBox listBox = new ListBox();
        public Dictionary<string, string> dicPath = new Dictionary<string, string>();
        readonly FtpClient client = new FtpClient();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Directory.Exists("NullZip"))
            {
            }
            else
            {
                Directory.CreateDirectory("NullZip");
            }
            if (File.Exists("stop.txt")) 
            { 
            }
            else
            {
                File.Create("stop.txt");
            }  
            Update2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.ShowDialog();
            dicPath.Clear();
            Update2();
            Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.ShowDialog();
            foreach (var item in fileDialog.FileNames)
            {
                listBox.Items.Add(item);
                if (listBox1.Items.Contains(item))
                {
                    MessageBox.Show("Данный файл уже выбран: " + item);
                }
                else
                {
                    listBox1.Items.Add(item);
                }
            }

        }
        private void Update2()
        {
            try
            {
                using (StreamReader sr = new StreamReader("Connect.txt"))
                {

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        dicPath.Add(line.Substring(line.LastIndexOf(@"~") + 1), line.Substring(0, line.IndexOf('~')));
                    }
                    checkedListBox1.DataSource = new BindingSource(dicPath, null);
                    checkedListBox1.DisplayMember = "Value";
                    checkedListBox1.ValueMember = "Key";
                }
            }
            catch
            {
                MessageBox.Show("Необходимо добавить получателей");                
            }

        }
        private void zipFile()
        {
            var list = listBox1.Items;
            string startPath = @"NullZip";
            string zipPath = @"result.zip";
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
                ZipFile.CreateFromDirectory(startPath, zipPath);
            }
            else
            {
                ZipFile.CreateFromDirectory(startPath, zipPath);
            }
            foreach (var item in list)
            {
                string PathFile = Path.Combine(Path.GetDirectoryName(item.ToString()), Path.GetFileName(item.ToString()));
                using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Open))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                    {
                        ZipArchiveEntry readmeEntry = archive.CreateEntryFromFile(PathFile, Path.GetFileName(item.ToString()));
                    }
                }
            }
        }
       async private void button3_Click(object sender, EventArgs e)
       {
            button3.Enabled = false;
            pictureBox1.Image = Image.FromFile("383.gif");            
            button3.Text = "Ждите.";           
            await Task.Run(() =>
            {
                zipFile();
                string zipPath = @"result.zip";
                foreach (var selected in checkedListBox1.CheckedItems)
                {
                    KeyValuePair<string, string> keyValuePair = (KeyValuePair<string, string>)Convert.ChangeType(selected, typeof(KeyValuePair<string, string>));
                    string PathFile = Path.Combine(Path.GetDirectoryName(zipPath), Path.GetFileName(zipPath));
                    string[] PathFtp = keyValuePair.Key.Split(';');
                    string FtpServer = PathFtp[0];
                    int Timeout = 300;
                    string Username = PathFtp[2];
                    string Password = PathFtp[3];
                    client.PassiveMode = true;
                    client.Connect(Timeout, FtpServer, 21);
                    client.Login(Timeout, Username, Password);
                    client.ChangeDirectory(30, PathFtp[1]);
                    client.PutFile(90, "stop.txt", "stop.txt");
                    client.PutFile(3000000, Path.GetFileName(zipPath), PathFile);
                    client.DeleteFile(90, "stop.txt");
                    client.Disconnect(Timeout);
                }                
                File.Delete(zipPath);
            });
            pictureBox1.Image = null;
            button3.Enabled = true;
            button3.Text = "Отправить.";
       }

        
    } 
}

        

        
    
