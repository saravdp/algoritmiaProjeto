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
    public partial class Gestao_Salas : Form
    {
        public Gestao_Salas()
        {
            InitializeComponent();
            On_Load();
        }
        private void On_Load()
        {
            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/salas.txt");
            //Read the first line of text
            line = sr.ReadLine();
            int a = 0;
            DataTable dt = new DataTable();
            string[] parts = line.Split(';');
            //CABEÇALHO
            dt.Columns.Add(parts[1]);
            while (line != null)
            {
                parts = line.Split(';');
                if (a != 0)
                {
                    dt.Rows.Add(parts[1]);
                }
                line = sr.ReadLine();
                a++;
            }
            this.dataGridView1.DataSource = dt;
            sr.Close();
        }
        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form gestao_categorias = new Gestao_Categorias();
            gestao_categorias.Closed += (s, args) => this.Close();
            gestao_categorias.Show();
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

        }

        private void comentáriosAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form comentarios_admin = new Comentarios_admin();
            comentarios_admin.Closed += (s, args) => this.Close();
            comentarios_admin.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ADICIONAR
            Int32 index = dataGridView1.Rows.Count - 2;
            DataGridViewRow row = dataGridView1.Rows[index];
            string salaAdded = row.Cells[0].Value.ToString(); //ler e converter para string o nome atribuido
            var lastLine = File.ReadLines("Ficheiros de Texto/salas.txt").Last(); //vai buscar a ultima linha
            char[] delimiters = new char[] { ';' };
            string[] parts = lastLine.Split(delimiters, StringSplitOptions.RemoveEmptyEntries); //Vai à ultima linha e divide-a onde existem ";"
            int id = Convert.ToInt16(parts[0]) + 1; //id da nova linha (id da ultima linha + 1)
            if (salaAdded != "")
            {
                StreamWriter sw = File.AppendText("Ficheiros de Texto/salas.txt");
                sw.WriteLine(id + ";" + salaAdded); //Escrever no ficheiro de texto
                MessageBox.Show("Sala Adicionada!");
                this.dataGridView1.EndEdit();
                this.dataGridView1.Refresh();
                sw.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            //REMOVER
            string salas = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (this.dataGridView1.SelectedRows.Count == 1)  
                {
                    // get information of 1st column from the row
                    string selected = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    salas = selected;
                }
            }
            StreamReader sr = new StreamReader("Ficheiros de texto/salas.txt");
            string line = sr.ReadLine();
            int a = 0;
            int idSala = -1; //valor -1 para ser diferente de 0, pois este ID existe

            while (line != null)
            {
                char delimiters = ';';
                string[] parts = line.Split(delimiters);
                if (parts[1] == salas) //se a salas selecionada for igual à lida na linha do ficheiro de texto
                {
                    idSala = Convert.ToInt16(parts[0]);
                }
                line = sr.ReadLine();
                a++;
            }
            sr.Close();

            int cont = 0;
            int numeroLinha = 0;
            if (salas != null)
            {
                string replace = idSala + ";" + salas; //o que vai procurar no ficheiro de texto para ser substituido por string.Empty
                StreamReader sa = new StreamReader("Ficheiros de texto/salas.txt");
                line = sa.ReadLine();
                while (line!=null)
                {
                    if (line == replace)
                    {
                        numeroLinha = cont; //quando a linha selecionada for igual à linha que o sistema leu
                    }
                    cont++;
                    line = sa.ReadLine();
                }
                sa.Close();
                string[] lines1 = File.ReadAllLines("Ficheiros de Texto/salas.txt");
                lines1[numeroLinha] = string.Empty; //apaga o conteúdo da linha selecionada
                File.WriteAllLines("Ficheiros de Texto/salas.txt", lines1);

                var lines = File.ReadAllLines("Ficheiros de Texto/salas.txt").Where(arg => !string.IsNullOrWhiteSpace(arg));
                File.WriteAllLines("Ficheiros de Texto/salas.txt", lines);
                MessageBox.Show("Sala Removida!");
            }
            else
            {
                MessageBox.Show("Nenhuma linha selecionada!!");
            }
        }
        
    }
}
