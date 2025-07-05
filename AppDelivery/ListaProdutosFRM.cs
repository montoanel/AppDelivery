using System;
using System.Configuration; // Necessário para ConfigurationManager
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // Usando Microsoft.Data.SqlClient

namespace AppDelivery
{
    public partial class ListaProdutosFRM : Form
    {
        private string connectionString;

        public ListaProdutosFRM()
        {
            InitializeComponent();
            // Pega a string de conexão do App.config
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;

            // Associa o evento Load ao método ListaProdutosFRM_Load
            this.Load += new EventHandler(ListaProdutosFRM_Load);
        }

        // Evento Load do formulário ListaProdutosFRM
        private void ListaProdutosFRM_Load(object sender, EventArgs e)
        {
            CarregarProdutosNoDataGridView(); // Carrega todos os produtos inicialmente
            PreencherComboBoxFiltroManual();  // Preenche o ComboBox com as opções de filtro
        }

        // Método para carregar os dados dos produtos no DataGridView
        private void CarregarProdutosNoDataGridView(string termoBusca = "", string colunaFiltro = "")
        {
            DataTable dtProdutos = new DataTable();
            // DECLARAÇÃO DA VARIÁVEL termoParam MOVIDA PARA CÁ PARA TER ESCOPO NO MÉTODO
            string termoParam = ""; // Inicializa com valor padrão, será preenchida se houver busca

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    // Query SQL para selecionar todos os campos da tabela tb_produtos
                    string query = "SELECT id_produto, codigo_interno, codigo_barras, nome, preco, custo, unidade_medida, grupo, tipo_produto, inativo FROM tb_produtos";

                    // Verifica se há termo de busca e coluna de filtro
                    if (!string.IsNullOrEmpty(termoBusca) && !string.IsNullOrEmpty(colunaFiltro))
                    {
                        // AGORA, APENAS ATRIBUA O VALOR, NÃO DECLARE NOVAMENTE
                        termoParam = "%" + termoBusca + "%"; // Adiciona wildcards para busca LIKE

                        if (colunaFiltro == "Automático")
                        {
                            // Filtra em múltiplas colunas para a busca "Automática"
                            // Certifique-se de fazer CAST para VARCHAR se a coluna não for string (ex: INT, DECIMAL)
                            query += $" WHERE CAST(id_produto AS VARCHAR(50)) LIKE @termo OR " +
                                     $"codigo_interno LIKE @termo OR " +
                                     $"codigo_barras LIKE @termo OR " +
                                     $"nome LIKE @termo OR " +
                                     $"CAST(preco AS VARCHAR(50)) LIKE @termo OR " +
                                     $"CAST(custo AS VARCHAR(50)) LIKE @termo OR " +
                                     $"unidade_medida LIKE @termo OR " +
                                     $"grupo LIKE @termo OR " +
                                     $"tipo_produto LIKE @termo OR " +
                                     $"inativo LIKE @termo";
                        }
                        else
                        {
                            // Mapeia o nome amigável do ComboBox para o nome da coluna no banco de dados
                            string nomeColunaBanco = MapearNomeAmigavelParaColunaBanco(colunaFiltro);

                            // Aplicar CAST para colunas que podem ser numéricas mas estão sendo filtradas por LIKE
                            if (nomeColunaBanco == "id_produto" || nomeColunaBanco == "preco" || nomeColunaBanco == "custo")
                            {
                                query += $" WHERE CAST({nomeColunaBanco} AS VARCHAR(50)) LIKE @termo";
                            }
                            else
                            {
                                query += $" WHERE {nomeColunaBanco} LIKE @termo";
                            }
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conexao);
                    // Adiciona o parâmetro @termo apenas se ele for necessário na query
                    if (!string.IsNullOrEmpty(termoBusca) && !string.IsNullOrEmpty(colunaFiltro))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@termo", termoParam);
                    }

                    adapter.Fill(dtProdutos);
                    dgvListaProdutos.DataSource = dtProdutos;

                    // --- Configurações dos cabeçalhos do DataGridView ---
                    // Verifica e altera o HeaderText das colunas para nomes amigáveis
                    if (dgvListaProdutos.Columns.Contains("id_produto"))
                    {
                        dgvListaProdutos.Columns["id_produto"].HeaderText = "Código";
                        dgvListaProdutos.Columns["id_produto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    if (dgvListaProdutos.Columns.Contains("codigo_interno"))
                    {
                        dgvListaProdutos.Columns["codigo_interno"].HeaderText = "Cód. Interno";
                        dgvListaProdutos.Columns["codigo_interno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    if (dgvListaProdutos.Columns.Contains("codigo_barras"))
                    {
                        dgvListaProdutos.Columns["codigo_barras"].HeaderText = "Cód. Barras";
                        dgvListaProdutos.Columns["codigo_barras"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    if (dgvListaProdutos.Columns.Contains("nome"))
                    {
                        dgvListaProdutos.Columns["nome"].HeaderText = "Nome";
                        dgvListaProdutos.Columns["nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Preenche o espaço restante
                    }
                    if (dgvListaProdutos.Columns.Contains("preco"))
                    {
                        dgvListaProdutos.Columns["preco"].HeaderText = "Preço";
                        dgvListaProdutos.Columns["preco"].DefaultCellStyle.Format = "C2"; // Formata como moeda
                        dgvListaProdutos.Columns["preco"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    if (dgvListaProdutos.Columns.Contains("custo"))
                    {
                        dgvListaProdutos.Columns["custo"].HeaderText = "Custo";
                        dgvListaProdutos.Columns["custo"].DefaultCellStyle.Format = "C2"; // Formata como moeda
                        dgvListaProdutos.Columns["custo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    if (dgvListaProdutos.Columns.Contains("unidade_medida"))
                    {
                        dgvListaProdutos.Columns["unidade_medida"].HeaderText = "Unid. Medida";
                        dgvListaProdutos.Columns["unidade_medida"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    if (dgvListaProdutos.Columns.Contains("grupo"))
                    {
                        dgvListaProdutos.Columns["grupo"].HeaderText = "Grupo";
                        dgvListaProdutos.Columns["grupo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    if (dgvListaProdutos.Columns.Contains("tipo_produto"))
                    {
                        dgvListaProdutos.Columns["tipo_produto"].HeaderText = "Tipo Produto";
                        dgvListaProdutos.Columns["tipo_produto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    if (dgvListaProdutos.Columns.Contains("inativo"))
                    {
                        dgvListaProdutos.Columns["inativo"].HeaderText = "Status"; // Mudando para Status
                        dgvListaProdutos.Columns["inativo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        // Opcional: Se quiser converter 'A'/'I' para 'Ativo'/'Inativo' na exibição:
                        // Você precisaria de um evento CellFormatting ou um DataGridViewComboBoxColumn mapeado.
                        // Por simplicidade, exibirá 'A' ou 'I'.
                    }

                    // Opcional: Definir AutoSizeColumnsMode para todas as colunas
                    // dgvListaProdutos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dgvListaProdutos.AllowUserToAddRows = false; // Impede a linha vazia no final
                    dgvListaProdutos.ReadOnly = true; // Torna o DataGridView somente leitura
                    dgvListaProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona a linha inteira
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Erro ao carregar produtos do banco de dados:\n" + ex.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro inesperado:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Método para preencher o ComboBox de filtro manualmente
        private void PreencherComboBoxFiltroManual()
        {
            cmbListaFiltroProdutos.Items.Clear(); // Limpa itens existentes
            cmbListaFiltroProdutos.Items.Add("Automático"); // Opção padrão para busca em todas as colunas
            cmbListaFiltroProdutos.Items.Add("Código"); // id_produto
            cmbListaFiltroProdutos.Items.Add("Cód. Interno"); // codigo_interno
            cmbListaFiltroProdutos.Items.Add("Cód. Barras"); // codigo_barras
            cmbListaFiltroProdutos.Items.Add("Nome"); // nome
            cmbListaFiltroProdutos.Items.Add("Preço"); // preco
            cmbListaFiltroProdutos.Items.Add("Custo"); // custo
            cmbListaFiltroProdutos.Items.Add("Unid. Medida"); // unidade_medida
            cmbListaFiltroProdutos.Items.Add("Grupo"); // grupo
            cmbListaFiltroProdutos.Items.Add("Tipo Produto"); // tipo_produto
            cmbListaFiltroProdutos.Items.Add("Status"); // inativo (que mostra 'A' ou 'I')

            cmbListaFiltroProdutos.SelectedIndex = 0; // Seleciona "Automático" como padrão
        }

        // Método auxiliar para mapear o nome amigável do ComboBox para o nome da coluna no banco de dados
        private string MapearNomeAmigavelParaColunaBanco(string nomeAmigavel)
        {
            switch (nomeAmigavel)
            {
                case "Código": return "id_produto";
                case "Cód. Interno": return "codigo_interno";
                case "Cód. Barras": return "codigo_barras";
                case "Nome": return "nome";
                case "Preço": return "preco";
                case "Custo": return "custo";
                case "Unid. Medida": return "unidade_medida";
                case "Grupo": return "grupo";
                case "Tipo Produto": return "tipo_produto";
                case "Status": return "inativo"; // O nome da coluna no DB é 'inativo'
                default: return string.Empty; // Retorna vazio ou lança exceção para nomes não mapeados
            }
        }

        // Evento Click do botão "Filtrar Produtos"
        private void btnFiltrarProdutos_Click(object sender, EventArgs e)
        {
            // Pega o termo de busca do TextBox
            string termoBusca = txtListarProdutos.Text.Trim();
            // Pega a coluna selecionada no ComboBox
            string colunaFiltro = cmbListaFiltroProdutos.SelectedItem?.ToString();

            // Chama o método para recarregar os produtos com o filtro
            CarregarProdutosNoDataGridView(termoBusca, colunaFiltro);
        }

        // Evento TextChanged do TextBox de busca (opcional: busca dinâmica ao digitar)
        private void txtListarProdutos_TextChanged(object sender, EventArgs e)
        {
            // Você pode comentar esta linha se quiser que a busca ocorra apenas ao clicar no botão.
            // Se descomentada, a cada digitação ele filtra. Pode ser pesado para muitos dados.
            // btnFiltrarProdutos_Click(sender, e); 
        }

        // --- Eventos dos novos botões (btnNovoProduto, btnEditarProduto, btnCancelar) ---

        private void btnNovoProduto_Click(object sender, EventArgs e)
        {
            // Lógica para abrir o formulário de cadastro/edição para um novo produto
            // Ex: using (CadastroProdutoFRM formCadastro = new CadastroProdutoFRM())
            // {
            //     if (formCadastro.ShowDialog() == DialogResult.OK)
            //     {
            //         CarregarProdutosNoDataGridView(); // Recarrega a lista após adicionar um novo produto
            //     }
            // }
            MessageBox.Show("Funcionalidade 'Novo Produto' a ser implementada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEditarProduto_Click(object sender, EventArgs e)
        {
            // Lógica para abrir o formulário de cadastro/edição com os dados do produto selecionado
            if (dgvListaProdutos.SelectedRows.Count > 0)
            {
                // Pega o ID do produto da linha selecionada no DataGridView
                int idProdutoSelecionado = Convert.ToInt32(dgvListaProdutos.SelectedRows[0].Cells["id_produto"].Value);

                // Ex: using (CadastroProdutoFRM formEdicao = new CadastroProdutoFRM(idProdutoSelecionado))
                // {
                //     if (formEdicao.ShowDialog() == DialogResult.OK)
                //     {
                //         CarregarProdutosNoDataGridView(); // Recarrega a lista após alterar o produto
                //     }
                // }
                MessageBox.Show($"Funcionalidade 'Editar Produto' para ID: {idProdutoSelecionado} a ser implementada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Selecione um produto para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Lógica para fechar o formulário atual
            this.Close();
        }

        

    }
}