using System;
using System.Data;
using Microsoft.Data.SqlClient; // Essencial para comunicação com SQL Server
using System.Windows.Forms;
using System.Configuration; // Necessário para ler a string de conexão do App.config
using System.Globalization;

namespace AppDelivery
{
    public partial class CadastroClienteFRM : Form
    {
        private string connectionString;
        private int _codClienteParaEditar = 0; // Armazena o código do cliente para edição
        private bool _modoEdicao = false;      // Sinaliza se estamos no modo de edição

        public CadastroClienteFRM()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            this.Load += new EventHandler(CadastroClienteFRM_Load);
            // No modo de cadastro, os campos já começam vazios, o que é o comportamento desejado.
        }

        // Construtor para MODO DE EDIÇÃO
        public CadastroClienteFRM(int codCliente)
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            _codClienteParaEditar = codCliente; // Atribui o valor passado
            _modoEdicao = true;                 // Sinaliza o modo de edição
            this.Load += new EventHandler(CadastroClienteFRM_Load); // Garante que o manipulador de evento Load esteja aqui
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =========================================================
        // MÉTODO PRINCIPAL: btnSalvar_Click
        // Este método é executado quando o usuário clica no botão "Salvar".
        // Agora, ele lida tanto com INSERÇÃO quanto com EDIÇÃO.
        // =========================================================
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // 1. Validação dos Dados Essenciais (AGORA TODOS OBRIGATÓRIOS)
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O campo 'Nome' é obrigatório. Por favor, preencha-o.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCpfCnpj.Text))
            {
                MessageBox.Show("O campo 'CPF/CNPJ' é obrigatório. Por favor, preencha-o.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCpfCnpj.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEndereco.Text))
            {
                MessageBox.Show("O campo 'Endereço' é obrigatório. Por favor, preencha-o.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEndereco.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("O campo 'Número' é obrigatório. Por favor, preencha-o.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBairro.Text))
            {
                MessageBox.Show("O campo 'Bairro' é obrigatório. Por favor, preencha-o.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBairro.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("O campo 'Telefone' é obrigatório. Por favor, preencha-o.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefone.Focus();
                return;
            }

            // Processar CPF/CNPJ: Remover caracteres de máscara
            string cpfCnpjLimpo = string.IsNullOrWhiteSpace(txtCpfCnpj.Text) ? null : txtCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");

            // Processar Telefone: Remover caracteres de máscara
            string telefoneLimpo = string.IsNullOrWhiteSpace(txtTelefone.Text) ? null : txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Replace("_", "");

            // Processar Tipo Pessoa: Converter para 0 (Física) ou 1 (Jurídica)
            int? tipoPessoaValor = null;
            if (cmbTipoPessoa.SelectedItem != null)
            {
                string selectedText = cmbTipoPessoa.SelectedItem.ToString();
                if (selectedText.Equals("Física", StringComparison.OrdinalIgnoreCase))
                {
                    tipoPessoaValor = 0;
                }
                else if (selectedText.Equals("Jurídica", StringComparison.OrdinalIgnoreCase))
                {
                    tipoPessoaValor = 1;
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione o 'Tipo de Pessoa'.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoPessoa.Focus();
                return;
            }

            // Definir a query SQL com base no modo (INSERT ou UPDATE)
            string query = "";
            if (_modoEdicao)
            {
                // Query para atualizar um cliente existente
                query = "UPDATE tb_clientes SET " +
                        "nome_cliente = @nome_cliente, " +
                        "cpf_cnpj = @cpf_cnpj, " +
                        "endereco = @endereco, " +
                        "numero = @numero, " +
                        "bairro = @bairro, " +
                        "telefone = @telefone, " +
                        "tipo_pessoa = @tipo_pessoa, " +
                        "complemento = @complemento " +
                        "WHERE cod_cliente = @cod_cliente"; // Condição WHERE para atualização
            }
            else
            {
                // Query para inserir um novo cliente
                query = "INSERT INTO tb_clientes " +
                        "(nome_cliente, cpf_cnpj, endereco, numero, bairro, telefone, tipo_pessoa, complemento) " +
                        "VALUES " +
                        "(@nome_cliente, @cpf_cnpj, @endereco, @numero, @bairro, @telefone, @tipo_pessoa, @complemento)";
            }

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    // Adicionar Parâmetros à Query
                    comando.Parameters.AddWithValue("@nome_cliente", txtNome.Text);
                    comando.Parameters.AddWithValue("@cpf_cnpj", string.IsNullOrWhiteSpace(cpfCnpjLimpo) ? (object)DBNull.Value : cpfCnpjLimpo);
                    comando.Parameters.AddWithValue("@endereco", string.IsNullOrWhiteSpace(txtEndereco.Text) ? (object)DBNull.Value : txtEndereco.Text);
                    comando.Parameters.AddWithValue("@numero", string.IsNullOrWhiteSpace(txtNumero.Text) ? (object)DBNull.Value : txtNumero.Text);
                    comando.Parameters.AddWithValue("@bairro", string.IsNullOrWhiteSpace(txtBairro.Text) ? (object)DBNull.Value : txtBairro.Text);
                    comando.Parameters.AddWithValue("@telefone", string.IsNullOrWhiteSpace(telefoneLimpo) ? (object)DBNull.Value : telefoneLimpo);
                    comando.Parameters.AddWithValue("@tipo_pessoa", tipoPessoaValor.HasValue ? (object)tipoPessoaValor.Value : DBNull.Value);
                    comando.Parameters.AddWithValue("@complemento", txtComplemento.Text);

                    if (_modoEdicao)
                    {
                        comando.Parameters.AddWithValue("@cod_cliente", _codClienteParaEditar); // Parâmetro essencial para o UPDATE
                    }

                    try
                    {
                        conexao.Open();
                        int linhasAfetadas = comando.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            if (_modoEdicao)
                            {
                                MessageBox.Show("Cliente atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.DialogResult = DialogResult.OK; // Indica sucesso para o formulário chamador
                                this.Close(); // Fecha o formulário de edição após a atualização
                            }
                            else
                            {
                                MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                                //ClearForm(); // Limpa todos os campos do formulário após o sucesso do cadastro
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nenhum cliente foi " + (_modoEdicao ? "atualizado" : "cadastrado") + ". Verifique os dados e tente novamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Erro ao " + (_modoEdicao ? "atualizar" : "cadastrar") + " cliente no banco de dados:\n" + ex.Message + "\n\nCódigo do Erro: " + ex.ErrorCode, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro inesperado ao " + (_modoEdicao ? "atualizar" : "cadastrar") + " o cliente:\n" + ex.Message, "Erro Genérico", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtNome.Text = string.Empty;
            txtCpfCnpj.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtComplemento.Text = string.Empty;
            // Garante que o ComboBox retorne ao padrão "Física" ao limpar
            cmbTipoPessoa.SelectedIndex = 0;

            txtNome.Focus(); // Coloca o foco no campo Nome, pronto para um novo cadastro
        }

        // =========================================================
        // MÉTODO PARA btnCancelar_Click
        // Este método é executado quando o usuário clica no botão "Cancelar".
        // Conforme sua especificação, ele apenas fecha o formulário.
        // =========================================================
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Fecha a instância atual do formulário
        }

        // =========================================================
        // MÉTODO PARA btnClear_Click (se você tiver um botão "Limpar")
        // Este método é executado quando o usuário clica no botão "Limpar".
        // =========================================================
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm(); // Chama o método auxiliar para limpar os campos
        }

        private void CadastroClienteFRM_Load(object sender, EventArgs e)
        {
            ConfigurarComboBoxTipoPessoa(); // Sempre configura o ComboBox

            if (_modoEdicao)
            {
                this.Text = "Editar Cliente"; // Muda o título do formulário
                CarregarDadosCliente(); // Carrega os dados do cliente para edição
            }
            else
            {
                this.Text = "Novo Cliente"; // Mantém o título para cadastro
            }
        }

        // NOVO MÉTODO: Carrega os dados do cliente no formulário para edição
        private void CarregarDadosCliente()
        {
            if (_codClienteParaEditar <= 0)
            {
                MessageBox.Show("ID do cliente para edição inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            string query = "SELECT cod_cliente, nome_cliente, cpf_cnpj, endereco, numero, bairro, telefone, tipo_pessoa, complemento " +
                           "FROM tb_clientes WHERE cod_cliente = @cod_cliente";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@cod_cliente", _codClienteParaEditar);

                    try
                    {
                        conexao.Open();
                        SqlDataReader reader = comando.ExecuteReader();

                        if (reader.Read())
                        {
                            // Preencher os campos com os dados do banco de dados
                            txtCodCli.Text = reader["cod_cliente"].ToString();
                            txtNome.Text = reader["nome_cliente"].ToString();
                            txtEndereco.Text = reader["endereco"].ToString();
                            txtNumero.Text = reader["numero"].ToString();
                            txtBairro.Text = reader["bairro"].ToString();
                            txtComplemento.Text = reader["complemento"].ToString();

                            // CPF/CNPJ: Aplicar a máscara corretamente
                            string cpfCnpjDB = reader["cpf_cnpj"].ToString();
                            int tipoPessoaDB = reader.IsDBNull(reader.GetOrdinal("tipo_pessoa")) ? -1 : Convert.ToInt32(reader["tipo_pessoa"]);

                            if (tipoPessoaDB == 0) // Física
                            {
                                cmbTipoPessoa.SelectedItem = "Física";
                                txtCpfCnpj.Mask = "000\\.000\\.000-00";
                                // Formatar o CPF/CNPJ lido do banco para a máscara
                                if (cpfCnpjDB.Length == 11)
                                {
                                    txtCpfCnpj.Text = Convert.ToInt64(cpfCnpjDB).ToString("000\\.000\\.000-00");
                                }
                                else
                                {
                                    txtCpfCnpj.Text = cpfCnpjDB; // Se não for 11 dígitos, apenas exibe como está
                                }
                            }
                            else if (tipoPessoaDB == 1) // Jurídica
                            {
                                cmbTipoPessoa.SelectedItem = "Jurídica";
                                txtCpfCnpj.Mask = "00\\.000\\.000/0000-00";
                                // Formatar o CPF/CNPJ lido do banco para a máscara
                                if (cpfCnpjDB.Length == 14)
                                {
                                    txtCpfCnpj.Text = Convert.ToInt64(cpfCnpjDB).ToString("00\\.000\\.000/0000-00");
                                }
                                else
                                {
                                    txtCpfCnpj.Text = cpfCnpjDB; // Se não for 14 dígitos, apenas exibe como está
                                }
                            }
                            else
                            {
                                cmbTipoPessoa.SelectedIndex = -1; // Nenhuma seleção se o tipo for desconhecido/null
                                txtCpfCnpj.Text = cpfCnpjDB; // Exibe o valor sem máscara se o tipo for inválido
                                txtCpfCnpj.Mask = ""; // Remove a máscara
                            }

                            // Telefone: Aplicar a máscara corretamente
                            string telefoneDB = reader["telefone"].ToString();
                            if (!string.IsNullOrEmpty(telefoneDB))
                            {
                                if (telefoneDB.Length == 10) // Telefone fixo
                                {
                                    txtTelefone.Text = string.Format("({0:00}){1:0000}-{2:0000}",
                                                                     Convert.ToInt64(telefoneDB) / 100000000,
                                                                     (Convert.ToInt64(telefoneDB) / 10000) % 10000,
                                                                     Convert.ToInt64(telefoneDB) % 10000);
                                }
                                else if (telefoneDB.Length == 11) // Telefone celular
                                {
                                    txtTelefone.Text = string.Format("({0:00}){1:00000}-{2:0000}",
                                                                     Convert.ToInt64(telefoneDB) / 1000000000,
                                                                     (Convert.ToInt64(telefoneDB) / 10000) % 100000,
                                                                     Convert.ToInt64(telefoneDB) % 10000);
                                }
                                else
                                {
                                    txtTelefone.Text = telefoneDB; // Caso o formato não seja padrão
                                }
                            }
                            else
                            {
                                txtTelefone.Text = string.Empty;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cliente não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Close();
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Erro ao carregar dados do cliente do banco de dados:\n" + ex.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro inesperado ao carregar os dados do cliente:\n" + ex.Message, "Erro Genérico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
        }

        private void ConfigurarComboBoxTipoPessoa()
        {
            cmbTipoPessoa.Items.Clear();
            cmbTipoPessoa.Items.Add("Física");
            cmbTipoPessoa.Items.Add("Jurídica");

            // Define "Física" como padrão apenas se não estiver no modo de edição e nenhum item estiver selecionado
            if (!_modoEdicao || cmbTipoPessoa.SelectedIndex == -1)
            {
                cmbTipoPessoa.SelectedIndex = 0;
            }
        }

        private void cmbTipoPessoa_SelectedValueChanged(object sender, EventArgs e)
        {
            // Limpa o texto atual para evitar que caracteres da máscara antiga
            // fiquem no campo ao mudar para a nova máscara.
            txtCpfCnpj.Text = string.Empty;
            // Opcional: Limpa também o texto formatado para garantir uma "limpeza" completa
            txtCpfCnpj.Clear();

            // Verifica qual opção foi selecionada no ComboBox
            if (cmbTipoPessoa.SelectedIndex == 0) // "Física" selecionado
            {
                // Define a máscara para CPF: ###.###.###-##
                txtCpfCnpj.Mask = "000\\.000\\.000-00";
                txtCpfCnpj.PromptChar = '_';
                txtCpfCnpj.HidePromptOnLeave = false;
            }
            else if (cmbTipoPessoa.SelectedIndex == 1) // "Jurídica" selecionado
            {
                // Define a máscara para CNPJ: ##.###.###/####-##
                txtCpfCnpj.Mask = "00\\.000\\.000/0000-00";
                txtCpfCnpj.PromptChar = '_';
                txtCpfCnpj.HidePromptOnLeave = false;
            }
        }

        private void txtCpfCnpj_Leave(object sender, EventArgs e)
        {
            if (cmbTipoPessoa.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, selecione o 'Tipo de Pessoa' (Física ou Jurídica) antes de preencher o CPF/CNPJ.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCpfCnpj.Clear();
                cmbTipoPessoa.Focus();
                return;
            }

            string cpfCnpjLimpo = txtCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");

            if (string.IsNullOrWhiteSpace(cpfCnpjLimpo))
            {
                return;
            }

            if (cmbTipoPessoa.SelectedIndex == 0) // "Física"
            {
                if (cpfCnpjLimpo.Length != 11)
                {
                    MessageBox.Show("O CPF deve conter exatamente 11 dígitos.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                    return;
                }

                if (!CpfValidator.IsCpfValid(cpfCnpjLimpo))
                {
                    MessageBox.Show("CPF inválido. Por favor, verifique o número digitado.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                }
            }
            else if (cmbTipoPessoa.SelectedIndex == 1) // "Jurídica"
            {
                if (cpfCnpjLimpo.Length != 14)
                {
                    MessageBox.Show("O CNPJ deve conter exatamente 14 dígitos.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                    return;
                }

                if (!CnpjValidator.IsCnpjValid(cpfCnpjLimpo))
                {
                    MessageBox.Show("CNPJ inválido. Por favor, verifique o número digitado.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                }
            }
        }

        private void txtTelefone_Leave(object sender, EventArgs e)
        {
            string telefone = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Replace("_", ""); // Adicionado .Replace("_", "")

            if (string.IsNullOrWhiteSpace(telefone))
            {
                txtTelefone.Text = string.Empty; // Garante que o campo fique vazio se o usuário apagar tudo
                return;
            }

            if (telefone.Length == 10) // Telefone fixo
            {
                if (long.TryParse(telefone, out long num))
                {
                    txtTelefone.Text = string.Format("({0:00}){1:0000}-{2:0000}",
                                                     num / 100000000,
                                                     (num / 10000) % 10000,
                                                     num % 10000);
                }
            }
            else if (telefone.Length == 11) // Telefone celular
            {
                if (long.TryParse(telefone, out long num))
                {
                    txtTelefone.Text = string.Format("({0:00}){1:00000}-{2:0000}",
                                                     num / 1000000000,
                                                     (num / 10000) % 100000,
                                                     num % 10000);
                }
            }
            else
            {
                MessageBox.Show("Número de telefone inválido. Por favor, insira um DDD + 8 ou 9 dígitos.", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefone.Focus();
            }
        }
    }
}