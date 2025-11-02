// Arquivo: AppDelivery/GestaoCaixaFRM.cs
using AppDelivery.DAL; // <--- PRECISA DESTE USING
using System;
using System.Drawing; // <--- PRECISA DESTE USING
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
                    MessageBox.Show("Este terminal não está configurado para um caixa ativo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// Atualiza os botões e labels do formulário com base no status da sessão
        /// </summary>
        private void AtualizarStatusInterface()
        {
            // Verifica se os controles existem antes de usá-los
            if (lblStatusCaixa == null || lblNomeCaixa == null)
            {
                MessageBox.Show("Erro de design: Labels 'lblStatusCaixa' ou 'lblNomeCaixa' não encontrados.");
                return;
            }

            if (ParametroSistema.SessaoAtual == null)
            {
                // CAIXA ESTÁ FECHADO
                lblStatusCaixa.Text = "CAIXA FECHADO";
                lblStatusCaixa.ForeColor = Color.Red;

                btnAbrirCaixa.Enabled = true;
                btnFecharCaixa.Enabled = false;
                btnSuprimento.Enabled = false; // Só pode suprir caixa aberto
                btnSangria.Enabled = false;
            }
            else
            {
                // CAIXA ESTÁ ABERTO
                lblStatusCaixa.Text = $"CAIXA ABERTO (Desde: {ParametroSistema.SessaoAtual.DataAbertura:G})";
                lblStatusCaixa.ForeColor = Color.Green;

                btnAbrirCaixa.Enabled = false;
                btnFecharCaixa.Enabled = true;
                btnSuprimento.Enabled = true;
                btnSangria.Enabled = true;
            }

            // Exibe o nome do caixa configurado
            lblNomeCaixa.Text = ParametroSistema.NomeCaixaAtual;
        }

        private void btnAbrirCaixa_Click(object sender, EventArgs e)
        {
            // PRÓXIMO PASSO: Criar este formulário 'AberturaCaixaFRM'

            // Usamos 'using' para garantir que o formulário popup seja destruído
            using (AberturaCaixaFRM frmAbertura = new AberturaCaixaFRM())
            {
                // ShowDialog() "trava" este formulário até o usuário fechar o popup
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

        // (Adicione aqui os eventos de clique para btnFecharCaixa, btnSangria, etc.)
    }
}