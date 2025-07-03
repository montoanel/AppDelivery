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
        public CadastroClienteFRM()
        {
            InitializeComponent();





        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =========================================================
        // M�TODO PRINCIPAL: btnSalvar_Click
        // Este m�todo � executado quando o usu�rio clica no bot�o "Salvar".
        // =========================================================

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // 1. Valida��o dos Dados Essenciais (AGORA TODOS OBRIGAT�RIOS)
            // Usamos uma abordagem para verificar cada campo e dar um feedback espec�fico.

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

            // ********************************************************************************
            // IN�CIO DAS MUDAN�AS AQUI!
            // ********************************************************************************

            // 1. Processar CPF/CNPJ: Remover caracteres de m�scara
            // Usamos 'null' ao inv�s de string vazia para melhor alinhamento com DBNull.Value
            string cpfCnpjLimpo = string.IsNullOrWhiteSpace(txtCpfCnpj.Text) ? null : txtCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "");

            // 2. Processar Telefone: Remover caracteres de m�scara
            string telefoneLimpo = string.IsNullOrWhiteSpace(txtTelefone.Text) ? null : txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

            // 3. Processar Tipo Pessoa: Converter para 0 (F�sica) ou 1 (Jur�dica)
            int? tipoPessoaValor = null; // int? permite que a vari�vel seja null se nenhuma op��o v�lida for selecionada
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
                // Se houver outras op��es ou a sele��o n�o for "F�sica" nem "Jur�dica",
                // tipoPessoaValor permanecer� null.
                // Voc� pode adicionar um else aqui para um tratamento de erro ou valor padr�o
                // se a combobox tiver a obrigatoriedade de sempre ter "F�sica" ou "Jur�dica" selecionada.
            }
            else
            {
                // Se nenhuma op��o for selecionada na combobox (raro se voc� tiver um item padr�o),
                // voc� pode adicionar uma mensagem de erro aqui ou deixar tipoPessoaValor como null
                // para que DBNull.Value seja enviado.
                MessageBox.Show("Por favor, selecione o 'Tipo de Pessoa'.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoPessoa.Focus();
                return; // Sai da fun��o se o tipo de pessoa n�o for selecionado
            }

            // ********************************************************************************
            // FIM DAS MUDAN�AS NA PREPARA��O DOS DADOS
            // ********************************************************************************


            // 2. Obter a String de Conex�o do App.config
            // "MinhaConexaoDB" � o 'name' definido no seu arquivo App.config.
            string connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;

            // 3. Definir a Query SQL para Inser��o de Dados
            // Usamos nomes de par�metros (@parametro) para prevenir SQL Injection e lidar com dados especiais.
            // A ordem dos campos na query (tb_clientes) deve corresponder � ordem dos valores.
            string query = "INSERT INTO tb_clientes " +
                            "(nome_cliente, cpf_cnpj, endereco, numero, bairro, telefone, tipo_pessoa, complemento) " +
                            "VALUES " +
                            "(@nome_cliente, @cpf_cnpj, @endereco, @numero, @bairro, @telefone, @tipo_pessoa, @complemento)";

            // 4. Estabelecer Conex�o e Executar o Comando SQL
            // O bloco 'using' garante que os objetos SqlConnection e SqlCommand sejam
            // corretamente descartados (fechados e liberados) mesmo em caso de erro.
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    // 5. Adicionar Par�metros � Query
                    // Cada par�metro '@nome' na query deve ter um correspondente aqui.
                    // Para campos que podem ser NULL no banco de dados (cpf_cnpj, endereco, etc.),
                    // se o TextBox estiver vazio, passamos DBNull.Value. Caso contr�rio, passamos o texto do TextBox.
                    comando.Parameters.AddWithValue("@nome_cliente", txtNome.Text);

                    // ********************************************************************************
                    // NOVAS MUDAN�AS: Usando as vari�veis limpas e convertidas
                    // ********************************************************************************
                    comando.Parameters.AddWithValue("@cpf_cnpj", string.IsNullOrWhiteSpace(cpfCnpjLimpo) ? (object)DBNull.Value : cpfCnpjLimpo);
                    comando.Parameters.AddWithValue("@endereco", string.IsNullOrWhiteSpace(txtEndereco.Text) ? (object)DBNull.Value : txtEndereco.Text);
                    comando.Parameters.AddWithValue("@numero", string.IsNullOrWhiteSpace(txtNumero.Text) ? (object)DBNull.Value : txtNumero.Text);
                    comando.Parameters.AddWithValue("@bairro", string.IsNullOrWhiteSpace(txtBairro.Text) ? (object)DBNull.Value : txtBairro.Text);
                    comando.Parameters.AddWithValue("@telefone", string.IsNullOrWhiteSpace(telefoneLimpo) ? (object)DBNull.Value : telefoneLimpo);
                    comando.Parameters.AddWithValue("@tipo_pessoa", tipoPessoaValor.HasValue ? (object)tipoPessoaValor.Value : DBNull.Value);
                    comando.Parameters.AddWithValue("@complemento", txtComplemento.Text);
                    // ********************************************************************************
                    // FIM DAS NOVAS MUDAN�AS NOS PAR�METROS
                    // ********************************************************************************

                    try
                    {
                        conexao.Open(); // Abre a conex�o com o banco de dados
                        int linhasAfetadas = comando.ExecuteNonQuery(); // Executa a query INSERT. Retorna o n�mero de linhas afetadas.

                        if (linhasAfetadas > 0)
                        {
                            MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm(); // Limpa todos os campos do formul�rio ap�s o sucesso
                        }
                        else
                        {
                            MessageBox.Show("Nenhum cliente foi cadastrado. Verifique os dados e tente novamente.", "Aten��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Captura e trata erros espec�ficos do SQL Server (ex: problemas de conex�o, sintaxe SQL inv�lida, viola��o de restri��o)
                        MessageBox.Show("Erro ao cadastrar cliente no banco de dados:\n" + ex.Message + "\n\nC�digo do Erro: " + ex.ErrorCode, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        // Captura e trata quaisquer outros erros inesperados
                        MessageBox.Show("Ocorreu um erro inesperado ao cadastrar o cliente:\n" + ex.Message, "Erro Gen�rico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } // SqlCommand 'comando' � automaticamente descartado (Dispose) aqui
            } // SqlConnection 'conexao' � automaticamente descartada (Dispose) aqui
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
            ConfigurarComboBoxTipoPessoa();
        }

        private void ConfigurarComboBoxTipoPessoa()
        {
            // Limpa qualquer item existente (�til se voc� estiver depurando e o formul�rio recarregar)
            cmbTipoPessoa.Items.Clear();
            // Adiciona as op��es ao ComboBox
            cmbTipoPessoa.Items.Add("F�sica");
            cmbTipoPessoa.Items.Add("Jur�dica");

            // Define a op��o "F�sica" como padr�o (selecionada por �ndice)
            // O �ndice 0 corresponde ao primeiro item adicionado ("F�sica")
            cmbTipoPessoa.SelectedIndex = 0;

            // Ou, se voc� preferir definir por texto (menos robusto se o texto mudar)
            // cmbTipoPessoa.SelectedItem = "F�sica";
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
                // Define um PromptChar (caractere que indica onde o usu�rio deve digitar)
                // O padr�o � underscore '_', mas voc� pode usar espa�o, etc.
                txtCpfCnpj.PromptChar = '_';
                // Opcional: Se quiser que o campo preencha automaticamente os espa�os
                // se o usu�rio n�o digitar o CPF completo (recomenda-se false para CPF/CNPJ)
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
            // Primeiro, verifique se o cmbTipoPessoa tem uma sele��o.
            // Isso � crucial porque a valida��o depende do tipo escolhido.
            if (cmbTipoPessoa.SelectedIndex == -1) // Nenhum item selecionado
            {
                MessageBox.Show("Por favor, selecione o 'Tipo de Pessoa' (F�sica ou Jur�dica) antes de preencher o CPF/CNPJ.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCpfCnpj.Clear(); // Limpa o campo, pois n�o sabemos como valid�-lo
                cmbTipoPessoa.Focus();
                return;
            }

            // Pega o texto da masked text box e remove os caracteres da m�scara
            // Este passo � essencial para obter apenas os d�gitos para valida��o.
            // Adicionado .Replace("_", "") para remover o PromptChar caso o campo n�o seja totalmente preenchido.
            string cpfCnpjLimpo = txtCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");

            // Se o campo estiver vazio ap�s a limpeza, e for obrigat�rio, voc� pode querer uma mensagem.
            // No entanto, o btnSalvar_Click j� lida com campos obrigat�rios, ent�o podemos apenas retornar aqui.
            if (string.IsNullOrWhiteSpace(cpfCnpjLimpo))
            {
                return;
            }

            // Determina o tipo de valida��o com base na sele��o do cmbTipoPessoa
            if (cmbTipoPessoa.SelectedIndex == 0) // "F�sica" selecionado, valida como CPF
            {
                // Verifica o comprimento exato antes de tentar a valida��o complexa.
                if (cpfCnpjLimpo.Length != 11)
                {
                    MessageBox.Show("O CPF deve conter exatamente 11 d�gitos.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                    // Opcional: Limpar o campo ou definir uma m�scara espec�fica aqui
                    // txtCpfCnpj.Clear();
                    return; // Interrompe a valida��o adicional
                }

                if (!CpfValidator.IsCpfValid(cpfCnpjLimpo))
                {
                    MessageBox.Show("CPF inv�lido. Por favor, verifique o n�mero digitado.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                    // Opcional: Limpar o campo ou definir uma m�scara espec�fica aqui
                    // txtCpfCnpj.Clear();
                }
            }
            else if (cmbTipoPessoa.SelectedIndex == 1) // "Jur�dica" selecionado, valida como CNPJ
            {
                // Verifica o comprimento exato antes de tentar a valida��o complexa.
                if (cpfCnpjLimpo.Length != 14)
                {
                    MessageBox.Show("O CNPJ deve conter exatamente 14 d�gitos.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                    // Opcional: Limpar o campo ou definir uma m�scara espec�fica aqui
                    // txtCnpjCpf.Clear();
                    return; // Interrompe a valida��o adicional
                }

                if (!CnpjValidator.IsCnpjValid(cpfCnpjLimpo))
                {
                    MessageBox.Show("CNPJ inv�lido. Por favor, verifique o n�mero digitado.", "Erro de Valida��o", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCpfCnpj.Focus();
                    // Opcional: Limpar o campo ou definir uma m�scara espec�fica aqui
                    // txtCnpjCpf.Clear();
                }
            }
            // N�o � necess�rio um 'else' para cmbTipoPessoa.SelectedIndex aqui, pois a verifica��o inicial
            // para -1 cobre o caso onde nenhum tipo � selecionado. Se o �ndice for
            // outra coisa (por exemplo, um novo item foi adicionado sem l�gica correspondente),
            // a valida��o simplesmente n�o ser� executada, o que � aceit�vel na maioria dos casos.
        }
    }
}



