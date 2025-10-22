// AppDelivery/ConfigCaixaFRM.cs

using AppDelivery.DAL;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;

namespace AppDelivery
{
    public partial class ConfigCaixaFRM : Form
    {
        private CaixaDAO caixaDAO = new CaixaDAO();
        private ConfiguracaoCaixaDAO configDAO = new ConfiguracaoCaixaDAO();

        // Variável para armazenar o ID da linha de configuração selecionada na grid
        private int idConfiguracao = 0;

        public ConfigCaixaFRM()
        {
            InitializeComponent();

            // Liga o evento CellClick da Grid (não estava no Designer.cs gerado)
            dgvConfiguracoes.CellClick += dgvConfiguracoes_CellClick;
            btnRemover.Click += btnRemover_Click; // Liga o novo botão
        }

        private void ConfigCaixaFRM_Load(object sender, EventArgs e)
        {
            // 1. Carrega os dados na tela
            CarregarCaixasAtivos();

            // 2. Preenche o nome da máquina local por padrão
            txtNomeMaquina.Text = System.Environment.MachineName.ToUpper();

            // 3. Limpa e desabilita o ID da Configuração e o botão Remover
            LimparCamposConfiguracao();

            // 4. Carrega a lista de configurações existentes
            CarregarConfiguracoesGrid();
        }

        // **********************************
        // MÉTODOS AUXILIARES
        // **********************************

        private void LimparCamposConfiguracao()
        {
            txtIDConfig.Clear();
            idConfiguracao = 0;
            btnRemover.Enabled = false;
            // Mantém cmbCaixa e txtNomeMaquina com os valores padrão/máquina atual
        }

        private void CarregarCaixasAtivos()
        {
            // Lista todos os caixas e filtra apenas os ativos
            List<Caixa> caixas = caixaDAO.ListarTodos()
                                         .Where(c => c.Ativo == 'A')
                                         .ToList();

            cmbCaixa.DataSource = caixas;
            cmbCaixa.DisplayMember = "Nome";
            cmbCaixa.ValueMember = "Id";

            // Adiciona um item para "Selecione..."
            if (caixas.Count > 0)
            {
                // Adiciona um item nulo no início da lista para forçar seleção
                caixas.Insert(0, new Caixa(0, "Selecione um Caixa...", 'A'));
            }
            cmbCaixa.SelectedIndex = 0;
        }

        private void CarregarConfiguracoesGrid()
        {
            LimparCamposConfiguracao(); // Limpa o ID e desabilita o botão antes de recarregar

            dgvConfiguracoes.DataSource = configDAO.ListarConfiguracoes();

            // Renomear colunas para exibição amigável
            if (dgvConfiguracoes.Columns.Contains("id_config"))
            {
                dgvConfiguracoes.Columns["id_config"].HeaderText = "ID Config";
                dgvConfiguracoes.Columns["id_config"].Width = 60;
            }
            if (dgvConfiguracoes.Columns.Contains("nome_maquina"))
                dgvConfiguracoes.Columns["nome_maquina"].HeaderText = "Máquina";
            if (dgvConfiguracoes.Columns.Contains("nome_caixa"))
                dgvConfiguracoes.Columns["nome_caixa"].HeaderText = "Caixa Vinculado";

            // Ocultar o id_caixa se for redundante, mas mantê-lo para a lógica de seleção
            if (dgvConfiguracoes.Columns.Contains("id_caixa"))
                dgvConfiguracoes.Columns["id_caixa"].Visible = false;
        }

        // **********************************
        // AÇÕES DA GRID (SELEÇÃO)
        // **********************************

        private void dgvConfiguracoes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    // Obtém o ID da Configuração (tabela tb_config_caixa)
                    idConfiguracao = Convert.ToInt32(dgvConfiguracoes.Rows[e.RowIndex].Cells["id_config"].Value);
                    txtIDConfig.Text = idConfiguracao.ToString();

                    // Obtém o Nome da Máquina e o ID do Caixa para preencher os campos de edição
                    string nomeMaquina = dgvConfiguracoes.Rows[e.RowIndex].Cells["nome_maquina"].Value.ToString();
                    int idCaixaVinculado = Convert.ToInt32(dgvConfiguracoes.Rows[e.RowIndex].Cells["id_caixa"].Value);

                    txtNomeMaquina.Text = nomeMaquina;
                    cmbCaixa.SelectedValue = idCaixaVinculado;

                    // Habilita o botão Remover e Salvar para possível alteração
                    btnRemover.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar a configuração selecionada: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimparCamposConfiguracao();
                }
            }
        }

        // **********************************
        // AÇÕES DOS BOTÕES
        // **********************************

        // Ação do botão SALVAR (VINCULAR/ATUALIZAR)
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cmbCaixa.SelectedValue == null || (int)cmbCaixa.SelectedValue == 0)
            {
                MessageBox.Show("Selecione um Caixa válido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNomeMaquina.Text))
            {
                MessageBox.Show("O nome da máquina é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idCaixaSelecionado = Convert.ToInt32(cmbCaixa.SelectedValue);
                string nomeMaquina = txtNomeMaquina.Text.Trim().ToUpper();

                configDAO.SalvarConfiguracao(nomeMaquina, idCaixaSelecionado);

                MessageBox.Show($"Máquina '{nomeMaquina}' vinculada/atualizada ao Caixa '{cmbCaixa.Text}' com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recarrega a lista e o parâmetro do sistema (se for a máquina local)
                CarregarConfiguracoesGrid();
                ParametroSistema.CarregarParametrosCaixa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar a configuração: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // NOVO MÉTODO: Ação do botão REMOVER (DESVINCULAR)
        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (idConfiguracao > 0)
            {
                DialogResult resultado = MessageBox.Show(
                    $"Deseja realmente REMOVER a vinculação da máquina '{txtNomeMaquina.Text}' ao Caixa? " +
                    "A máquina não terá mais um caixa definido após esta ação.",
                    "Confirmar Remoção",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        configDAO.RemoverConfiguracao(idConfiguracao);

                        MessageBox.Show("Vinculação removida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpa os campos, recarrega a grid e atualiza o parâmetro do sistema
                        LimparCamposConfiguracao();
                        CarregarConfiguracoesGrid();
                        ParametroSistema.CarregarParametrosCaixa();

                        // O nome da máquina local é mantido para facilitar nova configuração
                        txtNomeMaquina.Text = System.Environment.MachineName.ToUpper();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao remover a configuração: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione uma configuração na listagem para remover.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}