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
    public partial class Criar_novo_utilizador : Form
    {

        public string username;
        public string userType;

        public Criar_novo_utilizador()
        {
            InitializeComponent();
            getUsernameAndUserType();
            navBar();
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

        }

        private void criarNovoUtilizadorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
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

        private void comentáriosAdminToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form login = new Login();
            login.Closed += (s, args) => this.Close();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("Ficheiros de Texto/utilizadores.txt");
            string line = sr.ReadLine();
            int cont = 0;
            while (line != null)
            {
                string[] parts = line.Split(';');
                if (parts[1] == textBox1.Text)
                {
                    cont++;
                }
                line = sr.ReadLine();
            }
            sr.Close();
            if (comboBox1.Text != "")
            {
                if (cont == 0)
                {
                    var lastLine = File.ReadLines("Ficheiros de Texto/utilizadores.txt").Last();
                    char[] delimiters = new char[] { ';' };
                    string[] parts = lastLine.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    int id = Convert.ToInt16(parts[0]) + 1;
                    string user = textBox1.Text;
                    string email = textBox2.Text;
                    string password = textBox3.Text;
                    StreamWriter sw = File.AppendText("Ficheiros de Texto/utilizadores.txt");
                    sw.Write("\n" + id + ";" + user + ";" + email + ";" + password + ";" + comboBox1.Text);
                    MessageBox.Show("Registo efetuado com sucesso.");
                    sw.Close();
                    this.Hide();
                    Form criar_novo_utilizador = new Criar_novo_utilizador();
                    criar_novo_utilizador.Closed += (s, args) => this.Close();
                    criar_novo_utilizador.Show();
                }
                else
                {
                    MessageBox.Show("O user já existe!!");
                }
            }

        }
    }
}
