using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // Essencial para comunicação com SQL Server


namespace AppDelivery
{
    public partial class ListaClientes : Form
    {
        private string connectionString;

        public ListaClientes()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            this.Load += new EventHandler(ListaClientes_Load);
        }

        private void ListaClientes_Load(object sender, EventArgs e)
        {
            CarregarClientesNoDataGridView(); // Carrega todos os clientes inicialmente
            PreencherComboBoxFiltroManual();  // Preenche o ComboBox com as opções definidas manualmente
        }

        private void CarregarClientesNoDataGridView(string termoBusca = "", string colunaFiltro = "")
        {
            DataTable dtClientes = new DataTable();
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    string query = "SELECT cod_cliente, nome_cliente, cpf_cnpj, endereco, numero, complemento, bairro, telefone, tipo_pessoa FROM tb_clientes";

                    if (!string.IsNullOrEmpty(termoBusca) && !string.IsNullOrEmpty(colunaFiltro))
                    {
                        // Aqui garantimos que o termoBusca será usado APENAS se não for vazio
                        // A lógica do SQL Parameter já lida com o "%"
                        string termoParam = "%" + termoBusca + "%";

                        if (colunaFiltro == "Automático")
                        {
                            // Certifique-se de que todas as colunas que você quer buscar em "Automático"
                            // estão listadas aqui e com as conversões CAST corretas se não forem strings.
                            query += $" WHERE CAST(cod_cliente AS VARCHAR(50)) LIKE @termo OR " +
                                     $"nome_cliente LIKE @termo OR " +
                                     $"cpf_cnpj LIKE @termo OR " +
                                     $"endereco LIKE @termo OR " +
                                     $"CAST(numero AS VARCHAR(50)) LIKE @termo OR " + // Garantir CAST para 'numero'
                                     $"complemento LIKE @termo OR " +
                                     $"bairro LIKE @termo OR " +
                                     $"telefone LIKE @termo OR " +
                                     $"CAST(tipo_pessoa AS VARCHAR(50)) LIKE @termo"; // Garantir CAST para 'tipo_pessoa'
                        }
                        else
                        {
                            // Como MapearNomeAmigavelParaColunaBanco apenas retorna o nome,
                            // podemos usar diretamente o colunaFiltro (que é o nome da coluna do DB)
                            // string nomeColunaBanco = MapearNomeAmigavelParaColunaBanco(colunaFiltro); // Não é estritamente necessário se já é o nome do DB

                            // Verifique se o nome da coluna existe na sua tabela.
                            // Isso é importante para evitar erros de SQL se houver um nome incorreto.
                            // Para isso, você pode pré-validar os nomes das colunas ou confiar que
                            // o PreencherComboBoxFiltroManual() usa os nomes corretos do DB.

                            // A string do ComboBox JÁ É o nome da coluna do banco que queremos filtrar
                            string colunaParaFiltrar = colunaFiltro;

                            // Aplicar CAST para colunas que podem ser numéricas mas estão sendo filtradas por LIKE
                            if (colunaParaFiltrar == "cod_cliente" || colunaParaFiltrar == "numero" || colunaParaFiltrar == "tipo_pessoa")
                            {
                                query += $" WHERE CAST({colunaParaFiltrar} AS VARCHAR(50)) LIKE @termo";
                            }
                            else
                            {
                                query += $" WHERE {colunaParaFiltrar} LIKE @termo";
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conexao))
                    {
                        // Adicionar SqlParameter SOMENTE se houver um termo de busca e uma coluna de filtro definida
                        if (!string.IsNullOrEmpty(termoBusca) && !string.IsNullOrEmpty(colunaFiltro))
                        {
                            cmd.Parameters.AddWithValue("@termo", "%" + termoBusca + "%");
                        }
                        // Se termoBusca for vazio, não adicionamos @termo, e a query ficará sem WHERE
                        // o que fará com que todos os resultados sejam carregados (comportamento padrão).


                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtClientes);
                        }
                    }
                    dgvListaClientes.DataSource = dtClientes;
                    dgvListaClientes.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dgvListaClientes.AllowUserToAddRows = false;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Erro ao conectar ou consultar o banco de dados:\n" + ex.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro inesperado:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void PreencherComboBoxFiltroManual()
        {
            cmbListaFiltroClientes.Items.Clear();

            cmbListaFiltroClientes.Items.Add("Automático");
            cmbListaFiltroClientes.Items.Add("cod_cliente");
            cmbListaFiltroClientes.Items.Add("nome_cliente");
            cmbListaFiltroClientes.Items.Add("cpf_cnpj");
            cmbListaFiltroClientes.Items.Add("endereco");
            cmbListaFiltroClientes.Items.Add("numero");
            cmbListaFiltroClientes.Items.Add("complemento");
            cmbListaFiltroClientes.Items.Add("bairro");
            cmbListaFiltroClientes.Items.Add("telefone");
            cmbListaFiltroClientes.Items.Add("tipo_pessoa");

            if (cmbListaFiltroClientes.Items.Count > 0)
            {
                cmbListaFiltroClientes.SelectedIndex = 0;
            }
        }

        private string MapearNomeAmigavelParaColunaBanco(string nomeAmigavel)
        {
            return nomeAmigavel;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string termoBusca = txtListarCliente.Text.Trim();
            string colunaFiltro = cmbListaFiltroClientes.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(colunaFiltro))
            {
                MessageBox.Show("Por favor, selecione uma coluna para filtrar.", "Filtro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(termoBusca) && colunaFiltro != "Automático")
            {
                MessageBox.Show("Por favor, digite um termo para buscar.", "Filtro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CarregarClientesNoDataGridView(termoBusca, colunaFiltro);
        }

        // NOVO MÉTODO ADICIONADO AQUI
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Fecha o formulário atual
        }

        private void bntNovo_Click(object sender, EventArgs e)
        {
            // Cria uma nova instância do formulário de cadastro de clientes
            CadastroClienteFRM formCadastro = new CadastroClienteFRM();

            // Exibe o formulário de cadastro
            // Opção 1: Mostrar como uma janela modal (bloqueia o formulário pai até ser fechado)
            formCadastro.ShowDialog();

            // Opção 2: Mostrar como uma janela não modal (permite interagir com o formulário pai)
            // formCadastro.Show();

            // Opcional: Se você quiser que o DataGridView seja atualizado após o CadastroClienteFRM ser fechado (se o ShowDialog for usado)
            // Você pode chamar o método de carregamento novamente aqui para refletir novas adições ou edições
            CarregarClientesNoDataGridView(); // Recarrega todos os clientes, sem filtro
                                              // Ou CarregarClientesNoDataGridView(txtListarCliente.Text.Trim(), cmbListaFiltroClientes.SelectedItem?.ToString());
                                              // para manter o filtro e termo de busca atuais.
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // 1. Verificar se alguma linha está selecionada
            if (dgvListaClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecione um cliente para editar.", "Editar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Obter o cod_cliente da linha selecionada
            // Como o SelectionMode está como FullRowSelect, dgvListaClientes.SelectedRows[0] pegará a linha correta.
            // O 'cod_cliente' é a primeira coluna (índice 0) no seu DataGridView.
            int codClienteSelecionado;
            if (dgvListaClientes.SelectedRows[0].Cells["cod_cliente"].Value != null &&
                int.TryParse(dgvListaClientes.SelectedRows[0].Cells["cod_cliente"].Value.ToString(), out codClienteSelecionado))
            {
                // 3. Abrir o formulário CadastroClienteFRM
                // Precisamos de um construtor no CadastroClienteFRM que aceite o cod_cliente
                CadastroClienteFRM formCadastro = new CadastroClienteFRM(codClienteSelecionado);

                // Exibir o formulário de cadastro como modal
                formCadastro.ShowDialog();

                // 4. (Opcional) Recarregar os clientes no DataGridView após a edição
                // Isso garante que qualquer alteração feita no cliente seja refletida imediatamente.
                CarregarClientesNoDataGridView();
            }
            else
            {
                MessageBox.Show("Não foi possível obter o código do cliente selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }

}