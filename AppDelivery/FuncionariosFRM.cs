using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace AppDelivery
{
    public partial class FuncionariosFRM : Form
    {

        // =======================================================
        // >> PROPRIEDADES PÚBLICAS PARA RETORNO
        // =======================================================
        public int AtendenteSelecionadoID { get; private set; }
        public string AtendenteSelecionadoNome { get; private set; }
        // =======================================================

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
            dataGridView1.CellClick += dataGridView1_CellClick;

            btnNovo.Click += btnNovo_Click;
            btnEditar.Click += btnEditar_Click;
            btnSalvar.Click += btnSalvar_Click;
            btnCancelar.Click += btnCancelar_Click;

            // CONEXÃO ADICIONADA: Conecta o evento de clique para o botão Buscar.
            // *ASSUMIDO: O nome do botão de busca é 'btnBuscar'.
            btnBuscar.Click += btnBuscar_Click;
        }

        private void FuncionariosFRM_Load(object sender, EventArgs e)
        {
            // Carrega os dados na grade assim que o formulário é exibido.
            CarregarFuncionarios();
            HabilitarControles(false);

            // Define o filtro padrão como 'AUTOMATICO' se não estiver selecionado.
            // *ASSUMIDO: O nome do ComboBox de filtro é 'cmbFiltroAtendente' e 'AUTOMATICO' é o índice 0.
            if (cmbFiltroAtendente.SelectedIndex == -1 && cmbFiltroAtendente.Items.Count > 0)
            {
                cmbFiltroAtendente.SelectedIndex = 0;
            }
        }

        // Carrega os dados da tabela tb_funcionarios no DataGridView, aplicando filtros.
        private void CarregarFuncionarios(string filtro = "AUTOMATICO", string valor = "")
        {
            DataTable dtFuncionarios = new DataTable();
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    string queryBase = "SELECT id_atendente, nome, status, comissao FROM tb_funcionarios";
                    SqlCommand cmd;

                    // 1. Lógica de Filtragem: Apenas aplica filtro se o valor não estiver vazio.
                    if (!string.IsNullOrWhiteSpace(valor))
                    {
                        string valorBusca = valor.Trim();
                        string filtroUpper = filtro.ToUpper().Trim();

                        string whereClause = "";

                        // REVISÃO DO FILTRO AUTOMÁTICO: Busca genérica por Nome (LIKE) OU ID (LIKE em string).
                        if (filtroUpper == "AUTOMATICO")
                        {
                            // Busca o valor em 'nome' (LIKE) OU em 'id_atendente' (convertendo para string e usando LIKE).
                            // O uso do OR faz com que o termo 'ana' encontre 'Ana' no nome ou encontre '123' no ID se buscar '23'.
                            whereClause = " WHERE nome LIKE @valor OR CAST(id_atendente AS VARCHAR(10)) LIKE @valor";
                            queryBase += whereClause + " ORDER BY nome";
                            cmd = new SqlCommand(queryBase, conexao);
                            cmd.Parameters.AddWithValue("@valor", "%" + valorBusca + "%");
                        }
                        else if (filtroUpper == "ID")
                        {
                            // Filtro por ID: Busca exata.
                            whereClause = " WHERE id_atendente = @valor";
                            queryBase += whereClause + " ORDER BY nome";
                            cmd = new SqlCommand(queryBase, conexao);

                            // Tenta parsear o valor, se falhar (o que não deve ocorrer se o btnBuscar_Click funcionar), usa ID inválido.
                            if (int.TryParse(valorBusca, out int idValue))
                            {
                                cmd.Parameters.AddWithValue("@valor", idValue);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@valor", -1); // ID impossível para não retornar nada em caso de erro.
                            }
                        }
                        else if (filtroUpper == "NOME")
                        {
                            // Filtro por NOME: Busca usando LIKE.
                            whereClause = " WHERE nome LIKE @valor";
                            queryBase += whereClause + " ORDER BY nome";
                            cmd = new SqlCommand(queryBase, conexao);
                            cmd.Parameters.AddWithValue("@valor", "%" + valorBusca + "%");
                        }
                        else
                        {
                            // Filtro não reconhecido, retorna tudo.
                            queryBase += " ORDER BY nome";
                            cmd = new SqlCommand(queryBase, conexao);
                        }
                    }
                    else
                    {
                        // 2. Sem valor de busca, carrega todos os funcionários.
                        queryBase += " ORDER BY nome";
                        cmd = new SqlCommand(queryBase, conexao);
                    }

                    // Executa a query
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtFuncionarios);
                    dataGridView1.DataSource = dtFuncionarios;

                    // Formata as colunas do DataGridView.
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

        // Evento de clique para o botão "Buscar".
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtém o filtro selecionado (AUTOMATICO, ID, NOME)
            // *ASSUMIDO: O nome do ComboBox é 'cmbFiltroAtendente'
            string filtroSelecionado = cmbFiltroAtendente.SelectedItem?.ToString().ToUpper().Trim() ?? "AUTOMATICO";

            // Obtém o valor a ser buscado
            // *ASSUMIDO: O nome do TextBox de filtro é 'txtboxFiltro'
            string valorBusca = txtboxFiltro.Text.Trim();

            // Valida apenas se o filtro for explicitamente 'ID'
            if (filtroSelecionado == "ID" && !int.TryParse(valorBusca, out _))
            {
                MessageBox.Show("A busca por ID deve ser um valor numérico.", "Aviso de Filtro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxFiltro.Focus();
                return;
            }

            // Chama o método de carregamento com os parâmetros de filtro.
            CarregarFuncionarios(filtroSelecionado, valorBusca);
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
            // CORREÇÃO: Força o foco a sair dos campos de texto (como txtNome) antes de limpá-los.
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

            // CORREÇÃO: Força o foco para o DataGridView, um controle seguro.
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
                else if (status == "I") // 'I' para Inativo
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

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            // 1. Verifica se alguma linha foi selecionada no DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // 2. Obtém a primeira linha selecionada
                DataGridViewRow linhaSelecionada = dataGridView1.SelectedRows[0];

                try
                {
                    // 3. Pega o ID e o Nome da linha e armazena nas propriedades públicas
                    this.AtendenteSelecionadoID = Convert.ToInt32(linhaSelecionada.Cells["id_atendente"].Value);
                    this.AtendenteSelecionadoNome = linhaSelecionada.Cells["nome"].Value.ToString();

                    // 4. Define o DialogResult como OK para sinalizar sucesso e fechar o formulário
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    // Trata possíveis erros de conversão ou coluna inexistente
                    MessageBox.Show($"Erro ao capturar dados do funcionário: {ex.Message}", "Erro de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // 5. Exibe um aviso se nenhuma linha foi selecionada
                MessageBox.Show("Por favor, selecione um atendente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // OBS: O método FuncionariosFRM_Load_1 foi removido conforme solicitado.
    }
}
