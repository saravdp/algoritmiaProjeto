using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Consulta_Requisicoes : Form
    {
        public Consulta_Requisicoes()
        {
            InitializeComponent();
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
            Form listaEquipamentos = new Lista_Equipamentos();
            listaEquipamentos.Closed += (s, args) => this.Close();
            listaEquipamentos.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
