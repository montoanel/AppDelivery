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
            this.gbConfiguracao = new System.Windows.Forms.GroupBox();
            this.lblCaixa = new System.Windows.Forms.Label();
            this.cmbCaixa = new System.Windows.Forms.ComboBox();
            this.txtNomeMaquina = new System.Windows.Forms.TextBox();
            this.lblNomeMaquina = new System.Windows.Forms.Label();
            this.dgvConfiguracoes = new System.Windows.Forms.DataGridView();
            this.lblConfiguracoes = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.gbConfiguracao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfiguracoes)).BeginInit();
            this.SuspendLayout();
            // 
            // gbConfiguracao
            // 
            this.gbConfiguracao.Controls.Add(this.lblCaixa);
            this.gbConfiguracao.Controls.Add(this.cmbCaixa);
            this.gbConfiguracao.Controls.Add(this.txtNomeMaquina);
            this.gbConfiguracao.Controls.Add(this.lblNomeMaquina);
            this.gbConfiguracao.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gbConfiguracao.Location = new System.Drawing.Point(12, 12);
            this.gbConfiguracao.Name = "gbConfiguracao";
            this.gbConfiguracao.Size = new System.Drawing.Size(650, 107);
            this.gbConfiguracao.TabIndex = 0;
            this.gbConfiguracao.TabStop = false;
            this.gbConfiguracao.Text = "Vincular Máquina ao Caixa";
            // 
            // lblCaixa
            // 
            this.lblCaixa.AutoSize = true;
            this.lblCaixa.Location = new System.Drawing.Point(335, 30);
            this.lblCaixa.Name = "lblCaixa";
            this.lblCaixa.Size = new System.Drawing.Size(40, 17);
            this.lblCaixa.TabIndex = 3;
            this.lblCaixa.Text = "Caixa";
            // 
            // cmbCaixa
            // 
            this.cmbCaixa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCaixa.FormattingEnabled = true;
            this.cmbCaixa.Location = new System.Drawing.Point(335, 50);
            this.cmbCaixa.Name = "cmbCaixa";
            this.cmbCaixa.Size = new System.Drawing.Size(300, 25);
            this.cmbCaixa.TabIndex = 2;
            // 
            // txtNomeMaquina
            // 
            this.txtNomeMaquina.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeMaquina.Location = new System.Drawing.Point(15, 50);
            this.txtNomeMaquina.Name = "txtNomeMaquina";
            this.txtNomeMaquina.Size = new System.Drawing.Size(300, 25);
            this.txtNomeMaquina.TabIndex = 1;
            // 
            // lblNomeMaquina
            // 
            this.lblNomeMaquina.AutoSize = true;
            this.lblNomeMaquina.Location = new System.Drawing.Point(15, 30);
            this.lblNomeMaquina.Name = "lblNomeMaquina";
            this.lblNomeMaquina.Size = new System.Drawing.Size(95, 17);
            this.lblNomeMaquina.TabIndex = 0;
            this.lblNomeMaquina.Text = "Nome Máquina";
            // 
            // dgvConfiguracoes
            // 
            this.dgvConfiguracoes.AllowUserToAddRows = false;
            this.dgvConfiguracoes.AllowUserToDeleteRows = false;
            this.dgvConfiguracoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConfiguracoes.Location = new System.Drawing.Point(12, 166);
            this.dgvConfiguracoes.Name = "dgvConfiguracoes";
            this.dgvConfiguracoes.ReadOnly = true;
            this.dgvConfiguracoes.RowHeadersWidth = 51;
            this.dgvConfiguracoes.RowTemplate.Height = 25;
            this.dgvConfiguracoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConfiguracoes.Size = new System.Drawing.Size(650, 250);
            this.dgvConfiguracoes.TabIndex = 1;
            // 
            // lblConfiguracoes
            // 
            this.lblConfiguracoes.AutoSize = true;
            this.lblConfiguracoes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblConfiguracoes.Location = new System.Drawing.Point(12, 146);
            this.lblConfiguracoes.Name = "lblConfiguracoes";
            this.lblConfiguracoes.Size = new System.Drawing.Size(206, 17);
            this.lblConfiguracoes.TabIndex = 2;
            this.lblConfiguracoes.Text = "Configurações Atuais (Máquina)";
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.LightGreen;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSalvar.Location = new System.Drawing.Point(582, 422);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(80, 30);
            this.btnSalvar.TabIndex = 3;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.LightCoral;
            this.btnSair.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSair.Location = new System.Drawing.Point(496, 422);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(80, 30);
            this.btnSair.TabIndex = 4;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // ConfigCaixaFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 464);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lblConfiguracoes);
            this.Controls.Add(this.dgvConfiguracoes);
            this.Controls.Add(this.gbConfiguracao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigCaixaFRM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração de Caixa por Máquina";
            this.Load += new System.EventHandler(this.ConfigCaixaFRM_Load);
            this.gbConfiguracao.ResumeLayout(false);
            this.gbConfiguracao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfiguracoes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}