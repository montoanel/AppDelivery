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

        // CONSTRUTOR PADRÃO EXISTENTE (mantido para compatibilidade com o designer)
        public NovosAtendimentosFRM()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            this.Load += new EventHandler(NovosAtendimentosFRM_Load);
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
            // 1. Cria uma nova instância do formulário de Funcionários
            FuncionariosFRM formFuncionarios = new FuncionariosFRM();

            // 2. Exibe o formulário de forma modal e verifica o resultado
            if (formFuncionarios.ShowDialog() == DialogResult.OK)
            {
                // Se o resultado for OK, significa que o usuário selecionou um atendente e clicou em Aplicar

                // 3. Pega o ID e o Nome das propriedades públicas do formulário de funcionários

                // A. Armazena o ID (para gravação no banco de dados)
                // O ID será armazenado em um campo de texto ou em uma variável de classe (como a anterior). 
                // Vamos usar o campo de texto conforme solicitado:
                txtidatendente.Text = formFuncionarios.AtendenteSelecionadoID.ToString();

                // B. Armazena o Nome (para exibição na tela)
                txtNomeAtendente.Text = formFuncionarios.AtendenteSelecionadoNome;
            }

            // O formulário FuncionariosFRM é automaticamente descartado ao sair deste bloco.
        }

        private void btnInserirCliente_Click(object sender, EventArgs e)
        {
            // 1. Cria uma nova instância do formulário de Clientes
            ListaClientes formClientes = new ListaClientes();

            // 2. Exibe o formulário de forma modal e verifica o resultado
            if (formClientes.ShowDialog() == DialogResult.OK)
            {
                // Se o resultado for OK, significa que o usuário selecionou um cliente e clicou em Aplicar

                // 3. Pega o ID e o Nome das propriedades públicas do formulário de clientes

                // A. Armazena o ID (para gravação no banco de dados)
                txtIDcliente.Text = formClientes.ClienteSelecionadoID.ToString();

                // B. Armazena o Nome (para exibição na tela)
                txtNomeCliente.Text = formClientes.ClienteSelecionadoNome;
            }
        }
    }
}