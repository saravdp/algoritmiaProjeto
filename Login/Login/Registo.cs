using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Registo : Form
    {
        public Registo()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form login = new Login();
            login.Closed += (s, args) => this.Close();
            login.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("Ficheiros de Texto/utilizadores.txt");
            string line = sr.ReadLine();
            int cont = 0;
            while (line != null) 
            {
                string[] parts = line.Split(';');
                if (parts[1]==textBox1.Text)
                {
                    cont++;
                }
                line = sr.ReadLine();
            }
            sr.Close();
            if (cont == 0)
            {
                var lastLine = File.ReadLines("Ficheiros de Texto/utilizadores.txt").Last();
                char[] delimiters = new char[] { ';' };
                string[] parts = lastLine.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                int id = Convert.ToInt16(parts[0]) + 1;
                string user = textBox1.Text;
                string email = textBox3.Text;
                string password = textBox2.Text;
                StreamWriter sw = File.AppendText("Ficheiros de Texto/utilizadores.txt");
                sw.Write("\n" + id + ";" + user + ";" + email + ";" + password + ";" + "docente");
                MessageBox.Show("Registo efetuado com sucesso. \n Inicie sessão com a sua conta");
                sw.Close();
                this.Hide();
                Form login = new Login();
                login.Closed += (s, args) => this.Close();
                login.Show();
            }
            else
            {
                MessageBox.Show("O user já existe!!");
            }
        }
    }
}
