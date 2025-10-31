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

            // [AJUSTADO] Liga o evento CellClick da Grid e os botões
            dgvConfiguracoes.CellClick += dgvConfiguracoes_CellClick;

            // Certifique-se de que esses botões existam no seu Designer
            btnNovo.Click += btnNovo_Click;
            btnDesvincular.Click += btnDesvincular_Click;
        }

        // [MÉTODO AJUSTADO]
        private void ConfigCaixaFRM_Load(object sender, EventArgs e)
        {
            // 1. Carrega os dados na tela
            CarregarCaixasAtivos();

            // 2. Preenche o nome da máquina local por padrão
            txtNomeMaquina.Text = System.Environment.MachineName.ToUpper();

            // 3. Limpa os campos
            LimparCamposConfiguracao();

            // 4. Carrega a lista de configurações existentes
            CarregarConfiguracoesGrid();

            // 5. [NOVO PASSO] Tenta "Acusar" o vínculo existente
            TentarCarregarConfiguracaoLocal();
        }

        // [NOVO MÉTODO]
        // Procura na grid (que já foi carregada) se a máquina local existe
        private void TentarCarregarConfiguracaoLocal()
        {
            string maquinaLocal = txtNomeMaquina.Text;

            foreach (DataGridViewRow row in dgvConfiguracoes.Rows)
            {
                // Ignora linhas de cabeçalho ou inválidas
                if (row.IsNewRow || row.Cells["nome_maquina"].Value == null) continue;

                string nomeMaquinaNaGrid = row.Cells["nome_maquina"].Value.ToString();

                // Compara ignorando maiúsculas/minúsculas
                if (nomeMaquinaNaGrid.Equals(maquinaLocal, StringComparison.OrdinalIgnoreCase))
                {
                    // ENCONTROU!
                    // Vamos simular um clique na grid para carregar os dados
                    try
                    {
                        // Obtém os valores da linha encontrada
                        int idConfig = Convert.ToInt32(row.Cells["id_config"].Value);

                        // [AJUSTE DE NULO] Verifica se o id_caixa não é nulo
                        object idCaixaValue = row.Cells["id_caixa"].Value;
                        int idCaixa = (idCaixaValue == null || idCaixaValue == DBNull.Value) ? 0 : Convert.ToInt32(idCaixaValue);

                        // Preenche os campos do formulário
                        idConfiguracao = idConfig;
                        txtIDConfig.Text = idConfig.ToString();

                        if (idCaixa > 0)
                            cmbCaixa.SelectedValue = idCaixa;

                        btnDesvincular.Enabled = true; // Habilita o desvínculo

                        // Seleciona a linha na grid visualmente
                        row.Selected = true;
                        dgvConfiguracoes.CurrentCell = row.Cells[0];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Máquina local encontrada, mas houve erro ao carregar os dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Para a busca, pois já encontrou a máquina
                    return;
                }
            }

            // Se o loop terminar, a máquina não foi encontrada/vinculada.
            // O formulário permanece limpo (ação "Novo"), pronto para salvar.
        }

        // **********************************
        // MÉTODOS AUXILIARES
        // **********************************

        // [MÉTODO AJUSTADO]
        private void LimparCamposConfiguracao()
        {
            txtIDConfig.Clear();
            idConfiguracao = 0;
            btnDesvincular.Enabled = false;

            // Reseta o ComboBox para "Selecione..."
            if (cmbCaixa.Items.Count > 0)
                cmbCaixa.SelectedIndex = 0;

            // Mantém/Reseta o nome da máquina local
            txtNomeMaquina.Text = System.Environment.MachineName.ToUpper();
        }

        private void CarregarCaixasAtivos()
        {
            // Lista todos os caixas e filtra apenas os ativos
            List<Caixa> caixas = caixaDAO.ListarTodos()
                                         .Where(c => c.Ativo == 'A')
                                         .ToList();

            // Adiciona um item nulo no início da lista para forçar seleção
            caixas.Insert(0, new Caixa(0, "Selecione um Caixa...", 'A'));

            cmbCaixa.DataSource = caixas;
            cmbCaixa.DisplayMember = "Nome";
            cmbCaixa.ValueMember = "Id";
            cmbCaixa.SelectedIndex = 0;
        }

        private void CarregarConfiguracoesGrid()
        {
            // Limpa o ID e desabilita o botão antes de recarregar
            // LimparCamposConfiguracao(); // Removido daqui para não limpar a seleção local

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

        // [MÉTODO CORRIGIDO - JÁ ESTAVA OK NO SEU ARQUIVO]
        private void dgvConfiguracoes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignora o clique no cabeçalho
            if (e.RowIndex < 0)
            {
                return;
            }

            try
            {
                // 1. OBTENÇÃO ROBUSTA DO ID DE CONFIGURAÇÃO
                object idConfigValue = dgvConfiguracoes.Rows[e.RowIndex].Cells["id_config"].Value;

                // Verifica se o valor é nulo e converte de forma segura
                if (idConfigValue == null || idConfigValue == DBNull.Value)
                {
                    LimparCamposConfiguracao();
                    return;
                }

                // Converte o ID principal
                idConfiguracao = Convert.ToInt32(idConfigValue);
                txtIDConfig.Text = idConfiguracao.ToString();

                // 2. OBTENÇÃO ROBUSTA DO NOME DA MÁQUINA
                object nomeMaquinaValue = dgvConfiguracoes.Rows[e.RowIndex].Cells["nome_maquina"].Value;
                string nomeMaquina = (nomeMaquinaValue == null || nomeMaquinaValue == DBNull.Value) ? string.Empty : nomeMaquinaValue.ToString();

                // 3. OBTENÇÃO ROBUSTA DO ID DO CAIXA
                object idCaixaValue = dgvConfiguracoes.Rows[e.RowIndex].Cells["id_caixa"].Value;
                int idCaixaVinculado = (idCaixaValue == null || idCaixaValue == DBNull.Value) ? 0 : Convert.ToInt32(idCaixaValue);

                // Preenche os campos de edição
                txtNomeMaquina.Text = nomeMaquina;
                cmbCaixa.SelectedValue = idCaixaVinculado;

                // Habilita o botão
                btnDesvincular.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar a configuração selecionada: " + ex.Message, "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimparCamposConfiguracao();
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

                // O DAO cuida se é INSERT ou UPDATE
                configDAO.SalvarConfiguracao(nomeMaquina, idCaixaSelecionado);

                MessageBox.Show($"Máquina '{nomeMaquina}' vinculada/atualizada ao Caixa '{cmbCaixa.Text}' com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recarrega a lista e o parâmetro do sistema
                CarregarConfiguracoesGrid();

                // [AJUSTADO] Recarrega o vínculo local, se for a máquina atual
                TentarCarregarConfiguracaoLocal();

                ParametroSistema.CarregarParametrosCaixa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar a configuração: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // [MÉTODO AJUSTADO] (antigo btnRemover_Click)
        private void btnDesvincular_Click(object sender, EventArgs e)
        {
            if (idConfiguracao > 0)
            {
                DialogResult resultado = MessageBox.Show(
                    $"Deseja realmente DESVINCULAR a máquina '{txtNomeMaquina.Text}'?" +
                    "\nA máquina perderá o vínculo com o Caixa atual.",
                    "Confirmar Desvínculo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        configDAO.RemoverConfiguracao(idConfiguracao);

                        MessageBox.Show("Vinculação removida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recarrega a grid
                        CarregarConfiguracoesGrid();

                        // Limpa os campos (que automaticamente recarrega o nome da máquina local)
                        LimparCamposConfiguracao();

                        // Atualiza o parâmetro global
                        ParametroSistema.CarregarParametrosCaixa();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao remover a configuração: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione uma configuração na listagem para desvincular.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // [NOVO MÉTODO]
        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCamposConfiguracao();
            // Desseleciona qualquer linha da grid
            dgvConfiguracoes.ClearSelection();
        }
    }
}