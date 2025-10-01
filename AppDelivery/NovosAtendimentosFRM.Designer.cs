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
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtNumeroAtendimento = new TextBox();
            lblNumeroAtendimento = new Label();
            txtIDAtendimento = new TextBox();
            lblIDAtendimento = new Label();
            panel2 = new Panel();
            txtInserirCliente = new TextBox();
            btnInserirCliente = new Button();
            panel3 = new Panel();
            btnInserir = new Button();
            txtInserirProduto = new TextBox();
            dataGridView1 = new DataGridView();
            panel4 = new Panel();
            lblTipoAtendimento = new Label();
            richTextBox1 = new RichTextBox();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // txtNumeroAtendimento
            // 
            txtNumeroAtendimento.Location = new Point(159, 34);
            txtNumeroAtendimento.Name = "txtNumeroAtendimento";
            txtNumeroAtendimento.ReadOnly = true;
            txtNumeroAtendimento.Size = new Size(199, 31);
            txtNumeroAtendimento.TabIndex = 15;
            // 
            // lblNumeroAtendimento
            // 
            lblNumeroAtendimento.AutoSize = true;
            lblNumeroAtendimento.Location = new Point(12, 34);
            lblNumeroAtendimento.Name = "lblNumeroAtendimento";
            lblNumeroAtendimento.Size = new Size(141, 25);
            lblNumeroAtendimento.TabIndex = 14;
            lblNumeroAtendimento.Text = "N° Atendimento";
            // 
            // txtIDAtendimento
            // 
            txtIDAtendimento.Location = new Point(159, 3);
            txtIDAtendimento.Name = "txtIDAtendimento";
            txtIDAtendimento.ReadOnly = true;
            txtIDAtendimento.Size = new Size(199, 31);
            txtIDAtendimento.TabIndex = 9;
            // 
            // lblIDAtendimento
            // 
            lblIDAtendimento.AutoSize = true;
            lblIDAtendimento.Location = new Point(12, 9);
            lblIDAtendimento.Name = "lblIDAtendimento";
            lblIDAtendimento.Size = new Size(30, 25);
            lblIDAtendimento.TabIndex = 8;
            lblIDAtendimento.Text = "ID";
            // 
            // panel2
            // 
            panel2.Controls.Add(richTextBox1);
            panel2.Controls.Add(btnInserirCliente);
            panel2.Controls.Add(txtInserirCliente);
            panel2.Location = new Point(2, 84);
            panel2.Name = "panel2";
            panel2.Size = new Size(1057, 200);
            panel2.TabIndex = 1;
            // 
            // txtInserirCliente
            // 
            txtInserirCliente.Location = new Point(16, 24);
            txtInserirCliente.Name = "txtInserirCliente";
            txtInserirCliente.Size = new Size(340, 31);
            txtInserirCliente.TabIndex = 0;
            txtInserirCliente.Text = "Buscar (Nome, CPF, Telefone)";
            // 
            // btnInserirCliente
            // 
            btnInserirCliente.Location = new Point(362, 24);
            btnInserirCliente.Name = "btnInserirCliente";
            btnInserirCliente.Size = new Size(112, 34);
            btnInserirCliente.TabIndex = 1;
            btnInserirCliente.Text = "Inserir";
            btnInserirCliente.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(dataGridView1);
            panel3.Controls.Add(btnInserir);
            panel3.Controls.Add(txtInserirProduto);
            panel3.Location = new Point(2, 439);
            panel3.Name = "panel3";
            panel3.Size = new Size(1057, 380);
            panel3.TabIndex = 2;
            // 
            // btnInserir
            // 
            btnInserir.Location = new Point(362, 24);
            btnInserir.Name = "btnInserir";
            btnInserir.Size = new Size(112, 34);
            btnInserir.TabIndex = 1;
            btnInserir.Text = "Inserir";
            btnInserir.UseVisualStyleBackColor = true;
            // 
            // txtInserirProduto
            // 
            txtInserirProduto.Location = new Point(16, 24);
            txtInserirProduto.Name = "txtInserirProduto";
            txtInserirProduto.Size = new Size(340, 31);
            txtInserirProduto.TabIndex = 0;
            txtInserirProduto.Text = "Buscar (Nome, código)";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 64);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(891, 274);
            dataGridView1.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.Controls.Add(txtNumeroAtendimento);
            panel4.Controls.Add(lblTipoAtendimento);
            panel4.Controls.Add(lblNumeroAtendimento);
            panel4.Controls.Add(lblIDAtendimento);
            panel4.Controls.Add(txtIDAtendimento);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(1058, 78);
            panel4.TabIndex = 3;
            // 
            // lblTipoAtendimento
            // 
            lblTipoAtendimento.AutoSize = true;
            lblTipoAtendimento.Location = new Point(437, 34);
            lblTipoAtendimento.Name = "lblTipoAtendimento";
            lblTipoAtendimento.Size = new Size(156, 25);
            lblTipoAtendimento.TabIndex = 0;
            lblTipoAtendimento.Text = "Tipo Atendimento";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(16, 61);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(458, 121);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // NovosAtendimentosFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1058, 864);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Name = "NovosAtendimentosFRM";
            Text = "NovosAtendimentosFRM";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox txtNumeroAtendimento;
        private Label lblNumeroAtendimento;
        private TextBox txtIDAtendimento;
        private Label lblIDAtendimento;
        private Panel panel2;
        private TextBox txtInserirCliente;
        private Button btnInserirCliente;
        private Panel panel3;
        private DataGridView dataGridView1;
        private Button btnInserir;
        private TextBox txtInserirProduto;
        private Panel panel4;
        private Label lblTipoAtendimento;
        private RichTextBox richTextBox1;
    }
}