using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using AppDelivery.Enums;
using System.Collections.Generic; // Adicionado para Listas

namespace AppDelivery
{
    public partial class NovosAtendimentosFRM : Form
    {
        private string connectionString;

        // Campos existentes
        private TipoAtendimento tipoAtendimento;
        private int proximoNumeroAtendimento;
        private int idAtendimentoAtual;
        private int idAtendenteSelecionado = 0;
        private int idClienteSelecionado = 0;
        private bool atendimentoConcluido = false;

        // 🚨 NOVO: Flag para diferenciar Modo de Criação de Modo de Edição
        private bool modoCriacao = false;


        // CONSTRUTOR PADRÃO EXISTENTE (mantido para compatibilidade com o designer)
        public NovosAtendimentosFRM()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            this.Load += new EventHandler(NovosAtendimentosFRM_Load);
            this.FormClosing += NovosAtendimentosFRM_FormClosing;
        }

        // 🚨 CONSTRUTOR (MODO CRIAÇÃO) - Modificado para setar a flag
        public NovosAtendimentosFRM(TipoAtendimento tipo) : this()
        {
            this.tipoAtendimento = tipo;
            this.modoCriacao = true; // Indica que estamos criando um novo
        }

        // 🚨 NOVO CONSTRUTOR (MODO EDIÇÃO)
        public NovosAtendimentosFRM(int idAtendimentoExistente) : this()
        {
            this.idAtendimentoAtual = idAtendimentoExistente;
            this.modoCriacao = false; // Indica que estamos editando um existente
        }

        // ========================================================
        // 🚨 MODIFICADO: LÓGICA DE FECHAMENTO (BOTÃO X)
        // ========================================================
        private void NovosAtendimentosFRM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.atendimentoConcluido)
            {
                e.Cancel = false;
                return;
            }

            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.None)
            {
                // 🚨 SÓ PERGUNTA SOBRE EXCLUIR SE ESTIVER EM MODO DE CRIAÇÃO
                if (this.modoCriacao)
                {
                    DialogResult resultado = MessageBox.Show(
                        "Tem certeza que deseja desistir de abrir este novo atendimento? O registro provisório será excluído.",
                        "Confirmar Desistência",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (resultado == DialogResult.Yes)
                    {
                        ExcluirAtendimentoProvisorio();
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else // 🚨 Em MODO DE EDIÇÃO, apenas pergunta se quer sair
                {
                    DialogResult resultado = MessageBox.Show(
                       "Tem certeza que deseja fechar? As alterações não salvas (novos itens) serão perdidas.",
                       "Confirmar Fechamento",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Warning
                   );

                    if (resultado == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        // ========================================================
        // 🚨 MODIFICADO: FORM_LOAD (Decide entre Criar ou Carregar)
        // ========================================================
        private void NovosAtendimentosFRM_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();

            if (this.modoCriacao) // MODO DE CRIAÇÃO
            {
                InicializarCabecalho();
            }
            else if (this.idAtendimentoAtual > 0) // MODO DE EDIÇÃO
            {
                CarregarAtendimentoExistente();
            }
            else
            {
                MessageBox.Show("Formulário chamado de maneira incorreta.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        // Em NovosAtendimentosFRM.cs

        // Em NovosAtendimentosFRM.cs

        // ===============================================
        // 🚨 CORRIGIDO (Novamente): CORRIGE OS NOMES DAS COLUNAS DO JOIN
        // ===============================================
        private void CarregarAtendimentoExistente()
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();

                    // --- 1. CARREGAR CABEÇALHO ---
                    // 🚨 CORREÇÃO NA QUERY: Ajustando os nomes das colunas conforme sua sugestão
                    string queryCabecalho = @"
                SELECT 
                    a.*, 
                    ISNULL(f.nome, '') as nome_atendente,
                    ISNULL(c.nome_cliente, '') as nome_cliente
                FROM tb_atendimentos a
                LEFT JOIN tb_funcionarios f ON a.id_atendente = f.id_atendente 
                LEFT JOIN tb_clientes c ON a.id_cliente = c.cod_cliente         
                WHERE a.id_atendimento = @IdAtendimento";

                    DataRow cabecalho = null;
                    using (SqlCommand cmd = new SqlCommand(queryCabecalho, conexao))
                    {
                        cmd.Parameters.AddWithValue("@IdAtendimento", this.idAtendimentoAtual);
                        DataTable dt = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        if (dt.Rows.Count > 0)
                        {
                            cabecalho = dt.Rows[0];
                        }
                    }

                    if (cabecalho == null)
                    {
                        MessageBox.Show("Atendimento não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    // (Checagens de DBNull.Value - mantidas da correção anterior)
                    this.idAtendimentoAtual = Convert.ToInt32(cabecalho["id_atendimento"]);
                    this.proximoNumeroAtendimento = cabecalho["numero_atendimento"] != DBNull.Value ? Convert.ToInt32(cabecalho["numero_atendimento"]) : 0;
                    this.idAtendenteSelecionado = cabecalho["id_atendente"] != DBNull.Value ? Convert.ToInt32(cabecalho["id_atendente"]) : 0;
                    this.idClienteSelecionado = cabecalho["id_cliente"] != DBNull.Value ? Convert.ToInt32(cabecalho["id_cliente"]) : 0;
                    int tipoId = cabecalho["tipo_atendimento"] != DBNull.Value ? Convert.ToInt32(cabecalho["tipo_atendimento"]) : 1;
                    this.tipoAtendimento = (TipoAtendimento)tipoId;
                    txtIDAtendimento.Text = this.idAtendimentoAtual.ToString();
                    txtNumeroAtendimento.Text = this.proximoNumeroAtendimento.ToString();
                    txtTipoAtendimento.Text = this.tipoAtendimento.ToString();
                    txtidatendente.Text = this.idAtendenteSelecionado.ToString();
                    txtNomeAtendente.Text = cabecalho["nome_atendente"].ToString();
                    txtIDcliente.Text = this.idClienteSelecionado.ToString();
                    txtNomeCliente.Text = cabecalho["nome_cliente"].ToString();
                    txtObservacao.Text = cabecalho["observacoes"] != DBNull.Value ? cabecalho["observacoes"].ToString() : "";

                    // Trava campos
                    txtTipoAtendimento.ReadOnly = true;
                    txtNumeroAtendimento.ReadOnly = true;


                    // --- 2. CARREGAR ITENS DO ATENDIMENTO ---
                    // (Esta query já estava correta da etapa anterior)
                    string queryItens = @"
                SELECT 
                    i.id_produto, 
                    p.nome, 
                    i.quantidade, 
                    i.valor_unitario AS preco,
                    i.valor_total_item AS total
                FROM tb_itens_atendimento i
                JOIN tb_produtos p ON i.id_produto = p.id_produto
                WHERE i.id_atendimento = @IdAtendimento
                ORDER BY i.numero_sequencial";

                    using (SqlCommand cmdItens = new SqlCommand(queryItens, conexao))
                    {
                        cmdItens.Parameters.AddWithValue("@IdAtendimento", this.idAtendimentoAtual);
                        DataTable dtItens = new DataTable();
                        using (SqlDataAdapter daItens = new SqlDataAdapter(cmdItens))
                        {
                            daItens.Fill(dtItens);
                        }

                        // Popula o DataGridView (corrigido na etapa anterior)
                        foreach (DataRow itemRow in dtItens.Rows)
                        {
                            dgvProdutos.Rows.Add(
                                Convert.ToInt32(itemRow["id_produto"]),
                                itemRow["nome"].ToString(),
                                Convert.ToInt32(itemRow["quantidade"]),
                                Convert.ToDecimal(itemRow["preco"]),
                                Convert.ToDecimal(itemRow["total"])
                            );
                        }
                    }

                    // --- 3. ATUALIZAR TOTAIS ---
                    AtualizarTotalGeral();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar o atendimento existente: " + ex.Message, "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }


        // ===============================================
        // MÉTODO: INICIALIZA O CABEÇALHO (MODO CRIAÇÃO)
        // ===============================================
        private void InicializarCabecalho()
        {
            if (this.tipoAtendimento != default(TipoAtendimento))
            {
                try
                {
                    txtTipoAtendimento.Text = this.tipoAtendimento.ToString();

                    proximoNumeroAtendimento = GerarProximoNumeroAtendimento(this.tipoAtendimento);
                    txtNumeroAtendimento.Text = proximoNumeroAtendimento.ToString();

                    int tipoId = (int)this.tipoAtendimento;
                    idAtendimentoAtual = CriarNovoAtendimentoNoBanco(tipoId, proximoNumeroAtendimento);

                    if (idAtendimentoAtual > 0)
                    {
                        txtIDAtendimento.Text = idAtendimentoAtual.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Falha crítica ao gerar o ID do Atendimento. Fechando formulário.");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inicializar o cabeçalho do atendimento: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        // ========================================================
        // MÉTODO: GERA PRÓXIMO NÚMERO SEQUENCIAL
        // ========================================================
        private int GerarProximoNumeroAtendimento(TipoAtendimento tipo)
        {
            int tipoId = (int)tipo;
            int proximoNumero = 1;

            DateTime dataInicial = DateTime.Today;
            DateTime dataFinal = DateTime.Today.AddDays(1);

            string query = @"
                SELECT ISNULL(MAX(numero_atendimento), 0) + 1 
                FROM tb_atendimentos 
                WHERE tipo_atendimento = @TipoId
                  AND data_atendimento >= @DataInicial 
                  AND data_atendimento < @DataFinal";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@TipoId", tipoId);
                        cmd.Parameters.Add("@DataInicial", SqlDbType.DateTime).Value = dataInicial;
                        cmd.Parameters.Add("@DataFinal", SqlDbType.DateTime).Value = dataFinal;

                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            proximoNumero = Convert.ToInt32(result);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Erro ao buscar o próximo número de atendimento: " + ex.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1;
                }
            }
            return proximoNumero;
        }

        // ========================================================
        // MÉTODO: CRIA REGISTRO PROVISÓRIO E OBTÉM O ID
        // ========================================================
        private int CriarNovoAtendimentoNoBanco(int tipoId, int numero)
        {
            string query = @"
                INSERT INTO dbo.tb_atendimentos (
                    data_atendimento, id_atendente, id_cliente, valor_total_bruto, 
                    valor_total_liquido, status_atendimento, tipo_atendimento, numero_atendimento
                )
                VALUES (
                    @Data, @IdAtendente, @IdCliente, 0, 
                    0, 'Aberto', @TipoId, @Numero
                );
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conexao))
                    {
                        cmd.Parameters.Add("@Data", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.AddWithValue("@IdAtendente", idAtendenteSelecionado); // 0
                        cmd.Parameters.AddWithValue("@IdCliente", idClienteSelecionado);     // 0
                        cmd.Parameters.AddWithValue("@TipoId", tipoId);
                        cmd.Parameters.AddWithValue("@Numero", numero);

                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Erro ao criar registro inicial de atendimento:\n" + ex.Message,
                                    "Erro Crítico de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return 0;
        }

        // ========================================================
        // MÉTODO: EXCLUI O REGISTRO INICIAL PROVISÓRIO
        // ========================================================
        private void ExcluirAtendimentoProvisorio()
        {
            if (this.idAtendimentoAtual > 0)
            {
                string query = "DELETE FROM tb_atendimentos WHERE id_atendimento = @IdAtendimento";

                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    try
                    {
                        conexao.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conexao))
                        {
                            cmd.Parameters.AddWithValue("@IdAtendimento", this.idAtendimentoAtual);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Atenção: Falha ao excluir o atendimento inicial provisório:\n" + ex.Message,
                                        "Erro de Cancelamento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        // =============================
        // LÓGICA DE PRODUTOS (GRID)
        // =============================
        private void ConfigurarDataGridView()
        {
            dgvProdutos.Columns.Add("id_produto", "Código");
            dgvProdutos.Columns.Add("nome", "Produto");
            dgvProdutos.Columns.Add("quantidade", "Qtd");
            dgvProdutos.Columns.Add("preco", "Preço Unit.");
            dgvProdutos.Columns.Add("total", "Total");

            dgvProdutos.Columns["preco"].DefaultCellStyle.Format = "C2";
            dgvProdutos.Columns["total"].DefaultCellStyle.Format = "C2";

            dgvProdutos.AllowUserToAddRows = false;
            dgvProdutos.ReadOnly = true;
            dgvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private DataRow BuscarProdutoNoBanco(string termo)
        {
            DataTable dtProduto = new DataTable();
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    string query = @"SELECT TOP 1 id_produto, nome, codigo_barras, preco 
                                     FROM tb_produtos
                                     WHERE codigo_barras = @termo OR nome LIKE @nome";
                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@termo", termo);
                    cmd.Parameters.AddWithValue("@nome", "%" + termo + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtProduto);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Erro ao buscar produto no banco:\n" + ex.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dtProduto.Rows.Count > 0 ? dtProduto.Rows[0] : null;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            string termoBusca = txtBuscarProduto.Text.Trim();
            if (string.IsNullOrEmpty(termoBusca))
            {
                MessageBox.Show("Digite o nome, código ou selecione um produto!");
                return;
            }

            DataRow produto = BuscarProdutoNoBanco(termoBusca);
            if (produto == null)
            {
                MessageBox.Show("Produto não encontrado!");
                return;
            }

            if (!int.TryParse(txtQtd.Text.Trim(), out int qtd) || qtd <= 0)
            {
                MessageBox.Show("Quantidade inválida!");
                return;
            }

            int idProduto = Convert.ToInt32(produto["id_produto"]);
            string nomeProduto = produto["nome"].ToString();
            decimal preco = Convert.ToDecimal(produto["preco"]);
            decimal total = preco * qtd;

            dgvProdutos.Rows.Add(idProduto, nomeProduto, qtd, preco, total);

            AtualizarTotalGeral();
            LimparCamposProduto();
        }

        private void LimparCamposProduto()
        {
            txtBuscarProduto.Clear();
            txtQtd.Text = "1";
            txtBuscarProduto.Focus();
        }

        private void AtualizarTotalGeral()
        {
            decimal totalGeral = 0;
            foreach (DataGridViewRow row in dgvProdutos.Rows)
            {
                totalGeral += (decimal)row.Cells["total"].Value;
            }
            lblTotalGeral.Text = "Total: " + totalGeral.ToString("C2");
        }

        private void txtBuscarProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnInserir.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void btnBuscarProduto_Click(object sender, EventArgs e)
        {
            using (ListaProdutosFRM lista = new ListaProdutosFRM())
            {
                lista.ProdutoSelecionado += (id, nome, preco) =>
                {
                    txtBuscarProduto.Text = nome;
                    txtBuscarProduto.Tag = id;
                    txtQtd.Text = "1";
                };
                lista.ShowDialog();
            }
        }


        // =============================
        // LÓGICA DE ATENDENTE E CLIENTE
        // =============================

        private void btnInserirAtendente_Click(object sender, EventArgs e)
        {
            FuncionariosFRM formFuncionarios = new FuncionariosFRM();
            if (formFuncionarios.ShowDialog() == DialogResult.OK)
            {
                int idSelecionado = formFuncionarios.AtendenteSelecionadoID;
                txtidatendente.Text = idSelecionado.ToString();
                txtNomeAtendente.Text = formFuncionarios.AtendenteSelecionadoNome;
                this.idAtendenteSelecionado = idSelecionado;
                AtualizarAtendenteClienteNoBanco();
            }
        }

        private void btnInserirCliente_Click(object sender, EventArgs e)
        {
            ListaClientes formClientes = new ListaClientes();
            if (formClientes.ShowDialog() == DialogResult.OK)
            {
                int idSelecionado = formClientes.ClienteSelecionadoID;
                txtIDcliente.Text = idSelecionado.ToString();
                txtNomeCliente.Text = formClientes.ClienteSelecionadoNome;
                this.idClienteSelecionado = idSelecionado;
                AtualizarAtendenteClienteNoBanco();
            }
        }

        private void AtualizarAtendenteClienteNoBanco()
        {
            string query = @"
                UPDATE tb_atendimentos
                SET id_atendente = @IdAtendente,
                    id_cliente = @IdCliente
                WHERE id_atendimento = @IdAtendimentoAtual";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@IdAtendente", this.idAtendenteSelecionado);
                        cmd.Parameters.AddWithValue("@IdCliente", this.idClienteSelecionado);
                        cmd.Parameters.AddWithValue("@IdAtendimentoAtual", this.idAtendimentoAtual);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Erro ao atualizar IDs de Atendente/Cliente no banco:\n" + ex.Message, "Erro de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // ========================================================
        // 🚨 MODIFICADO: LÓGICA DE CONCLUIR ATENDIMENTO
        // ========================================================
        private void btnConcluir_Click(object sender, EventArgs e)
        {
            if (dgvProdutos.Rows.Count == 0)
            {
                MessageBox.Show("Não é possível concluir um atendimento sem produtos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Gravar os ITENS (Método SalvarItensNoBanco foi modificado)
                SalvarItensNoBanco();

                // 2. Atualizar o CABEÇALHO
                decimal valorTotalLiquido = ObterValorTotalLiquido();
                string observacao = txtObservacao.Text.Trim();

                // 🚨 LÓGICA DE STATUS ATUALIZADA
                string statusAtual = ObterStatusAtualDoBanco();
                string novoStatus = statusAtual;

                if (statusAtual == "Aberto")
                {
                    novoStatus = "Em atendimento";
                }
                // Se já estava 'Em atendimento' ou 'Em trânsito', permanece assim.

                AtualizarCabecalhoFinal(valorTotalLiquido, novoStatus, observacao);

                // 3. Define a flag para evitar o cancelamento
                this.atendimentoConcluido = true;
                this.DialogResult = DialogResult.OK;

                // 4. Exibição da Mensagem de Sucesso
                string dataConclusao = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                string tituloMensagem = this.modoCriacao ? "Atendimento Concluído" : "Atendimento Atualizado";

                string mensagemSucesso =
                    $"Atendimento salvo com sucesso!\n\n" +
                    $"ID: {this.idAtendimentoAtual}\n" +
                    $"Número: {this.proximoNumeroAtendimento}\n" +
                    $"Tipo: {this.tipoAtendimento.ToString()}\n" +
                    $"Data/Hora da Ação: {dataConclusao}";

                MessageBox.Show(
                    mensagemSucesso,
                    tituloMensagem,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // 5. Fechar o formulário
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao concluir e salvar o atendimento:\n" + ex.Message,
                                "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.atendimentoConcluido = false;
            }
        }

        private string ObterStatusAtualDoBanco()
        {
            string query = "SELECT status_atendimento FROM tb_atendimentos WHERE id_atendimento = @IdAtendimento";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@IdAtendimento", this.idAtendimentoAtual);
                        object result = cmd.ExecuteScalar();
                        return result?.ToString() ?? "Aberto";
                    }
                }
                catch
                {
                    return "Aberto"; // Fallback
                }
            }
        }

        private void AtualizarCabecalhoFinal(decimal valorTotal, string status, string observacao)
        {
            string query = @"
                UPDATE tb_atendimentos
                SET 
                    valor_total_liquido = @ValorTotal,
                    valor_total_bruto = @ValorTotal, 
                    status_atendimento = @Status,
                    observacoes = @Observacao
                WHERE 
                    id_atendimento = @IdAtendimentoAtual";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@ValorTotal", valorTotal);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@Observacao", observacao);
                        cmd.Parameters.AddWithValue("@IdAtendimentoAtual", this.idAtendimentoAtual);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Falha ao atualizar o cabeçalho final do atendimento.", ex);
                }
            }
        }

        // ========================================================
        // 🚨 MODIFICADO: Salva Itens (agora limpa antes de inserir)
        // ========================================================
        private void SalvarItensNoBanco()
        {
            string insertQuery = @"
                INSERT INTO tb_itens_atendimento (
                    id_atendimento, id_produto, quantidade, valor_unitario, 
                    valor_total_item, valor_desconto_item, numero_sequencial
                )
                VALUES (
                    @IdAtendimento, @IdProduto, @Quantidade, @ValorUnitario, 
                    @ValorTotalItem, @ValorDescontoItem, @NumeroSequencial
                )";

            string deleteQuery = "DELETE FROM tb_itens_atendimento WHERE id_atendimento = @IdAtendimento";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                using (SqlTransaction transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        // 🚨 ETAPA 1: LIMPAR ITENS ANTIGOS (Para Modo Edição)
                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conexao, transacao))
                        {
                            deleteCmd.Parameters.AddWithValue("@IdAtendimento", this.idAtendimentoAtual);
                            deleteCmd.ExecuteNonQuery();
                        }

                        // 🚨 ETAPA 2: INSERIR TODOS OS ITENS ATUAIS DO GRID
                        int sequencial = 1;
                        foreach (DataGridViewRow row in dgvProdutos.Rows)
                        {
                            int idProduto = Convert.ToInt32(row.Cells["id_produto"].Value);
                            int quantidade = Convert.ToInt32(row.Cells["quantidade"].Value);
                            decimal precoUnitario = Convert.ToDecimal(row.Cells["preco"].Value);
                            decimal totalItem = Convert.ToDecimal(row.Cells["total"].Value);

                            using (SqlCommand cmd = new SqlCommand(insertQuery, conexao, transacao))
                            {
                                cmd.Parameters.AddWithValue("@IdAtendimento", this.idAtendimentoAtual);
                                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                                cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                                cmd.Parameters.AddWithValue("@ValorUnitario", precoUnitario);
                                cmd.Parameters.AddWithValue("@ValorTotalItem", totalItem);
                                cmd.Parameters.AddWithValue("@ValorDescontoItem", 0.00M);
                                cmd.Parameters.AddWithValue("@NumeroSequencial", sequencial++);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();
                        throw new Exception("Falha ao salvar itens do atendimento no banco de dados.", ex);
                    }
                }
            }
        }

        // ========================================================
        // MÉTODOS AUXILIARES DE CÁLCULO DE TOTAL
        // ========================================================
        private decimal ObterValorTotalLiquido()
        {
            string textoTotal = lblTotalGeral.Text.Replace("Total: ", "").Replace("R$", "").Trim();
            if (decimal.TryParse(textoTotal, System.Globalization.NumberStyles.Currency, System.Globalization.CultureInfo.CurrentCulture, out decimal total))
            {
                return total;
            }
            return CalcularTotalGeralGrid();
        }

        private decimal CalcularTotalGeralGrid()
        {
            decimal totalGeral = 0;
            foreach (DataGridViewRow row in dgvProdutos.Rows)
            {
                if (row.Cells["total"].Value != null && decimal.TryParse(row.Cells["total"].Value.ToString(), out decimal totalLinha))
                {
                    totalGeral += totalLinha;
                }
            }
            return totalGeral;
        }

        // 🚨 Adicionei de volta o método ObterPrecoDoProduto caso ele não exista no seu
        // (Seu arquivo original não o mostrava, mas o btnInserir_Click o utiliza implicitamente)
        private decimal ObterPrecoDoProduto(int idProduto)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                SqlCommand cmd = new SqlCommand("SELECT preco FROM tb_produtos WHERE id_produto = @id", conexao);
                cmd.Parameters.AddWithValue("@id", idProduto);
                object result = cmd.ExecuteScalar();
                return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

    }
}