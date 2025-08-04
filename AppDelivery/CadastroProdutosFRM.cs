using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // Essencial para comunicação com SQL Server
using System.Configuration; // Necessário para ler a string de conexão do App.config
using System.Globalization; // Para formatação de moeda, se necessário

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
            this.Load += new EventHandler(CadastroProdutosFrm_Load); // Garante que o manipulador de evento Load esteja aqui
        }

        // Evento Load do Formulário
        private void CadastroProdutosFrm_Load(object sender, EventArgs e)
        {
            ConfigurarComboBoxProdutoInativo();
            ConfigurarComboBoxTipoProduto(); // Se você tiver um ComboBox para Tipo de Produto

            if (_modoEdicao)
            {
                this.Text = "Editar Produto"; // Muda o título do formulário
                CarregarDadosProduto();       // Carrega os dados do produto para edição
            }
            else
            {
                this.Text = "Novo Produto"; // Mantém o título para cadastro
                // Para o campo ID, no modo de cadastro, ele pode ser desabilitado ou apenas não preenchido
                txtIDProduto.Enabled = false; // ID é auto incremento, o usuário não deve digitá-lo
                txtIDProduto.Text = "Novo (Automático)"; // Sugestão de texto
            }

            // O campo txtCodigoInterno é auto incremento, mas o usuário pode alterar.
            // No modo de cadastro, ele pode ser gerado ou deixado em branco para o usuário digitar.
            // Por enquanto, vamos deixar a lógica de geração/incremento para o banco de dados.
            // Se precisar gerar no C#, faremos um método para isso.
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

        // TODO: Implementar este método para carregar os tipos de produto do banco de dados (se for o caso)
        // Ou, se for uma lista fixa, configurar aqui.
        private void ConfigurarComboBoxTipoProduto()
        {
            // Exemplo de configuração fixa:
            cmbTipoProduto.Items.Clear(); // Adicionado para garantir limpeza
            cmbTipoProduto.Items.Add("Unitário");
            cmbTipoProduto.Items.Add("Inteiro");
            //cmbTipoProduto.Items.Add("");
            if (!_modoEdicao) // Define padrão apenas no modo de cadastro
            {
                cmbTipoProduto.SelectedIndex = 0; // Define um padrão se não for edição
            }

            // Se for do banco, a lógica seria similar ao CadastroClienteFRM
            // Faremos isso em um próximo passo se for necessário carregar do DB.
        }


        private void bntCancelarProduto_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblCodBarras_Click(object sender, EventArgs e)
        {
            // Evento de clique no label (geralmente não usado para lógica)
        }

        // =========================================================
        // MÉTODO PRINCIPAL: btnSalvarProduto_Click
        // Este método é executado quando o usuário clica no botão "Salvar".
        // Ele lida tanto com INSERÇÃO quanto com EDIÇÃO de produtos.
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

            // Determinar o valor 'inativo' para o banco de dados (A para Ativo, I para Inativo)
            string statusInativo = (cmbProdutoInativo.SelectedItem != null && cmbProdutoInativo.SelectedItem.ToString() == "Inativo") ? "I" : "A";

            // Definir a query SQL com base no modo (INSERT ou UPDATE)
            string query = "";
            int idProdutoSalvo = _idProdutoParaEditar; // Para o modo de edição, já temos o ID

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
                        "tipo_produto = @tipo_produto, " + // Assumindo que este campo existe
                        "inativo = @inativo " +
                        "WHERE id_produto = @id_produto"; // Condição WHERE para atualização
            }
            else
            {
                // Query para inserir um novo produto
                // Adicionado "SELECT SCOPE_IDENTITY()" para obter o ID do produto recém-inserido
                query = "INSERT INTO tb_produtos " +
                        "(codigo_interno, codigo_barras, nome, preco, custo, unidade_medida, grupo, tipo_produto, inativo) " +
                        "VALUES " +
                        "(@codigo_interno, @codigo_barras, @nome, @preco, @custo, @unidade_medida, @grupo, @tipo_produto, @inativo);" +
                        "SELECT SCOPE_IDENTITY();"; // Retorna o ID do último registro inserido
            }

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    // Adicionar Parâmetros à Query
                    // Para txtIDProduto, não adicionamos como parâmetro no INSERT (auto incremento)
                    // No UPDATE, ele será usado no WHERE.
                    if (_modoEdicao)
                    {
                        comando.Parameters.AddWithValue("@id_produto", _idProdutoParaEditar);
                    }

                    // Se txtCodigoInterno estiver vazio no cadastro, passamos DBNull.Value
                    // para que o banco gere um. Se estiver preenchido, passamos o valor.
                    comando.Parameters.AddWithValue("@codigo_interno", string.IsNullOrWhiteSpace(txtCodigoInterno.Text) ? (object)DBNull.Value : txtCodigoInterno.Text);
                    comando.Parameters.AddWithValue("@codigo_barras", string.IsNullOrWhiteSpace(txtCodigoBarras.Text) ? (object)DBNull.Value : txtCodigoBarras.Text);
                    comando.Parameters.AddWithValue("@nome", txtNomeProduto.Text);
                    comando.Parameters.AddWithValue("@preco", preco); // Já validado como decimal
                    comando.Parameters.AddWithValue("@custo", custo); // Já validado como decimal
                    comando.Parameters.AddWithValue("@unidade_medida", string.IsNullOrWhiteSpace(txtUnidadeMedida.Text) ? (object)DBNull.Value : txtUnidadeMedida.Text);
                    comando.Parameters.AddWithValue("@grupo", string.IsNullOrWhiteSpace(txtGrupo.Text) ? (object)DBNull.Value : txtGrupo.Text);

                    // TODO: Para Tipo Produto, você precisará adaptar conforme ele for carregado (ex: ID do tipo)
                    // Por enquanto, vou assumir que é o texto selecionado, mas pode ser um ID.
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
                                idProdutoSalvo = Convert.ToInt32(result);
                                MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearForm(); // Limpa todos os campos do formulário após o sucesso do cadastro
                                // Se você quiser fechar o form após o cadastro, use this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Nenhum produto foi cadastrado. Verifique os dados e tente novamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Erro ao " + (_modoEdicao ? "atualizar" : "cadastrar") + " produto no banco de dados:\n" + ex.Message + "\n\nCódigo do Erro: " + ex.ErrorCode, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro inesperado ao " + (_modoEdicao ? "atualizar" : "cadastrar") + " o produto:\n" + ex.Message, "Erro Genérico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // =========================================================
        // MÉTODO AUXILIAR: ClearForm()
        // Usado para limpar todos os campos do formulário.
        // =========================================================
        private void ClearForm()
        {
            txtIDProduto.Text = "Novo (Automático)"; // ID auto incremento
            txtCodigoInterno.Text = string.Empty;
            txtCodigoBarras.Text = string.Empty;
            txtNomeProduto.Text = string.Empty;
            txtPreco.Text = string.Empty;
            txtCusto.Text = string.Empty;
            txtUnidadeMedida.Text = string.Empty;
            txtGrupo.Text = string.Empty;
            cmbProdutoInativo.SelectedIndex = 0; // Volta para "Ativo"
            // cmbTipoProduto.SelectedIndex = 0; // Se tiver um padrão

            txtNomeProduto.Focus(); // Coloca o foco no campo Nome, pronto para um novo cadastro
        }

        // NOVO MÉTODO: Carrega os dados do produto no formulário para edição
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
                            // Preencher os campos com os dados do banco de dados
                            txtIDProduto.Text = reader["id_produto"].ToString();
                            txtIDProduto.Enabled = false; // Desabilita edição do ID

                            // Código Interno (pode ser nulo no banco, mas editável)
                            txtCodigoInterno.Text = reader.IsDBNull(reader.GetOrdinal("codigo_interno")) ? string.Empty : reader["codigo_interno"].ToString();
                            txtCodigoBarras.Text = reader.IsDBNull(reader.GetOrdinal("codigo_barras")) ? string.Empty : reader["codigo_barras"].ToString();
                            txtNomeProduto.Text = reader["nome"].ToString();

                            // Preço e Custo (formatar para moeda local se necessário, use CultureInfo.CurrentCulture)
                            txtPreco.Text = reader["preco"].ToString(); // Pode precisar de formatação específica, ex: .ToString("C2", CultureInfo.CurrentCulture);
                            txtCusto.Text = reader["custo"].ToString(); // Pode precisar de formatação específica

                            txtUnidadeMedida.Text = reader.IsDBNull(reader.GetOrdinal("unidade_medida")) ? string.Empty : reader["unidade_medida"].ToString();
                            txtGrupo.Text = reader.IsDBNull(reader.GetOrdinal("grupo")) ? string.Empty : reader["grupo"].ToString();

                            // Tipo Produto
                            string tipoProdutoDB = reader.IsDBNull(reader.GetOrdinal("tipo_produto")) ? string.Empty : reader["tipo_produto"].ToString();
                            if (!string.IsNullOrEmpty(tipoProdutoDB))
                            {
                                // Tenta encontrar o item pelo texto e selecioná-lo
                                int index = cmbTipoProduto.FindStringExact(tipoProdutoDB);
                                if (index != -1)
                                {
                                    cmbTipoProduto.SelectedIndex = index;
                                }
                                else
                                {
                                    cmbTipoProduto.SelectedIndex = -1; // Nenhuma seleção se não encontrar
                                }
                            }
                            else
                            {
                                cmbTipoProduto.SelectedIndex = -1; // Nenhuma seleção
                            }

                            // Ativo/Inativo
                            string inativoDB = reader["inativo"].ToString();
                            if (inativoDB == "I")
                            {
                                cmbProdutoInativo.SelectedItem = "Inativo";
                            }
                            else
                            {
                                cmbProdutoInativo.SelectedItem = "Ativo";
                            }

                            reader.Close(); // Fechar o reader antes de chamar outro método que acessa o DB
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
    }
}