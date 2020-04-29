using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            ConnectString connectString = new ConnectString();
            {
                if(textBox1.Text.Length <= 0)
                {
                    MessageBox.Show("Вы не заполнили: Имя");                   
                }
                else 
                {
                    connectString.Name = textBox1.Text;
                }

                if (textBox2.Text.Length <= 0)
                {
                    MessageBox.Show("Вы не заполнили: Папку пользователя");                    
                }
                else 
                {
                    connectString.UserFolder = textBox2.Text;
                }

                if (textBox3.Text.Length <= 0)
                {
                    MessageBox.Show("Вы не заполнили: Сервер");
                    
                }
                else
                {
                    connectString.SrvAdress = textBox3.Text;
                }


                if (textBox4.Text.Length <= 0)
                {
                    MessageBox.Show("Вы не заполнили: Логин");
                   
                }
                else
                {
                    connectString.Login = textBox4.Text;
                }

                if (textBox5.Text.Length <= 0)
                {
                    MessageBox.Show("Вы не заполнили: Пароль");
                    
                }
                else
                {
                    connectString.Password = textBox5.Text;
                }                
            }


            if (connectString.Name == null || connectString.SrvAdress == null || connectString.UserFolder == null || connectString.Login == null || connectString.Password == null)
            {
                MessageBox.Show("Необходимо заполнить все поля");
            }
            else 
            {
                using (var sw = new StreamWriter("Connect.txt", true))
                {
                    sw.WriteLine(connectString.Name + "~"
                        + connectString.SrvAdress + ";"
                        + connectString.UserFolder + ";"
                        + connectString.Login + ";"
                        + connectString.Password);
                }
                MessageBox.Show("Успешно!");
                Close();

            }
            
        }

       
    }


}   

