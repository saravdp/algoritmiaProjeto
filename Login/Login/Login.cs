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
using System.Security.Principal;

namespace Login
{



    public partial class Login : Form
    {
        public string username;
        public string password;
        user utilizador = new user();
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/utilizadores.txt");
            //Read the first line of text
            line = sr.ReadLine();

            //Continue to read until you reach end of file
            while (line != null)
            {
                username = textBox1.Text;
                password = textBox2.Text;
                char delimiters = ';';
                string[] parts = line.Split(delimiters);
               
                if (parts[1]==username)
                {
                    if (parts[3] == password)
                    {
                        //se login == successful
                        this.Hide();
                        Form home = new Lista_Equipamentos();
                        home.Closed += (s, args) => this.Close();
                        home.Show();
                        utilizador.setNome(username);
                        MessageBox.Show( "TOKEN \t" + WindowsIdentity.GetCurrent().Token.ToString());
                        StreamWriter sw =  File.CreateText("Ficheiros de Texto/userLogged");
                        sw.WriteLine(username);
                        sw.Close();
                       MessageBox.Show("Bem vindo: "+ utilizador.getNome());
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Password Errada");

                    }
                }

                //write the lie to console window
                //Console.WriteLine(line);
                //Read the next line
                line = sr.ReadLine();
            }
           /* {
                MessageBox.Show("O user não existe");
            }*/

            //close the file
            sr.Close();




            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form registo = new Registo();
            registo.Closed += (s, args) => this.Close();
            registo.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
