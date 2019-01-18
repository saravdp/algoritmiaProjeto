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
    public partial class Lista_Equipamentos : Form
    {
        public string username;
        public string userType;

        CheckBox[] box = new CheckBox[20] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };

        public Lista_Equipamentos()
        {

            InitializeComponent();
            getUsernameAndUserType();
            On_Load();
            navBar();
            Categorias_list();
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

        private void On_Load()
        {
            String line;
            int cont = 0;
            StreamReader sr = new StreamReader("Ficheiros de Texto/salas.txt");
            line = sr.ReadLine();
            while (line != null)
            {
                if (cont == 0)
                {
                    cont++;
                }
                if (cont != 1)
                {
                    string[] parts = line.Split(';');
                    comboBox1.Items.Add(parts[1]);

                }

                cont++;
                line = sr.ReadLine();
            }
            sr.Close();

            //Não permite editar visualmente as DataGrids
            if (userType == "docente")
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
        }

        public void navBar()
        {
            //GESTAO DE PERFIS
            if (userType == "docente")
            {
                comentáriosAdminToolStripMenuItem.Visible = false;
                gestãoDeSalasToolStripMenuItem.Visible = false;
            }
            else if (userType == "admin")
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




        private void Categorias_list()
        {
            int a = 0;
            int cont = 0;
            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/categorias.txt");
            //Read the first line of text
            line = sr.ReadLine();
            groupBox1.Enabled = true;
            while (line != null)
            {
                if (cont != 0)
                {
                    string[] parts = line.Split(';');
                    box[a] = new CheckBox();
                    box[a].Tag = parts[1].ToString();
                    box[a].Text = parts[1];
                    box[a].TabIndex = a;
                    box[a].AutoSize = true;
                    box[a].Location = new Point(10, 20 + a * 20);
                    groupBox1.Controls.Add(box[a]);
                    a++;
                }
                line = sr.ReadLine();
                cont++;
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
            DateTime dataReq = DateTime.Now;
            string nameFile = "Ficheiros de Texto/Requisicoes/R_" + dataReq.ToString("ddMMyy");
            string[] parts;
            string idEquipamento = "";
            int id = 0;//= Convert.ToInt16(parts[0]);
            if (comboBox1.Text != "")
            {//Cria ficheiro de texto para o dia se nao existir e escreve o user, data,hora,sala,tipo_objeto 
                StreamWriter sw;
                StreamReader sr = File.OpenText("Ficheiros de texto/userLogged");
                String.Format("{0:MM dd yy}", dataReq);
                string categoria = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (this.dataGridView1.SelectedRows.Count == 1)
                    {

                        // get information of 1st column from the row
                        string selected = this.dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                        categoria = selected;
                        idEquipamento = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                    }
                }
                if (this.dataGridView1.SelectedRows[0].Cells[4].Value.ToString() == "disponivel" && this.dataGridView1.SelectedRows[0].Cells[3].Value.ToString() != "0")
                {
                    if (File.Exists(nameFile))
                    {
                        var lastLine = File.ReadLines(nameFile).Last();
                        parts = lastLine.Split(';');
                        id = Convert.ToInt16(parts[0]) + 1;
                        sw = File.AppendText(nameFile);
                    }
                    else
                    {
                        sw = File.CreateText(nameFile);
                    }
                    string user = sr.ReadLine();
                    sr.Close();
                    string data = dataReq.ToString("dd-MM-yyyy"); ;
                    string hora = String.Format("{0:t}", dataReq).ToString();
                    string sala = comboBox1.Text;
                    string line;
                    string cat = "";


                    StreamReader file = new StreamReader("Ficheiros de texto/categorias.txt");
                    while ((line = file.ReadLine()) != null)
                    {
                        parts = line.Split(';');


                        if (parts[1] == categoria)
                        {

                            cat = parts[0];
                            break;
                        }

                    }

                    string idObjeto = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    id = id + 1;
                    // MessageBox.Show("Resumo de requisição:  \n Sala: {0} \n ");
                    sw.WriteLine("\n" + id + ";" + user + ";" + data + ";" + hora + ";" + sala + ";" + idObjeto + "; ; ");
                    //BAIXAR STOCK----------------------------------------------------------
                    string fileName = "Ficheiros de Texto/equipamentos.txt";
                    StreamReader reader = new StreamReader(fileName);
                    //Read the first line of text
                    line = reader.ReadLine();
                    int a = 0;
                    char delimiters = ';';
                    string linhaSaved = "";
                    int linhaAlterar = -1;
                    parts = line.Split(delimiters);
                    while (line != null)
                    {
                        parts = line.Split(delimiters);
                        if (parts[0] == idEquipamento)
                        {
                            linhaAlterar = a;
                            int stock = Convert.ToInt16(parts[3]) - 1;
                            linhaSaved = parts[0] + ";" + parts[1] + ";" + parts[2] + ";" + stock.ToString() + ";" + parts[4];
                            MessageBox.Show(linhaSaved);
                        }
                        line = reader.ReadLine();
                        a++;
                    }
                    reader.Close();
                    string[] lines1 = File.ReadAllLines(fileName);
                    lines1[linhaAlterar] = linhaSaved;
                    File.WriteAllLines(fileName, lines1);
                    var lines = File.ReadAllLines(fileName).Where(arg => !string.IsNullOrWhiteSpace(arg));
                    File.WriteAllLines(fileName, lines);



                    MessageBox.Show("Requisitado!");
                    sw.Close();
                    this.Hide();
                    Form lista_Equipamento = new Lista_Equipamentos();
                    lista_Equipamento.Closed += (s, args) => this.Close();
                    lista_Equipamento.Show();
                }
                else { MessageBox.Show("Equipamento Indisponivel de momento"); }
            }
            else
            {
                MessageBox.Show("É obrigatório escolher uma sala!");
            }
        }

        private void comentáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void comentáriosAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form comentarios_admin = new Comentarios_admin();
            comentarios_admin.Closed += (s, args) => this.Close();
            comentarios_admin.Show();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] cats = new string[20];
            int i = 0;
            int a = 0;
            while (box[a] != null)
            {
                if (box[a].CheckState.ToString() == "Checked")
                {
                    cats[i] = box[a].Text;
                    i++;
                }
                //MessageBox.Show(box[a].CheckState.ToString());
                a++;
            }
            string filtro = "";
            
            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/equipamentos.txt");
            //Read the first line of text
            line = sr.ReadLine();
            int cont = 0;
            int counter = 1;
            DataTable dt = new DataTable();
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            string[] parts = line.Split(';');
            
                while (cats[cont] != null)//Compara cada linha com o array das categorias selecionadas
                {
                // filtro += "[Categoria] Like '%" + cats[cont] + "%' ";
                filtro += dataGridView1.Columns["Categoria"].HeaderText.ToString() + " LIKE '%" + cats[cont] + "%' ";

                cont++;
                }
            MessageBox.Show(filtro + " FILTRO");
            bs.Filter = filtro;
            dataGridView1.DataSource = bs;


            /*   //Inserir dentro do array das checkboxes
            
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            string filter = "";
            int tamanho = Convert.ToInt16(filter.Length.ToString());
            // Check if text fields are not null before adding to filter. 
            for (int j = 0; j < box.Length; j++)
            {
                if (box[j].Checked == true)
                {
                    for (int q = 0; q < cats.Length; q++)
                    {
                        if (q == 0)
                        {
                            if (!string.IsNullOrEmpty(cats[i]))
                            {
                                filter += dataGridView1.Columns["Sala"].HeaderText.ToString() + " LIKE '%" + cats[q] + "%' ";
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(cats[q]))
                            {
                                //if (cats[i] == true)
                                // {
                                tamanho = Convert.ToInt16(filter.Length.ToString());
                                if (cats.Length > 0) filter += "OR ";

                                filter += dataGridView1.Columns["Dia_de_requisicao"].HeaderText.ToString() + " LIKE '%" + cats[q] + "%' ";
                                //  }
                            }
                        }
                    }
                }
            }
            bs.Filter = filter;
            dataGridView1.DataSource = bs;*/

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void devoluçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form devolucoes = new Devolucoes();
            devolucoes.Closed += (s, args) => this.Close();
            devolucoes.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form login = new Login();
            login.Closed += (s, args) => this.Close();
            login.Show();
        }
    }
}
