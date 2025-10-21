using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Configuration;
using AppDelivery.Enums;
using System.Linq;

namespace AppDelivery
{
    public partial class AtendimentosFRM : Form
    {
        private string connectionString;

        public AtendimentosFRM()
        {
            InitializeComponent();

            // 1. Definição das Datas Padrão
            DateTime hoje = DateTime.Today;
            dateTimePicker1.Value = hoje.AddDays(-1);
            dateTimePicker2.Value = hoje.AddDays(1).AddSeconds(-1);

            // 2. Carregamento da Connection String
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar a Connection String: " + ex.Message, "Erro de Configuração", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connectionString = null;
            }

            // Associa o evento Load do formulário
            this.Load += new EventHandler(AtendimentosFRM_Load); //
            // Associa o evento Click do botão Filtrar
            button1.Click += Button1_Click; //

            // 🚨 NOVO: Associa o evento de clique duplo na grid
           
        }

        // =====================================================
        // 🚨 CORREÇÃO: MÉTODO QUE EU TINHA APAGADO
        // =====================================================
        private void AtendimentosFRM_Load(object sender, EventArgs e)
        {
            // Inicia o carregamento dos atendimentos usando as datas padrão configuradas no construtor
            CarregarAtendimentos();
        }

        // --- LÓGICA DE FILTRAGEM ---
        private string GerarClausulaWhere()
        {
            var condicoes = new List<string>();

            // 1. Filtros de Tipo de Atendimento
            Dictionary<CheckBox, string> tipoMapeamento = new Dictionary<CheckBox, string>
            {
                { checkBox5, "VendaRapida" },
                { checkBox1, "Delivery" },
                { checkBox2, "Retirada" },
                { checkBox3, "Encomenda" }
            };

            var tiposSelecionados = new List<int>();

            if (!checkBox4.Checked)
            {
                foreach (var pair in tipoMapeamento)
                {
                    if (pair.Key.Checked)
                    {
                        if (Enum.TryParse(pair.Value, out TipoAtendimento tipoEnum))
                        {
                            tiposSelecionados.Add((int)tipoEnum);
                        }
                    }
                }
            }

            if (tiposSelecionados.Any())
            {
                condicoes.Add($"tipo_atendimento IN ({string.Join(", ", tiposSelecionados)})");
            }

            // 2. Filtros de Situação (Status)
            Dictionary<CheckBox, string> statusMapeamento = new Dictionary<CheckBox, string>
            {
                { checkBoxAberto, "'Aberto'" },
                { checkBoxEmAtendimento, "'Em atendimento'" },
                { checkBoxEmTransito, "'Em trânsito'" },
                { checkBoxRecebido, "'Recebido'" },
                { checkBoxCancelado, "'Cancelado'" }
            };

            var statusSelecionados = statusMapeamento
                .Where(pair => pair.Key.Checked)
                .Select(pair => pair.Value)
                .ToList();

            if (statusSelecionados.Any())
            {
                condicoes.Add($"status_atendimento IN ({string.Join(", ", statusSelecionados)})");
            }

            // 3. Filtros de Data/Hora (Sempre aplicados)
            condicoes.Add("data_atendimento >= @DataInicial AND data_atendimento <= @DataFinal");

            return condicoes.Any() ? " WHERE " + string.Join(" AND ", condicoes) : "";
        }


        /// <summary>
        /// Evento Click do botão Filtrar.
        /// </summary>
        private void Button1_Click(object sender, EventArgs e)
        {
            CarregarAtendimentos(); //
        }


        // --- LÓGICA DE DADOS ---
        private void CarregarAtendimentos()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                return;
            }

            DataTable dtAtendimentos = new DataTable();
            string clausulaWhere = GerarClausulaWhere();

            // Query SQL
            string query = $@"
                SELECT 
                    id_atendimento,
                    tipo_atendimento,
                    numero_atendimento, 
                    id_atendente, 
                    id_cliente, 
                    valor_total_liquido, 
                    data_atendimento, 
                    status_atendimento
                FROM 
                    tb_atendimentos
                {clausulaWhere}";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conexao))
                    {
                        // Adiciona os parâmetros de Data/Hora
                        cmd.Parameters.AddWithValue("@DataInicial", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@DataFinal", dateTimePicker2.Value);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtAtendimentos);
                        }
                    }

                    // Lógica de Tradução de ID Numérico
                    if (dtAtendimentos.Columns.Contains("tipo_atendimento"))
                    {
                        dtAtendimentos.Columns.Add("TipoTexto", typeof(string));

                        foreach (DataRow row in dtAtendimentos.Rows)
                        {
                            if (row["tipo_atendimento"] != DBNull.Value && int.TryParse(row["tipo_atendimento"].ToString(), out int tipoId))
                            {
                                string nomeTipo = Enum.GetName(typeof(TipoAtendimento), tipoId);
                                row["TipoTexto"] = !string.IsNullOrEmpty(nomeTipo) ?
                                    nomeTipo : "Desconhecido";
                            }
                            else
                            {
                                row["TipoTexto"] = "N/A";
                            }
                        }

                        dtAtendimentos.Columns.Remove("tipo_atendimento");
                    }

                    // Configura e preenche o DataGridView
                    dataGridView1.DataSource = dtAtendimentos;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dataGridView1.AllowUserToAddRows = false;

                    AjustarNomesDasColunas();

                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Erro ao conectar ou consultar o banco de dados:\n" + ex.Message + "\nQuery: " + query, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro inesperado:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Método para melhorar a leitura das colunas
        private void AjustarNomesDasColunas()
        {
            if (dataGridView1.DataSource != null)
            {
                Dictionary<string, string> mapeamentoColunas = new Dictionary<string, string>
                {
                    { "TipoTexto", "Tipo" },
                    { "id_atendimento", "ID" },
                    { "numero_atendimento", "Nº Atendimento." },
                    { "data_atendimento", "Data/Hora" },
                    { "id_atendente", "Cód. Atendente" },
                    { "id_cliente", "Cód. Cliente" },
                    { "valor_total_liquido", "Total Líquido (R$)" },
                    { "status_atendimento", "Status" }
                };

                int displayIndex = 0;

                foreach (var item in mapeamentoColunas)
                {
                    if (dataGridView1.Columns.Contains(item.Key))
                    {
                        dataGridView1.Columns[item.Key].HeaderText = item.Value;
                        dataGridView1.Columns[item.Key].DisplayIndex = displayIndex;
                        displayIndex++;
                    }
                }
            }
        }

        // Cole este método no lugar do DataGridView1_CellDoubleClick antigo

        // =====================================================
        // 🚨 MÉTODO CORRIGIDO: CLIQUE DUPLO NA GRID (COM SEGURANÇA)
        // =====================================================
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Verifica se o clique foi no cabeçalho (linha < 0)
            if (e.RowIndex < 0) return;

            // 2. 🚨 VERIFICAÇÃO DE SEGURANÇA:
            // Garante que o índice da linha clicada existe na coleção de linhas
            // E que a linha clicada não é a "linha nova" (placeholder)
            if (e.RowIndex >= dataGridView1.Rows.Count || dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                return; // Sai do método silenciosamente, pois não é uma linha de dados
            }

            try
            {
                // 3. Obter os dados da linha selecionada (agora seguro)
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Pega o ID (a coluna "id_atendimento" está no seu SELECT)
                int idSelecionado = Convert.ToInt32(selectedRow.Cells["id_atendimento"].Value);

                // Pega o Status (a coluna "status_atendimento" está no seu SELECT)
                string statusAtual = selectedRow.Cells["status_atendimento"].Value.ToString();

                // 4. Verificar se o status permite continuação
                List<string> statusPermitidos = new List<string> { "Aberto", "Em atendimento", "Em trânsito" };

                if (statusPermitidos.Contains(statusAtual))
                {
                    // 5. Abrir o formulário de NovosAtendimentos no "Modo de Edição"
                    using (NovosAtendimentosFRM frmEdicao = new NovosAtendimentosFRM(idSelecionado))
                    {
                        frmEdicao.ShowDialog();
                    }

                    // 6. Após fechar o formulário de edição, atualiza a lista principal
                    CarregarAtendimentos(); //
                }
                else
                {
                    MessageBox.Show(
                        $"Não é possível continuar este atendimento, pois seu status é '{statusAtual}'.\n" +
                        "Apenas atendimentos 'Aberto', 'Em atendimento' ou 'Em trânsito' podem ser editados.",
                        "Ação Não Permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar abrir o atendimento: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // EVENTOS DE MOUSE (Mantidos do seu código)
        private void pctNovoAtendimento_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                pictureBox.BackColor = Color.Transparent;
            }
        }

        private void pctNovoAtendimento_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                pictureBox.BackColor = Color.LightGray;
            }
        }

        private void pctDelivery_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                pictureBox.BackColor = Color.Transparent;
            }
        }

        private void pctDelivery_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                pictureBox.BackColor = Color.LightGray;
            }
        }

        private void pctRetiradaBalcao_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                pictureBox.BackColor = Color.LightGray;
            }
        }

        private void pctRetiradaBalcao_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                pictureBox.BackColor = Color.Transparent;
            }
        }

        private void pctEncomenda_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                pictureBox.BackColor = Color.Transparent;
            }
        }


        private void pctEncomenda_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                pictureBox.BackColor = Color.LightGray;
            }
        }

        // MÉTODOS DE CLIQUE (Mantidos do seu código)
        private void pctDelivery_Click(object sender, EventArgs e)
        {
            try
            {
                TipoAtendimento tipo = TipoAtendimento.Delivery;
                using (NovosAtendimentosFRM novoAtendimento = new NovosAtendimentosFRM(tipo))
                {
                    novoAtendimento.ShowDialog();
                }
                CarregarAtendimentos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao iniciar o novo atendimento de Delivery: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pctRetiradaBalcao_Click(object sender, EventArgs e)
        {
            try
            {
                TipoAtendimento tipo = TipoAtendimento.Retirada;
                using (NovosAtendimentosFRM novoAtendimento = new NovosAtendimentosFRM(tipo))
                {
                    novoAtendimento.ShowDialog();
                }
                CarregarAtendimentos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao iniciar o novo atendimento de Retirada/Balcão: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pctEncomenda_Click(object sender, EventArgs e)
        {
            try
            {
                TipoAtendimento tipo = TipoAtendimento.Encomenda;
                using (NovosAtendimentosFRM novoAtendimento = new NovosAtendimentosFRM(tipo))
                {
                    novoAtendimento.ShowDialog();
                }
                CarregarAtendimentos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao iniciar o novo atendimento de Encomenda: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}