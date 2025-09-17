using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace AppDelivery
{
    public partial class FormasPagamentoFRM : Form
    {
        // Variável para armazenar a string de conexão.
        private string connectionString;

        // Variável para controlar o modo do formulário (novo ou edição).
        private bool modoEdicao = false;

        public FormasPagamentoFRM()
        {
            InitializeComponent();

            // Acessando a connection string com base no seu exemplo.
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;

            // Define o estado inicial do formulário
            ConfigurarFormularioInicial();

            this.Load += new EventHandler(FormasPagamentoFRM_Load);

            // Aqui está a linha que causava o erro.
            // Agora ela está conectada ao método que está mais abaixo no código.
            dataGridView1.CellClick += dataGridView1_CellClick;

            btNovo.Click += btNovo_Click;
            btEditar.Click += btEditar_Click;
            btSalvar.Click += btSalvar_Click;
            btCancelar.Click += btCancelar_Click;
        }

        private void FormasPagamentoFRM_Load(object sender, EventArgs e)
        {
            CarregarFormasPagamento();
            HabilitarControles(false);
        }

        // Carrega os dados da tabela tb_formas_pagamento no DataGridView.
        private void CarregarFormasPagamento()
        {
            DataTable dtFormasPagamento = new DataTable();
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT id_pagamento, nome_pagamento, inativo, data_cadastro FROM tb_formas_pagamento ORDER BY nome_pagamento";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conexao);
                    adapter.Fill(dtFormasPagamento);
                    dataGridView1.DataSource = dtFormasPagamento;

                    // Formata as colunas do DataGridView
                    if (dataGridView1.Columns.Contains("id_pagamento")) dataGridView1.Columns["id_pagamento"].HeaderText = "ID";
                    if (dataGridView1.Columns.Contains("nome_pagamento")) dataGridView1.Columns["nome_pagamento"].HeaderText = "Nome";
                    if (dataGridView1.Columns.Contains("inativo")) dataGridView1.Columns["inativo"].HeaderText = "Status";
                    if (dataGridView1.Columns.Contains("data_cadastro")) dataGridView1.Columns["data_cadastro"].HeaderText = "Data de Cadastro";

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.AllowUserToAddRows = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar formas de pagamento: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Habilita ou desabilita os campos de texto e a combo box.
        private void HabilitarControles(bool enabled)
        {
            txtboxNome.Enabled = enabled;
            cmbStatus.Enabled = enabled;
            btNovo.Enabled = !enabled;
            btEditar.Enabled = !enabled;
            btSalvar.Enabled = enabled;
            btCancelar.Enabled = !enabled;
        }

        // Limpa os campos de texto.
        private void LimparCampos()
        {
            txtID.Clear();
            txtboxNome.Clear();
            cmbStatus.SelectedIndex = -1; // Desseleciona o item
        }

        // Configura o estado inicial do formulário ao abrir.
        private void ConfigurarFormularioInicial()
        {
            HabilitarControles(false);
            LimparCampos();
            btNovo.Enabled = true;
            btEditar.Enabled = true;
            btSalvar.Enabled = false;
            btCancelar.Enabled = true;
            modoEdicao = false;
        }

        private void btNovo_Click(object sender, EventArgs e)
        {
            HabilitarControles(true);
            LimparCampos();
            modoEdicao = false;
            txtID.Text = "Novo Cadastro";
            txtboxNome.Focus();
        }

        private void btEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                HabilitarControles(true);
                modoEdicao = true;
                txtboxNome.Focus();
            }
            else
            {
                MessageBox.Show("Selecione uma forma de pagamento para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtboxNome.Text))
            {
                MessageBox.Show("O nome da forma de pagamento é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nomePagamento = txtboxNome.Text.Trim();
            string inativoStatus = cmbStatus.SelectedIndex == 0 ? "A" : "I"; // 'A' para Ativo, 'I' para Inativo

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    SqlCommand cmd;

                    if (modoEdicao)
                    {
                        // Lógica para EDIÇÃO
                        string queryUpdate = "UPDATE tb_formas_pagamento SET nome_pagamento = @nome, inativo = @inativo WHERE id_pagamento = @id";
                        cmd = new SqlCommand(queryUpdate, conexao);
                        cmd.Parameters.AddWithValue("@nome", nomePagamento);
                        cmd.Parameters.AddWithValue("@inativo", inativoStatus);
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));
                    }
                    else
                    {
                        // Lógica para NOVO CADASTRO
                        string queryInsert = "INSERT INTO tb_formas_pagamento (nome_pagamento, inativo, data_cadastro) VALUES (@nome, @inativo, GETDATE())";
                        cmd = new SqlCommand(queryInsert, conexao);
                        cmd.Parameters.AddWithValue("@nome", nomePagamento);
                        cmd.Parameters.AddWithValue("@inativo", inativoStatus);
                    }

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Recarrega os dados e volta ao estado inicial
            CarregarFormasPagamento();
            ConfigurarFormularioInicial();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Lógica para preencher os campos quando uma linha do DataGridView é clicada.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtID.Text = row.Cells["id_pagamento"].Value.ToString();
                txtboxNome.Text = row.Cells["nome_pagamento"].Value.ToString();

                string status = row.Cells["inativo"].Value.ToString();
                cmbStatus.SelectedIndex = status == "A" ? 0 : 1; // 0 para "Ativo", 1 para "Inativo"
            }
        }
    }
}