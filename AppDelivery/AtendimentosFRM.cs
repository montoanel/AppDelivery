using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Configuration;
using AppDelivery.Enums; // Essencial para a tradução do TipoAtendimento

namespace AppDelivery
{
    public partial class AtendimentosFRM : Form
    {
        private string connectionString;

        public AtendimentosFRM()
        {
            InitializeComponent();

            // 1. Carregamento da Connection String
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["MinhaConexaoDB"].ConnectionString;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar a Connection String: " + ex.Message, "Erro de Configuração", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connectionString = null;
            }

            this.Load += new EventHandler(AtendimentosFRM_Load);
        }

        private void AtendimentosFRM_Load(object sender, EventArgs e)
        {
            // Inicia o carregamento dos atendimentos ao abrir o formulário
            CarregarAtendimentos();
        }

        // Método principal para carregar os dados no DataGridView
        private void CarregarAtendimentos()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                return;
            }

            DataTable dtAtendimentos = new DataTable();

            // Query correta (sem a coluna 'numero_sequencial', conforme o seu DB)
            string query = "SELECT id_atendimento,tipo_atendimento,numero_atendimento,  id_atendente, id_cliente, valor_total_liquido, data_atendimento, status_atendimento  FROM tb_atendimentos";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conexao))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtAtendimentos);
                        }
                    }
                    
                    // ***************************************************************
                    // TRADUÇÃO DO ID NUMÉRICO (INT) PARA O NOME DO ENUM (STRING)
                    // Solução: Cria uma coluna string temporária para evitar o erro de conversão
                    // ***************************************************************
                    if (dtAtendimentos.Columns.Contains("tipo_atendimento"))
                    {
                        // 1. Adiciona a coluna que vai armazenar o nome do tipo
                        dtAtendimentos.Columns.Add("TipoTexto", typeof(string));

                        foreach (DataRow row in dtAtendimentos.Rows)
                        {
                            if (row["tipo_atendimento"] != DBNull.Value && int.TryParse(row["tipo_atendimento"].ToString(), out int tipoId))
                            {
                                // Tenta obter o nome do Enum (ex: "Delivery")
                                string nomeTipo = Enum.GetName(typeof(TipoAtendimento), tipoId);

                                if (!string.IsNullOrEmpty(nomeTipo))
                                {
                                    // 2. Salva o nome na nova coluna (TipoTexto)
                                    row["TipoTexto"] = nomeTipo;
                                }
                                else
                                {
                                    row["TipoTexto"] = "Desconhecido";
                                }
                            }
                            else
                            {
                                row["TipoTexto"] = "N/A";
                            }
                        }
                        
                        // 3. Remove a coluna original 'tipo_atendimento' (Int32) para que
                        // apenas o nome amigável apareça na tela.
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
                    MessageBox.Show("Erro ao conectar ou consultar o banco de dados:\n" + ex.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro inesperado:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Método para melhorar a leitura das colunas na tela
        // Dentro da classe AtendimentosFRM
        // Método para melhorar a leitura das colunas na tela
        private void AjustarNomesDasColunas()
        {
            if (dataGridView1.DataSource != null)
            {
                // Ordem desejada das colunas: Tipo, ID, Nº Atendimento, Data/Hora...
                Dictionary<string, string> mapeamentoColunas = new Dictionary<string, string>
        {
            // Ordem 0: TipoTexto (string amigável)
            { "TipoTexto", "Tipo" },
            
            // Ordem 1: id_atendimento
            { "id_atendimento", "ID" },
            
            // Ordem 2: NOVO CAMPO - numero_atendimento
            { "numero_atendimento", "Nº Atendimento." },
            
            // As demais colunas
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
                        // 1. Define o cabeçalho amigável
                        dataGridView1.Columns[item.Key].HeaderText = item.Value;

                        // 2. Define a ordem de exibição
                        dataGridView1.Columns[item.Key].DisplayIndex = displayIndex;

                        // 3. Incrementa o índice para a próxima coluna
                        displayIndex++;
                    }
                }
            }
        }

        // ***************************************************************
        // EVENTOS DE MOUSE (Mantidos, mas não relacionados a Nova Venda)
        // ***************************************************************

        private void pctNovoAtendimento_MouseLeave(object sender, EventArgs e)
        {
            // Lógica para controle visual do mouse out
            if (sender is PictureBox pictureBox)
            {
                 pictureBox.BackColor = Color.Transparent;
            }
        }

        private void pctNovoAtendimento_MouseEnter(object sender, EventArgs e)
        {
            // Lógica para controle visual do mouse over
            if (sender is PictureBox pictureBox)
            {
                 pictureBox.BackColor = Color.LightGray;
            }
        }
        
        // ***************************************************************
        // EVENTOS DE CLIQUE PARA NOVAS VENDAS REMOVIDOS/IGNORADOS
        // (Ex: pctVendaRapida_Click não está aqui)
        // ***************************************************************
    }
}