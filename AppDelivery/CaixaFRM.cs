// Arquivo: AppDelivery/CaixaFRM.cs
using AppDelivery.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDelivery
{
    public partial class CaixaFRM : Form
    {
        private CaixaDAO caixaDAO = new CaixaDAO();
        // Variável de controle: 0 para Novo, > 0 para Edição
        private int idCaixa = 0;
        // Variável para rastrear se houve alteração nos campos na sessão atual
        private bool camposAlterados = false;

        public CaixaFRM()
        {
            InitializeComponent();

            // Liga os eventos dos botões (caso não estejam no Designer.cs)
            btnEditar.Click += btnEditar_Click;
            btnSalvar.Click += btnSalvar_Click;
            btnSair.Click += btnSair_Click;

            // Adiciona eventos para rastrear alterações (para o btnSair)
            txtNome.TextChanged += new EventHandler(Campo_TextChanged);
            chkAtivo.CheckedChanged += new EventHandler(Campo_Changed);
            // txtID é apenas leitura
        }

        private void CaixaFRM_Load(object sender, EventArgs e)
        {
            HabilitarDesabilitarCampos(false);
            CarregarGrid();
        }

        // ********************************
        // MÉTODOS AUXILIARES DE INTERFACE E CONTROLE
        // ********************************

        // Métodos para rastrear se algum campo foi alterado
        private void Campo_TextChanged(object sender, EventArgs e)
        {
            // Só marca como alterado se o formulário estiver em modo de edição/novo
            if (gbDados.Enabled)
            {
                camposAlterados = true;
            }
        }

        private void Campo_Changed(object sender, EventArgs e)
        {
            if (gbDados.Enabled)
            {
                camposAlterados = true;
            }
        }

        private void HabilitarDesabilitarCampos(bool habilitar)
        {
            // Habilita/Desabilita a área de entrada de dados
            gbDados.Enabled = habilitar;

            // Habilita/Desabilita Salvar (apenas em modo Novo/Edição)
            btnSalvar.Enabled = habilitar;

            // Botões de ação principal (oposto ao modo atual)
            btnNovo.Enabled = !habilitar;
            gridCaixas.Enabled = !habilitar;

            // O botão Editar só deve ser habilitado se houver um registro carregado (idCaixa > 0)
            btnEditar.Enabled = (idCaixa > 0) && !habilitar;
            // O campo txtID é sempre apenas leitura
            txtID.Enabled = false;
        }

        private void LimparCampos()
        {
            txtID.Clear();
            txtNome.Clear();
            chkAtivo.Checked = true; // Novo registro sempre começa como Ativo
            idCaixa = 0; // Indica Novo Registro
            camposAlterados = false; // Reset da flag de alteração
        }

        private void CarregarGrid()
        {
            try
            {
                gridCaixas.DataSource = null;
                gridCaixas.DataSource = caixaDAO.ListarTodos();

                // Configurar colunas
                if (gridCaixas.Columns.Contains("Id")) gridCaixas.Columns["Id"].HeaderText = "ID";
                if (gridCaixas.Columns.Contains("Nome")) gridCaixas.Columns["Nome"].HeaderText = "Nome";
                if (gridCaixas.Columns.Contains("Ativo")) gridCaixas.Columns["Ativo"].HeaderText = "Status";
                if (gridCaixas.Columns.Contains("Nome")) gridCaixas.Columns["Nome"].Width = 300;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os dados do Caixa: " + ex.Message, "Erro de Acesso a Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ********************************
        // AÇÕES DOS BOTÕES (Eventos Click)
        // ********************************

        // 1. Ação do botão NOVO
        private void btnNovo_Click(object sender, EventArgs e)
        {
            // Se houver alterações não salvas antes de iniciar o "Novo", pergunta ao usuário
            if (gbDados.Enabled && camposAlterados)
            {
                DialogResult resultado = MessageBox.Show(
                    "Há um cadastro em andamento. Deseja descartar e iniciar um novo?",
                    "Confirmar Novo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.No)
                {
                    return;
                }
            }

            LimparCampos();
            HabilitarDesabilitarCampos(true);
            txtNome.Focus();
        }

        // 2. Ação do botão SALVAR (INSERT/UPDATE)
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            Caixa caixa = new Caixa();

            caixa.Nome = txtNome.Text.Trim();
            // Mapeamento: Checado ('A') ou Desmarcado ('I')
            caixa.Ativo = chkAtivo.Checked ? 'A' : 'I';

            try
            {
                // NOVO REGISTRO (idCaixa == 0)
                if (idCaixa == 0)
                {
                    caixaDAO.Inserir(caixa);
                    MessageBox.Show("Caixa cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // EDIÇÃO / INATIVAÇÃO (idCaixa > 0)
                else
                {
                    caixa.Id = idCaixa;
                    caixaDAO.Atualizar(caixa);
                    MessageBox.Show("Caixa atualizado (Nome e/ou Status) com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CarregarGrid();
                HabilitarDesabilitarCampos(false);
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o caixa: " + ex.Message, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Ação do botão EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idCaixa > 0)
            {
                HabilitarDesabilitarCampos(true);
                txtNome.Focus();
            }
            else
            {
                MessageBox.Show("Selecione um caixa na listagem para editar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // Limpa a flag ao entrar em modo edição para que só altere após a primeira modificação neste modo.
            camposAlterados = false;
        }

        // 4. Ação do botão SAIR
        private void btnSair_Click(object sender, EventArgs e)
        {
            // Verifica se os campos estão habilitados (modo Novo ou Edição) E se houve alterações
            if (gbDados.Enabled && camposAlterados)
            {
                string acao = (idCaixa == 0) ? "cadastrar um novo registro" : "salvar as alterações";

                DialogResult resultado = MessageBox.Show(
                    $"Você está prestes a {acao}. Deseja sair e descartar as alterações não salvas?",
                    "Confirmar Saída",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }


        // ********************************
        // AÇÕES DA GRID (Seleção)
        // ********************************

        private void gridCaixas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    // Obtém o ID da linha selecionada
                    int idSelecionado = (int)gridCaixas.Rows[e.RowIndex].Cells["Id"].Value;

                    // Busca o objeto completo no banco de dados para carregar todos os dados
                    Caixa caixa = caixaDAO.BuscarPorId(idSelecionado);

                    if (caixa != null)
                    {
                        // Preenche os campos
                        idCaixa = caixa.Id;
                        txtID.Text = caixa.Id.ToString();
                        txtNome.Text = caixa.Nome;
                        // Mapeamento de 'A'/'I' para CheckBox
                        chkAtivo.Checked = (caixa.Ativo == 'A');

                        // Habilita a interface em modo de visualização (pronto para clicar em Editar ou Novo)
                        HabilitarDesabilitarCampos(false);
                        camposAlterados = false; // Garante que a flag de alteração está limpa após carregar
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar os dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}