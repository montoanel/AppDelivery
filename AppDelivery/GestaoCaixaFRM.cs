// Arquivo: AppDelivery/GestaoCaixaFRM.cs
using AppDelivery.DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AppDelivery
{
    public partial class GestaoCaixaFRM : Form
    {
        public GestaoCaixaFRM()
        {
            InitializeComponent();
        }

        private void GestaoCaixaFRM_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Verifica se a máquina está vinculada a um caixa
                if (!ParametroSistema.IsCaixaAtivo)
                {
                    MessageBox.Show("Este terminal não está configurado para um caixa ativo.\n\nVá em 'Configurações -> Vincular Caixa' no menu principal.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // 2. Carrega a SESSÃO ATUAL (Aberto/Fechado) do ParametroSistema
                ParametroSistema.CarregarSessaoAtual();

                // 3. Atualiza os botões e labels da tela
                AtualizarStatusInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro fatal ao carregar gestão de caixa: " + ex.Message);
                this.Close();
            }
        }

        /// <summary>
        /// *** MÉTODO CORRIGIDO ***
        /// Atualiza os botões e labels do formulário com base no status da sessão
        /// </summary>
        private void AtualizarStatusInterface()
        {
            // 1. Preenche o ID e o Nome do Caixa
            lblIdCaixa.Text = ParametroSistema.IdCaixaAtual.ToString();
            lblNomeCaixa.Text = ParametroSistema.NomeCaixaAtual;

            // 2. Verifica o Status da Sessão
            // [CORREÇÃO] Verificamos o *objeto* da sessão, não o booleano.
            if (ParametroSistema.SessaoAtual != null)
            {
                // O caixa ESTÁ ABERTO
                // Esta linha agora é segura
                lblSituacaoCaixa.Text = $"ABERTO (Desde: {ParametroSistema.SessaoAtual.DataAbertura:G})";
                lblSituacaoCaixa.ForeColor = Color.Green;

                // Habilita/Desabilita os botões
                btnAbrirCaixa.Enabled = false;
                btnFecharCaixa.Enabled = true;
                btnSuprimento.Enabled = true;
                btnSangria.Enabled = true;
            }
            else
            {
                // O caixa ESTÁ FECHADO (SessaoAtual é null)
                lblSituacaoCaixa.Text = "FECHADO";
                lblSituacaoCaixa.ForeColor = Color.Red;

                // Habilita/Desabilita os botões
                btnAbrirCaixa.Enabled = true;
                btnFecharCaixa.Enabled = false;
                btnSuprimento.Enabled = false;
                btnSangria.Enabled = false;
            }
        }

        private void btnAbrirCaixa_Click(object sender, EventArgs e)
        {
            // Verifique se você já tem este formulário 'AberturaCaixaFRM' criado
            using (AberturaCaixaFRM frmAbertura = new AberturaCaixaFRM())
            {
                if (frmAbertura.ShowDialog(this) == DialogResult.OK)
                {
                    // Se o usuário confirmou a abertura lá,
                    // recarregamos o status aqui.
                    ParametroSistema.CarregarSessaoAtual();
                    AtualizarStatusInterface();
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Adicione aqui os eventos para:
        // private void btnFecharCaixa_Click(object sender, EventArgs e) { ... }
        // private void btnSuprimento_Click(object sender, EventArgs e) { ... }
        // private void btnSangria_Click(object sender, EventArgs e) { ... }
    }
}