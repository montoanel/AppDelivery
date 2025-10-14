using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace AppDelivery
{
    public partial class NovosAtendimentosFRM : Form
    {
        private string connectionString;

        public NovosAtendimentosFRM()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            this.Load += new EventHandler(NovosAtendimentosFRM_Load);
        }

        private void NovosAtendimentosFRM_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
        }

        // =============================
        // CONFIGURAÇÃO DO DATAGRIDVIEW
        // =============================
        private void ConfigurarDataGridView()
        {
            dgvProdutos.Columns.Add("id_produto", "Código");
            dgvProdutos.Columns.Add("nome", "Produto");
            dgvProdutos.Columns.Add("quantidade", "Qtd");
            dgvProdutos.Columns.Add("preco", "Preço Unit.");
            dgvProdutos.Columns.Add("total", "Total");

            dgvProdutos.Columns["preco"].DefaultCellStyle.Format = "C2";
            dgvProdutos.Columns["total"].DefaultCellStyle.Format = "C2";

            dgvProdutos.AllowUserToAddRows = false;
            dgvProdutos.ReadOnly = true;
            dgvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // =============================
        // BUSCAR PRODUTO NO BANCO
        // =============================
        private DataRow BuscarProdutoNoBanco(string termo)
        {
            DataTable dtProduto = new DataTable();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();

                    string query = @"SELECT TOP 1 id_produto, nome, codigo_barras, preco 
                                     FROM tb_produtos
                                     WHERE codigo_barras = @termo OR nome LIKE @nome";

                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@termo", termo);
                    cmd.Parameters.AddWithValue("@nome", "%" + termo + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtProduto);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Erro ao buscar produto no banco:\n" + ex.Message,
                                    "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro inesperado:\n" + ex.Message,
                                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (dtProduto.Rows.Count > 0)
                return dtProduto.Rows[0];
            else
                return null;
        }

        // =============================
        // INSERIR PRODUTO NO GRID
        // =============================
        private void btnInserir_Click(object sender, EventArgs e)
        {
            string termoBusca = txtBuscarProduto.Text.Trim();

            if (string.IsNullOrEmpty(termoBusca))
            {
                MessageBox.Show("Digite o nome, código ou selecione um produto!");
                return;
            }

            // 🔹 Busca o produto no banco (pode ser por ID, código ou nome)
            DataRow produto = BuscarProdutoNoBanco(termoBusca);

            if (produto == null)
            {
                MessageBox.Show("Produto não encontrado!");
                return;
            }

            // 🔹 Valida quantidade
            if (!int.TryParse(txtQtd.Text.Trim(), out int qtd) || qtd <= 0)
            {
                MessageBox.Show("Quantidade inválida!");
                return;
            }

            int idProduto = Convert.ToInt32(produto["id_produto"]);
            string nomeProduto = produto["nome"].ToString();
            decimal preco = Convert.ToDecimal(produto["preco"]);
            decimal total = preco * qtd;

            // ⛔️ REMOÇÃO DO BLOCO DE AGRUPAMENTO (foreach):
            // Todo o bloco 'foreach (DataGridViewRow row in dgvProdutos.Rows)'
            // que verificava se o produto já existia e somava a quantidade foi REMOVIDO.
            // Isso garante que o código sempre chegue na linha de adição.

            // 🔹 Adiciona novo produto à grade (sempre uma nova linha)
            dgvProdutos.Rows.Add(
                idProduto,
                nomeProduto,
                qtd,
                preco,
                total
            );

            AtualizarTotalGeral();
            LimparCamposProduto();
        }
        private decimal ObterPrecoDoProduto(int idProduto)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                SqlCommand cmd = new SqlCommand("SELECT preco FROM tb_produtos WHERE id_produto = @id", conexao);
                cmd.Parameters.AddWithValue("@id", idProduto);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
        }



        // 🔸 Método auxiliar para limpar campos
        private void LimparCamposProduto()
        {
            txtBuscarProduto.Clear();
            txtQtd.Text = "1";
            txtBuscarProduto.Focus();
        }


        // =============================
        // CALCULAR TOTAL GERAL
        // =============================
        private void AtualizarTotalGeral()
        {
            decimal totalGeral = 0;

            foreach (DataGridViewRow row in dgvProdutos.Rows)
            {
                // ✅ Certo: O valor na célula já é um decimal, apenas converta o tipo `object`
                totalGeral += (decimal)row.Cells["total"].Value;
            }

            // A formatação para a label está correta
            lblTotalGeral.Text = "Total: " + totalGeral.ToString("C2");
        }

        // =============================
        // ATALHO: ENTER PARA INSERIR
        // =============================
        private void txtBuscarProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnInserir.PerformClick();
                e.SuppressKeyPress = true; // Evita beep
            }
        }

        private void btnBuscarProduto_Click(object sender, EventArgs e)
        {
            using (ListaProdutosFRM lista = new ListaProdutosFRM())
            {
                // Quando o usuário selecionar um produto
                lista.ProdutoSelecionado += (id, nome, preco) =>
                {
                    // Preenche os campos automaticamente
                    txtBuscarProduto.Text = nome;
                    txtBuscarProduto.Tag = id; // Guarda o ID do produto
                    txtQtd.Text = "1"; // Define quantidade padrão
                };

                lista.ShowDialog();
            }
        }

    }
}
