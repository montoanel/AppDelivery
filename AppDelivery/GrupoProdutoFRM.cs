using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace AppDelivery
{
    public partial class GrupoProdutoFRM : Form
    {
        private string connectionString;
        private int? selectedId = null;
        public int GrupoSelecionadoID { get; private set; }
        public string GrupoSelecionadoNome { get; private set; }

        public GrupoProdutoFRM()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;

            // 1. O formulário é inicializado no modo de visualização.
            ConfigurarModoVisualizacao();
            CarregarDados();
        }

        private void ConfigurarModoVisualizacao()
        {
            txtNomeGrupo.Enabled = false;
            chkStatus.Enabled = false;
            btnNovo.Enabled = true;
            btnEditar.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            dataGridView1.Enabled = true;
            selectedId = null;
        }

        private void ConfigurarModoEdicao()
        {
            txtNomeGrupo.Enabled = true;
            chkStatus.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            dataGridView1.Enabled = false;
        }

        private void ConfigurarModoNovo()
        {
            txtNomeGrupo.Enabled = true;
            chkStatus.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            dataGridView1.Enabled = false;
            // 3. Por padrão, ao criar um novo cadastro, o status é ativo.
            chkStatus.Checked = true;
        }

        private void CarregarDados()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id_grupo, nome_grupo, status FROM grupo_produtos";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao carregar dados: {ex.Message}");
                }
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            // 2. Entra em modo de novo cadastro.
            LimparCampos();
            ConfigurarModoNovo();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // 4. Identifica o grupo selecionado e entra em modo de edição.
                selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_grupo"].Value);
                txtNomeGrupo.Text = dataGridView1.SelectedRows[0].Cells["nome_grupo"].Value.ToString();

                string statusValue = dataGridView1.SelectedRows[0].Cells["status"].Value.ToString();
                chkStatus.Checked = (statusValue == "A");

                ConfigurarModoEdicao();
            }
            else
            {
                MessageBox.Show("Selecione um registro para editar.");
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // 5. Lógica para salvar (INSERT ou UPDATE).
            if (string.IsNullOrWhiteSpace(txtNomeGrupo.Text))
            {
                MessageBox.Show("O nome do grupo não pode ser vazio.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query;

                    if (selectedId == null)
                    {
                        // Novo registro
                        query = "INSERT INTO grupo_produtos (nome_grupo, status) VALUES (@nome_grupo, @status)";
                    }
                    else
                    {
                        // Atualizar registro
                        query = "UPDATE grupo_produtos SET nome_grupo = @nome_grupo, status = @status WHERE id_grupo = @id_grupo";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome_grupo", txtNomeGrupo.Text);
                        string statusForDatabase = chkStatus.Checked ? "A" : "I";
                        cmd.Parameters.AddWithValue("@status", statusForDatabase);

                        if (selectedId != null)
                        {
                            cmd.Parameters.AddWithValue("@id_grupo", selectedId);
                        }

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Registro salvo com sucesso!");
                    ConfigurarModoVisualizacao();
                    CarregarDados();
                    LimparCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar registro: {ex.Message}");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // 6. Botão cancelar.
            LimparCampos();
            ConfigurarModoVisualizacao();
        }

        private void LimparCampos()
        {
            txtNomeGrupo.Clear();
            chkStatus.Checked = false;
            selectedId = null;
        }

        // Evento de clique do botão OK
        private void btOk_Click(object sender, EventArgs e)
        {
            // 1. Verifica se alguma linha foi selecionada no DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // 2. Obtém a linha selecionada
                DataGridViewRow linhaSelecionada = dataGridView1.SelectedRows[0];

                // 3. Pega o ID e o Nome da linha e armazena nas propriedades públicas
                // Certifique-se de que o nome das colunas está correto
                this.GrupoSelecionadoID = Convert.ToInt32(linhaSelecionada.Cells["id_grupo"].Value);
                this.GrupoSelecionadoNome = linhaSelecionada.Cells["nome_grupo"].Value.ToString();

                // 4. Define o DialogResult como OK para sinalizar sucesso ao formulário chamador
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Por favor, selecione um grupo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}