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
    public partial class Historico_Comentarios : Form
    {
        public string username;
        public string userType;
        public Historico_Comentarios()
        {
            InitializeComponent();
            getUsernameAndUserType();
            navBar();
            On_Load();
            
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
            //TO DO
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/comentarios.txt");
            //Read the first line of text
            line = sr.ReadLine();
            int a = 0;
            DataTable dt = new DataTable();
            string[] parts = line.Split(';');
            //CABEÇALHO
            for (int i = 2; i < parts.Length; i++)
            {
                dt.Columns.Add(parts[i]);
            }
            while (line != null)
            {
                string estadoResposta = "";
                string resposta = "";
                parts = line.Split(';');
                if (a != 0)
                {
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
                    else
                    {
                        resposta = parts[5];
                    } 

                    if (username==parts[1]) {
                        dt.Rows.Add(parts[2], parts[3], estadoResposta, resposta);
                    }
                }
                line = sr.ReadLine();
                a++;
            }


            //    }

            this.dataGridView1.DataSource = dt;
            //dataGridView1.AllowUserToAddRows = false; //do not show the last line    
            sr.Close();

            //Não permite editar visualmente as DataGrids
            if (userType == "docente")
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
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
            
        }
        public void navBar()
        {
            //GESTAO DE PERFIS
            if (userType == "docente")
            {
                comentáriosAdminToolStripMenuItem.Visible=false;
                gestãoDeSalasToolStripMenuItem.Visible = false; 
            }
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {//Checkbox filters
            if (radioButton1.Checked == true)
            {
                String line;
                StreamReader sr = new StreamReader("Ficheiros de Texto/comentarios.txt");
                //Read the first line of text
                line = sr.ReadLine();
                DataTable dt = new DataTable();
                BindingSource bs = new BindingSource();
                bs.DataSource = dataGridView1.DataSource;
                int cont = 0;
                while (line != null)
                {
                    string[] parts = line.Split(';');
                    string value = "0";
                    MessageBox.Show("");
                     bs.Filter = "[Estado do Comentario] Like '%"+value+"%'";

                   // bs.Filter = "Estado do Comentario Like value";
                    dataGridView1.DataSource = bs;
                    line = sr.ReadLine();
                    cont++;

                }
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
          /*  DataTable dt = new DataTable();
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            string value = "0";
            bs.Filter = "[Estado do Comentario] Like '%" + value+ "%'";
            dataGridView1.DataSource = bs;*/
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
