// AppDelivery/ConfigCaixaFRM.cs
using AppDelivery.DAL;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AppDelivery
{
    public partial class ConfigCaixaFRM : Form
    {
        // O CaixaDAO ainda é necessário para listar os caixas
        private CaixaDAO caixaDAO = new CaixaDAO();

        public ConfigCaixaFRM()
        {
            InitializeComponent();
            // Removemos todos os eventos da antiga grid e botões
        }

        private void ConfigCaixaFRM_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Carrega os caixas cadastrados no ComboBox
                CarregarCaixasAtivos();

                // 2. Lê a configuração local e seleciona o item
                CarregarConfiguracaoLocal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar configurações: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Carrega o ComboBox com os caixas do banco
        private void CarregarCaixasAtivos()
        {
            // Lista todos os caixas e filtra apenas os ativos
            List<Caixa> caixas = caixaDAO.ListarTodos()
                                         .Where(c => c.Ativo == 'A')
                                         .ToList();

            // Adiciona um item nulo no início da lista para "Desvincular"
            caixas.Insert(0, new Caixa(0, "Selecione um Caixa...", 'A'));

            cmbCaixa.DataSource = caixas;
            cmbCaixa.DisplayMember = "Nome";
            cmbCaixa.ValueMember = "Id";
        }

        // Lê o config.ini e seleciona o valor no ComboBox
        private void CarregarConfiguracaoLocal()
        {
            int idCaixaSalvo = ConfigLocal.LerIdCaixa();

            // Tenta selecionar o valor salvo
            if (cmbCaixa.Items.Count > 0)
            {
                var item = cmbCaixa.Items.Cast<Caixa>().FirstOrDefault(c => c.Id == idCaixaSalvo);
                if (item != null)
                {
                    cmbCaixa.SelectedValue = idCaixaSalvo;
                }
                else
                {
                    cmbCaixa.SelectedIndex = 0; // "Selecione..."
                }
            }
        }


        // **********************************
        // AÇÕES DOS BOTÕES
        // **********************************

        // Ação do botão SALVAR (VINCULAR/ATUALIZAR)
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cmbCaixa.SelectedValue == null)
            {
                MessageBox.Show("Erro ao ler seleção.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idCaixaSelecionado = Convert.ToInt32(cmbCaixa.SelectedValue);

                // Salva o ID no arquivo config.ini
                ConfigLocal.SalvarIdCaixa(idCaixaSelecionado);

                MessageBox.Show($"Estação configurada com o Caixa '{cmbCaixa.Text}' com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Força o ParametroSistema a recarregar o caixa (CRÍTICO!)
                ParametroSistema.CarregarParametrosCaixa();

                // Opcional: Fecha a tela após salvar
                // this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar a configuração local: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ... Remova todos os outros métodos (dgvConfiguracoes_CellClick, btnNovo_Click, btnDesvincular_Click, TentarCarregarConfiguracaoLocal, etc) ...
    }
}