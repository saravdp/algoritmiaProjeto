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
    public partial class Consulta_Requisicoes : Form
    {
        public string username;
        public string userType;
        public Consulta_Requisicoes()
        {
            InitializeComponent();
            getUsernameAndUserType();
            navBar();
            On_Load();
            LoadDataGrid();
            dateTimePicker1.Enabled = false;
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
            if (userType == "docente")
            {
                comentáriosAdminToolStripMenuItem.Visible = false;
                gestãoDeSalasToolStripMenuItem.Visible = false;
            }

        }

        private void On_Load()
        {
            //Não permite editar visualmente as DataGrids
            if (userType == "docente")
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            StreamReader sr = new StreamReader("Ficheiros de Texto/salas.txt");
            string line = sr.ReadLine();
            while (line != null)
            {
                string[] parts = line.Split(';');
                comboBox2.Items.Add(parts[1]);

                line = sr.ReadLine();
            }
            sr.Close();
            StreamReader sa = new StreamReader("Ficheiros de Texto/equipamentos.txt");
            line = sa.ReadLine();
            while (line != null)
            {
                string[] parts = line.Split(';');
                comboBox1.Items.Add(parts[1]);

                line = sa.ReadLine();
            }
            sa.Close();
            StreamReader so = new StreamReader("Ficheiros de Texto/utilizadores.txt");
            line = so.ReadLine();
            while (line != null)
            {
                string[] parts = line.Split(';');
                if (parts[4] == "docente")
                {
                    comboBox3.Items.Add(parts[1]);
                }

                line = so.ReadLine();
            }
            so.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            this.Hide();
            Form comentarios_admin = new Comentarios_admin();
            comentarios_admin.Closed += (s, args) => this.Close();
            comentarios_admin.Show();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void LoadDataGrid()
        {
            DataTable dt = new DataTable();
            int cont = 0;
            //Foreach file in directory 
            string[] fileEntries = Directory.GetFiles("Ficheiros de Texto/Requisicoes");
            foreach (string fileName in fileEntries)
            {
                String line;
                StreamReader sr = new StreamReader(fileName);
                //Read the first line of text
                line = sr.ReadLine();
                int a = 0;
                char delimiters = ';';
                string[] parts = line.Split(delimiters);
                //CABEÇALHO
                if (cont == 0)
                {
                    // if (userType == "admin" || userType == "seguranca")
                    //  {
                    for (int i = 0; i < parts.Length; i++)
                    {
                        dt.Columns.Add(parts[i]);
                    }
                    /*   }
                       else
                       {
                           for (int i = 2; i < parts.Length; i++)
                           {
                               dt.Columns.Add(parts[i]);
                           }
                       }*/
                    cont++;
                }
                while (line != null)
                {
                    if (line != "")
                    {
                        parts = line.Split(delimiters);
                        if (a != 0)
                        {
                            string equipamento = "";
                            StreamReader sa = new StreamReader("Ficheiros de Texto/equipamentos.txt");
                            string lines = sa.ReadLine();
                            while (lines != null)
                            {
                                string[] part = lines.Split(';');
                                if (part[0] == parts[5])
                                {
                                    equipamento = part[1];


                                    // if (userType == "admin" || userType == "seguranca")
                                    //  {
                                    dt.Rows.Add(parts[0], parts[1], parts[2], parts[3], parts[4], equipamento, parts[6], parts[7]);
                                    /*   }
                                       else
                                       {
                                           if (parts[1] == username)
                                           {*/
                                    //     dt.Rows.Add(parts[2], parts[3], parts[4], equipamento, parts[6], parts[7]);
                                    //   }
                                    //  }
                                }
                                lines = sa.ReadLine();
                            }
                            sa.Close();
                        }
                    }
                    line = sr.ReadLine();
                    a++;
                }
                //    }
                this.dataGridView1.DataSource = dt;
                sr.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form login = new Login();
            login.Closed += (s, args) => this.Close();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int contador = 0;
            if (contador < 0)
            {
                LoadDataGrid();
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;

            string filter = "";
            int tamanho = Convert.ToInt16(filter.Length.ToString());
            // Check if text fields are not null before adding to filter. 

            if (!string.IsNullOrEmpty(comboBox2.Text))
            {
                filter += dataGridView1.Columns["Sala"].HeaderText.ToString() + " LIKE '%" + comboBox2.Text + "%' ";               
            }
            if (!string.IsNullOrEmpty(dateTimePicker1.Text))
            {
                if (radioButton1.Checked == true)
                {
                    tamanho = Convert.ToInt16(filter.Length.ToString());
                if (tamanho > 0) filter += "AND ";
               
                    filter += dataGridView1.Columns["Dia_de_requisicao"].HeaderText.ToString() + " LIKE '%" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "%' ";
                }
            }
            if (!string.IsNullOrEmpty(dateTimePicker1.Text))
            {
                if (radioButton2.Checked == true)
                {
                    tamanho = Convert.ToInt16(filter.Length.ToString());
                    if (tamanho > 0) filter += "AND ";
                   // filter += dataGridView1.Columns["Data_de_devolucao"].HeaderText.ToString() + " LIKE '%" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "%' ";
                    filter += dataGridView1.Columns["Data_de_devolucao"].HeaderText.ToString() + " LIKE '%" + dateTimePicker1.Value.ToString("dd-MM-yy") + "%' ";
                    MessageBox.Show(filter);
                }
            }
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                tamanho = Convert.ToInt16(filter.Length.ToString());
                if (tamanho > 0) filter += "AND ";
                filter += dataGridView1.Columns["Equipamento"].HeaderText.ToString() + " LIKE '%" + comboBox1.Text + "%' ";
            }
            if (!string.IsNullOrEmpty(comboBox3.Text))
            {
                tamanho = Convert.ToInt16(filter.Length.ToString());
                if (tamanho > 0) filter += "AND ";
                filter += dataGridView1.Columns["User"].HeaderText.ToString() + " LIKE '%" + comboBox3.Text + "%' ";
            }
            MessageBox.Show(filter);
            contador++;
            bs.Filter = filter;
            dataGridView1.DataSource = bs;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form consulta_Requisicoes = new Consulta_Requisicoes();
            consulta_Requisicoes.Closed += (s, args) => this.Close();
            consulta_Requisicoes.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                dateTimePicker1.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                dateTimePicker1.Enabled = true;
            }
        }

        private void devoluçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form devolucoes = new Devolucoes();
            devolucoes.Closed += (s, args) => this.Close();
            devolucoes.Show();
        }
    }
}
