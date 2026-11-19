using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SistemaCadastro
{
    public partial class Sistema : Form
    {
        private object txtdataHora;
        private object txtalunos;
        private object txtNome;

        int idAlterar;



        public Sistema()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCadastra_Click(object sender, EventArgs e)
        {
            marcador.Height = btnCadastra.Height;
            marcador.Top = btnCadastra.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }


        private void btnBusca_Click(object sender, EventArgs e)
        {
            marcador.Height = btnBusca.Height;
            marcador.Top = btnBusca.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }


        private void Sistema_Load(object sender, EventArgs e)
        {
            listaCBOficinas();
            lista_Escolas();
        }

        void lista_Escolas() { 
        
        ConectaBanco con = new ConectaBanco();
            dgEscolas.DataSource = con.listaEscolas();
            dgEscolas.Columns["idescolas"].Visible = false;
        }

        public void listaCBOficinas()
        {
            ConectaBanco con = new ConectaBanco();
            DataTable tabelaDados = new DataTable();
            tabelaDados = con.listaOficinas();
            cbOficinas.DataSource = tabelaDados;
            cbOficinas.DisplayMember = "oficinas";
            cbOficinas.ValueMember = "idoficinas";

            cbAlteraOficinas.DataSource = tabelaDados;
            cbAlteraOficinas.DisplayMember = "oficinas";
            cbAlteraOficinas.ValueMember = "idoficinas";
        }




        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            (dgEscolas.DataSource as DataTable).DefaultView.RowFilter = 
                string.Format("nome like '%{0}%'", txtBusca.Text);
        }

        private void btnRemoveEscola_Click(object sender, EventArgs e)
        {
            int linha = dgEscolas.CurrentRow.Index;
            int id = Convert.ToInt32(
                    dgEscolas.Rows[linha].Cells["idescolas"].Value.ToString());
            DialogResult resp = MessageBox.Show("Tem certeza que deseja cancelar esta oficina?",
                "Remove Escola", MessageBoxButtons.OKCancel);
            if (resp == DialogResult.OK)
            {
                ConectaBanco con = new ConectaBanco();
                bool retorno = con.deletaEscola(id);
                if (retorno == true)
                {
                    MessageBox.Show("Oficina cancelada com sucesso!");
                    lista_Escolas();
                }// fim if retorno true
                else
                    MessageBox.Show(con.mensagem);
            }// fim if Ok Cancela
            else
                MessageBox.Show("Cancelamento nao efetuado");


        }

        private void btnAlteracao_Click(object sender, EventArgs e)
        {
            int linha = dgEscolas.CurrentRow.Index;
            idAlterar = Convert.ToInt32(
              dgEscolas.Rows[linha].Cells["idescolas"].Value.ToString());
            txtAlteraNome.Text = 
              dgEscolas.Rows[linha].Cells["nome"].Value.ToString());
            txtAlteraAlunos.Text = 
              dgEscolas.Rows[linha].Cells["alunos"].Value.ToString());
            txtAlteraDataHora.Text = 
              dgEscolas.Rows[linha].Cells["dataHora"].Value.ToString());
            cbAlteraOficinas.SelectedValue = dgEscolas.Rows[linha].Cells["oficinas"].Value.ToString();
              dgEscolas.Rows[linha].Cells["oficinas"].Value.ToString());
            tabControl1.SelectedTab = tabAlterar;
        }

        private void btnConfirmaAlteracao_Click(object sender, EventArgs e)
        {

            ConectaBanco con = new ConectaBanco();
            Escola novaEscola = new Escola();

            novaEscola.Nome = txtAlteraNome.Text;
            novaEscola.alunos = Convert.ToInt32(txtAlteraAlunos.Text);
            novaEscola.dataHora = Convert.ToDateTime(txtAlteraDataHora.Text);
            novaEscola.oficinas = Convert.ToInt32(cbAlteraOficinas.SelectedValue);

            bool retorno = con.alteraEscola(novaEscola);

            if (retorno == false)
                MessageBox.Show(con.mensagem);
            else
                MessageBox.Show("Alteração efetuada com sucesso!");

            lista_Escolas()

        }

        private void bntAddOficinas_Click(object sender, EventArgs e)
        {

        }

        void limpacampos()
        { 
        txtnome.Clear();
            listaCBOficinas.Text = "";
            txtalunos.Clear();
            txtdataHora.Clear();
            txtnome.Focus();
        }

        private void BtnConfirmaCadastro_Click(object sender, EventArgs e)
        {
            ConectaBanco con = new ConectaBanco();
            Escola novaEscola = new Escola();

            novaEscola.Nome = txtnome.Text;
            novaEscola.alunos = Convert.ToInt32(txtalunos.Text);
            novaEscola.dataHora = Convert.ToDateTime(txtdataHora.Text);
            novaEscola.oficinas = Convert.ToInt32(listaCBOficinas.SelectedValue;

            bool retorno = con.insereEscola(novaEscola);

            if (retorno == false)
                MessageBox.Show(con.mensagem);

            limpacampos();
            lista_Escolas();
        }
    }
}
