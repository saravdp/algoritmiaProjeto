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
                    if (userType == "admin"||userType== "seguranca")
                    {
                        for (int i = 0; i < parts.Length; i++)
                        {

                            dt.Columns.Add(parts[i]);

                        }
                    }
                    else
                    {
                        for (int i = 2; i < parts.Length; i++)
                        {

                            dt.Columns.Add(parts[i]);

                        }
                    }
                    cont++;
                }
            while (line != null)
            {
                parts = line.Split(delimiters);
                if (a != 0)
                {
                       
                            if (userType == "admin" || userType == "seguranca")
                            {
                                dt.Rows.Add(parts[0],parts[1],parts[2], parts[3], parts[4], parts[5], parts[6]);
                            }
                            else
                            {
                                if (parts[1] == username)
                                {
                                    dt.Rows.Add(parts[2], parts[3], parts[4], parts[5], parts[6]);
                                }
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
    }
}
