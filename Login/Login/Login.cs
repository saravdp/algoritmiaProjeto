﻿using System;
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
    public partial class Login : Form
    {
        public string username;
        public string password;
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
            //Registo --> StreamWriter sw = new StreamWriter("Ficheiros de Texto/utilizadores.txt");
            StreamReader sr = new StreamReader("Ficheiros de Texto/utilizadores.txt");
            //Read the first line of text
            line = sr.ReadLine();

            //Continue to read until you reach end of file
            while (line != null)
            {
                username = textBox1.Text;
                password = textBox2.Text;
                char[] delimiters = new char[] {';'};
                string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                if (parts[1]==username)
                {
                    if (parts[2] == password)
                    {
                        Console.WriteLine("LOGIN");
                        //se login == successful
                        this.Hide();
                        Form home = new Lista_Equipamentos();
                        home.Closed += (s, args) => this.Close();
                        home.Show();
                    }
                }
                //write the lie to console window
                //Console.WriteLine(line);
                //Read the next line
                line = sr.ReadLine();
            }

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
