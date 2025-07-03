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
        public CadastroClienteFRM()
        {
            InitializeComponent();





        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =========================================================
        // MÉTODO PRINCIPAL: btnSalvar_Click
        // Este método é executado quando o usuário clica no botão "Salvar".
        // =========================================================

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // 1. Validação dos Dados Essenciais (AGORA TODOS OBRIGATÓRIOS)
            // Usamos uma abordagem para verificar cada campo e dar um feedback específico.

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

            // ********************************************************************************
            // INÍCIO DAS MUDANÇAS AQUI!
            // ********************************************************************************

            // 1. Processar CPF/CNPJ: Remover caracteres de máscara
            // Usamos 'null' ao invés de string vazia para melhor alinhamento com DBNull.Value
            string cpfCnpjLimpo = string.IsNullOrWhiteSpace(txtCpfCnpj.Text) ? null : txtCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "");

            // 2. Processar Telefone: Remover caracteres de máscara
            string telefoneLimpo = string.IsNullOrWhiteSpace(txtTelefone.Text) ? null : txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

            // 3. Processar Tipo Pessoa: Converter para 0 (Física) ou 1 (Jurídica)
            int? tipoPessoaValor = null; // int? permite que a variável seja null se nenhuma opção válida for selecionada
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
                // Se houver outras opções ou a seleção não for "Física" nem "Jurídica",
                // tipoPessoaValor permanecerá null.
                // Você pode adicionar um else aqui para um tratamento de erro ou valor padrão
                // se a combobox tiver a obrigatoriedade de sempre ter "Física" ou "Jurídica" selecionada.
            }
            else
            {
                // Se nenhuma opção for selecionada na combobox (raro se você tiver um item padrão),
                // você pode adicionar uma mensagem de erro aqui ou deixar tipoPessoaValor como null
                // para que DBNull.Value seja enviado.
                MessageBox.Show("Por favor, selecione o 'Tipo de Pessoa'.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoPessoa.Focus();
                return; // Sai da função se o tipo de pessoa não for selecionado
            }

            // ********************************************************************************
            // FIM DAS MUDANÇAS NA PREPARAÇÃO DOS DADOS
            // ********************************************************************************


            // 2. Obter a String de Conexão do App.config
            // "MinhaConexaoDB" é o 'name' definido no seu arquivo App.config.
            string connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;

            // 3. Definir a Query SQL para Inserção de Dados
            // Usamos nomes de parâmetros (@parametro) para prevenir SQL Injection e lidar com dados especiais.
            // A ordem dos campos na query (tb_clientes) deve corresponder à ordem dos valores.
            string query = "INSERT INTO tb_clientes " +
                            "(nome_cliente, cpf_cnpj, endereco, numero, bairro, telefone, tipo_pessoa, complemento) " +
                            "VALUES " +
                            "(@nome_cliente, @cpf_cnpj, @endereco, @numero, @bairro, @telefone, @tipo_pessoa, @complemento)";

            // 4. Estabelecer Conexão e Executar o Comando SQL
            // O bloco 'using' garante que os objetos SqlConnection e SqlCommand sejam
            // corretamente descartados (fechados e liberados) mesmo em caso de erro.
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    // 5. Adicionar Parâmetros à Query
                    // Cada parâmetro '@nome' na query deve ter um correspondente aqui.
                    // Para campos que podem ser NULL no banco de dados (cpf_cnpj, endereco, etc.),
                    // se o TextBox estiver vazio, passamos DBNull.Value. Caso contrário, passamos o texto do TextBox.
                    comando.Parameters.AddWithValue("@nome_cliente", txtNome.Text);

                    // ********************************************************************************
                    // NOVAS MUDANÇAS: Usando as variáveis limpas e convertidas
                    // ********************************************************************************
                    comando.Parameters.AddWithValue("@cpf_cnpj", string.IsNullOrWhiteSpace(cpfCnpjLimpo) ? (object)DBNull.Value : cpfCnpjLimpo);
                    comando.Parameters.AddWithValue("@endereco", string.IsNullOrWhiteSpace(txtEndereco.Text) ? (object)DBNull.Value : txtEndereco.Text);
                    comando.Parameters.AddWithValue("@numero", string.IsNullOrWhiteSpace(txtNumero.Text) ? (object)DBNull.Value : txtNumero.Text);
                    comando.Parameters.AddWithValue("@bairro", string.IsNullOrWhiteSpace(txtBairro.Text) ? (object)DBNull.Value : txtBairro.Text);
                    comando.Parameters.AddWithValue("@telefone", string.IsNullOrWhiteSpace(telefoneLimpo) ? (object)DBNull.Value : telefoneLimpo);
                    comando.Parameters.AddWithValue("@tipo_pessoa", tipoPessoaValor.HasValue ? (object)tipoPessoaValor.Value : DBNull.Value);
                    comando.Parameters.AddWithValue("@complemento", txtComplemento.Text);
                    // ********************************************************************************
                    // FIM DAS NOVAS MUDANÇAS NOS PARÂMETROS
                    // ********************************************************************************

                    try
                    {
                        conexao.Open(); // Abre a conexão com o banco de dados
                        int linhasAfetadas = comando.ExecuteNonQuery(); // Executa a query INSERT. Retorna o número de linhas afetadas.

                        if (linhasAfetadas > 0)
                        {
                            MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm(); // Limpa todos os campos do formulário após o sucesso
                        }
                        else
                        {
                            MessageBox.Show("Nenhum cliente foi cadastrado. Verifique os dados e tente novamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Captura e trata erros específicos do SQL Server (ex: problemas de conexão, sintaxe SQL inválida, violação de restrição)
                        MessageBox.Show("Erro ao cadastrar cliente no banco de dados:\n" + ex.Message + "\n\nCódigo do Erro: " + ex.ErrorCode, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        // Captura e trata quaisquer outros erros inesperados
                        MessageBox.Show("Ocorreu um erro inesperado ao cadastrar o cliente:\n" + ex.Message, "Erro Genérico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } // SqlCommand 'comando' é automaticamente descartado (Dispose) aqui
            } // SqlConnection 'conexao' é automaticamente descartada (Dispose) aqui
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
            ConfigurarComboBoxTipoPessoa();
        }

        private void ConfigurarComboBoxTipoPessoa()
        {
            // Limpa qualquer item existente (útil se você estiver depurando e o formulário recarregar)
            cmbTipoPessoa.Items.Clear();
            // Adiciona as opções ao ComboBox
            cmbTipoPessoa.Items.Add("Física");
            cmbTipoPessoa.Items.Add("Jurídica");

            // Define a opção "Física" como padrão (selecionada por índice)
            // O índice 0 corresponde ao primeiro item adicionado ("Física")
            cmbTipoPessoa.SelectedIndex = 0;

            // Ou, se você preferir definir por texto (menos robusto se o texto mudar)
            // cmbTipoPessoa.SelectedItem = "Física";
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
                // Define um PromptChar (caractere que indica onde o usuário deve digitar)
                // O padrão é underscore '_', mas você pode usar espaço, etc.
                txtCpfCnpj.PromptChar = '_';
                // Opcional: Se quiser que o campo preencha automaticamente os espaços
                // se o usuário não digitar o CPF completo (recomenda-se false para CPF/CNPJ)
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
            // Primeiro, verifique se o cmbTipoPessoa tem uma seleção.
            // Isso é crucial porque a validação depende do tipo escolhido.
            if (cmbTipoPessoa.SelectedIndex == -1) // Nenhum item selecionado
            {
                MessageBox.Show("Por favor, selecione o 'Tipo de Pessoa' (Física ou Jurídica) antes de preencher o CPF/CNPJ.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCpfCnpj.Clear(); // Limpa o campo, pois não sabemos como validá-lo
                cmbTipoPessoa.Focus();
                return;
            }

            // Pega o texto da masked text box e remove os caracteres da máscara
            // Este passo é essencial para obter apenas os dígitos para validação.
            // Adicionado .Replace("_", "") para remover o PromptChar caso o campo não seja totalmente preenchido.
            string cpfCnpjLimpo = txtCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");

            // Se o campo estiver vazio após a limpeza, e for obrigatório, você pode querer uma mensagem.
            // No entanto, o btnSalvar_Click já lida com campos obrigatórios, então podemos apenas retornar aqui.
            if (string.IsNullOrWhiteSpace(cpfCnpjLimpo))
            {
                return;
            }

            // Determina o tipo de validação com base na seleção do cmbTipoPessoa
            if (cmbTipoPessoa.SelectedIndex == 0) // "Física" selecionado, valida como CPF
            {
                // Verifica o comprimento exato antes de tentar a validação complexa.
                if (cpfCnpjLimpo.Length != 11)
                {
                    MessageBox.Show("O CPF deve conter exatamente 11 dígitos.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                    // Opcional: Limpar o campo ou definir uma máscara específica aqui
                    // txtCpfCnpj.Clear();
                    return; // Interrompe a validação adicional
                }

                if (!CpfValidator.IsCpfValid(cpfCnpjLimpo))
                {
                    MessageBox.Show("CPF inválido. Por favor, verifique o número digitado.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                    // Opcional: Limpar o campo ou definir uma máscara específica aqui
                    // txtCpfCnpj.Clear();
                }
            }
            else if (cmbTipoPessoa.SelectedIndex == 1) // "Jurídica" selecionado, valida como CNPJ
            {
                // Verifica o comprimento exato antes de tentar a validação complexa.
                if (cpfCnpjLimpo.Length != 14)
                {
                    MessageBox.Show("O CNPJ deve conter exatamente 14 dígitos.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                    // Opcional: Limpar o campo ou definir uma máscara específica aqui
                    // txtCnpjCpf.Clear();
                    return; // Interrompe a validação adicional
                }

                if (!CnpjValidator.IsCnpjValid(cpfCnpjLimpo))
                {
                    MessageBox.Show("CNPJ inválido. Por favor, verifique o número digitado.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                    // Opcional: Limpar o campo ou definir uma máscara específica aqui
                    // txtCnpjCpf.Clear();
                }
            }
            // Não é necessário um 'else' para cmbTipoPessoa.SelectedIndex aqui, pois a verificação inicial
            // para -1 cobre o caso onde nenhum tipo é selecionado. Se o índice for
            // outra coisa (por exemplo, um novo item foi adicionado sem lógica correspondente),
            // a validação simplesmente não será executada, o que é aceitável na maioria dos casos.
        }
    }
}



