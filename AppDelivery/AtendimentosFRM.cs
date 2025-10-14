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
            DateTime hoje = DateTime.Today; // Pega a data de hoje, com hora 00:00:00

            // dateTimePicker1: Ontem à 00:00
            dateTimePicker1.Value = hoje.AddDays(-1);

            // dateTimePicker2: Hoje às 23:59:59.999...
            // Usamos .Today.AddDays(1).AddTicks(-1) para pegar o último milissegundo do dia de hoje.
            // Para simplificar, podemos usar DateTime.Now para a hora atual, ou 23:59:59.
            dateTimePicker2.Value = hoje.AddDays(1).AddSeconds(-1);

            // 2. Carregamento da Connection String
            try
            {
                // Conforme o seu código original
                connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar a Connection String: " + ex.Message, "Erro de Configuração", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connectionString = null;
            }

            // Associa o evento Load do formulário
            this.Load += new EventHandler(AtendimentosFRM_Load);
            // Associa o evento Click do botão Filtrar
            button1.Click += Button1_Click;
        }

        // ***************************************************************
        // CÓDIGO RESTANTE (GerarClausulaWhere, CarregarAtendimentos, etc.)
        // MANTÉM-SE EXATAMENTE IGUAL À SUGESTÃO ANTERIOR.
        // ***************************************************************

        private void AtendimentosFRM_Load(object sender, EventArgs e)
        {
            // Inicia o carregamento dos atendimentos usando as datas padrão configuradas no construtor
            CarregarAtendimentos();
        }

        // --- LÓGICA DE FILTRAGEM ---

        /// <summary>
        /// Gera a cláusula WHERE da consulta SQL baseada nas seleções do usuário.
        /// </summary>
        private string GerarClausulaWhere()
        {
            var condicoes = new List<string>();

            // 1. Filtros de Tipo de Atendimento
            Dictionary<CheckBox, string> tipoMapeamento = new Dictionary<CheckBox, string>
            {
                { checkBox5, "VendaRapida" },
                { checkBox1, "Delivery" },
                { checkBox2, "RetiradaBalcao" },
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
            CarregarAtendimentos();
        }


        // --- LÓGICA DE DADOS ---

        /// <summary>
        /// Método principal para carregar os dados no DataGridView, agora aceitando filtro.
        /// </summary>
        private void CarregarAtendimentos()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                return;
            }

            DataTable dtAtendimentos = new DataTable();
            string clausulaWhere = GerarClausulaWhere();

            // Query SQL completa com a cláusula WHERE gerada
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
                        // Adiciona os parâmetros de Data/Hora para segurança e precisão
                        cmd.Parameters.AddWithValue("@DataInicial", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@DataFinal", dateTimePicker2.Value);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtAtendimentos);
                        }
                    }

                    // Lógica de Tradução de ID Numérico (INT) para o Nome do ENUM (STRING)
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

        // Método para melhorar a leitura das colunas na tela (mantido do seu código)
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
    }
}