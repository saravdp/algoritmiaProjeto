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
    public partial class Novo_Comentario : Form
    {
        public string username;
        public string userType;
        string user = "";
        string idreq = "";


        


        public Novo_Comentario()
        {
            InitializeComponent();
            getUsernameAndUserType();
            navBar();
            On_load();
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
                devoluçõesToolStripMenuItem.Visible = false;
                criarNovoUtilizadorToolStripMenuItem.Visible = false;
            }

        }

        public void On_load()
        {
            StreamReader sr = File.OpenText("Ficheiros de texto/userLogged");
            user = sr.ReadLine();
            sr.Close();
            try
            {
                string[] dirs = Directory.GetFiles(@"Ficheiros de Texto\Requisicoes");
                foreach (string dir in dirs)
                {
                    String line;
                    StreamReader sa = new StreamReader(dir);
                    //Read the first line of text
                    line = sa.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        string[] parts = line.Split(';');

                        if (parts[1] == user)
                        {
                            DateTime a = Convert.ToDateTime(parts[2]);
                            comboBox1.Items.Add(a.ToString("dd-MM-yy")  + " às " + parts[3] + " com o id: " + parts[0]);
                            idreq = parts[0];
                        }
                        line = sa.ReadLine();
                    }
                    sa.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
        private void Comentarios_Load(object sender, EventArgs e)
        {

        }

        private void hitóricoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form historico_comentarios = new Historico_Comentarios();
            historico_comentarios.Closed += (s, args) => this.Close();
            historico_comentarios.Show();
        }

        private void inserirNovoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gestãoDeSalasToolStripMenuItem_Click(object sender, EventArgs e)
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
            this.Hide();
            Form Consultar_Requisicoes = new Consulta_Requisicoes();
            Consultar_Requisicoes.Closed += (s, args) => this.Close();
            Consultar_Requisicoes.Show();
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

        private void button1_Click(object sender, EventArgs e)
        {
            //Novo comentário
            var lastLine = File.ReadLines("Ficheiros de texto/comentarios.txt").Last();
            string[] parts = lastLine.Split(';');
            int idComent = Convert.ToInt16(parts[0]) + 1;
            string line = comboBox1.Text;
            parts = line.Split(' ');
            idreq =parts[parts.Length-1];
           // string fileName = "R_" + parts[0];
            StreamWriter sw;
            sw = File.AppendText("Ficheiros de texto/comentarios.txt");
            sw.WriteLine("\n"+idComent + ";" + user + ";" + idreq + ";"+richTextBox1.Text+";0;0");
            sw.Close();
            richTextBox1.Text = "";
            comboBox1.Text = "";
            MessageBox.Show("Submetido");

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form login = new Login();
            login.Closed += (s, args) => this.Close();
            login.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void devoluçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form devolucoes = new Devolucoes();
            devolucoes.Closed += (s, args) => this.Close();
            devolucoes.Show();
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
