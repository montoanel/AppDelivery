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

            // 2. Obter a String de Conexão do App.config
            // "MinhaConexaoDB" é o 'name' definido no seu arquivo App.config.
            string connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;

            // 3. Definir a Query SQL para Inserção de Dados
            // Usamos nomes de parâmetros (@parametro) para prevenir SQL Injection e lidar com dados especiais.
            // A ordem dos campos na query (tb_clientes) deve corresponder à ordem dos valores.
            string query = "INSERT INTO tb_clientes " +
                           "(nome_cliente, cpf_cnpj, endereco, numero, bairro, telefone, tipo_pessoa) " +
                           "VALUES " +
                           "(@nome_cliente, @cpf_cnpj, @endereco, @numero, @bairro, @telefone, @tipo_pessoa)";

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
                    comando.Parameters.AddWithValue("@cpf_cnpj", string.IsNullOrWhiteSpace(txtCpfCnpj.Text) ? (object)DBNull.Value : txtCpfCnpj.Text);
                    comando.Parameters.AddWithValue("@endereco", string.IsNullOrWhiteSpace(txtEndereco.Text) ? (object)DBNull.Value : txtEndereco.Text);
                    comando.Parameters.AddWithValue("@numero", string.IsNullOrWhiteSpace(txtNumero.Text) ? (object)DBNull.Value : txtNumero.Text);
                    comando.Parameters.AddWithValue("@bairro", string.IsNullOrWhiteSpace(txtBairro.Text) ? (object)DBNull.Value : txtBairro.Text);
                    comando.Parameters.AddWithValue("@telefone", string.IsNullOrWhiteSpace(txtTelefone.Text) ? (object)DBNull.Value : txtTelefone.Text);
                    comando.Parameters.AddWithValue("@tipo_pessoa", cmbTipoPessoa.SelectedItem.ToString()); // Pega o texto selecionado

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

        }


    }
}



