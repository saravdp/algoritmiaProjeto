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
    public partial class Lista_Equipamentos : Form
    {

        public Lista_Equipamentos()
        {
            InitializeComponent();
            categorias_list();
            dataGrid_load();
            filters();

            
        }
        private void filters() {
            int a = 0;
            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/categorias.txt");
            //Read the first line of text
            line = sr.ReadLine();
            CheckBox[] box = new CheckBox[20];
            groupBox1.Enabled = true;
            while (line != null)
            {
                char delimiters = ';';
                string[] parts = line.Split(delimiters);
                string RowFilter = string.Empty;
                //---------------------------------------------IMP.FILTERS
             //   CreateOrAppendToFilter(box[a], ref RowFilter);
                
                if (RowFilter.Length > 0)
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        //Check an see what's in the dgv
                        DataView dv = new DataView(dt);
                        dv.RowFilter = RowFilter;
                        dataGridView1.DataSource = dv;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Can’t find the column", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                line = sr.ReadLine();
                a++;
            }
            sr.Close();
            }
                private void CreateOrAppendToFilter(CheckBox cb, ref string RowFilter)
{
    if (RowFilter.Length > 0)
    {
        RowFilter += " OR ";
    }
    RowFilter += (cb.Checked) ? string.Format("[AreaCode] = {0}", cb.Text.Trim()) : string.Empty;
}
private void categorias_list()
        {
            int a = 0;
            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/categorias.txt");
            //Read the first line of text
            line = sr.ReadLine();
            CheckBox[] box = new CheckBox[20];
            groupBox1.Enabled = true;
            while (line != null)
            {
                char delimiters = ';';
                string[] parts = line.Split(delimiters);
                box[a] = new CheckBox();
                box[a].Tag = parts[1].ToString();
                box[a].Text = parts[1];
                box[a].TabIndex = a;
                box[a].AutoSize = true;
                box[a].Location = new Point(10, 20 + a * 20);
                groupBox1.Controls.Add(box[a]);
                line = sr.ReadLine();
                a++;
            }
            sr.Close();

        }
        private void dataGrid_load()
        {
            String line;
             StreamReader sr = new StreamReader("Ficheiros de Texto/equipamentos.txt");
             //Read the first line of text
             line = sr.ReadLine();
            DataTable dt = new DataTable();
            int a = 0;
            char delimiters = ';';
            string[] parts = line.Split(delimiters);
            //CABEÇALHO
            for (int i = 0; i < parts.Length; i++)
                {
                dt.Columns.Add(parts[i]);
                }
            while (line != null)
            {
                parts = line.Split(delimiters);
                if (a != 0)
                {
                    dt.Rows.Add(parts[0], parts[1], parts[2], parts[3], parts[4]);
                }
                line = sr.ReadLine();
                a++;
            }


            //    }

            this.dataGridView1.DataSource = dt;
            //dataGridView1.AllowUserToAddRows = false; //do not show the last line    
               sr.Close();
        }

        private void consultasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form consultarRequisicoes = new Consulta_Requisicoes();
            consultarRequisicoes.Closed += (s, args) => this.Close();
            consultarRequisicoes.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listaEquipamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
       
        private void groupBox1_Enter(object sender, EventArgs e)
        {
            

        }
        private void Form1_Activated(object sender, System.EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Cria ficheiro de texto para o dia se nao existir e escreve o user, data,hora,sala,tipo_objeto 
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (this.dataGridView1.SelectedRows.Count == 1)
                {
                    // get information of 1st column from the row
                    string selectedUser = this.dataGridView1.SelectedRows[0].Cells[0].ToString();
                }
            }
            DateTime dataReq = DateTime.Now;
            MessageBox.Show(dataReq.ToString("d"));//data
            //string[] parts= 
            MessageBox.Show(dataReq.ToString("%h"));//hora 

            user username = new user();
            MessageBox.Show(username.email);



        }
    }
}
