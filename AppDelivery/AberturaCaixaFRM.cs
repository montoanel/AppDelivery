// Arquivo: AppDelivery/AberturaCaixaFRM.cs
using AppDelivery.DAL;
using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // <--- Adicione este
using System.Collections.Generic; // <--- Adicione este

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
                    // Lembre-se que sua tabela é tb_funcionarios, campo id_atendente
                    // (Assumindo que tb_funcionarios tenha uma coluna 'ativo')
                    string sql = "SELECT id_atendente, nome FROM tb_funcionarios WHERE ativo = 'A' ORDER BY nome";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaAtendentes.Add(
                                    Convert.ToInt32(reader["id_atendente"]),
                                    reader["nome"].ToString()
                                );
                            }
                        }
                    }
                }

                // Preenche o ComboBox
                cmbAtendente.DataSource = new BindingSource(listaAtendentes, null);
                cmbAtendente.DisplayMember = "Value";
                cmbAtendente.ValueMember = "Key";
                cmbAtendente.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar atendentes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            // --- Validações ---
            if (cmbAtendente.SelectedIndex == 0 || cmbAtendente.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecione o atendente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAtendente.Focus();
                return;
            }

            if (!decimal.TryParse(txtValorAbertura.Text, out decimal valorAbertura))
            {
                MessageBox.Show("Valor de abertura inválido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValorAbertura.Focus();
                return;
            }

            if (valorAbertura < 0)
            {
                MessageBox.Show("O valor de abertura não pode ser negativo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValorAbertura.Focus();
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

                // 2. Chama o DAO para salvar no banco
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