namespace AppDelivery
{
    partial class ConfigCaixaFRM
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
            gbConfiguracao = new GroupBox();
            lblIDConfig = new Label();
            txtIDConfig = new TextBox();
            lblCaixa = new Label();
            cmbCaixa = new ComboBox();
            txtNomeMaquina = new TextBox();
            lblNomeMaquina = new Label();
            dgvConfiguracoes = new DataGridView();
            lblConfiguracoes = new Label();
            btnSalvar = new Button();
            btnSair = new Button();
            btnDesvincular = new Button();
            btnNovo = new Button();
            gbConfiguracao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConfiguracoes).BeginInit();
            SuspendLayout();
            // 
            // gbConfiguracao
            // 
            gbConfiguracao.Controls.Add(lblIDConfig);
            gbConfiguracao.Controls.Add(txtIDConfig);
            gbConfiguracao.Controls.Add(lblCaixa);
            gbConfiguracao.Controls.Add(cmbCaixa);
            gbConfiguracao.Controls.Add(txtNomeMaquina);
            gbConfiguracao.Controls.Add(lblNomeMaquina);
            gbConfiguracao.Font = new Font("Segoe UI", 9.75F);
            gbConfiguracao.Location = new Point(17, 20);
            gbConfiguracao.Margin = new Padding(4, 5, 4, 5);
            gbConfiguracao.Name = "gbConfiguracao";
            gbConfiguracao.Padding = new Padding(4, 5, 4, 5);
            gbConfiguracao.Size = new Size(929, 178);
            gbConfiguracao.TabIndex = 0;
            gbConfiguracao.TabStop = false;
            gbConfiguracao.Text = "Vincular Máquina ao Caixa";
            // 
            // lblIDConfig
            // 
            lblIDConfig.AutoSize = true;
            lblIDConfig.Location = new Point(21, 50);
            lblIDConfig.Margin = new Padding(4, 0, 4, 0);
            lblIDConfig.Name = "lblIDConfig";
            lblIDConfig.Size = new Size(31, 28);
            lblIDConfig.TabIndex = 4;
            lblIDConfig.Text = "ID";
            // 
            // txtIDConfig
            // 
            txtIDConfig.Enabled = false;
            txtIDConfig.Location = new Point(21, 83);
            txtIDConfig.Margin = new Padding(4, 5, 4, 5);
            txtIDConfig.Name = "txtIDConfig";
            txtIDConfig.Size = new Size(77, 33);
            txtIDConfig.TabIndex = 5;
            // 
            // lblCaixa
            // 
            lblCaixa.AutoSize = true;
            lblCaixa.Location = new Point(479, 50);
            lblCaixa.Margin = new Padding(4, 0, 4, 0);
            lblCaixa.Name = "lblCaixa";
            lblCaixa.Size = new Size(58, 28);
            lblCaixa.TabIndex = 3;
            lblCaixa.Text = "Caixa";
            // 
            // cmbCaixa
            // 
            cmbCaixa.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCaixa.FormattingEnabled = true;
            cmbCaixa.Location = new Point(479, 83);
            cmbCaixa.Margin = new Padding(4, 5, 4, 5);
            cmbCaixa.Name = "cmbCaixa";
            cmbCaixa.Size = new Size(427, 36);
            cmbCaixa.TabIndex = 2;
            // 
            // txtNomeMaquina
            // 
            txtNomeMaquina.CharacterCasing = CharacterCasing.Upper;
            txtNomeMaquina.Location = new Point(121, 83);
            txtNomeMaquina.Margin = new Padding(4, 5, 4, 5);
            txtNomeMaquina.Name = "txtNomeMaquina";
            txtNomeMaquina.Size = new Size(327, 33);
            txtNomeMaquina.TabIndex = 1;
            // 
            // lblNomeMaquina
            // 
            lblNomeMaquina.AutoSize = true;
            lblNomeMaquina.Location = new Point(121, 50);
            lblNomeMaquina.Margin = new Padding(4, 0, 4, 0);
            lblNomeMaquina.Name = "lblNomeMaquina";
            lblNomeMaquina.Size = new Size(148, 28);
            lblNomeMaquina.TabIndex = 0;
            lblNomeMaquina.Text = "Nome Máquina";
            // 
            // dgvConfiguracoes
            // 
            dgvConfiguracoes.AllowUserToAddRows = false;
            dgvConfiguracoes.AllowUserToDeleteRows = false;
            dgvConfiguracoes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvConfiguracoes.Location = new Point(17, 277);
            dgvConfiguracoes.Margin = new Padding(4, 5, 4, 5);
            dgvConfiguracoes.Name = "dgvConfiguracoes";
            dgvConfiguracoes.ReadOnly = true;
            dgvConfiguracoes.RowHeadersWidth = 51;
            dgvConfiguracoes.RowTemplate.Height = 25;
            dgvConfiguracoes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvConfiguracoes.Size = new Size(929, 417);
            dgvConfiguracoes.TabIndex = 1;
            dgvConfiguracoes.CellClick += dgvConfiguracoes_CellClick;
            // 
            // lblConfiguracoes
            // 
            lblConfiguracoes.AutoSize = true;
            lblConfiguracoes.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblConfiguracoes.Location = new Point(17, 243);
            lblConfiguracoes.Margin = new Padding(4, 0, 4, 0);
            lblConfiguracoes.Name = "lblConfiguracoes";
            lblConfiguracoes.Size = new Size(316, 28);
            lblConfiguracoes.TabIndex = 2;
            lblConfiguracoes.Text = "Configurações Atuais (Máquina)";
            // 
            // btnSalvar
            // 
            btnSalvar.BackColor = Color.LightGreen;
            btnSalvar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSalvar.Location = new Point(831, 703);
            btnSalvar.Margin = new Padding(4, 5, 4, 5);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(114, 50);
            btnSalvar.TabIndex = 3;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = false;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnSair
            // 
            btnSair.BackColor = Color.LightCoral;
            btnSair.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSair.Location = new Point(709, 703);
            btnSair.Margin = new Padding(4, 5, 4, 5);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(114, 50);
            btnSair.TabIndex = 4;
            btnSair.Text = "Sair";
            btnSair.UseVisualStyleBackColor = false;
            btnSair.Click += btnSair_Click;
            // 
            // btnDesvincular
            // 
            btnDesvincular.BackColor = Color.OrangeRed;
            btnDesvincular.Enabled = false;
            btnDesvincular.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnDesvincular.Location = new Point(562, 703);
            btnDesvincular.Margin = new Padding(4, 5, 4, 5);
            btnDesvincular.Name = "btnDesvincular";
            btnDesvincular.Size = new Size(138, 50);
            btnDesvincular.TabIndex = 5;
            btnDesvincular.Text = "Desvincular";
            btnDesvincular.UseVisualStyleBackColor = false;
            btnDesvincular.Click += btnDesvincular_Click;
            // 
            // btnNovo
            // 
            btnNovo.BackColor = Color.Yellow;
            btnNovo.Enabled = false;
            btnNovo.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnNovo.Location = new Point(440, 703);
            btnNovo.Margin = new Padding(4, 5, 4, 5);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(114, 50);
            btnNovo.TabIndex = 6;
            btnNovo.Text = "Novo";
            btnNovo.UseVisualStyleBackColor = false;
            btnNovo.Click += btnNovo_Click;
            // 
            // ConfigCaixaFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(963, 773);
            Controls.Add(btnNovo);
            Controls.Add(btnDesvincular);
            Controls.Add(btnSair);
            Controls.Add(btnSalvar);
            Controls.Add(lblConfiguracoes);
            Controls.Add(dgvConfiguracoes);
            Controls.Add(gbConfiguracao);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfigCaixaFRM";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Configuração de Caixa por Máquina";
            Load += ConfigCaixaFRM_Load;
            gbConfiguracao.ResumeLayout(false);
            gbConfiguracao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConfiguracoes).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbConfiguracao;
        private System.Windows.Forms.Label lblCaixa;
        private System.Windows.Forms.ComboBox cmbCaixa;
        private System.Windows.Forms.TextBox txtNomeMaquina;
        private System.Windows.Forms.Label lblNomeMaquina;
        private System.Windows.Forms.DataGridView dgvConfiguracoes;
        private System.Windows.Forms.Label lblConfiguracoes;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnDesvincular; // Adicionado
        private System.Windows.Forms.TextBox txtIDConfig; // Adicionado
        private System.Windows.Forms.Label lblIDConfig; // Adicionado
        private Button btnNovo;
    }
}