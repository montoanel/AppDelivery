// Arquivo: AppDelivery/AberturaCaixaFRM.cs
using AppDelivery.DAL; // Importe o namespace do DAO
using System;
using System.Collections.Generic; // Para usar List<>
using System.Globalization;
using System.Windows.Forms;

namespace AppDelivery
{
    public partial class AberturaCaixaFRM : Form
    {
        public AberturaCaixaFRM()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento Load do formulário, disparado quando a tela abre
        /// </summary>
        private void AberturaCaixaFRM_Load(object sender, EventArgs e)
        {
            CarregarAtendentes();
        }

        /// <summary>
        /// Busca os atendentes ativos no banco e preenche o ComboBox
        /// </summary>
        private void CarregarAtendentes()
        {
            try
            {
                // 1. Instancia o DAO que criamos
                FuncionarioDAO funcDAO = new FuncionarioDAO();
                List<Funcionario> atendentes = funcDAO.BuscarAtendentesAtivos();

                // 2. Configura o ComboBox
                cmbAtendente.DataSource = atendentes;
                cmbAtendente.DisplayMember = "Nome"; // O que o usuário VÊ

                // Usamos "IdFuncionario" pois é o nome da propriedade na classe Funcionario.cs
                cmbAtendente.ValueMember = "IdFuncionario"; // O que o código OBTÉM

                // 3. Inicia sem ninguém selecionado
                cmbAtendente.SelectedIndex = -1;
                cmbAtendente.Text = "Selecione o atendente...";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar lista de atendentes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // Fecha a tela se não conseguir carregar atendentes
            }
        }

        /// <summary>
        /// Evento do botão Confirmar
        /// </summary>
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            // --- 1. Validação dos Campos ---
            if (cmbAtendente.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecione o atendente.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAtendente.Focus();
                return;
            }

            if (!decimal.TryParse(txtValorAbertura.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal valorAbertura))
            {
                MessageBox.Show("Valor de abertura inválido. Por favor, digite um número.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValorAbertura.Focus();
                return;
            }

            if (valorAbertura < 0)
            {
                MessageBox.Show("O valor de abertura não pode ser negativo.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValorAbertura.Focus();
                return;
            }

            // --- 2. Preparação do Objeto ---
            int idAtendenteSelecionado = (int)cmbAtendente.SelectedValue;

            CaixaSessao novaSessao = new CaixaSessao
            {
                IdCaixa = ParametroSistema.IdCaixaAtual,
                IdAtendenteAbertura = idAtendenteSelecionado, // <--- DADO VINDO DO COMBOBOX
                DataAbertura = DateTime.Now,
                ValorAbertura = valorAbertura,
                StatusSessao = 'A' // 'A' de Aberta
            };

            // --- 3. Execução da Abertura (com a Transação) ---
            try
            {
                // Usamos o DAO de CaixaSessao
                CaixaSessaoDAO caixaSessaoDAO = new CaixaSessaoDAO();

                // Este método já executa a transação para as 2 tabelas
                // (tb_caixa_sessoes e tb_caixa_movimentacao)
                //
                caixaSessaoDAO.AbrirSessao(novaSessao);

                MessageBox.Show("Caixa aberto com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Informa ao GestaoCaixaFRM que a operação deu certo
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir o caixa: \n" + ex.Message, "Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Não fecha a tela, permite ao usuário tentar novamente
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}