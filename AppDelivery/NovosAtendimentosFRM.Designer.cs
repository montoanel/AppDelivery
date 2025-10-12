namespace AppDelivery
{
    partial class NovosAtendimentosFRM
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelHeader = new Panel();
            lblIDAtendimento = new Label();
            txtIDAtendimento = new TextBox();
            lblNumeroAtendimento = new Label();
            txtNumeroAtendimento = new TextBox();
            lblTipoAtendimento = new Label();
            txtTipoAtendimento = new TextBox();
            groupBoxAtendente = new GroupBox();
            lblAtendente = new Label();
            txtInserirAtendente = new TextBox();
            btnInserirAtendente = new Button();
            groupBoxCliente = new GroupBox();
            lblCliente = new Label();
            txtInserirCliente = new TextBox();
            btnInserirCliente = new Button();
            groupBoxObservacoes = new GroupBox();
            richTextBoxObs = new RichTextBox();
            groupBoxProdutos = new GroupBox();
            btnBuscarProduto = new Button();
            txtQtd = new TextBox();
            lblQtd = new Label();
            lblProduto = new Label();
            txtBuscarProduto = new TextBox();
            btnInserir = new Button();
            dgvProdutos = new DataGridView();
            btnConcluir = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            lblTotalGeral = new Label();
            panelHeader.SuspendLayout();
            groupBoxAtendente.SuspendLayout();
            groupBoxCliente.SuspendLayout();
            groupBoxObservacoes.SuspendLayout();
            groupBoxProdutos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.Controls.Add(lblIDAtendimento);
            panelHeader.Controls.Add(txtIDAtendimento);
            panelHeader.Controls.Add(lblNumeroAtendimento);
            panelHeader.Controls.Add(txtNumeroAtendimento);
            panelHeader.Controls.Add(lblTipoAtendimento);
            panelHeader.Controls.Add(txtTipoAtendimento);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1091, 100);
            panelHeader.TabIndex = 0;
            // 
            // lblIDAtendimento
            // 
            lblIDAtendimento.Location = new Point(181, 29);
            lblIDAtendimento.Name = "lblIDAtendimento";
            lblIDAtendimento.Size = new Size(100, 23);
            lblIDAtendimento.TabIndex = 0;
            // 
            // txtIDAtendimento
            // 
            txtIDAtendimento.Location = new Point(12, 66);
            txtIDAtendimento.Name = "txtIDAtendimento";
            txtIDAtendimento.ReadOnly = true;
            txtIDAtendimento.Size = new Size(100, 31);
            txtIDAtendimento.TabIndex = 1;
            // 
            // lblNumeroAtendimento
            // 
            lblNumeroAtendimento.Location = new Point(0, 0);
            lblNumeroAtendimento.Name = "lblNumeroAtendimento";
            lblNumeroAtendimento.Size = new Size(100, 23);
            lblNumeroAtendimento.TabIndex = 2;
            // 
            // txtNumeroAtendimento
            // 
            txtNumeroAtendimento.Location = new Point(310, 66);
            txtNumeroAtendimento.Name = "txtNumeroAtendimento";
            txtNumeroAtendimento.ReadOnly = true;
            txtNumeroAtendimento.Size = new Size(100, 31);
            txtNumeroAtendimento.TabIndex = 3;
            // 
            // lblTipoAtendimento
            // 
            lblTipoAtendimento.Location = new Point(0, 0);
            lblTipoAtendimento.Name = "lblTipoAtendimento";
            lblTipoAtendimento.Size = new Size(100, 23);
            lblTipoAtendimento.TabIndex = 4;
            // 
            // txtTipoAtendimento
            // 
            txtTipoAtendimento.Location = new Point(203, 26);
            txtTipoAtendimento.Name = "txtTipoAtendimento";
            txtTipoAtendimento.ReadOnly = true;
            txtTipoAtendimento.Size = new Size(314, 31);
            txtTipoAtendimento.TabIndex = 5;
            // 
            // groupBoxAtendente
            // 
            groupBoxAtendente.Controls.Add(lblAtendente);
            groupBoxAtendente.Controls.Add(txtInserirAtendente);
            groupBoxAtendente.Controls.Add(btnInserirAtendente);
            groupBoxAtendente.Location = new Point(10, 110);
            groupBoxAtendente.Name = "groupBoxAtendente";
            groupBoxAtendente.Size = new Size(400, 70);
            groupBoxAtendente.TabIndex = 1;
            groupBoxAtendente.TabStop = false;
            groupBoxAtendente.Text = "Atendente";
            // 
            // lblAtendente
            // 
            lblAtendente.Location = new Point(10, 30);
            lblAtendente.Name = "lblAtendente";
            lblAtendente.Size = new Size(100, 23);
            lblAtendente.TabIndex = 0;
            lblAtendente.Text = "Buscar Atendente:";
            // 
            // txtInserirAtendente
            // 
            txtInserirAtendente.Location = new Point(130, 26);
            txtInserirAtendente.Name = "txtInserirAtendente";
            txtInserirAtendente.PlaceholderText = "Nome, CPF, Telefone";
            txtInserirAtendente.Size = new Size(180, 31);
            txtInserirAtendente.TabIndex = 1;
            // 
            // btnInserirAtendente
            // 
            btnInserirAtendente.Location = new Point(320, 25);
            btnInserirAtendente.Name = "btnInserirAtendente";
            btnInserirAtendente.Size = new Size(75, 32);
            btnInserirAtendente.TabIndex = 2;
            btnInserirAtendente.Text = "Inserir";
            // 
            // groupBoxCliente
            // 
            groupBoxCliente.Controls.Add(lblCliente);
            groupBoxCliente.Controls.Add(txtInserirCliente);
            groupBoxCliente.Controls.Add(btnInserirCliente);
            groupBoxCliente.Location = new Point(420, 110);
            groupBoxCliente.Name = "groupBoxCliente";
            groupBoxCliente.Size = new Size(548, 70);
            groupBoxCliente.TabIndex = 2;
            groupBoxCliente.TabStop = false;
            groupBoxCliente.Text = "Cliente";
            // 
            // lblCliente
            // 
            lblCliente.Location = new Point(10, 30);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(100, 23);
            lblCliente.TabIndex = 0;
            lblCliente.Text = "Buscar Cliente:";
            // 
            // txtInserirCliente
            // 
            txtInserirCliente.Location = new Point(120, 26);
            txtInserirCliente.Name = "txtInserirCliente";
            txtInserirCliente.PlaceholderText = "Nome, CPF, Telefone";
            txtInserirCliente.Size = new Size(180, 31);
            txtInserirCliente.TabIndex = 1;
            // 
            // btnInserirCliente
            // 
            btnInserirCliente.Location = new Point(310, 25);
            btnInserirCliente.Name = "btnInserirCliente";
            btnInserirCliente.Size = new Size(75, 32);
            btnInserirCliente.TabIndex = 2;
            btnInserirCliente.Text = "Inserir";
            // 
            // groupBoxObservacoes
            // 
            groupBoxObservacoes.Controls.Add(richTextBoxObs);
            groupBoxObservacoes.Location = new Point(10, 190);
            groupBoxObservacoes.Name = "groupBoxObservacoes";
            groupBoxObservacoes.Size = new Size(958, 100);
            groupBoxObservacoes.TabIndex = 3;
            groupBoxObservacoes.TabStop = false;
            groupBoxObservacoes.Text = "Observações";
            // 
            // richTextBoxObs
            // 
            richTextBoxObs.Dock = DockStyle.Fill;
            richTextBoxObs.Location = new Point(3, 27);
            richTextBoxObs.Name = "richTextBoxObs";
            richTextBoxObs.Size = new Size(952, 70);
            richTextBoxObs.TabIndex = 0;
            richTextBoxObs.Text = "";
            // 
            // groupBoxProdutos
            // 
            groupBoxProdutos.Controls.Add(btnBuscarProduto);
            groupBoxProdutos.Controls.Add(txtQtd);
            groupBoxProdutos.Controls.Add(lblQtd);
            groupBoxProdutos.Controls.Add(lblProduto);
            groupBoxProdutos.Controls.Add(txtBuscarProduto);
            groupBoxProdutos.Controls.Add(btnInserir);
            groupBoxProdutos.Controls.Add(dgvProdutos);
            groupBoxProdutos.Location = new Point(10, 300);
            groupBoxProdutos.Name = "groupBoxProdutos";
            groupBoxProdutos.Size = new Size(958, 300);
            groupBoxProdutos.TabIndex = 4;
            groupBoxProdutos.TabStop = false;
            groupBoxProdutos.Text = "Produtos";
            // 
            // btnBuscarProduto
            // 
            btnBuscarProduto.Location = new Point(539, 32);
            btnBuscarProduto.Name = "btnBuscarProduto";
            btnBuscarProduto.Size = new Size(78, 31);
            btnBuscarProduto.TabIndex = 6;
            btnBuscarProduto.Text = "Buscar";
            btnBuscarProduto.UseVisualStyleBackColor = true;
            btnBuscarProduto.Click += btnBuscarProduto_Click;
            // 
            // txtQtd
            // 
            txtQtd.Location = new Point(776, 27);
            txtQtd.Name = "txtQtd";
            txtQtd.Size = new Size(75, 31);
            txtQtd.TabIndex = 5;
            // 
            // lblQtd
            // 
            lblQtd.Location = new Point(709, 27);
            lblQtd.Name = "lblQtd";
            lblQtd.Size = new Size(61, 31);
            lblQtd.TabIndex = 4;
            lblQtd.Text = "Qtd:";
            lblQtd.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblProduto
            // 
            lblProduto.Location = new Point(116, 32);
            lblProduto.Name = "lblProduto";
            lblProduto.Size = new Size(100, 31);
            lblProduto.TabIndex = 0;
            lblProduto.Text = "Buscar Produto:";
            lblProduto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtBuscarProduto
            // 
            txtBuscarProduto.Location = new Point(210, 32);
            txtBuscarProduto.Name = "txtBuscarProduto";
            txtBuscarProduto.PlaceholderText = "Nome, código";
            txtBuscarProduto.Size = new Size(323, 31);
            txtBuscarProduto.TabIndex = 1;
            txtBuscarProduto.TextAlign = HorizontalAlignment.Center;
            // 
            // btnInserir
            // 
            btnInserir.Location = new Point(857, 27);
            btnInserir.Name = "btnInserir";
            btnInserir.Size = new Size(75, 31);
            btnInserir.TabIndex = 2;
            btnInserir.Text = "Inserir";
            btnInserir.Click += btnInserir_Click;
            // 
            // dgvProdutos
            // 
            dgvProdutos.ColumnHeadersHeight = 34;
            dgvProdutos.Location = new Point(-10, 67);
            dgvProdutos.Name = "dgvProdutos";
            dgvProdutos.RowHeadersWidth = 62;
            dgvProdutos.Size = new Size(942, 210);
            dgvProdutos.TabIndex = 3;
            // 
            // btnConcluir
            // 
            btnConcluir.Location = new Point(650, 620);
            btnConcluir.Name = "btnConcluir";
            btnConcluir.Size = new Size(170, 40);
            btnConcluir.TabIndex = 5;
            btnConcluir.Text = "F12 - Concluir";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // lblTotalGeral
            // 
            lblTotalGeral.AutoSize = true;
            lblTotalGeral.Location = new Point(495, 613);
            lblTotalGeral.Name = "lblTotalGeral";
            lblTotalGeral.Size = new Size(59, 25);
            lblTotalGeral.TabIndex = 6;
            lblTotalGeral.Text = "label1";
            // 
            // NovosAtendimentosFRM
            // 
            ClientSize = new Size(1091, 680);
            Controls.Add(lblTotalGeral);
            Controls.Add(panelHeader);
            Controls.Add(groupBoxAtendente);
            Controls.Add(groupBoxCliente);
            Controls.Add(groupBoxObservacoes);
            Controls.Add(groupBoxProdutos);
            Controls.Add(btnConcluir);
            Name = "NovosAtendimentosFRM";
            Text = "Novos Atendimentos";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            groupBoxAtendente.ResumeLayout(false);
            groupBoxAtendente.PerformLayout();
            groupBoxCliente.ResumeLayout(false);
            groupBoxCliente.PerformLayout();
            groupBoxObservacoes.ResumeLayout(false);
            groupBoxProdutos.ResumeLayout(false);
            groupBoxProdutos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelHeader;
        private Label lblIDAtendimento;
        private TextBox txtIDAtendimento;
        private Label lblNumeroAtendimento;
        private TextBox txtNumeroAtendimento;
        private Label lblTipoAtendimento;
        private TextBox txtTipoAtendimento;

        private GroupBox groupBoxAtendente;
        private Label lblAtendente;
        private TextBox txtInserirAtendente;
        private Button btnInserirAtendente;

        private GroupBox groupBoxCliente;
        private Label lblCliente;
        private TextBox txtInserirCliente;
        private Button btnInserirCliente;

        private GroupBox groupBoxObservacoes;
        private RichTextBox richTextBoxObs;

        private GroupBox groupBoxProdutos;
        private Label lblProduto;
        private TextBox txtBuscarProduto;
        private Button btnInserir;
        private DataGridView dgvProdutos;

        private Button btnConcluir;
        private TextBox txtQtd;
        private Label lblQtd;
        private ContextMenuStrip contextMenuStrip1;
        private Button btnBuscarProduto;
        private Label lblTotalGeral;
    }
}