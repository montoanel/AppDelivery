using System;
using System.Data;
using Microsoft.Data.SqlClient; // Essencial para comunicação com SQL Server
using System.Windows.Forms;
using System.Configuration; // Necessário para ler a string de conexão do App.config
using System.Globalization; // Para formatação de moeda, se necessário
using System.Drawing;
using System.Text;
using System.Linq;

namespace AppDelivery
{
    public partial class CadastroProdutosFrm : Form
    {
        private string connectionString;
        private int _idProdutoParaEditar = 0; // Armazena o ID do produto para edição
        private bool _modoEdicao = false;     // Sinaliza se estamos no modo de edição

        // Construtor para MODO DE CADASTRO (Novo Produto)
        public CadastroProdutosFrm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            this.Load += new EventHandler(CadastroProdutosFrm_Load);
            // No modo de cadastro, os campos já começam vazios, o que é o comportamento desejado.
        }

        // Construtor para MODO DE EDIÇÃO
        public CadastroProdutosFrm(int idProduto)
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            _idProdutoParaEditar = idProduto; // Atribui o valor passado
            _modoEdicao = true;               // Sinaliza o modo de edição
            this.Load += new EventHandler(CadastroProdutosFrm_Load);
        }

        // Evento Load do Formulário
        private void CadastroProdutosFrm_Load(object sender, EventArgs e)
        {
            // Configura os ComboBoxes com os valores fixos
            ConfigurarComboBoxProdutoInativo();
            ConfigurarComboBoxTipoProduto();

            // Adiciona o manipulador de evento para a tecla F1
            this.KeyPreview = true; // Permite que o formulário receba eventos de teclado antes dos controles
            this.KeyDown += new KeyEventHandler(CadastroProdutosFrm_KeyDown);

            if (_modoEdicao)
            {
                this.Text = "Editar Produto"; // Muda o título do formulário
                CarregarDadosProduto();       // Carrega os dados do produto para edição
            }
            else
            {
                this.Text = "Novo Produto"; // Mantém o título para cadastro
                txtIDProduto.Enabled = false; // Desabilita o campo de ID
                txtIDProduto.Text = "Novo (Automático)";
                ObterProximoCodigoInterno(); // Adicionado: Preenche o campo de código interno automaticamente
            }
        }

        // NOVO: Evento KeyDown para capturar a tecla F1
        private void CadastroProdutosFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // Se o campo de código de barras estiver vazio, gera um novo código
                if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text))
                {
                    bntGerarCodBarras_Click(sender, e);
                    e.Handled = true; // Impede que o evento seja processado por outros controles
                }
            }
        }

        // Configura o ComboBox de Ativo/Inativo
        private void ConfigurarComboBoxProdutoInativo()
        {
            cmbProdutoInativo.Items.Clear();
            cmbProdutoInativo.Items.Add("Ativo");
            cmbProdutoInativo.Items.Add("Inativo");

            if (!_modoEdicao)
            {
                // Valor padrão "Ativo" no modo de cadastro
                cmbProdutoInativo.SelectedIndex = 0; // "Ativo"
            }
            // No modo de edição, o valor será carregado pelo CarregarDadosProduto()
        }

        // Configura o ComboBox de Tipo de Produto
        private void ConfigurarComboBoxTipoProduto()
        {
            cmbTipoProduto.Items.Clear();
            cmbTipoProduto.Items.Add("Principal");
            cmbTipoProduto.Items.Add("Adicional");

            if (!_modoEdicao)
            {
                cmbTipoProduto.SelectedIndex = 0; // Define um padrão se não for edição
            }
        }

        private void bntCancelarProduto_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =========================================================
        // NOVO MÉTODO: bntGerarCodBarras_Click
        // =========================================================
        private void bntGerarCodBarras_Click(object sender, EventArgs e)
        {
            try
            {
                // Chama o novo método para obter o próximo código de barras gerado
                string novoCodigoBarras = ObterProximoCodigoBarras();
                txtCodigoBarras.Text = novoCodigoBarras;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar o código de barras: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // MÉTODO PRINCIPAL: btnSalvarProduto_Click
        // =========================================================
        private void btnSalvarProduto_Click(object sender, EventArgs e)
        {
            // 1. Validação dos Dados Essenciais
            if (string.IsNullOrWhiteSpace(txtNomeProduto.Text))
            {
                MessageBox.Show("O campo 'Nome do Produto' é obrigatório. Por favor, preencha-o.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNomeProduto.Focus();
                return;
            }

            // Validação de Preço e Custo (assumindo que devem ser numéricos)
            decimal preco;
            if (!decimal.TryParse(txtPreco.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out preco))
            {
                MessageBox.Show("O campo 'Preço' deve ser um valor numérico válido.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPreco.Focus();
                return;
            }

            decimal custo;
            if (!decimal.TryParse(txtCusto.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out custo))
            {
                MessageBox.Show("O campo 'Custo' deve ser um valor numérico válido.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCusto.Focus();
                return;
            }

            // =========================================================
            // NOVO TRATAMENTO: Validação de Código de Barras
            // =========================================================
            // Só executa a verificação de duplicidade se o campo não estiver vazio
            if (!string.IsNullOrWhiteSpace(txtCodigoBarras.Text))
            {
                // Chama o novo método para verificar a unicidade
                if (VerificarCodigoBarrasDuplicado(txtCodigoBarras.Text.Trim(), _idProdutoParaEditar))
                {
                    MessageBox.Show("O código de barras inserido já está em uso por outro produto. Por favor, insira um código de barras único.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigoBarras.Focus();
                    return;
                }
            }
            // Se o campo estiver vazio, a validação é ignorada, e o valor nulo será inserido.

            // =========================================================
            // Validação de Nome do Produto
            // =========================================================
            if (VerificarNomeDuplicado(txtNomeProduto.Text.Trim(), _idProdutoParaEditar))
            {
                MessageBox.Show("Já existe um produto com este nome. Por favor, insira um nome único.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNomeProduto.Focus();
                return;
            }


            // Determinar o valor 'inativo' para o banco de dados ('A' para Ativo, 'I' para Inativo)
            string statusInativo = (cmbProdutoInativo.SelectedItem != null && cmbProdutoInativo.SelectedItem.ToString() == "Inativo") ? "I" : "A";

            // Definir a query SQL com base no modo (INSERT ou UPDATE)
            string query = "";

            if (_modoEdicao)
            {
                // Query para atualizar um produto existente
                query = "UPDATE tb_produtos SET " +
                        "codigo_interno = @codigo_interno, " +
                        "codigo_barras = @codigo_barras, " +
                        "nome = @nome, " +
                        "preco = @preco, " +
                        "custo = @custo, " +
                        "unidade_medida = @unidade_medida, " +
                        "grupo = @grupo, " +
                        "tipo_produto = @tipo_produto, " +
                        "inativo = @inativo " +
                        "WHERE id_produto = @id_produto";
            }
            else
            {
                // Query para inserir um novo produto
                query = "INSERT INTO tb_produtos " +
                        "(codigo_interno, codigo_barras, nome, preco, custo, unidade_medida, grupo, tipo_produto, inativo) " +
                        "VALUES " +
                        "(@codigo_interno, @codigo_barras, @nome, @preco, @custo, @unidade_medida, @grupo, @tipo_produto, @inativo);" +
                        "SELECT SCOPE_IDENTITY();"; // Retorna o ID do último registro inserido
            }

            // ATENÇÃO: A mensagem de erro "Violação da restrição UNIQUE KEY" ainda aparece porque o banco de dados
            // ainda tem uma restrição que proíbe valores nulos duplicados. O código C# abaixo está correto,
            // mas só funcionará após você executar o script SQL de correção fornecido na conversa.
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    // Adicionar Parâmetros à Query
                    if (_modoEdicao)
                    {
                        // Parâmetro essencial para o UPDATE
                        comando.Parameters.AddWithValue("@id_produto", _idProdutoParaEditar);
                    }

                    comando.Parameters.AddWithValue("@codigo_interno", string.IsNullOrWhiteSpace(txtCodigoInterno.Text) ? (object)DBNull.Value : txtCodigoInterno.Text);
                    // Passa DBNull.Value se o campo estiver vazio para permitir o valor nulo no banco
                    comando.Parameters.AddWithValue("@codigo_barras", string.IsNullOrWhiteSpace(txtCodigoBarras.Text) ? (object)DBNull.Value : txtCodigoBarras.Text);
                    comando.Parameters.AddWithValue("@nome", txtNomeProduto.Text);
                    comando.Parameters.AddWithValue("@preco", preco); // Já validado como decimal
                    comando.Parameters.AddWithValue("@custo", custo); // Já validado como decimal
                    comando.Parameters.AddWithValue("@unidade_medida", string.IsNullOrWhiteSpace(txtUnidadeMedida.Text) ? (object)DBNull.Value : txtUnidadeMedida.Text);
                    comando.Parameters.AddWithValue("@grupo", string.IsNullOrWhiteSpace(txtGrupo.Text) ? (object)DBNull.Value : txtGrupo.Text);
                    comando.Parameters.AddWithValue("@tipo_produto", cmbTipoProduto.SelectedItem != null ? cmbTipoProduto.SelectedItem.ToString() : (object)DBNull.Value);
                    comando.Parameters.AddWithValue("@inativo", statusInativo); // 'A' ou 'I'

                    try
                    {
                        conexao.Open();
                        if (_modoEdicao)
                        {
                            int linhasAfetadas = comando.ExecuteNonQuery();
                            if (linhasAfetadas > 0)
                            {
                                MessageBox.Show("Produto atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.DialogResult = DialogResult.OK; // Indica sucesso para o formulário chamador
                                this.Close(); // Fecha o formulário de edição após a atualização
                            }
                            else
                            {
                                MessageBox.Show("Nenhum produto foi atualizado. Verifique os dados e tente novamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else // Modo de cadastro
                        {
                            // Usa ExecuteScalar para obter o ID do produto recém-inserido
                            object result = comando.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Alteração: Agora o formulário fecha após o cadastro, em vez de apenas limpar os campos.
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Nenhum produto foi cadastrado. Verifique os dados e tente novamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Erro ao " + (_modoEdicao ? "atualizar" : "cadastrar") + " produto no banco de dados:\n" + ex.Message + "\n\nCódigo do Erro: " + ex.Number, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro inesperado ao " + (_modoEdicao ? "atualizar" : "cadastrar") + " o produto:\n" + ex.Message, "Erro Genérico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Verifica se um nome de produto já existe no banco de dados.
        /// No modo de edição, ele ignora o próprio produto que está sendo editado.
        /// A query utiliza COLLATE SQL_Latin1_General_CP1_CI_AS para ser insensível a acentuação e maiúsculas/minúsculas.
        /// </summary>
        /// <param name="nomeProduto">O nome do produto a ser verificado.</param>
        /// <param name="idProdutoAtual">O ID do produto que está sendo editado (0 para novos cadastros).</param>
        /// <returns>Verdadeiro se o nome for duplicado, falso caso contrário.</returns>
        private bool VerificarNomeDuplicado(string nomeProduto, int idProdutoAtual)
        {
            string query = "SELECT COUNT(*) FROM tb_produtos WHERE nome COLLATE SQL_Latin1_General_CP1_CI_AS = @nome_produto AND id_produto <> @id_produto";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@nome_produto", nomeProduto);
                    comando.Parameters.AddWithValue("@id_produto", idProdutoAtual);

                    try
                    {
                        conexao.Open();
                        int count = (int)comando.ExecuteScalar();
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao verificar duplicidade de nome: " + ex.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Retorna true para evitar a operação de salvar caso haja um erro de conexão.
                        return true;
                    }
                }
            }
        }

        /// <summary>
        /// NOVO MÉTODO: Verifica se um código de barras já existe no banco de dados.
        /// No modo de edição, ele ignora o próprio produto que está sendo editado.
        /// Este método NÃO é executado se o campo estiver vazio, permitindo a inserção de NULL.
        /// </summary>
        /// <param name="codigoBarras">O código de barras a ser verificado.</param>
        /// <param name="idProdutoAtual">O ID do produto que está sendo editado (0 para novos cadastros).</param>
        /// <returns>Verdadeiro se o código de barras for duplicado, falso caso contrário.</returns>
        private bool VerificarCodigoBarrasDuplicado(string codigoBarras, int idProdutoAtual)
        {
            string query = "SELECT COUNT(*) FROM tb_produtos WHERE codigo_barras = @codigo_barras AND id_produto <> @id_produto";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@codigo_barras", codigoBarras);
                    comando.Parameters.AddWithValue("@id_produto", idProdutoAtual); // No modo de cadastro, idProdutoAtual será 0

                    try
                    {
                        conexao.Open();
                        int count = (int)comando.ExecuteScalar();
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao verificar duplicidade de código de barras: " + ex.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Retorna true para evitar a operação de salvar caso haja um erro de conexão.
                        return true;
                    }
                }
            }
        }

        // Método auxiliar para limpar os campos do formulário
        private void ClearForm()
        {
            txtIDProduto.Text = "Novo (Automático)";
            // Adicionado: Chamada ao método para obter o próximo código interno
            ObterProximoCodigoInterno();
            txtCodigoBarras.Text = string.Empty;
            txtNomeProduto.Text = string.Empty;
            txtPreco.Text = string.Empty;
            txtCusto.Text = string.Empty;
            txtUnidadeMedida.Text = string.Empty;
            txtGrupo.Text = string.Empty;
            cmbProdutoInativo.SelectedIndex = 0;
            if (cmbTipoProduto.Items.Count > 0)
            {
                cmbTipoProduto.SelectedIndex = 0;
            }

            txtNomeProduto.Focus();
        }

        // Método auxiliar para carregar os dados de um produto para edição
        private void CarregarDadosProduto()
        {
            if (_idProdutoParaEditar <= 0)
            {
                MessageBox.Show("ID do produto para edição inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            string query = "SELECT id_produto, codigo_interno, codigo_barras, nome, preco, custo, unidade_medida, grupo, tipo_produto, inativo " +
                            "FROM tb_produtos WHERE id_produto = @id_produto";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@id_produto", _idProdutoParaEditar);

                    try
                    {
                        conexao.Open();
                        SqlDataReader reader = comando.ExecuteReader();

                        if (reader.Read())
                        {
                            txtIDProduto.Text = reader["id_produto"].ToString();
                            txtIDProduto.Enabled = false;

                            txtCodigoInterno.Text = reader.IsDBNull(reader.GetOrdinal("codigo_interno")) ? string.Empty : reader["codigo_interno"].ToString();
                            txtCodigoBarras.Text = reader.IsDBNull(reader.GetOrdinal("codigo_barras")) ? string.Empty : reader["codigo_barras"].ToString();
                            txtNomeProduto.Text = reader["nome"].ToString();

                            // Use `ToString("C2")` para formatar como moeda, ou apenas `ToString()` se preferir o formato puro.
                            txtPreco.Text = reader["preco"].ToString();
                            txtCusto.Text = reader["custo"].ToString();

                            txtUnidadeMedida.Text = reader.IsDBNull(reader.GetOrdinal("unidade_medida")) ? string.Empty : reader["unidade_medida"].ToString();
                            txtGrupo.Text = reader.IsDBNull(reader.GetOrdinal("grupo")) ? string.Empty : reader["grupo"].ToString();

                            // Selecionar o item correto no ComboBox de Tipo de Produto
                            string tipoProdutoDB = reader.IsDBNull(reader.GetOrdinal("tipo_produto")) ? string.Empty : reader["tipo_produto"].ToString();
                            if (!string.IsNullOrEmpty(tipoProdutoDB))
                            {
                                int index = cmbTipoProduto.FindStringExact(tipoProdutoDB);
                                if (index != -1)
                                {
                                    cmbTipoProduto.SelectedIndex = index;
                                }
                                else
                                {
                                    cmbTipoProduto.SelectedIndex = -1;
                                }
                            }
                            else
                            {
                                cmbTipoProduto.SelectedIndex = -1;
                            }

                            // Selecionar o item correto no ComboBox de Ativo/Inativo
                            string inativoDB = reader["inativo"].ToString();
                            if (inativoDB == "I")
                            {
                                cmbProdutoInativo.SelectedItem = "Inativo";
                            }
                            else
                            {
                                cmbProdutoInativo.SelectedItem = "Ativo";
                            }

                            reader.Close();
                        }
                        else
                        {
                            MessageBox.Show("Produto não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Close();
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Erro ao carregar dados do produto do banco de dados:\n" + ex.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro inesperado ao carregar os dados do produto:\n" + ex.Message, "Erro Genérico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Obtém o próximo código interno sequencial (ex: INT011).
        /// Esta função é chamada no modo de cadastro para preencher o campo automaticamente.
        /// </summary>
        private void ObterProximoCodigoInterno()
        {
            string query = "SELECT MAX(codigo_interno) FROM tb_produtos WHERE codigo_interno LIKE 'INT%'";
            int proximoNumero = 1; // Valor inicial caso a tabela esteja vazia ou não haja códigos "INT"

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    try
                    {
                        conexao.Open();
                        object resultado = comando.ExecuteScalar();

                        if (resultado != null && resultado != DBNull.Value)
                        {
                            string ultimoCodigo = resultado.ToString();
                            // Extrai a parte numérica do código (ex: "INT010" -> "010")
                            string numeroStr = new string(ultimoCodigo.Where(char.IsDigit).ToArray());

                            if (int.TryParse(numeroStr, out int ultimoNumero))
                            {
                                proximoNumero = ultimoNumero + 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao obter o próximo código interno: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            // Formata o novo código para o padrão "INT0XX"
            txtCodigoInterno.Text = $"INT{proximoNumero:D3}";
        }

        /// <summary>
        /// NOVO MÉTODO: Obtém o próximo código de barras sequencial (ex: GERADO011).
        /// Esta função é chamada quando o botão bntGerarCodBarras ou a tecla F1 é pressionada.
        /// O padrão "GERADO" é usado para distinguir dos códigos de barras reais.
        /// </summary>
        private string ObterProximoCodigoBarras()
        {
            string query = "SELECT MAX(codigo_barras) FROM tb_produtos WHERE codigo_barras LIKE 'GERADO%'";
            int proximoNumero = 1; // Valor inicial caso a tabela esteja vazia ou não haja códigos "GERADO"

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    try
                    {
                        conexao.Open();
                        object resultado = comando.ExecuteScalar();

                        if (resultado != null && resultado != DBNull.Value)
                        {
                            string ultimoCodigo = resultado.ToString();
                            // Extrai a parte numérica do código (ex: "GERADO010" -> "010")
                            string numeroStr = new string(ultimoCodigo.Where(char.IsDigit).ToArray());

                            if (int.TryParse(numeroStr, out int ultimoNumero))
                            {
                                proximoNumero = ultimoNumero + 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro, apenas lança a exceção para ser tratada no método chamador
                        throw new Exception("Erro ao obter o próximo código de barras gerado: " + ex.Message, ex);
                    }
                }
            }

            // Formata o novo código para o padrão "GERADO0XX"
            return $"GERADO{proximoNumero:D3}";
        }
    }
}
