using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    
    public partial class Form2 : Form
    {
        //private Form1 rParentForm = null;
        public Form2(/*Form1 rParent*/)
        {
            InitializeComponent();

            //rParentForm = rParent;
            //Console.WriteLine($"First value: {rParentForm.checkedListBox1.Items[0].ToString()}");
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            newForm.ShowDialog();
            listBox1.Items.Clear();
            setting1();
            Show();

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            setting1();
        }
        public void setting1() 
        {
            try
            {
                using (StreamReader sr = new StreamReader("Connect.txt"))
                {

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Необходимо добавить получателей");               
            }
        }
        

      
    }
}
