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
                consultasToolStripMenuItem.Visible = false;
                comentáriosToolStripMenuItem.Visible = false;

            }
            else if (userType == "seguranca")
            {
                comentáriosToolStripMenuItem.Visible = false;
                consultasToolStripMenuItem.Visible = false;
                gestãoDeSalasToolStripMenuItem.Visible = false;
            }
        }

        private void On_Load()
        {
            //  groupBox2.Enabled = false;
            //Não permite editar visualmente as DataGrids
            if (userType == "docente")
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
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
            for (int i = 0; i < parts.Length; i++)
            {
                dt.Columns.Add(parts[i]);
            }
            while (line != null)
            {
                parts = line.Split(delimiters);
                if (a != 0)
                {
                    dt.Rows.Add(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]);
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
                if (radioButton1.Checked == true)
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
                if (radioButton1.Checked == true)
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
                }
            }
            else
            {
                MessageBox.Show("É obrigatório escolher uma linha!");
            }

        }
    }
}
