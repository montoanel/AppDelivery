namespace AppDelivery
{
    partial class CadastroClienteFRM
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblNome = new Label();
            lblCPF = new Label();
            lblEndereco = new Label();
            lblTelefone = new Label();
            txtNome = new TextBox();
            txtEndereco = new TextBox();
            lblNumero = new Label();
            label1 = new Label();
            txtNumero = new TextBox();
            txtBairro = new TextBox();
            lblCodCli = new Label();
            txtCodCli = new TextBox();
            btnSalvar = new Button();
            btnCancelar = new Button();
            txtTelefone = new MaskedTextBox();
            cmbTipoPessoa = new ComboBox();
            txtCpfCnpj = new MaskedTextBox();
            lblComplemento = new Label();
            txtComplemento = new TextBox();
            lblTipoPessoa = new Label();
            SuspendLayout();
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Location = new Point(12, 59);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(65, 25);
            lblNome.TabIndex = 2;
            lblNome.Text = "Nome:";
            // 
            // lblCPF
            // 
            lblCPF.AutoSize = true;
            lblCPF.Location = new Point(12, 103);
            lblCPF.Name = "lblCPF";
            lblCPF.Size = new Size(102, 25);
            lblCPF.TabIndex = 3;
            lblCPF.Text = "CPF / CNPJ:";
            // 
            // lblEndereco
            // 
            lblEndereco.AutoSize = true;
            lblEndereco.Location = new Point(12, 147);
            lblEndereco.Name = "lblEndereco";
            lblEndereco.Size = new Size(89, 25);
            lblEndereco.TabIndex = 4;
            lblEndereco.Text = "Endereço:";
            // 
            // lblTelefone
            // 
            lblTelefone.AutoSize = true;
            lblTelefone.Location = new Point(12, 332);
            lblTelefone.Name = "lblTelefone";
            lblTelefone.Size = new Size(81, 25);
            lblTelefone.TabIndex = 5;
            lblTelefone.Text = "Telefone:";
            // 
            // txtNome
            // 
            txtNome.BackColor = SystemColors.Window;
            txtNome.Cursor = Cursors.IBeam;
            txtNome.Location = new Point(120, 53);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(423, 31);
            txtNome.TabIndex = 2;
            // 
            // txtEndereco
            // 
            txtEndereco.Location = new Point(120, 141);
            txtEndereco.Name = "txtEndereco";
            txtEndereco.Size = new Size(423, 31);
            txtEndereco.TabIndex = 4;
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.Location = new Point(12, 194);
            lblNumero.Name = "lblNumero";
            lblNumero.Size = new Size(81, 25);
            lblNumero.TabIndex = 10;
            lblNumero.Text = "Número:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 288);
            label1.Name = "label1";
            label1.Size = new Size(62, 25);
            label1.TabIndex = 11;
            label1.Text = "Bairro:";
            // 
            // txtNumero
            // 
            txtNumero.Location = new Point(148, 194);
            txtNumero.Name = "txtNumero";
            txtNumero.Size = new Size(395, 31);
            txtNumero.TabIndex = 5;
            // 
            // txtBairro
            // 
            txtBairro.Location = new Point(148, 288);
            txtBairro.Name = "txtBairro";
            txtBairro.Size = new Size(395, 31);
            txtBairro.TabIndex = 7;
            // 
            // lblCodCli
            // 
            lblCodCli.AutoSize = true;
            lblCodCli.Location = new Point(12, 9);
            lblCodCli.Name = "lblCodCli";
            lblCodCli.Size = new Size(71, 25);
            lblCodCli.TabIndex = 14;
            lblCodCli.Text = "Codigo";
            // 
            // txtCodCli
            // 
            txtCodCli.BorderStyle = BorderStyle.None;
            txtCodCli.Location = new Point(89, 9);
            txtCodCli.Name = "txtCodCli";
            txtCodCli.ReadOnly = true;
            txtCodCli.Size = new Size(89, 24);
            txtCodCli.TabIndex = 15;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(700, 849);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(112, 34);
            btnSalvar.TabIndex = 8;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(818, 849);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 34);
            btnCancelar.TabIndex = 10;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // txtTelefone
            // 
            txtTelefone.Location = new Point(148, 325);
            txtTelefone.Name = "txtTelefone";
            txtTelefone.Size = new Size(395, 31);
            txtTelefone.TabIndex = 8;
            txtTelefone.Leave += txtTelefone_Leave;
            // 
            // cmbTipoPessoa
            // 
            cmbTipoPessoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoPessoa.FormattingEnabled = true;
            cmbTipoPessoa.Location = new Point(345, 9);
            cmbTipoPessoa.Name = "cmbTipoPessoa";
            cmbTipoPessoa.Size = new Size(198, 33);
            cmbTipoPessoa.TabIndex = 1;
            cmbTipoPessoa.SelectedValueChanged += cmbTipoPessoa_SelectedValueChanged;
            // 
            // txtCpfCnpj
            // 
            txtCpfCnpj.Location = new Point(120, 103);
            txtCpfCnpj.Name = "txtCpfCnpj";
            txtCpfCnpj.Size = new Size(423, 31);
            txtCpfCnpj.TabIndex = 3;
            txtCpfCnpj.Leave += txtCpfCnpj_Leave;
            // 
            // lblComplemento
            // 
            lblComplemento.AutoSize = true;
            lblComplemento.Location = new Point(12, 240);
            lblComplemento.Name = "lblComplemento";
            lblComplemento.Size = new Size(130, 25);
            lblComplemento.TabIndex = 19;
            lblComplemento.Text = "Complemento:";
            // 
            // txtComplemento
            // 
            txtComplemento.Location = new Point(148, 240);
            txtComplemento.Name = "txtComplemento";
            txtComplemento.Size = new Size(395, 31);
            txtComplemento.TabIndex = 6;
            // 
            // lblTipoPessoa
            // 
            lblTipoPessoa.AutoSize = true;
            lblTipoPessoa.Location = new Point(233, 9);
            lblTipoPessoa.Name = "lblTipoPessoa";
            lblTipoPessoa.Size = new Size(106, 25);
            lblTipoPessoa.TabIndex = 20;
            lblTipoPessoa.Text = "Tipo Pessoa";
            // 
            // CadastroClienteFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 1024);
            Controls.Add(lblTipoPessoa);
            Controls.Add(txtComplemento);
            Controls.Add(lblComplemento);
            Controls.Add(txtCpfCnpj);
            Controls.Add(cmbTipoPessoa);
            Controls.Add(txtTelefone);
            Controls.Add(btnCancelar);
            Controls.Add(btnSalvar);
            Controls.Add(txtCodCli);
            Controls.Add(lblCodCli);
            Controls.Add(txtBairro);
            Controls.Add(txtNumero);
            Controls.Add(label1);
            Controls.Add(lblNumero);
            Controls.Add(txtEndereco);
            Controls.Add(txtNome);
            Controls.Add(lblTelefone);
            Controls.Add(lblEndereco);
            Controls.Add(lblCPF);
            Controls.Add(lblNome);
            Name = "CadastroClienteFRM";
            Text = "Cadastro de Clientes";
            Load += CadastroClienteFRM_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblNome;
        private Label lblCPF;
        private Label lblEndereco;
        private Label lblTelefone;
        private TextBox txtNome;
        private TextBox txtEndereco;
        private Label lblNumero;
        private Label label1;
        private TextBox txtNumero;
        private TextBox txtBairro;
        private Label lblCodCli;
        private TextBox txtCodCli;
        private Button btnSalvar;
        private Button btnCancelar;
        private MaskedTextBox txtTelefone;
        private ComboBox cmbTipoPessoa;
        private MaskedTextBox txtCpfCnpj;
        private Label lblComplemento;
        private TextBox txtComplemento;
        private Label lblTipoPessoa;
    }
}
