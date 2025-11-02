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
            lblCaixa = new Label();
            cmbCaixa = new ComboBox();
            btnSalvar = new Button();
            btnSair = new Button();
            lblSelecionarCaixa = new Label();
            gbConfiguracao.SuspendLayout();
            SuspendLayout();
            // 
            // gbConfiguracao
            // 
            gbConfiguracao.Controls.Add(lblSelecionarCaixa);
            gbConfiguracao.Controls.Add(lblCaixa);
            gbConfiguracao.Controls.Add(cmbCaixa);
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
            // lblCaixa
            // 
            lblCaixa.AutoSize = true;
            lblCaixa.Location = new Point(367, 44);
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
            cmbCaixa.Location = new Point(166, 77);
            cmbCaixa.Margin = new Padding(4, 5, 4, 5);
            cmbCaixa.Name = "cmbCaixa";
            cmbCaixa.Size = new Size(427, 36);
            cmbCaixa.TabIndex = 2;
            // 
            // btnSalvar
            // 
            btnSalvar.BackColor = Color.LightGreen;
            btnSalvar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSalvar.Location = new Point(710, 703);
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
            btnSair.Location = new Point(832, 703);
            btnSair.Margin = new Padding(4, 5, 4, 5);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(114, 50);
            btnSair.TabIndex = 4;
            btnSair.Text = "Sair";
            btnSair.UseVisualStyleBackColor = false;
            btnSair.Click += btnSair_Click;
            // 
            // lblSelecionarCaixa
            // 
            lblSelecionarCaixa.AutoSize = true;
            lblSelecionarCaixa.Location = new Point(8, 83);
            lblSelecionarCaixa.Margin = new Padding(4, 0, 4, 0);
            lblSelecionarCaixa.Name = "lblSelecionarCaixa";
            lblSelecionarCaixa.Size = new Size(160, 28);
            lblSelecionarCaixa.TabIndex = 4;
            lblSelecionarCaixa.Text = "Selecione o caixa";
            // 
            // ConfigCaixaFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(963, 773);
            Controls.Add(btnSair);
            Controls.Add(btnSalvar);
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
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbConfiguracao;
        private System.Windows.Forms.Label lblCaixa;
        private System.Windows.Forms.ComboBox cmbCaixa;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnSair;
        private Label lblSelecionarCaixa;
    }
}