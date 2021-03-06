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
    public partial class Comentarios_admin : Form
    {
        public string username;
        public string userType;
        public string selected;
        public Comentarios_admin()
        {
            InitializeComponent();
            getUsernameAndUserType();
            navBar();
            On_Load();
            dataGrid_load();
        }

        public void getUsernameAndUserType()
        {
            StreamReader sa = File.OpenText("Ficheiros de texto/userLogged"); //get username
            username = sa.ReadLine();
            sa.Close();
            //get usertype
            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/utilizadores.txt");
            //Read the first line of text
            line = sr.ReadLine();
            string[] parts = line.Split(';');
            while (line != null)
            {
                parts = line.Split(';');
                if (parts[1] == username)
                {
                    userType = parts[4];
                }
                line = sr.ReadLine();
            }
            sr.Close();
        }

        public void navBar()
        {
            //GESTAO DE PERFIS

            if (userType == "admin")
            {
                comentáriosToolStripMenuItem.Visible = false;
                devoluçõesToolStripMenuItem.Visible = false;
            }
            else if (userType == "seguranca")
            {
                comentáriosToolStripMenuItem.Visible = false;
                criarNovoUtilizadorToolStripMenuItem.Visible = false;
            }
        }

        private void On_Load()
        {
            //  groupBox2.Enabled = false;
            //Não permite editar visualmente as DataGrids
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void dataGrid_load()
        {
            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/comentarios.txt");
            //Read the first line of text
            line = sr.ReadLine();
            DataTable dt = new DataTable();
            int a = 0;
            char delimiters = ';';
            string[] parts = line.Split(delimiters);
            //CABEÇALHO

            if (userType == "admin" || userType == "seguranca")
            {
                for (int i = 0; i < parts.Length; i++)
                {
                    dt.Columns.Add(parts[i]);
                }
            }
            if ( userType == "docente")
            {
                for (int i = 1; i < parts.Length; i++)
                {
                    dt.Columns.Add(parts[i]);
                }
            }
            while (line != null && line != "")
            {
                parts = line.Split(delimiters);
                if (a != 0)
                {
                    string estadoResposta = "";
                    string resposta = parts[5];
                    if (parts[4] == "1")
                    {
                        estadoResposta = "Respondido";
                    }
                    else if (parts[4] == "0")
                    {
                        estadoResposta = "Por Responder";
                    }
                    if (parts[5] == "0")
                    {
                        resposta = "";
                    }
                    if (userType == "admin" || userType == "seguranca")
                    {
                        dt.Rows.Add(parts[0],parts[1], parts[2], parts[3], estadoResposta, resposta);
                    }
                    else
                    {
                        if (username == parts[1])
                        {
                            dt.Rows.Add(parts[1], parts[2], parts[3], estadoResposta, resposta);
                        }
                    }
                }
                line = sr.ReadLine();
                a++;
            }
            //    }
            this.dataGridView1.DataSource = dt;
            //dataGridView1.AllowUserToAddRows = false; //do not show the last line    
            sr.Close();
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            string text = radioButton3.Text.ToString();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (this.dataGridView1.SelectedRows.Count == 1)
                {
                    // get information of 1st column from the row
                    string selected = this.dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                }
            }
            if (selected != "")
            {
                if (radioButton3.Checked == true)
                {
                    string value = dataGridView1.SelectedRows[Convert.ToInt16(selected)].Cells[0].Value.ToString();
                    //procura id no ficheiro de texto
                    String line;
                    StreamReader sr = new StreamReader("Ficheiros de Texto/comentarios.txt");
                    //Read the first line of text
                    line = sr.ReadLine();
                    string[] parts = line.Split(';');
                    int cont = 0;
                    int linha = -1;
                    string linhaAntiga = "";
                    string linhaSaved = "";
                    while (line != null)
                    {
                        parts = line.Split(';');
                        if (parts[0] == value)
                        {
                            linha = cont;
                            linhaSaved = parts[0] + ";" + parts[1] + ";" + parts[2] + ";" + parts[3] + ";1;" + text;
                            linhaAntiga = line;
                        }
                        line = sr.ReadLine();
                        cont++;
                    }
                    sr.Close();
                    string[] lines1 = File.ReadAllLines("Ficheiros de Texto/comentarios.txt");
                    lines1[linha] = linhaSaved;
                    File.WriteAllLines("Ficheiros de Texto/comentarios.txt", lines1);
                    MessageBox.Show("Estado Alterado!");
                    this.Hide();
                    Form comentarios_admin = new Comentarios_admin();
                    comentarios_admin.Closed += (s, args) => this.Close();
                    comentarios_admin.Show();
                }
            }
            else
            {
                MessageBox.Show("É obrigatório escolher uma linha!");
            }

        }

        private void listaEquipamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form lista_equipamentos = new Lista_Equipamentos();
            lista_equipamentos.Closed += (s, args) => this.Close();
            lista_equipamentos.Show();
        }

        private void consultasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form Consultar_Requisicoes = new Consulta_Requisicoes();
            Consultar_Requisicoes.Closed += (s, args) => this.Close();
            Consultar_Requisicoes.Show();
        }

        private void inserirNovoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form novo_comentario = new Novo_Comentario();
            novo_comentario.Closed += (s, args) => this.Close();
            novo_comentario.Show();
        }

        private void históricoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form historico_comentarios = new Historico_Comentarios();
            historico_comentarios.Closed += (s, args) => this.Close();
            historico_comentarios.Show();
        }

        private void comentáriosAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form gestao_salas = new Gestao_Salas();
            gestao_salas.Closed += (s, args) => this.Close();
            gestao_salas.Show();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form gestao_categorias = new Gestao_Categorias();
            gestao_categorias.Closed += (s, args) => this.Close();
            gestao_categorias.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dataGridView1.DataSource;

                string filter = "";
                int tamanho = Convert.ToInt16(filter.Length.ToString());
                // Check if text fields are not null before adding to filter. 

                if (!string.IsNullOrEmpty(radioButton4.Text))
                {
                    filter += dataGridView1.Columns["Estado_do_Comentario"].HeaderText.ToString() + " LIKE '%" + "Respondido" + "%' ";

                }
                bs.Filter = filter;
                dataGridView1.DataSource = bs;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            string text = radioButton1.Text.ToString();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (this.dataGridView1.SelectedRows.Count == 1)
                {
                    // get information of 1st column from the row
                    string selected = this.dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                }
            }
            if (selected != "")
            {
                if (radioButton1.Checked == true)
                {
                    MessageBox.Show("a"+selected+"a");
                    string value = dataGridView1.SelectedRows[Convert.ToInt16(selected)].Cells[0].Value.ToString();
                    //procura id no ficheiro de texto
                    String line;
                    StreamReader sr = new StreamReader("Ficheiros de Texto/comentarios.txt");
                    //Read the first line of text
                    line = sr.ReadLine();
                    string[] parts = line.Split(';');
                    int cont = 0;
                    int linha = -1;
                    string linhaAntiga = "";
                    string linhaSaved = "";
                    while (line != null)
                    {
                        parts = line.Split(';');
                        if (parts[0] == value)
                        {
                            linha = cont;
                            linhaSaved = parts[0] + ";" + parts[1] + ";" + parts[2] + ";" + parts[3] + ";1;" + text;
                            linhaAntiga = line;
                        }
                        line = sr.ReadLine();
                        cont++;
                    }
                    sr.Close();
                    string[] lines1 = File.ReadAllLines("Ficheiros de Texto/comentarios.txt");
                    lines1[linha] = linhaSaved;
                    File.WriteAllLines("Ficheiros de Texto/comentarios.txt", lines1);
                    MessageBox.Show("Estado Alterado!");
                    this.Hide();
                    Form comentarios_admin = new Comentarios_admin();
                    comentarios_admin.Closed += (s, args) => this.Close();
                    comentarios_admin.Show();
                }
            }
            else
            {
                MessageBox.Show("É obrigatório escolher uma linha!");
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            string text = radioButton2.Text.ToString();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (this.dataGridView1.SelectedRows.Count == 1)
                {
                    // get information of 1st column from the row
                    string selected = this.dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                }
            }
            if (selected != "")
            {
                if (radioButton2.Checked == true)
                {
                    string value = dataGridView1.SelectedRows[Convert.ToInt16(selected)].Cells[0].Value.ToString();
                    //procura id no ficheiro de texto
                    String line;
                    StreamReader sr = new StreamReader("Ficheiros de Texto/comentarios.txt");
                    //Read the first line of text
                    line = sr.ReadLine();
                    string[] parts = line.Split(';');
                    int cont = 0;
                    int linha = -1;
                    string linhaAntiga = "";
                    string linhaSaved = "";
                    while (line != null)
                    {
                        parts = line.Split(';');
                        if (parts[0] == value)
                        {
                            linha = cont;
                            linhaSaved = parts[0] + ";" + parts[1] + ";" + parts[2] + ";" + parts[3] + ";1;" + text;
                            linhaAntiga = line;
                        }
                        line = sr.ReadLine();
                        cont++;
                    }
                    sr.Close();
                    string[] lines1 = File.ReadAllLines("Ficheiros de Texto/comentarios.txt");
                    lines1[linha] = linhaSaved;
                    File.WriteAllLines("Ficheiros de Texto/comentarios.txt", lines1);
                    MessageBox.Show("Estado Alterado!");
                    this.Hide();
                    Form comentarios_admin = new Comentarios_admin();
                    comentarios_admin.Closed += (s, args) => this.Close();
                    comentarios_admin.Show();
                }
            }
            else
            {
                MessageBox.Show("É obrigatório escolher uma linha!");
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form login = new Login();
            login.Closed += (s, args) => this.Close();
            login.Show();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dataGridView1.DataSource;

                string filter = "";
                int tamanho = Convert.ToInt16(filter.Length.ToString());
                // Check if text fields are not null before adding to filter. 

                if (!string.IsNullOrEmpty(radioButton5.Text))
                {
                    filter += dataGridView1.Columns["Estado_do_Comentario"].HeaderText.ToString() + " LIKE '%" + "Por Responder" + "%' ";

                }
                bs.Filter = filter;
                dataGridView1.DataSource = bs;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form comentarios_admin = new Comentarios_admin();
            comentarios_admin.Closed += (s, args) => this.Close();
            comentarios_admin.Show();
        }

        private void devoluçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form devolucoes = new Devolucoes();
            devolucoes.Closed += (s, args) => this.Close();
            devolucoes.Show();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void criarNovoUtilizadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form criarNovoUtilizador = new Criar_novo_utilizador();
            criarNovoUtilizador.Closed += (s, args) => this.Close();
            criarNovoUtilizador.Show();
        }
    }
}
