// Arquivo: AppDelivery/AberturaCaixaFRM.cs
using AppDelivery.DAL;
using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace AppDelivery
{
    public partial class AberturaCaixaFRM : Form
    {
        public AberturaCaixaFRM()
        {
            InitializeComponent();
        }

        private void AberturaCaixaFRM_Load(object sender, EventArgs e)
        {
            CarregarAtendentes();
            txtValorAbertura.Text = "0,00";
            // Define o botão "Confirmar" como o botão padrão (para a tecla ENTER)
            this.AcceptButton = btnConfirmar;
            // Define o botão "Cancelar" como o botão de cancelamento (para a tecla ESC)
            this.CancelButton = btnCancelar;
        }

        private void CarregarAtendentes()
        {
            // O ideal é criar um 'AtendenteDAO.cs', mas por simplicidade, faremos a consulta aqui.
            var listaAtendentes = new Dictionary<int, string>();
            listaAtendentes.Add(0, "Selecione um atendente..."); // Item padrão

            try
            {
                using (SqlConnection con = Conexao.GetConnection())
                {
                    // Lembre-se que sua tabela é tb_funcionarios, e o ID é id_funcionario
                    string sql = "SELECT id_funcionario, nome FROM tb_funcionarios WHERE status = 'A' ORDER BY nome";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string nome = reader.GetString(1);

                                // *** ESTA É A LINHA MODIFICADA (Antiga Linha 51) ***
                                // Agora exibe "ID - Nome"
                                listaAtendentes.Add(id, $"{id} - {nome}");
                            }
                        }
                    }
                }

                // Configura o ComboBox
                cmbAtendente.DataSource = new BindingSource(listaAtendentes, null);
                cmbAtendente.DisplayMember = "Value"; // Mostra o texto "ID - Nome"
                cmbAtendente.ValueMember = "Key";   // Armazena internamente o 'id_funcionario'
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar atendentes: " + ex.Message);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            // --- Validações ---
            decimal valorAbertura;
            if (!decimal.TryParse(txtValorAbertura.Text, out valorAbertura) || valorAbertura < 0)
            {
                MessageBox.Show("Por favor, insira um valor de abertura válido (ex: 50,00).", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValorAbertura.SelectAll();
                txtValorAbertura.Focus();
                return;
            }

            if (cmbAtendente.SelectedIndex == 0 || cmbAtendente.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecione o atendente responsável pela abertura.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAtendente.Focus();
                return;
            }

            // --- Lógica de Abertura ---
            try
            {
                // 1. Monta o objeto Sessao
                CaixaSessao novaSessao = new CaixaSessao();
                novaSessao.IdCaixa = ParametroSistema.IdCaixaAtual;
                novaSessao.IdAtendenteAbertura = Convert.ToInt32(cmbAtendente.SelectedValue);
                novaSessao.ValorAbertura = valorAbertura;
                novaSessao.DataAbertura = DateTime.Now;
                novaSessao.StatusSessao = 'A'; // 'A' de Aberto

                // 2. Chama o DAO para salvar no banco (Sessão + Movimentação)
                // (Vamos criar este DAO na Modificação 2)
                CaixaSessaoDAO sessaoDAO = new CaixaSessaoDAO();
                sessaoDAO.AbrirSessao(novaSessao);

                MessageBox.Show($"Caixa {ParametroSistema.NomeCaixaAtual} aberto com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 3. Avisa ao GestaoCaixaFRM que a abertura deu certo
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar a abertura de caixa no banco de dados:\n" + ex.Message, "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}