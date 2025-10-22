// AppDelivery/ConfigCaixaFRM.cs

using AppDelivery.DAL;
using System;
using System.Windows.Forms;
using System.Data;

namespace AppDelivery
{
    public partial class ConfigCaixaFRM : Form
    {
        private CaixaDAO caixaDAO = new CaixaDAO();
        private ConfiguracaoCaixaDAO configDAO = new ConfiguracaoCaixaDAO();

        public ConfigCaixaFRM()
        {
            InitializeComponent();
        }

        private void ConfigCaixaFRM_Load(object sender, EventArgs e)
        {
            // 1. Preenche a combo box com os caixas ativos cadastrados
            CarregarCaixasAtivos();

            // 2. Preenche o nome da máquina local por padrão (para facilitar a configuração)
            txtNomeMaquina.Text = System.Environment.MachineName;

            // 3. Carrega a lista de configurações existentes
            CarregarConfiguracoesGrid();
        }

        private void CarregarCaixasAtivos()
        {
            // O CaixaDAO.ListarTodos() retorna todos ('A' e 'I'). 
            // Filtrar para mostrar apenas os ativos na Combo Box
            List<Caixa> caixas = caixaDAO.ListarTodos()
                                         .Where(c => c.Ativo == 'A')
                                         .ToList();

            cmbCaixa.DataSource = caixas;
            cmbCaixa.DisplayMember = "Nome"; // O que será exibido (ex: "Caixa Delivery")
            cmbCaixa.ValueMember = "Id";     // O valor interno (id_caixa)

            cmbCaixa.SelectedIndex = -1; // Nenhum item selecionado por padrão
        }

        private void CarregarConfiguracoesGrid()
        {
            try
            {
                dgvConfiguracoes.DataSource = configDAO.ListarConfiguracoes();
                // Ocultar coluna id_config, se desejar
                if (dgvConfiguracoes.Columns.Contains("id_config"))
                    dgvConfiguracoes.Columns["id_config"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar configurações: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // **********************************
        // AÇÃO DO BOTÃO SALVAR (VINCULAR)
        // **********************************

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cmbCaixa.SelectedValue == null)
            {
                MessageBox.Show("Selecione um Caixa ativo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                // Salva a configuração (INSERT ou UPDATE)
                configDAO.SalvarConfiguracao(nomeMaquina, idCaixaSelecionado);

                MessageBox.Show($"Máquina '{nomeMaquina}' vinculada ao Caixa '{cmbCaixa.Text}' com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recarrega a lista
                CarregarConfiguracoesGrid();

                // Força o sistema a recarregar o parâmetro de caixa atual, se for a máquina local
                if (nomeMaquina == ParametroSistema.NomeMaquina.ToUpper())
                {
                    ParametroSistema.CarregarParametrosCaixa();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar a configuração: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // **********************************
        // OUTRAS AÇÕES
        // **********************************

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}