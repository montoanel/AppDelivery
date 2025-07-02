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

            // 2. Obter a String de Conex�o do App.config
            // "MinhaConexaoDB" � o 'name' definido no seu arquivo App.config.
            string connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;

            // 3. Definir a Query SQL para Inser��o de Dados
            // Usamos nomes de par�metros (@parametro) para prevenir SQL Injection e lidar com dados especiais.
            // A ordem dos campos na query (tb_clientes) deve corresponder � ordem dos valores.
            string query = "INSERT INTO tb_clientes " +
                           "(nome_cliente, cpf_cnpj, endereco, numero, bairro, telefone, tipo_pessoa) " +
                           "VALUES " +
                           "(@nome_cliente, @cpf_cnpj, @endereco, @numero, @bairro, @telefone, @tipo_pessoa)";

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
                    comando.Parameters.AddWithValue("@cpf_cnpj", string.IsNullOrWhiteSpace(txtCpfCnpj.Text) ? (object)DBNull.Value : txtCpfCnpj.Text);
                    comando.Parameters.AddWithValue("@endereco", string.IsNullOrWhiteSpace(txtEndereco.Text) ? (object)DBNull.Value : txtEndereco.Text);
                    comando.Parameters.AddWithValue("@numero", string.IsNullOrWhiteSpace(txtNumero.Text) ? (object)DBNull.Value : txtNumero.Text);
                    comando.Parameters.AddWithValue("@bairro", string.IsNullOrWhiteSpace(txtBairro.Text) ? (object)DBNull.Value : txtBairro.Text);
                    comando.Parameters.AddWithValue("@telefone", string.IsNullOrWhiteSpace(txtTelefone.Text) ? (object)DBNull.Value : txtTelefone.Text);
                    comando.Parameters.AddWithValue("@tipo_pessoa", cmbTipoPessoa.SelectedItem.ToString()); // Pega o texto selecionado

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

        }


    }
}



