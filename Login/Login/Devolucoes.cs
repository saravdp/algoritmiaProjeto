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
    public partial class Devolucoes : Form
    {
        public string username;
        public string userType;
        public Devolucoes()
        {
            InitializeComponent();
            getUsernameAndUserType();
            navBar();
            On_Load();
            Load_Grid();
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
            if (userType == "seguranca")
            {
                comentáriosToolStripMenuItem.Visible = false;
                gestãoDeSalasToolStripMenuItem.Visible = false;
                criarNovoUtilizadorToolStripMenuItem.Visible = false;
            }

        }

        private void On_Load()
        {
            //Não permite editar visualmente as DataGrids
            
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        public void Load_Grid()
        {
            DataTable dt = new DataTable();
            int cont = 0;
            //Foreach file in directory 
            string[] fileEntries = Directory.GetFiles("Ficheiros de Texto/Requisicoes");
            DateTime dataDevolucao = DateTime.Now;
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
                    for (int i = 0; i < parts.Length; i++)
                    {
                        dt.Columns.Add(parts[i]);
                    }
                    cont++;
                }
                while (line != null)
                {
                    if (line != "" && line != "\n")
                    {
                        parts = line.Split(delimiters);
                        if (a != 0)
                        {
                              if (parts[6] == " " )
                              {
                                 dt.Rows.Add(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6]+parts[7]);
                              }
                        }
                    }
                    line = sr.ReadLine();
                    a++;
                }
                this.dataGridView1.DataSource = dt;
                sr.Close();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string lineSelected = "";
            DateTime date = DateTime.Now;
            string data=date.ToString("dd-MM-yy");
            string idReq = "";
            int value = 0;
            string idEquipamento = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (this.dataGridView1.SelectedRows.Count == 1)
                {
                    // get information of 1st column from the row
                    string selected = this.dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    lineSelected = selected;
                    date= Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[2].Value);
                    idReq= dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    idEquipamento = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                }
            }
            string fileName = "Ficheiros de Texto/Requisicoes/R_" + DateTime.Parse(lineSelected).ToString("ddMMyy");
            String line;
            StreamReader sr = new StreamReader(fileName);
            //Read the first line of text
            line = sr.ReadLine();
            int a = 0;
            char delimiters = ';';
            string linhaSaved="";
            int linhaAlterar = -1;
            string hora = String.Format("{0:t}", DateTime.Now).ToString();
            MessageBox.Show("HORA:" +hora);
            string[] parts = line.Split(delimiters);           
            while (line != null&&line!="")
            {
                parts = line.Split(delimiters);
                if (value == 0) {
                    value++;
                } else if (idReq==parts[0])
                {
                    linhaAlterar = a;
                    linhaSaved = parts[0] +";"+ parts[1] +";"+parts[2]+ ";"+ parts[3] + ";"+ parts[4] + ";"+ parts[5] + ";" + data+";" + hora;
                    MessageBox.Show(linhaSaved);
                }
                line = sr.ReadLine();
                a++;
            }
            sr.Close();
            string[] lines1 = File.ReadAllLines(fileName);
            lines1[linhaAlterar] = linhaSaved;
            File.WriteAllLines(fileName, lines1);            
            var lines = File.ReadAllLines(fileName).Where(arg => !string.IsNullOrWhiteSpace(arg));
            File.WriteAllLines(fileName, lines);
            MessageBox.Show("Equipamento Devolvido!");


            //AUMENTA STOCK----------------------------------------------------------
             fileName = "Ficheiros de Texto/equipamentos.txt";
            StreamReader reader = new StreamReader(fileName);
            //Read the first line of text
            line = reader.ReadLine();
            a = 0;
            delimiters = ';';
            linhaSaved = "";
            linhaAlterar = -1;
            parts = line.Split(delimiters);
            while (line != null)
            {
                parts = line.Split(delimiters);
                if (parts[0] == idEquipamento)
                {
                    linhaAlterar = a;
                    int stock = Convert.ToInt16(parts[3]) + 1;
                    linhaSaved = parts[0] + ";" + parts[1] + ";" + parts[2] + ";" + stock.ToString() + ";" + parts[4];
                    MessageBox.Show(linhaSaved);
                }
                line = reader.ReadLine();
                a++;
            }
            reader.Close();
            string[] lines2 = File.ReadAllLines(fileName);
            lines2[linhaAlterar] = linhaSaved;
            //for(int i=0; i < lines2.Length; i++)
           // {
           //     MessageBox.Show(lines2[i]);
          //  }
            File.WriteAllLines(fileName, lines2);
             lines = File.ReadAllLines(fileName).Where(arg => !string.IsNullOrWhiteSpace(arg));
            File.WriteAllLines(fileName, lines);
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

        private void gestãoDeSalasToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void comentáriosAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form comentarios_admin = new Comentarios_admin();
            comentarios_admin.Closed += (s, args) => this.Close();
            comentarios_admin.Show();
        }

        private void devoluçõesToolStripMenuItem_Click(object sender, EventArgs e)
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
