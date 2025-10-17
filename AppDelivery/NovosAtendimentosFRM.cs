using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using AppDelivery.Enums; // 🚨 Adiciona o using para TipoAtendimento

namespace AppDelivery
{
    public partial class NovosAtendimentosFRM : Form
    {
        private string connectionString;

        // 🚨 NOVOS CAMPOS: Para armazenar o tipo e o número sequencial E OS IDs
        private TipoAtendimento tipoAtendimento;
        private int proximoNumeroAtendimento;
        private int idAtendimentoAtual;       // 🚨 NOVO: ID gerado pelo banco ao abrir
        private int idAtendenteSelecionado = 0; // 🚨 NOVO: ID do atendente vinculado (0 inicialmente)
        private int idClienteSelecionado = 0;   // 🚨 NOVO: ID do cliente vinculado (0 inicialmente)

        // 🚨 NOVO: Flag para indicar que o atendimento foi concluído com sucesso
        private bool atendimentoConcluido = false;

        // CONSTRUTOR PADRÃO EXISTENTE (mantido para compatibilidade com o designer)
        public NovosAtendimentosFRM()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            this.Load += new EventHandler(NovosAtendimentosFRM_Load);
            this.FormClosing += NovosAtendimentosFRM_FormClosing;
        }

        // ========================================================
        // NOVO MÉTODO: INTERCEPTA O FECHAMENTO DO FORMULÁRIO (BOTÃO X)
        // ========================================================
        private void NovosAtendimentosFRM_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 🚨 ATUALIZADO: Se o atendimento foi concluído com sucesso, permite o fechamento
            if (this.atendimentoConcluido)
            {
                e.Cancel = false;
                return;
            }

            // Antiga lógica de cancelamento (se não foi concluído)
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.None)
            {
                DialogResult resultado = MessageBox.Show(
                    "Tem certeza que deseja desistir de abrir este novo atendimento? Todas as alterações serão perdidas.",
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
        }

        // ========================================================
        // NOVO MÉTODO: EXCLUI O REGISTRO INICIAL PROVISÓRIO
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
                        // Alerta o usuário que o cancelamento falhou (o registro pode persistir)
                        MessageBox.Show("Atenção: Falha ao excluir o atendimento inicial provisório:\n" + ex.Message,
                                        "Erro de Cancelamento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // 🚨 NOVO CONSTRUTOR: Recebe o tipo de atendimento (PASSO 2)
        public NovosAtendimentosFRM(TipoAtendimento tipo) : this()
        {
            this.tipoAtendimento = tipo;
        }

        private void NovosAtendimentosFRM_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();

            // 🚨 NOVO: Inicializa o cabeçalho (que irá preencher o Tipo e o Número)
            InicializarCabecalho();
        }

        // ===============================================
        // NOVO MÉTODO: INICIALIZA O CABEÇALHO (COM GERAÇÃO DE ID)
        // ===============================================
        private void InicializarCabecalho()
        {
            if (this.tipoAtendimento != default(TipoAtendimento))
            {
                try
                {
                    // 1. Exibir o Tipo de Atendimento
                    txtTipoAtendimento.Text = this.tipoAtendimento.ToString();

                    // 2. Gerar o Número de Atendimento Sequencial do Dia
                    proximoNumeroAtendimento = GerarProximoNumeroAtendimento(this.tipoAtendimento);
                    txtNumeroAtendimento.Text = proximoNumeroAtendimento.ToString();

                    // 🚨 NOVO: CRIA REGISTRO NO BANCO E OBTÉM O ID
                    int tipoId = (int)this.tipoAtendimento;
                    idAtendimentoAtual = CriarNovoAtendimentoNoBanco(tipoId, proximoNumeroAtendimento);

                    if (idAtendimentoAtual > 0)
                    {
                        // 3. Exibir o ID gerado no campo correto
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
        // NOVO MÉTODO: GERA PRÓXIMO NÚMERO SEQUENCIAL (PASSO 3)
        // ========================================================
        private int GerarProximoNumeroAtendimento(TipoAtendimento tipo)
        {
            int tipoId = (int)tipo;
            int proximoNumero = 1;

            // Criamos objetos DateTime puros
            DateTime dataInicial = DateTime.Today; // Começo do dia (00:00:00)
            DateTime dataFinal = DateTime.Today.AddDays(1); // Começo do próximo dia

            // A query SQL permanece a mesma
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

                        // 🚨 CORREÇÃO: Passamos os objetos DateTime explicitamente
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
        SELECT SCOPE_IDENTITY();"; // Retorna o ID gerado

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conexao))
                    {
                        cmd.Parameters.Add("@Data", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.AddWithValue("@IdAtendente", idAtendenteSelecionado); // Usará 0
                        cmd.Parameters.AddWithValue("@IdCliente", idClienteSelecionado);     // Usará 0
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

        // =============================
        // CONFIGURAÇÃO DO DATAGRIDVIEW (EXISTENTE)
        // =============================
        private void ConfigurarDataGridView()
        {
            // Seu código existente...
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

        // =============================
        // BUSCAR PRODUTO NO BANCO (EXISTENTE)
        // =============================
        private DataRow BuscarProdutoNoBanco(string termo)
        {
            // Seu código existente...
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
                    MessageBox.Show("Erro ao buscar produto no banco:\n" + ex.Message,
                                    "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro inesperado:\n" + ex.Message,
                                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (dtProduto.Rows.Count > 0)
                return dtProduto.Rows[0];
            else
                return null;
        }

        // =============================
        // INSERIR PRODUTO NO GRID (EXISTENTE)
        // =============================
        private void btnInserir_Click(object sender, EventArgs e)
        {
            // Seu código existente...
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

            dgvProdutos.Rows.Add(
                idProduto,
                nomeProduto,
                qtd,
                preco,
                total
            );

            AtualizarTotalGeral();
            LimparCamposProduto();
        }

        // =============================
        // OBTER PREÇO DO PRODUTO (EXISTENTE)
        // =============================
        private decimal ObterPrecoDoProduto(int idProduto)
        {
            // Seu código existente...
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                SqlCommand cmd = new SqlCommand("SELECT preco FROM tb_produtos WHERE id_produto = @id", conexao);
                cmd.Parameters.AddWithValue("@id", idProduto);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
        }


        // 🔸 Método auxiliar para limpar campos (EXISTENTE)
        private void LimparCamposProduto()
        {
            // Seu código existente...
            txtBuscarProduto.Clear();
            txtQtd.Text = "1";
            txtBuscarProduto.Focus();
        }


        // =============================
        // CALCULAR TOTAL GERAL (EXISTENTE)
        // =============================
        private void AtualizarTotalGeral()
        {
            // Seu código existente...
            decimal totalGeral = 0;

            foreach (DataGridViewRow row in dgvProdutos.Rows)
            {
                totalGeral += (decimal)row.Cells["total"].Value;
            }

            lblTotalGeral.Text = "Total: " + totalGeral.ToString("C2");
        }

        // =============================
        // ATALHO: ENTER PARA INSERIR (EXISTENTE)
        // =============================
        private void txtBuscarProduto_KeyDown(object sender, KeyEventArgs e)
        {
            // Seu código existente...
            if (e.KeyCode == Keys.Enter)
            {
                btnInserir.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        // =============================
        // BUSCAR PRODUTO PELA LISTA (EXISTENTE)
        // =============================
        private void btnBuscarProduto_Click(object sender, EventArgs e)
        {
            // Seu código existente...
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

        private void btnInserirAtendente_Click(object sender, EventArgs e)
        {
            FuncionariosFRM formFuncionarios = new FuncionariosFRM();

            if (formFuncionarios.ShowDialog() == DialogResult.OK)
            {
                // 3. Pega o ID e o Nome das propriedades públicas do formulário de funcionários

                // A. Armazena o ID no campo de texto
                int idSelecionado = formFuncionarios.AtendenteSelecionadoID;
                txtidatendente.Text = idSelecionado.ToString();

                // B. Armazena o Nome no campo de texto
                txtNomeAtendente.Text = formFuncionarios.AtendenteSelecionadoNome;

                // C. 🚨 NOVO: ATUALIZA A VARIÁVEL DE CLASSE COM O ID SELECIONADO
                this.idAtendenteSelecionado = idSelecionado;

                // 4. 🚨 NOVO: ATUALIZA O BANCO DE DADOS IMEDIATAMENTE APÓS A SELEÇÃO
                AtualizarAtendenteClienteNoBanco();
            }
        }

        private void btnInserirCliente_Click(object sender, EventArgs e)
        {
            ListaClientes formClientes = new ListaClientes();

            if (formClientes.ShowDialog() == DialogResult.OK)
            {
                // 3. Pega o ID e o Nome das propriedades públicas do formulário de clientes

                // A. Armazena o ID no campo de texto
                int idSelecionado = formClientes.ClienteSelecionadoID;
                txtIDcliente.Text = idSelecionado.ToString();

                // B. Armazena o Nome no campo de texto
                txtNomeCliente.Text = formClientes.ClienteSelecionadoNome;

                // C. 🚨 NOVO: ATUALIZA A VARIÁVEL DE CLASSE COM O ID SELECIONADO
                this.idClienteSelecionado = idSelecionado;

                // 4. 🚨 NOVO: ATUALIZA O BANCO DE DADOS IMEDIATAMENTE APÓS A SELEÇÃO
                AtualizarAtendenteClienteNoBanco();
            }
        }

        // ========================================================
        // NOVO MÉTODO: ATUALIZA OS IDs DE ATENDENTE E CLIENTE NO BANCO
        // ========================================================
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
                    // O usuário pode continuar o atendimento, mas será alertado do erro
                    MessageBox.Show("Erro ao atualizar IDs de Atendente/Cliente no banco:\n" + ex.Message,
                                    "Erro de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Em NovosAtendimentosFRM.cs

        private void btnConcluir_Click(object sender, EventArgs e)
        {
            // Verifica se há itens na grade
            if (dgvProdutos.Rows.Count == 0)
            {
                MessageBox.Show("Não é possível concluir um atendimento sem produtos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Gravar os ITENS do Grid na tabela tb_itens_atendimento
                SalvarItensNoBanco();

                // 2. Atualizar o CABEÇALHO na tb_atendimentos (Total, Status, Observação)
                decimal valorTotalLiquido = ObterValorTotalLiquido();
                string observacao = txtObservacao.Text.Trim();
                string novoStatus = "Em atendimento";

                AtualizarCabecalhoFinal(valorTotalLiquido, novoStatus, observacao);

                // 3. Define a flag para evitar o cancelamento
                this.atendimentoConcluido = true;
                this.DialogResult = DialogResult.OK;

                // =========================================================
                // 🚨 NOVIDADE: EXIBIÇÃO DA MENSAGEM DE SUCESSO
                // =========================================================

                // Obtendo a Data/Hora de abertura
                // Se você não armazenou a data de abertura em uma variável de classe:
                string dataInicio = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                // Se você não tem a data de abertura, use a data e hora atual como referência
                // (melhor seria ter armazenado DateTime.Now no construtor)

                string mensagemSucesso =
                    $"Atendimento concluído com sucesso!\n\n" +
                    $"ID: {this.idAtendimentoAtual}\n" +
                    $"Número: {this.proximoNumeroAtendimento}\n" +
                    $"Tipo: {this.tipoAtendimento.ToString()}\n" +
                    $"Data/Hora de Conclusão: {dataInicio}"; // Usando data/hora atual (de conclusão)

                // Exibe o MessageBox e espera o usuário clicar em OK
                MessageBox.Show(
                    mensagemSucesso,
                    "Atendimento Concluído",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // 4. Fechar o formulário (só depois que o OK for clicado)
                this.Close();
            }
            catch (Exception ex)
            {
                // Se a gravação falhar, alertamos o usuário e não fechamos
                MessageBox.Show("Erro ao concluir e salvar o atendimento:\n" + ex.Message,
                                "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.atendimentoConcluido = false;
            }
        }

        // Em NovosAtendimentosFRM.cs

        private void AtualizarCabecalhoFinal(decimal valorTotal, string status, string observacao)
        {
            string query = @"
        UPDATE tb_atendimentos
        SET 
            valor_total_liquido = @ValorTotal,
            valor_total_bruto = @ValorTotal, -- Assumindo que Liquido = Bruto se não houver desconto
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

        // Em NovosAtendimentosFRM.cs

        private void SalvarItensNoBanco()
        {
            string query = @"
        INSERT INTO tb_itens_atendimento (
            id_atendimento, id_produto, quantidade, valor_unitario, 
            valor_total_item, valor_desconto_item, numero_sequencial
        )
        VALUES (
            @IdAtendimento, @IdProduto, @Quantidade, @ValorUnitario, 
            @ValorTotalItem, @ValorDescontoItem, @NumeroSequencial
        )";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                // Usamos uma transação para garantir que, se um item falhar, todos os outros sejam revertidos
                using (SqlTransaction transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        int sequencial = 1;
                        foreach (DataGridViewRow row in dgvProdutos.Rows)
                        {
                            // 🚨 IMPORTANT: Garanta que as colunas 'id_produto', 'quantidade', 'preco' e 'total' existem e contêm os dados
                            int idProduto = Convert.ToInt32(row.Cells["id_produto"].Value);
                            int quantidade = Convert.ToInt32(row.Cells["quantidade"].Value);
                            decimal precoUnitario = Convert.ToDecimal(row.Cells["preco"].Value);
                            decimal totalItem = Convert.ToDecimal(row.Cells["total"].Value);

                            using (SqlCommand cmd = new SqlCommand(query, conexao, transacao))
                            {
                                cmd.Parameters.AddWithValue("@IdAtendimento", this.idAtendimentoAtual);
                                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                                cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                                cmd.Parameters.AddWithValue("@ValorUnitario", precoUnitario);
                                cmd.Parameters.AddWithValue("@ValorTotalItem", totalItem);
                                cmd.Parameters.AddWithValue("@ValorDescontoItem", 0.00M); // Mantendo 0.00 por enquanto
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

        // 🔸 Método auxiliar para obter o valor total (você já o calcula na tela)
        private decimal ObterValorTotalLiquido()
        {
            // Extrai o valor do texto do Label (ex: "Total: R$ 41,97")
            string textoTotal = lblTotalGeral.Text.Replace("Total: ", "").Replace("R$", "").Trim();

            // Tenta converter o valor
            if (decimal.TryParse(textoTotal, System.Globalization.NumberStyles.Currency, System.Globalization.CultureInfo.CurrentCulture, out decimal total))
            {
                return total;
            }
            // Se falhar a conversão (o que não deveria ocorrer se o label estiver formatado)
            // Recorre ao recálculo pelo grid
            return CalcularTotalGeralGrid();
        }

        // 🔸 Método auxiliar para recalcular pelo Grid (caso o label falhe)
        private decimal CalcularTotalGeralGrid()
        {
            decimal totalGeral = 0;
            foreach (DataGridViewRow row in dgvProdutos.Rows)
            {
                // Tenta obter o valor da coluna 'total'. Lembre-se que ela é decimal.
                if (row.Cells["total"].Value != null && decimal.TryParse(row.Cells["total"].Value.ToString(), out decimal totalLinha))
                {
                    totalGeral += totalLinha;
                }
            }
            return totalGeral;
        }
    }
}