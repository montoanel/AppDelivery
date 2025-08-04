using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace AppDelivery
{
    public partial class ListaProdutosFRM : Form
    {
        private string connectionString;

        public ListaProdutosFRM()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            this.Load += new EventHandler(ListaProdutosFRM_Load);
        }

        private void ListaProdutosFRM_Load(object sender, EventArgs e)
        {
            CarregarProdutosNoDataGridView();
            PreencherComboBoxFiltroManual();
        }

        private void CarregarProdutosNoDataGridView(string termoBusca = "", string colunaFiltro = "")
        {
            DataTable dtProdutos = new DataTable();
            string termoParam = "";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    string query = "SELECT id_produto, codigo_interno, codigo_barras, nome, preco, custo, unidade_medida, grupo, tipo_produto, inativo FROM tb_produtos";

                    if (!string.IsNullOrEmpty(termoBusca) && !string.IsNullOrEmpty(colunaFiltro))
                    {
                        termoParam = "%" + termoBusca + "%";

                        if (colunaFiltro == "Automático")
                        {
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
                            string nomeColunaBanco = MapearNomeAmigavelParaColunaBanco(colunaFiltro);
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
                    if (!string.IsNullOrEmpty(termoBusca) && !string.IsNullOrEmpty(colunaFiltro))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@termo", termoParam);
                    }

                    adapter.Fill(dtProdutos);
                    dgvListaProdutos.DataSource = dtProdutos;

                    if (dgvListaProdutos.Columns.Contains("id_produto")) { dgvListaProdutos.Columns["id_produto"].HeaderText = "Código"; dgvListaProdutos.Columns["id_produto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
                    if (dgvListaProdutos.Columns.Contains("codigo_interno")) { dgvListaProdutos.Columns["codigo_interno"].HeaderText = "Cód. Interno"; dgvListaProdutos.Columns["codigo_interno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
                    if (dgvListaProdutos.Columns.Contains("codigo_barras")) { dgvListaProdutos.Columns["codigo_barras"].HeaderText = "Cód. Barras"; dgvListaProdutos.Columns["codigo_barras"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
                    if (dgvListaProdutos.Columns.Contains("nome")) { dgvListaProdutos.Columns["nome"].HeaderText = "Nome"; dgvListaProdutos.Columns["nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; }
                    if (dgvListaProdutos.Columns.Contains("preco")) { dgvListaProdutos.Columns["preco"].HeaderText = "Preço"; dgvListaProdutos.Columns["preco"].DefaultCellStyle.Format = "C2"; dgvListaProdutos.Columns["preco"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
                    if (dgvListaProdutos.Columns.Contains("custo")) { dgvListaProdutos.Columns["custo"].HeaderText = "Custo"; dgvListaProdutos.Columns["custo"].DefaultCellStyle.Format = "C2"; dgvListaProdutos.Columns["custo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
                    if (dgvListaProdutos.Columns.Contains("unidade_medida")) { dgvListaProdutos.Columns["unidade_medida"].HeaderText = "Unid. Medida"; dgvListaProdutos.Columns["unidade_medida"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
                    if (dgvListaProdutos.Columns.Contains("grupo")) { dgvListaProdutos.Columns["grupo"].HeaderText = "Grupo"; dgvListaProdutos.Columns["grupo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
                    if (dgvListaProdutos.Columns.Contains("tipo_produto")) { dgvListaProdutos.Columns["tipo_produto"].HeaderText = "Tipo Produto"; dgvListaProdutos.Columns["tipo_produto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
                    if (dgvListaProdutos.Columns.Contains("inativo")) { dgvListaProdutos.Columns["inativo"].HeaderText = "Status"; dgvListaProdutos.Columns["inativo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }

                    dgvListaProdutos.AllowUserToAddRows = false;
                    dgvListaProdutos.ReadOnly = true;
                    dgvListaProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

        private void PreencherComboBoxFiltroManual()
        {
            cmbListaFiltroProdutos.Items.Clear();
            cmbListaFiltroProdutos.Items.Add("Automático");
            cmbListaFiltroProdutos.Items.Add("Código");
            cmbListaFiltroProdutos.Items.Add("Cód. Interno");
            cmbListaFiltroProdutos.Items.Add("Cód. Barras");
            cmbListaFiltroProdutos.Items.Add("Nome");
            cmbListaFiltroProdutos.Items.Add("Preço");
            cmbListaFiltroProdutos.Items.Add("Custo");
            cmbListaFiltroProdutos.Items.Add("Unid. Medida");
            cmbListaFiltroProdutos.Items.Add("Grupo");
            cmbListaFiltroProdutos.Items.Add("Tipo Produto");
            cmbListaFiltroProdutos.Items.Add("Status");
            cmbListaFiltroProdutos.SelectedIndex = 0;
        }

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
                case "Status": return "inativo";
                default: return string.Empty;
            }
        }

        private void btnFiltrarProdutos_Click(object sender, EventArgs e)
        {
            string termoBusca = txtListarProdutos.Text.Trim();
            string colunaFiltro = cmbListaFiltroProdutos.SelectedItem?.ToString();
            CarregarProdutosNoDataGridView(termoBusca, colunaFiltro);
        }

        private void txtListarProdutos_TextChanged(object sender, EventArgs e)
        {
            // Opcional: A busca automática foi comentada no código que você forneceu.
        }

        // Eventos dos novos botões (btnNovoProduto, btnEditarProduto, btnCancelar)
        private void btnNovoProduto_Click(object sender, EventArgs e)
        {
            using (CadastroProdutosFrm cadastroProdutos = new CadastroProdutosFrm())
            {
                if (cadastroProdutos.ShowDialog() == DialogResult.OK)
                {
                    CarregarProdutosNoDataGridView();
                }
            }
        }

        private void btnEditarProduto_Click(object sender, EventArgs e)
        {
            if (dgvListaProdutos.SelectedRows.Count > 0)
            {
                int idProdutoSelecionado = Convert.ToInt32(dgvListaProdutos.SelectedRows[0].Cells["id_produto"].Value);

                using (CadastroProdutosFrm formEdicao = new CadastroProdutosFrm(idProdutoSelecionado))
                {
                    if (formEdicao.ShowDialog() == DialogResult.OK)
                    {
                        CarregarProdutosNoDataGridView();
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um produto para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}