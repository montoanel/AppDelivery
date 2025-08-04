namespace AppDelivery
{
    partial class CadastroProdutosFrm
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
            bntCancelarProduto = new Button();
            bntSalvarProduto = new Button();
            lblIdProduto = new Label();
            lblCodInterno = new Label();
            lblNome = new Label();
            lblCodBarras = new Label();
            lblGrupo = new Label();
            lblUnidadeMedida = new Label();
            lblcusto = new Label();
            lblPreco = new Label();
            lblTipoProduto = new Label();
            cmbProdutoInativo = new ComboBox();
            txtIDProduto = new TextBox();
            txtCodigoInterno = new TextBox();
            txtNomeProduto = new TextBox();
            txtCodigoBarras = new TextBox();
            txtCusto = new TextBox();
            txtPreco = new TextBox();
            txtGrupo = new TextBox();
            txtUnidadeMedida = new TextBox();
            lblProdutoInativo = new Label();
            printDialog1 = new PrintDialog();
            cmbTipoProduto = new ComboBox();
            SuspendLayout();
            // 
            // bntCancelarProduto
            // 
            bntCancelarProduto.Location = new Point(666, 631);
            bntCancelarProduto.Name = "bntCancelarProduto";
            bntCancelarProduto.Size = new Size(112, 34);
            bntCancelarProduto.TabIndex = 0;
            bntCancelarProduto.Text = "Cancelar";
            bntCancelarProduto.UseVisualStyleBackColor = true;
            bntCancelarProduto.Click += bntCancelarProduto_Click;
            // 
            // bntSalvarProduto
            // 
            bntSalvarProduto.Location = new Point(548, 631);
            bntSalvarProduto.Name = "bntSalvarProduto";
            bntSalvarProduto.Size = new Size(112, 34);
            bntSalvarProduto.TabIndex = 1;
            bntSalvarProduto.Text = "Salvar";
            bntSalvarProduto.UseVisualStyleBackColor = true;
            // 
            // lblIdProduto
            // 
            lblIdProduto.AutoSize = true;
            lblIdProduto.Location = new Point(12, 61);
            lblIdProduto.Name = "lblIdProduto";
            lblIdProduto.Size = new Size(30, 25);
            lblIdProduto.TabIndex = 2;
            lblIdProduto.Text = "ID";
            // 
            // lblCodInterno
            // 
            lblCodInterno.AutoSize = true;
            lblCodInterno.Location = new Point(12, 118);
            lblCodInterno.Name = "lblCodInterno";
            lblCodInterno.Size = new Size(133, 25);
            lblCodInterno.TabIndex = 3;
            lblCodInterno.Text = "Código Interno";
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Location = new Point(12, 232);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(61, 25);
            lblNome.TabIndex = 5;
            lblNome.Text = "Nome";
            // 
            // lblCodBarras
            // 
            lblCodBarras.AutoSize = true;
            lblCodBarras.Location = new Point(12, 175);
            lblCodBarras.Name = "lblCodBarras";
            lblCodBarras.Size = new Size(124, 25);
            lblCodBarras.TabIndex = 4;
            lblCodBarras.Text = "Código Barras";
            lblCodBarras.Click += lblCodBarras_Click;
            // 
            // lblGrupo
            // 
            lblGrupo.AutoSize = true;
            lblGrupo.Location = new Point(12, 460);
            lblGrupo.Name = "lblGrupo";
            lblGrupo.Size = new Size(62, 25);
            lblGrupo.TabIndex = 9;
            lblGrupo.Text = "Grupo";
            // 
            // lblUnidadeMedida
            // 
            lblUnidadeMedida.AutoSize = true;
            lblUnidadeMedida.Location = new Point(12, 403);
            lblUnidadeMedida.Name = "lblUnidadeMedida";
            lblUnidadeMedida.Size = new Size(143, 25);
            lblUnidadeMedida.TabIndex = 8;
            lblUnidadeMedida.Text = "Unidade Medida";
            // 
            // lblcusto
            // 
            lblcusto.AutoSize = true;
            lblcusto.Location = new Point(12, 346);
            lblcusto.Name = "lblcusto";
            lblcusto.Size = new Size(58, 25);
            lblcusto.TabIndex = 7;
            lblcusto.Text = "Custo";
            // 
            // lblPreco
            // 
            lblPreco.AutoSize = true;
            lblPreco.Location = new Point(12, 289);
            lblPreco.Name = "lblPreco";
            lblPreco.Size = new Size(56, 25);
            lblPreco.TabIndex = 6;
            lblPreco.Text = "Preço";
            // 
            // lblTipoProduto
            // 
            lblTipoProduto.AutoSize = true;
            lblTipoProduto.Location = new Point(12, 501);
            lblTipoProduto.Name = "lblTipoProduto";
            lblTipoProduto.Size = new Size(117, 25);
            lblTipoProduto.TabIndex = 10;
            lblTipoProduto.Text = "Tipo Produto";
            // 
            // cmbProdutoInativo
            // 
            cmbProdutoInativo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProdutoInativo.FormattingEnabled = true;
            cmbProdutoInativo.Location = new Point(231, 12);
            cmbProdutoInativo.Name = "cmbProdutoInativo";
            cmbProdutoInativo.Size = new Size(182, 33);
            cmbProdutoInativo.TabIndex = 11;
            // 
            // txtIDProduto
            // 
            txtIDProduto.Location = new Point(247, 61);
            txtIDProduto.Name = "txtIDProduto";
            txtIDProduto.ReadOnly = true;
            txtIDProduto.Size = new Size(150, 31);
            txtIDProduto.TabIndex = 12;
            // 
            // txtCodigoInterno
            // 
            txtCodigoInterno.Location = new Point(247, 118);
            txtCodigoInterno.Name = "txtCodigoInterno";
            txtCodigoInterno.Size = new Size(150, 31);
            txtCodigoInterno.TabIndex = 13;
            // 
            // txtNomeProduto
            // 
            txtNomeProduto.Location = new Point(247, 232);
            txtNomeProduto.Name = "txtNomeProduto";
            txtNomeProduto.Size = new Size(150, 31);
            txtNomeProduto.TabIndex = 15;
            // 
            // txtCodigoBarras
            // 
            txtCodigoBarras.Location = new Point(247, 175);
            txtCodigoBarras.Name = "txtCodigoBarras";
            txtCodigoBarras.Size = new Size(150, 31);
            txtCodigoBarras.TabIndex = 14;
            // 
            // txtCusto
            // 
            txtCusto.Location = new Point(247, 355);
            txtCusto.Name = "txtCusto";
            txtCusto.Size = new Size(150, 31);
            txtCusto.TabIndex = 17;
            // 
            // txtPreco
            // 
            txtPreco.Location = new Point(247, 298);
            txtPreco.Name = "txtPreco";
            txtPreco.Size = new Size(150, 31);
            txtPreco.TabIndex = 16;
            // 
            // txtGrupo
            // 
            txtGrupo.Location = new Point(247, 460);
            txtGrupo.Name = "txtGrupo";
            txtGrupo.Size = new Size(150, 31);
            txtGrupo.TabIndex = 19;
            // 
            // txtUnidadeMedida
            // 
            txtUnidadeMedida.Location = new Point(247, 403);
            txtUnidadeMedida.Name = "txtUnidadeMedida";
            txtUnidadeMedida.Size = new Size(150, 31);
            txtUnidadeMedida.TabIndex = 18;
            // 
            // lblProdutoInativo
            // 
            lblProdutoInativo.AutoSize = true;
            lblProdutoInativo.Location = new Point(12, 17);
            lblProdutoInativo.Name = "lblProdutoInativo";
            lblProdutoInativo.Size = new Size(79, 25);
            lblProdutoInativo.TabIndex = 22;
            lblProdutoInativo.Text = "Situação";
            // 
            // printDialog1
            // 
            printDialog1.UseEXDialog = true;
            // 
            // cmbTipoProduto
            // 
            cmbTipoProduto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoProduto.FormattingEnabled = true;
            cmbTipoProduto.Location = new Point(238, 501);
            cmbTipoProduto.Name = "cmbTipoProduto";
            cmbTipoProduto.Size = new Size(169, 33);
            cmbTipoProduto.TabIndex = 24;
            // 
            // CadastroProdutosFrm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 1024);
            Controls.Add(cmbTipoProduto);
            Controls.Add(lblProdutoInativo);
            Controls.Add(txtGrupo);
            Controls.Add(txtUnidadeMedida);
            Controls.Add(txtCusto);
            Controls.Add(txtPreco);
            Controls.Add(txtNomeProduto);
            Controls.Add(txtCodigoBarras);
            Controls.Add(txtCodigoInterno);
            Controls.Add(txtIDProduto);
            Controls.Add(cmbProdutoInativo);
            Controls.Add(lblTipoProduto);
            Controls.Add(lblGrupo);
            Controls.Add(lblUnidadeMedida);
            Controls.Add(lblcusto);
            Controls.Add(lblPreco);
            Controls.Add(lblNome);
            Controls.Add(lblCodBarras);
            Controls.Add(lblCodInterno);
            Controls.Add(lblIdProduto);
            Controls.Add(bntSalvarProduto);
            Controls.Add(bntCancelarProduto);
            Name = "CadastroProdutosFrm";
            Text = "Cadastrar Produtos";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bntCancelarProduto;
        private Button bntSalvarProduto;
        private Label lblIdProduto;
        private Label lblCodInterno;
        private Label lblNome;
        private Label lblCodBarras;
        private Label lblGrupo;
        private Label lblUnidadeMedida;
        private Label lblcusto;
        private Label lblPreco;
        private Label lblTipoProduto;
        private ComboBox cmbProdutoInativo;
        private TextBox txtIDProduto;
        private TextBox txtCodigoInterno;
        private TextBox txtNomeProduto;
        private TextBox txtCodigoBarras;
        private TextBox txtCusto;
        private TextBox txtPreco;
        private TextBox txtGrupo;
        private TextBox txtUnidadeMedida;
        private Label lblProdutoInativo;
        private PrintDialog printDialog1;
        private ComboBox cmbTipoProduto;
    }
}