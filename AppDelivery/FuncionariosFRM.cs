using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace AppDelivery
{
    public partial class FuncionariosFRM : Form
    {
        // Variável para armazenar a string de conexão do banco de dados.
        private string connectionString;

        // Variável para controlar o modo do formulário: false para novo, true para edição.
        private bool modoEdicao = false;

        public FuncionariosFRM()
        {
            InitializeComponent();

            // Pega a string de conexão do arquivo App.config.
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;

            // Configura o estado inicial do formulário ao ser carregado.
            ConfigurarFormularioInicial();

            // Adiciona os event handlers para os botões e o DataGridView.
            this.Load += new EventHandler(FuncionariosFRM_Load);

            // Conecta o evento de clique na célula do DataGridView ao método de preenchimento.
            dataGridView1.CellClick += dataGridView1_CellClick;

            btnNovo.Click += btnNovo_Click;
            btnEditar.Click += btnEditar_Click;
            btnSalvar.Click += btnSalvar_Click;
            btnCancelar.Click += btnCancelar_Click;
        }

        private void FuncionariosFRM_Load(object sender, EventArgs e)
        {
            // Carrega os dados na grade assim que o formulário é exibido.
            CarregarFuncionarios();
            HabilitarControles(false);
        }

        // Carrega os dados da tabela tb_funcionarios no DataGridView.
        private void CarregarFuncionarios()
        {
            DataTable dtFuncionarios = new DataTable();
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT id, nome, status, comissao FROM tb_funcionarios ORDER BY nome";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conexao);
                    adapter.Fill(dtFuncionarios);
                    dataGridView1.DataSource = dtFuncionarios;

                    // Formata as colunas do DataGridView para melhor visualização.
                    if (dataGridView1.Columns.Contains("id")) dataGridView1.Columns["id"].HeaderText = "ID";
                    if (dataGridView1.Columns.Contains("nome")) dataGridView1.Columns["nome"].HeaderText = "Nome";
                    if (dataGridView1.Columns.Contains("status")) dataGridView1.Columns["status"].HeaderText = "Status";
                    if (dataGridView1.Columns.Contains("comissao")) dataGridView1.Columns["comissao"].HeaderText = "Comissão";

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.AllowUserToDeleteRows = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar funcionários: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Habilita ou desabilita os controles de entrada de dados.
        private void HabilitarControles(bool enabled)
        {
            txtNome.Enabled = enabled;
            txtComissao.Enabled = enabled;
            cmbStatus.Enabled = enabled;

            // Controla a visibilidade dos botões.
            btnNovo.Enabled = !enabled;
            btnEditar.Enabled = !enabled;
            btnSalvar.Enabled = enabled;
            btnCancelar.Enabled = enabled;
        }

        // Limpa todos os campos de entrada de dados.
        private void LimparCampos()
        {
            txtID.Clear();
            txtNome.Clear();
            txtComissao.Clear();
            cmbStatus.SelectedIndex = -1;
        }

        // Configura o estado do formulário para o padrão inicial.
        private void ConfigurarFormularioInicial()
        {
            HabilitarControles(false);
            LimparCampos();
            btnNovo.Enabled = true;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            modoEdicao = false;
        }

        // Evento de clique do botão "Novo".
        private void btnNovo_Click(object sender, EventArgs e)
        {
            HabilitarControles(true);
            LimparCampos();
            modoEdicao = false;
            txtID.Text = "Novo Cadastro";
            cmbStatus.SelectedIndex = 0;
            txtNome.Focus();
        }

        // Evento de clique do botão "Editar".
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                HabilitarControles(true);
                modoEdicao = true;
                txtNome.Focus();
            }
            else
            {
                MessageBox.Show("Selecione um funcionário para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Evento de clique do botão "Salvar".
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Validação dos campos obrigatórios: Nome e Comissão.
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtComissao.Text))
            {
                MessageBox.Show("Os campos Nome e Comissão são obrigatórios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Converte a comissão para o tipo numérico e valida.
            if (!decimal.TryParse(txtComissao.Text, out decimal comissao))
            {
                MessageBox.Show("A comissão deve ser um valor numérico válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pega os valores dos campos.
            string nomeFuncionario = txtNome.Text.Trim();
            string statusFuncionario = cmbStatus.SelectedItem.ToString()[0].ToString();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    SqlCommand cmd;

                    if (modoEdicao)
                    {
                        // Lógica para EDIÇÃO: se modoEdicao for 'true', atualiza o registro.
                        string queryUpdate = "UPDATE tb_funcionarios SET nome = @nome, status = @status, comissao = @comissao WHERE id = @id";
                        cmd = new SqlCommand(queryUpdate, conexao);
                        cmd.Parameters.AddWithValue("@nome", nomeFuncionario);
                        cmd.Parameters.AddWithValue("@status", statusFuncionario);
                        cmd.Parameters.AddWithValue("@comissao", comissao);
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));
                    }
                    else
                    {
                        // Lógica para NOVO CADASTRO: se modoEdicao for 'false', insere um novo registro.
                        string queryInsert = "INSERT INTO tb_funcionarios (nome, status, comissao) VALUES (@nome, @status, @comissao)";
                        cmd = new SqlCommand(queryInsert, conexao);
                        cmd.Parameters.AddWithValue("@nome", nomeFuncionario);
                        cmd.Parameters.AddWithValue("@status", statusFuncionario);
                        cmd.Parameters.AddWithValue("@comissao", comissao);
                    }

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Recarrega os dados e volta ao estado inicial do formulário.
            CarregarFuncionarios();
            ConfigurarFormularioInicial();
        }

        // Evento de clique do botão "Cancelar".
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Volta o formulário para o estado inicial.
            ConfigurarFormularioInicial();
            CarregarFuncionarios();
        }

        // Lógica para preencher os campos quando uma linha do DataGridView é clicada.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Preenche os TextBoxes.
                txtID.Text = row.Cells["id"].Value.ToString();
                txtNome.Text = row.Cells["nome"].Value.ToString();
                txtComissao.Text = row.Cells["comissao"].Value.ToString();

                // Preenche o ComboBox com base no valor do banco de dados.
                string status = row.Cells["status"].Value.ToString();
                if (status == "A")
                {
                    cmbStatus.SelectedIndex = 0; // Define o índice do ComboBox para "Ativo"
                }
                else
                {
                    cmbStatus.SelectedIndex = 1; // Define o índice do ComboBox para "Inativo"
                }

                // Habilita o botão de editar, pois agora há uma linha selecionada.
                btnEditar.Enabled = true;
            }
        }
    }
}