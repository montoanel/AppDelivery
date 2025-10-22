// AppDelivery/CaixaFRM.cs
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
// Lembre-se de adicionar o using para sua camada de dados (ex: AppDelivery.DAL)

namespace AppDelivery
{
    public partial class CaixaFRM : Form
    {

        private CaixaDAO caixaDAO = new CaixaDAO();

        // Lista para simular o banco de dados temporariamente
        private List<Caixa> listaDeCaixas = new List<Caixa>();
        private int proximoId = 1; // Para simular o Auto-Incremento

        public CaixaFRM()
        {
            InitializeComponent();
        }

        private void CaixaFRM_Load(object sender, EventArgs e)
        {
            HabilitarDesabilitarCampos(false);
            CarregarGrid();
        }

        private void HabilitarDesabilitarCampos(bool habilitar)
        {
            gbDados.Enabled = habilitar;
            btnSalvar.Enabled = habilitar;
            // btnExcluir.Enabled = habilitar; // LINHA REMOVIDA

            btnNovo.Enabled = !habilitar;
            gridCaixas.Enabled = !habilitar;
        }

        private void LimparCampos()
        {
            txtID.Clear();
            txtNome.Clear();
            chkAtivo.Checked = true;
        }

        private void CarregarGrid()
        {
            try
            {
                // 1. CHAMA O DAO para buscar a lista do banco de dados
                List<Caixa> listaDeCaixas = caixaDAO.ListarTodos();

                // --- Toda a lógica de simulação/listaDeCaixas.Add() será removida daqui ---

                // 2. Lógica de Filtro (se você quiser manter o filtro)
                // Por enquanto, vamos manter a lista completa:
                var listaParaGrid = listaDeCaixas;

                // 3. Associa a lista do banco de dados ao DataGridView
                gridCaixas.DataSource = null; // Limpa a fonte de dados anterior
                gridCaixas.DataSource = listaParaGrid; // Re-associa a lista atualizada

                // 4. Configurar colunas (MANTENHA esta parte)
                gridCaixas.Columns["Id"].HeaderText = "ID";
                gridCaixas.Columns["Nome"].HeaderText = "Nome";
                gridCaixas.Columns["Ativo"].HeaderText = "Status";
                gridCaixas.Columns["Nome"].Width = 300;
            }
            catch (Exception ex)
            {
                // É crucial tratar exceções que podem ocorrer ao acessar o banco
                MessageBox.Show("Erro ao carregar os dados do Caixa: " + ex.Message, "Erro de Acesso a Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
            HabilitarDesabilitarCampos(true);
            txtNome.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            // TODO: Implementar lógica de salvar (INSERT ou UPDATE) no banco de dados

            Caixa caixa;

            // Se o ID está vazio, é um NOVO registro (INSERT)
            if (string.IsNullOrEmpty(txtID.Text))
            {
                caixa = new Caixa();
                caixa.Id = proximoId++; // Simulação de Auto-Incremento
                caixa.Nome = txtNome.Text;
                caixa.Ativo = chkAtivo.Checked;

                // Ex: CaixaDAO.Inserir(caixa);
                listaDeCaixas.Add(caixa); // Simulação

                MessageBox.Show("Caixa cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Se o ID NÃO está vazio, é uma EDIÇÃO (UPDATE)
            else
            {
                int id = int.Parse(txtID.Text);

                // Ex: caixa = CaixaDAO.BuscarPorId(id);
                caixa = listaDeCaixas.FirstOrDefault(c => c.Id == id); // Simulação

                if (caixa != null)
                {
                    caixa.Nome = txtNome.Text;
                    caixa.Ativo = chkAtivo.Checked; // <- A lógica de INATIVAR acontece aqui
                    // Ex: CaixaDAO.Atualizar(caixa);
                }
                MessageBox.Show("Caixa atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CarregarGrid();
            HabilitarDesabilitarCampos(false);
            LimparCampos();
        }

        // MÉTODO btnExcluir_Click FOI COMPLETAMENTE REMOVIDO

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridCaixas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Certifica-se de que o clique foi em uma linha válida
            if (e.RowIndex >= 0)
            {
                // Pega a linha clicada
                DataGridViewRow row = gridCaixas.Rows[e.RowIndex];

                // TODO: Se os dados vierem do banco, você pode precisar buscar o objeto completo
                // Caixa caixa = CaixaDAO.BuscarPorId((int)row.Cells["Id"].Value);

                // Simulação (pegando direto da lista)
                int id = (int)row.Cells["Id"].Value;
                Caixa caixa = listaDeCaixas.FirstOrDefault(c => c.Id == id);

                if (caixa != null)
                {
                    // Preenche os campos do formulário
                    txtID.Text = caixa.Id.ToString();
                    txtNome.Text = caixa.Nome;
                    chkAtivo.Checked = caixa.Ativo;

                    // Habilita os campos para edição/inativação
                    HabilitarDesabilitarCampos(true);
                }
            }
        }
    }
}