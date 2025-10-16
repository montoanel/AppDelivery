namespace AppDelivery
{
    partial class NovosAtendimentosFRM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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
            textBox1 = new TextBox();
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
            groupBox1 = new GroupBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            textBox2 = new TextBox();
            panelHeader.SuspendLayout();
            groupBoxAtendente.SuspendLayout();
            groupBoxCliente.SuspendLayout();
            groupBoxObservacoes.SuspendLayout();
            groupBoxProdutos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).BeginInit();
            groupBox1.SuspendLayout();
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
            panelHeader.Size = new Size(1898, 60);
            panelHeader.TabIndex = 0;
            // 
            // lblIDAtendimento
            // 
            lblIDAtendimento.AutoSize = true;
            lblIDAtendimento.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIDAtendimento.Location = new Point(13, 15);
            lblIDAtendimento.Name = "lblIDAtendimento";
            lblIDAtendimento.Size = new Size(163, 28);
            lblIDAtendimento.TabIndex = 0;
            lblIDAtendimento.Text = "ID Atendimento";
            // 
            // txtIDAtendimento
            // 
            txtIDAtendimento.Location = new Point(190, 15);
            txtIDAtendimento.Name = "txtIDAtendimento";
            txtIDAtendimento.ReadOnly = true;
            txtIDAtendimento.Size = new Size(163, 31);
            txtIDAtendimento.TabIndex = 1;
            // 
            // lblNumeroAtendimento
            // 
            lblNumeroAtendimento.AutoSize = true;
            lblNumeroAtendimento.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNumeroAtendimento.Location = new Point(362, 19);
            lblNumeroAtendimento.Name = "lblNumeroAtendimento";
            lblNumeroAtendimento.Size = new Size(89, 28);
            lblNumeroAtendimento.TabIndex = 2;
            lblNumeroAtendimento.Text = "Número";
            // 
            // txtNumeroAtendimento
            // 
            txtNumeroAtendimento.Location = new Point(465, 13);
            txtNumeroAtendimento.Name = "txtNumeroAtendimento";
            txtNumeroAtendimento.ReadOnly = true;
            txtNumeroAtendimento.Size = new Size(163, 31);
            txtNumeroAtendimento.TabIndex = 3;
            // 
            // lblTipoAtendimento
            // 
            lblTipoAtendimento.AutoSize = true;
            lblTipoAtendimento.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTipoAtendimento.Location = new Point(678, 13);
            lblTipoAtendimento.Name = "lblTipoAtendimento";
            lblTipoAtendimento.Size = new Size(54, 28);
            lblTipoAtendimento.TabIndex = 4;
            lblTipoAtendimento.Text = "Tipo";
            // 
            // txtTipoAtendimento
            // 
            txtTipoAtendimento.Location = new Point(746, 16);
            txtTipoAtendimento.Name = "txtTipoAtendimento";
            txtTipoAtendimento.ReadOnly = true;
            txtTipoAtendimento.Size = new Size(163, 31);
            txtTipoAtendimento.TabIndex = 5;
            // 
            // groupBoxAtendente
            // 
            groupBoxAtendente.Controls.Add(textBox1);
            groupBoxAtendente.Controls.Add(lblAtendente);
            groupBoxAtendente.Controls.Add(txtInserirAtendente);
            groupBoxAtendente.Controls.Add(btnInserirAtendente);
            groupBoxAtendente.Location = new Point(10, 70);
            groupBoxAtendente.Name = "groupBoxAtendente";
            groupBoxAtendente.Size = new Size(464, 70);
            groupBoxAtendente.TabIndex = 1;
            groupBoxAtendente.TabStop = false;
            groupBoxAtendente.Text = "Atendente";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(80, 27);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "ID";
            textBox1.Size = new Size(45, 31);
            textBox1.TabIndex = 3;
            // 
            // lblAtendente
            // 
            lblAtendente.Location = new Point(10, 30);
            lblAtendente.Name = "lblAtendente";
            lblAtendente.Size = new Size(78, 23);
            lblAtendente.TabIndex = 0;
            lblAtendente.Text = "Codigo";
            lblAtendente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtInserirAtendente
            // 
            txtInserirAtendente.Location = new Point(131, 27);
            txtInserirAtendente.Name = "txtInserirAtendente";
            txtInserirAtendente.PlaceholderText = "Nome, CPF, Telefone";
            txtInserirAtendente.Size = new Size(242, 31);
            txtInserirAtendente.TabIndex = 1;
            // 
            // btnInserirAtendente
            // 
            btnInserirAtendente.Location = new Point(379, 26);
            btnInserirAtendente.Name = "btnInserirAtendente";
            btnInserirAtendente.Size = new Size(75, 32);
            btnInserirAtendente.TabIndex = 2;
            btnInserirAtendente.Text = "Buscar";
            // 
            // groupBoxCliente
            // 
            groupBoxCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxCliente.Controls.Add(textBox2);
            groupBoxCliente.Controls.Add(lblCliente);
            groupBoxCliente.Controls.Add(txtInserirCliente);
            groupBoxCliente.Controls.Add(btnInserirCliente);
            groupBoxCliente.Location = new Point(470, 70);
            groupBoxCliente.Name = "groupBoxCliente";
            groupBoxCliente.Size = new Size(1418, 70);
            groupBoxCliente.TabIndex = 2;
            groupBoxCliente.TabStop = false;
            groupBoxCliente.Text = "Cliente";
            // 
            // lblCliente
            // 
            lblCliente.Location = new Point(10, 30);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(70, 23);
            lblCliente.TabIndex = 0;
            lblCliente.Text = "Buscar:";
            // 
            // txtInserirCliente
            // 
            txtInserirCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtInserirCliente.Location = new Point(137, 26);
            txtInserirCliente.Name = "txtInserirCliente";
            txtInserirCliente.PlaceholderText = "Nome, CPF, Telefone";
            txtInserirCliente.Size = new Size(1105, 31);
            txtInserirCliente.TabIndex = 1;
            // 
            // btnInserirCliente
            // 
            btnInserirCliente.Location = new Point(1298, 25);
            btnInserirCliente.Name = "btnInserirCliente";
            btnInserirCliente.Size = new Size(75, 32);
            btnInserirCliente.TabIndex = 2;
            btnInserirCliente.Text = "Inserir";
            // 
            // groupBoxObservacoes
            // 
            groupBoxObservacoes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxObservacoes.Controls.Add(richTextBoxObs);
            groupBoxObservacoes.Location = new Point(10, 150);
            groupBoxObservacoes.Name = "groupBoxObservacoes";
            groupBoxObservacoes.Size = new Size(1878, 100);
            groupBoxObservacoes.TabIndex = 3;
            groupBoxObservacoes.TabStop = false;
            groupBoxObservacoes.Text = "Observações";
            // 
            // richTextBoxObs
            // 
            richTextBoxObs.Dock = DockStyle.Fill;
            richTextBoxObs.Location = new Point(3, 27);
            richTextBoxObs.Name = "richTextBoxObs";
            richTextBoxObs.Size = new Size(1872, 70);
            richTextBoxObs.TabIndex = 0;
            richTextBoxObs.Text = "";
            // 
            // groupBoxProdutos
            // 
            groupBoxProdutos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxProdutos.Controls.Add(btnBuscarProduto);
            groupBoxProdutos.Controls.Add(txtQtd);
            groupBoxProdutos.Controls.Add(lblQtd);
            groupBoxProdutos.Controls.Add(lblProduto);
            groupBoxProdutos.Controls.Add(txtBuscarProduto);
            groupBoxProdutos.Controls.Add(btnInserir);
            groupBoxProdutos.Controls.Add(dgvProdutos);
            groupBoxProdutos.Location = new Point(10, 260);
            groupBoxProdutos.Name = "groupBoxProdutos";
            groupBoxProdutos.Size = new Size(1878, 684);
            groupBoxProdutos.TabIndex = 4;
            groupBoxProdutos.TabStop = false;
            groupBoxProdutos.Text = "Produtos";
            // 
            // btnBuscarProduto
            // 
            btnBuscarProduto.Location = new Point(934, 30);
            btnBuscarProduto.Name = "btnBuscarProduto";
            btnBuscarProduto.Size = new Size(75, 31);
            btnBuscarProduto.TabIndex = 6;
            btnBuscarProduto.Text = "Buscar";
            btnBuscarProduto.Click += btnBuscarProduto_Click;
            // 
            // txtQtd
            // 
            txtQtd.Location = new Point(1137, 32);
            txtQtd.Name = "txtQtd";
            txtQtd.Size = new Size(70, 31);
            txtQtd.TabIndex = 5;
            // 
            // lblQtd
            // 
            lblQtd.Location = new Point(1081, 32);
            lblQtd.Name = "lblQtd";
            lblQtd.Size = new Size(50, 31);
            lblQtd.TabIndex = 4;
            lblQtd.Text = "Qtd:";
            lblQtd.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblProduto
            // 
            lblProduto.Location = new Point(10, 32);
            lblProduto.Name = "lblProduto";
            lblProduto.Size = new Size(70, 31);
            lblProduto.TabIndex = 0;
            lblProduto.Text = "Buscar:";
            lblProduto.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtBuscarProduto
            // 
            txtBuscarProduto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscarProduto.Location = new Point(80, 32);
            txtBuscarProduto.Name = "txtBuscarProduto";
            txtBuscarProduto.PlaceholderText = "Nome, código";
            txtBuscarProduto.Size = new Size(804, 31);
            txtBuscarProduto.TabIndex = 1;
            // 
            // btnInserir
            // 
            btnInserir.Location = new Point(1213, 32);
            btnInserir.Name = "btnInserir";
            btnInserir.Size = new Size(75, 31);
            btnInserir.TabIndex = 2;
            btnInserir.Text = "Inserir";
            btnInserir.Click += btnInserir_Click;
            // 
            // dgvProdutos
            // 
            dgvProdutos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProdutos.ColumnHeadersHeight = 34;
            dgvProdutos.Location = new Point(3, 75);
            dgvProdutos.Name = "dgvProdutos";
            dgvProdutos.RowHeadersWidth = 62;
            dgvProdutos.Size = new Size(1872, 604);
            dgvProdutos.TabIndex = 3;
            // 
            // btnConcluir
            // 
            btnConcluir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConcluir.Location = new Point(1768, 954);
            btnConcluir.Name = "btnConcluir";
            btnConcluir.Size = new Size(120, 60);
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
            lblTotalGeral.Location = new Point(610, 27);
            lblTotalGeral.Name = "lblTotalGeral";
            lblTotalGeral.Size = new Size(94, 25);
            lblTotalGeral.TabIndex = 6;
            lblTotalGeral.Text = "Valor Total";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox1.Controls.Add(lblTotalGeral);
            groupBox1.Location = new Point(10, 954);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(750, 60);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Totais";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(86, 27);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "ID";
            textBox2.Size = new Size(45, 31);
            textBox2.TabIndex = 4;
            // 
            // NovosAtendimentosFRM
            // 
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1898, 1024);
            Controls.Add(groupBox1);
            Controls.Add(btnConcluir);
            Controls.Add(groupBoxProdutos);
            Controls.Add(groupBoxObservacoes);
            Controls.Add(groupBoxCliente);
            Controls.Add(groupBoxAtendente);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedSingle;
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
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
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
        private GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TextBox textBox1;
        private TextBox textBox2;
    }
}