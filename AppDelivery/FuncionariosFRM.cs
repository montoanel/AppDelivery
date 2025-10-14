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
                    string query = "SELECT id_atendente, nome, status, comissao FROM tb_funcionarios ORDER BY nome";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conexao);
                    adapter.Fill(dtFuncionarios);
                    dataGridView1.DataSource = dtFuncionarios;

                    // Formata as colunas do DataGridView para melhor visualização.
                    // Nota: O nome da coluna do ID no Select é "id_atendente", não "id".
                    if (dataGridView1.Columns.Contains("id_atendente")) dataGridView1.Columns["id_atendente"].HeaderText = "ID";
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
            // CORREÇÃO: Força o foco a sair dos campos de texto (como txtNome) antes de limpá-los,
            // evitando que o evento de LostFocus dispare a validação no campo vazio.
            this.Focus();

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
            // LINHA REMOVIDA: this.ActiveControl = null; - Sua observação sobre o foco estar correta torna esta linha desnecessária.

            // 1. VALIDAÇÃO DE OBRIGATORIEDADE: Nome
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            // 2. VALIDAÇÃO E CONVERSÃO DA COMISSÃO:
            string comissaoTexto = txtComissao.Text;
            if (string.IsNullOrWhiteSpace(comissaoTexto))
            {
                comissaoTexto = "0";
            }

            if (!decimal.TryParse(comissaoTexto, out decimal comissao))
            {
                MessageBox.Show("A comissão deve ser um valor numérico válido (ex: 10,00 ou 0).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtComissao.Focus();
                return;
            }

            // Pega os valores dos campos.
            string nomeFuncionario = txtNome.Text.Trim();

            // Otimização para status:
            string statusFuncionario = cmbStatus.SelectedItem != null
                                           ? cmbStatus.SelectedItem.ToString()[0].ToString()
                                           : "A";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    SqlCommand cmd;

                    if (modoEdicao)
                    {
                        string queryUpdate = "UPDATE tb_funcionarios SET nome = @nome, status = @status, comissao = @comissao WHERE id_atendente = @id_atendente";
                        cmd = new SqlCommand(queryUpdate, conexao);
                        cmd.Parameters.AddWithValue("@nome", nomeFuncionario);
                        cmd.Parameters.AddWithValue("@status", statusFuncionario);
                        cmd.Parameters.AddWithValue("@comissao", comissao);
                        cmd.Parameters.AddWithValue("@id_atendente", Convert.ToInt32(txtID.Text));
                    }
                    else
                    {
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
                    return; // Interrompe a execução para não resetar o formulário se houve erro.
                }
            }

            // Ações pós-salvamento:
            ConfigurarFormularioInicial();
            CarregarFuncionarios();

            // CORREÇÃO: Força o foco para o DataGridView, um controle seguro, evitando que ele caia no txtNome vazio.
            dataGridView1.Focus();
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

                // Função auxiliar para garantir que o valor não seja nulo e retorne string vazia se for.
                Func<string, string> GetSafeString = (colName) =>
                    row.Cells[colName].Value == null || row.Cells[colName].Value == DBNull.Value
                    ? string.Empty
                    : row.Cells[colName].Value.ToString();

                // Preenche os TextBoxes.
                txtID.Text = GetSafeString("id_atendente");
                txtNome.Text = GetSafeString("nome");
                txtComissao.Text = GetSafeString("comissao");

                // Preenche o ComboBox com base no valor do banco de dados.
                string status = GetSafeString("status");
                if (status == "A")
                {
                    cmbStatus.SelectedIndex = 0; // Define o índice do ComboBox para "Ativo"
                }
                else if (status == "I") // Adicionei 'I' como condição segura
                {
                    cmbStatus.SelectedIndex = 1; // Define o índice do ComboBox para "Inativo"
                }
                else
                {
                    cmbStatus.SelectedIndex = -1; // Se for um valor inesperado, não selecione nada.
                }

                // Habilita o botão de editar.
                btnEditar.Enabled = true;
            }
        }
    }
}
