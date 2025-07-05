using System;
using System.Data;
using Microsoft.Data.SqlClient; // Essencial para comunica��o com SQL Server
using System.Windows.Forms;
using System.Configuration; // Necess�rio para ler a string de conex�o do App.config
using System.Globalization;

namespace AppDelivery
{
    public partial class CadastroClienteFRM : Form
    {
        private string connectionString;
        private int _codClienteParaEditar = 0; // Armazena o c�digo do cliente para edi��o
        private bool _modoEdicao = false;      // Sinaliza se estamos no modo de edi��o

        public CadastroClienteFRM()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            this.Load += new EventHandler(CadastroClienteFRM_Load);
            // No modo de cadastro, os campos j� come�am vazios, o que � o comportamento desejado.
        }

        // Construtor para MODO DE EDI��O
        public CadastroClienteFRM(int codCliente)
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            _codClienteParaEditar = codCliente; // Atribui o valor passado
            _modoEdicao = true;                 // Sinaliza o modo de edi��o
            this.Load += new EventHandler(CadastroClienteFRM_Load); // Garante que o manipulador de evento Load esteja aqui
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =========================================================
        // M�TODO PRINCIPAL: btnSalvar_Click
        // Este m�todo � executado quando o usu�rio clica no bot�o "Salvar".
        // Agora, ele lida tanto com INSER��O quanto com EDI��O.
        // =========================================================
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // 1. Valida��o dos Dados Essenciais (AGORA TODOS OBRIGAT�RIOS)
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O campo 'Nome' � obrigat�rio. Por favor, preencha-o.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCpfCnpj.Text))
            {
                MessageBox.Show("O campo 'CPF/CNPJ' � obrigat�rio. Por favor, preencha-o.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCpfCnpj.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEndereco.Text))
            {
                MessageBox.Show("O campo 'Endere�o' � obrigat�rio. Por favor, preencha-o.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEndereco.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("O campo 'N�mero' � obrigat�rio. Por favor, preencha-o.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBairro.Text))
            {
                MessageBox.Show("O campo 'Bairro' � obrigat�rio. Por favor, preencha-o.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBairro.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("O campo 'Telefone' � obrigat�rio. Por favor, preencha-o.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefone.Focus();
                return;
            }

            // Processar CPF/CNPJ: Remover caracteres de m�scara
            string cpfCnpjLimpo = string.IsNullOrWhiteSpace(txtCpfCnpj.Text) ? null : txtCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");

            // Processar Telefone: Remover caracteres de m�scara
            string telefoneLimpo = string.IsNullOrWhiteSpace(txtTelefone.Text) ? null : txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Replace("_", "");

            // Processar Tipo Pessoa: Converter para 0 (F�sica) ou 1 (Jur�dica)
            int? tipoPessoaValor = null;
            if (cmbTipoPessoa.SelectedItem != null)
            {
                string selectedText = cmbTipoPessoa.SelectedItem.ToString();
                if (selectedText.Equals("F�sica", StringComparison.OrdinalIgnoreCase))
                {
                    tipoPessoaValor = 0;
                }
                else if (selectedText.Equals("Jur�dica", StringComparison.OrdinalIgnoreCase))
                {
                    tipoPessoaValor = 1;
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione o 'Tipo de Pessoa'.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        "WHERE cod_cliente = @cod_cliente"; // Condi��o WHERE para atualiza��o
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
                    // Adicionar Par�metros � Query
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
                        comando.Parameters.AddWithValue("@cod_cliente", _codClienteParaEditar); // Par�metro essencial para o UPDATE
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
                                this.DialogResult = DialogResult.OK; // Indica sucesso para o formul�rio chamador
                                this.Close(); // Fecha o formul�rio de edi��o ap�s a atualiza��o
                            }
                            else
                            {
                                MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                                //ClearForm(); // Limpa todos os campos do formul�rio ap�s o sucesso do cadastro
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nenhum cliente foi " + (_modoEdicao ? "atualizado" : "cadastrado") + ". Verifique os dados e tente novamente.", "Aten��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Erro ao " + (_modoEdicao ? "atualizar" : "cadastrar") + " cliente no banco de dados:\n" + ex.Message + "\n\nC�digo do Erro: " + ex.ErrorCode, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro inesperado ao " + (_modoEdicao ? "atualizar" : "cadastrar") + " o cliente:\n" + ex.Message, "Erro Gen�rico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // =========================================================
        // M�TODO AUXILIAR: ClearForm()
        // Usado para limpar todos os campos do formul�rio.
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
            // Garante que o ComboBox retorne ao padr�o "F�sica" ao limpar
            cmbTipoPessoa.SelectedIndex = 0;

            txtNome.Focus(); // Coloca o foco no campo Nome, pronto para um novo cadastro
        }

        // =========================================================
        // M�TODO PARA btnCancelar_Click
        // Este m�todo � executado quando o usu�rio clica no bot�o "Cancelar".
        // Conforme sua especifica��o, ele apenas fecha o formul�rio.
        // =========================================================
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Fecha a inst�ncia atual do formul�rio
        }

        // =========================================================
        // M�TODO PARA btnClear_Click (se voc� tiver um bot�o "Limpar")
        // Este m�todo � executado quando o usu�rio clica no bot�o "Limpar".
        // =========================================================
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm(); // Chama o m�todo auxiliar para limpar os campos
        }

        private void CadastroClienteFRM_Load(object sender, EventArgs e)
        {
            ConfigurarComboBoxTipoPessoa(); // Sempre configura o ComboBox

            if (_modoEdicao)
            {
                this.Text = "Editar Cliente"; // Muda o t�tulo do formul�rio
                CarregarDadosCliente(); // Carrega os dados do cliente para edi��o
            }
            else
            {
                this.Text = "Novo Cliente"; // Mant�m o t�tulo para cadastro
            }
        }

        // NOVO M�TODO: Carrega os dados do cliente no formul�rio para edi��o
        private void CarregarDadosCliente()
        {
            if (_codClienteParaEditar <= 0)
            {
                MessageBox.Show("ID do cliente para edi��o inv�lido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                            // CPF/CNPJ: Aplicar a m�scara corretamente
                            string cpfCnpjDB = reader["cpf_cnpj"].ToString();
                            int tipoPessoaDB = reader.IsDBNull(reader.GetOrdinal("tipo_pessoa")) ? -1 : Convert.ToInt32(reader["tipo_pessoa"]);

                            if (tipoPessoaDB == 0) // F�sica
                            {
                                cmbTipoPessoa.SelectedItem = "F�sica";
                                txtCpfCnpj.Mask = "000\\.000\\.000-00";
                                // Formatar o CPF/CNPJ lido do banco para a m�scara
                                if (cpfCnpjDB.Length == 11)
                                {
                                    txtCpfCnpj.Text = Convert.ToInt64(cpfCnpjDB).ToString("000\\.000\\.000-00");
                                }
                                else
                                {
                                    txtCpfCnpj.Text = cpfCnpjDB; // Se n�o for 11 d�gitos, apenas exibe como est�
                                }
                            }
                            else if (tipoPessoaDB == 1) // Jur�dica
                            {
                                cmbTipoPessoa.SelectedItem = "Jur�dica";
                                txtCpfCnpj.Mask = "00\\.000\\.000/0000-00";
                                // Formatar o CPF/CNPJ lido do banco para a m�scara
                                if (cpfCnpjDB.Length == 14)
                                {
                                    txtCpfCnpj.Text = Convert.ToInt64(cpfCnpjDB).ToString("00\\.000\\.000/0000-00");
                                }
                                else
                                {
                                    txtCpfCnpj.Text = cpfCnpjDB; // Se n�o for 14 d�gitos, apenas exibe como est�
                                }
                            }
                            else
                            {
                                cmbTipoPessoa.SelectedIndex = -1; // Nenhuma sele��o se o tipo for desconhecido/null
                                txtCpfCnpj.Text = cpfCnpjDB; // Exibe o valor sem m�scara se o tipo for inv�lido
                                txtCpfCnpj.Mask = ""; // Remove a m�scara
                            }

                            // Telefone: Aplicar a m�scara corretamente
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
                                    txtTelefone.Text = telefoneDB; // Caso o formato n�o seja padr�o
                                }
                            }
                            else
                            {
                                txtTelefone.Text = string.Empty;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cliente n�o encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        MessageBox.Show("Ocorreu um erro inesperado ao carregar os dados do cliente:\n" + ex.Message, "Erro Gen�rico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
        }

        private void ConfigurarComboBoxTipoPessoa()
        {
            cmbTipoPessoa.Items.Clear();
            cmbTipoPessoa.Items.Add("F�sica");
            cmbTipoPessoa.Items.Add("Jur�dica");

            // Define "F�sica" como padr�o apenas se n�o estiver no modo de edi��o e nenhum item estiver selecionado
            if (!_modoEdicao || cmbTipoPessoa.SelectedIndex == -1)
            {
                cmbTipoPessoa.SelectedIndex = 0;
            }
        }

        private void cmbTipoPessoa_SelectedValueChanged(object sender, EventArgs e)
        {
            // Limpa o texto atual para evitar que caracteres da m�scara antiga
            // fiquem no campo ao mudar para a nova m�scara.
            txtCpfCnpj.Text = string.Empty;
            // Opcional: Limpa tamb�m o texto formatado para garantir uma "limpeza" completa
            txtCpfCnpj.Clear();

            // Verifica qual op��o foi selecionada no ComboBox
            if (cmbTipoPessoa.SelectedIndex == 0) // "F�sica" selecionado
            {
                // Define a m�scara para CPF: ###.###.###-##
                txtCpfCnpj.Mask = "000\\.000\\.000-00";
                txtCpfCnpj.PromptChar = '_';
                txtCpfCnpj.HidePromptOnLeave = false;
            }
            else if (cmbTipoPessoa.SelectedIndex == 1) // "Jur�dica" selecionado
            {
                // Define a m�scara para CNPJ: ##.###.###/####-##
                txtCpfCnpj.Mask = "00\\.000\\.000/0000-00";
                txtCpfCnpj.PromptChar = '_';
                txtCpfCnpj.HidePromptOnLeave = false;
            }
        }

        private void txtCpfCnpj_Leave(object sender, EventArgs e)
        {
            if (cmbTipoPessoa.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, selecione o 'Tipo de Pessoa' (F�sica ou Jur�dica) antes de preencher o CPF/CNPJ.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCpfCnpj.Clear();
                cmbTipoPessoa.Focus();
                return;
            }

            string cpfCnpjLimpo = txtCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");

            if (string.IsNullOrWhiteSpace(cpfCnpjLimpo))
            {
                return;
            }

            if (cmbTipoPessoa.SelectedIndex == 0) // "F�sica"
            {
                if (cpfCnpjLimpo.Length != 11)
                {
                    MessageBox.Show("O CPF deve conter exatamente 11 d�gitos.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                    return;
                }

                if (!CpfValidator.IsCpfValid(cpfCnpjLimpo))
                {
                    MessageBox.Show("CPF inv�lido. Por favor, verifique o n�mero digitado.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                }
            }
            else if (cmbTipoPessoa.SelectedIndex == 1) // "Jur�dica"
            {
                if (cpfCnpjLimpo.Length != 14)
                {
                    MessageBox.Show("O CNPJ deve conter exatamente 14 d�gitos.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                    return;
                }

                if (!CnpjValidator.IsCnpjValid(cpfCnpjLimpo))
                {
                    MessageBox.Show("CNPJ inv�lido. Por favor, verifique o n�mero digitado.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                }
            }
        }

        private void txtTelefone_Leave(object sender, EventArgs e)
        {
            string telefone = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Replace("_", ""); // Adicionado .Replace("_", "")

            if (string.IsNullOrWhiteSpace(telefone))
            {
                txtTelefone.Text = string.Empty; // Garante que o campo fique vazio se o usu�rio apagar tudo
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
                MessageBox.Show("N�mero de telefone inv�lido. Por favor, insira um DDD + 8 ou 9 d�gitos.", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefone.Focus();
            }
        }
    }
}