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

        CheckBox[] box = new CheckBox[20] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };

        public Lista_Equipamentos()
        {
            InitializeComponent();
            categorias_list();
            dataGrid_load();
            filters();
            On_Load();

        }
        private void On_Load()
        {
            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/salas.txt");
            line = sr.ReadLine();
            while (line != null)
            {
                comboBox1.Items.Add(line);
                line = sr.ReadLine();

            }
            sr.Close();
        }
        private void filters()
        {
            int a = 0;
            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/categorias.txt");
            //Read the first line of text
            line = sr.ReadLine();
            CheckBox[] box = new CheckBox[20];
            groupBox1.Enabled = true;
            while (line != null)
            {
                char delimiters = ';';
                string[] parts = line.Split(delimiters);
                string RowFilter = string.Empty;
                //---------------------------------------------IMP.FILTERS
                //   CreateOrAppendToFilter(box[a], ref RowFilter);

                if (RowFilter.Length > 0)
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        //Check an see what's in the dgv
                        DataView dv = new DataView(dt);
                        dv.RowFilter = RowFilter;
                        dataGridView1.DataSource = dv;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Can’t find the column", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                line = sr.ReadLine();
                a++;
            }
            sr.Close();
        }
        private void CreateOrAppendToFilter(CheckBox cb, ref string RowFilter)
        {
            if (RowFilter.Length > 0)
            {
                RowFilter += " OR ";
            }
            RowFilter += (cb.Checked) ? string.Format("[AreaCode] = {0}", cb.Text.Trim()) : string.Empty;
        }
        private void categorias_list()
        {
            int a = 0;
            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/categorias.txt");
            //Read the first line of text
            line = sr.ReadLine();
            groupBox1.Enabled = true;
            while (line != null)
            {
                char delimiters = ';';
                string[] parts = line.Split(delimiters);
                box[a] = new CheckBox();
                box[a].Tag = parts[1].ToString();
                box[a].Text = parts[1];
                box[a].TabIndex = a;
                box[a].AutoSize = true;
                box[a].Location = new Point(10, 20 + a * 20);
                groupBox1.Controls.Add(box[a]);
                line = sr.ReadLine();
                a++;
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
            int id=0;//= Convert.ToInt16(parts[0]);
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
                    }
                }
                if (File.Exists(nameFile))
                {
                    var lastLine = File.ReadLines(nameFile).Last();
                    parts = lastLine.Split(';');
                    id = Convert.ToInt16(parts[0])+1;
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

                string tipoObjeto = cat;
                id = id + 1;
                // MessageBox.Show("Resumo de requisição:  \n Sala: {0} \n ");
                sw.WriteLine("\n"+id+";"+user + ";" + data + ";" + hora + ";" + sala + ";" + tipoObjeto);
                MessageBox.Show("Requisitado!");

                sw.Close();

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


            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/equipamentos.txt");
            //Read the first line of text
            line = sr.ReadLine();
            int cont = 0;
            int counter = 1;
            DataTable dt = new DataTable();
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;

            while (line != null)
            {
                string[] parts = line.Split(';');
                if (a != 0)
                {

                    while (cats[cont] != null)//Compara cada linha com o array das categorias selecionadas
                    {
                        if (parts[2] == cats[cont])
                        {
                            bs.Filter = "[Categoria] Like '%" + cats[cont] + "%'";//Só permite uma categoria
                            dataGridView1.DataSource = bs;
                        }
                        cont++;
                    }
                }
                line = sr.ReadLine();
                a++;
                counter++;
                cont = 0;
            }
        }


    }
}
