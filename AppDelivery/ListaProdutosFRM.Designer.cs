namespace AppDelivery
{
    partial class ListaProdutosFRM
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
            dgvListaProdutos = new DataGridView();
            btnFiltrarProdutos = new Button();
            txtListarProdutos = new TextBox();
            cmbListaFiltroProdutos = new ComboBox();
            btnCancelar = new Button();
            btnEditarProduto = new Button();
            btnNovoProduto = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvListaProdutos).BeginInit();
            SuspendLayout();
            // 
            // dgvListaProdutos
            // 
            dgvListaProdutos.AllowUserToAddRows = false;
            dgvListaProdutos.AllowUserToDeleteRows = false;
            dgvListaProdutos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListaProdutos.Location = new Point(3, 12);
            dgvListaProdutos.Name = "dgvListaProdutos";
            dgvListaProdutos.ReadOnly = true;
            dgvListaProdutos.RowHeadersWidth = 62;
            dgvListaProdutos.Size = new Size(1891, 641);
            dgvListaProdutos.TabIndex = 0;
            // 
            // btnFiltrarProdutos
            // 
            btnFiltrarProdutos.Location = new Point(685, 668);
            btnFiltrarProdutos.Name = "btnFiltrarProdutos";
            btnFiltrarProdutos.Size = new Size(112, 34);
            btnFiltrarProdutos.TabIndex = 9;
            btnFiltrarProdutos.Text = "Buscar";
            btnFiltrarProdutos.UseVisualStyleBackColor = true;
            btnFiltrarProdutos.Click += btnFiltrarProdutos_Click;
            // 
            // txtListarProdutos
            // 
            txtListarProdutos.Location = new Point(191, 668);
            txtListarProdutos.Name = "txtListarProdutos";
            txtListarProdutos.Size = new Size(488, 31);
            txtListarProdutos.TabIndex = 8;
            // 
            // cmbListaFiltroProdutos
            // 
            cmbListaFiltroProdutos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbListaFiltroProdutos.FormattingEnabled = true;
            cmbListaFiltroProdutos.Location = new Point(3, 668);
            cmbListaFiltroProdutos.Name = "cmbListaFiltroProdutos";
            cmbListaFiltroProdutos.Size = new Size(182, 33);
            cmbListaFiltroProdutos.TabIndex = 7;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(865, 895);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 34);
            btnCancelar.TabIndex = 12;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnEditarProduto
            // 
            btnEditarProduto.Location = new Point(747, 895);
            btnEditarProduto.Name = "btnEditarProduto";
            btnEditarProduto.Size = new Size(112, 34);
            btnEditarProduto.TabIndex = 11;
            btnEditarProduto.Text = "Editar";
            btnEditarProduto.UseVisualStyleBackColor = true;
            // 
            // btnNovoProduto
            // 
            btnNovoProduto.Location = new Point(629, 895);
            btnNovoProduto.Name = "btnNovoProduto";
            btnNovoProduto.Size = new Size(112, 34);
            btnNovoProduto.TabIndex = 10;
            btnNovoProduto.Text = "Novo";
            btnNovoProduto.UseVisualStyleBackColor = true;
            btnNovoProduto.Click += btnNovoProduto_Click;
            // 
            // ListaProdutosFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 1024);
            Controls.Add(btnCancelar);
            Controls.Add(btnEditarProduto);
            Controls.Add(btnNovoProduto);
            Controls.Add(btnFiltrarProdutos);
            Controls.Add(txtListarProdutos);
            Controls.Add(cmbListaFiltroProdutos);
            Controls.Add(dgvListaProdutos);
            Name = "ListaProdutosFRM";
            Text = "Relação de Produtos";
            ((System.ComponentModel.ISupportInitialize)dgvListaProdutos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvListaProdutos;
        private Button btnFiltrarProdutos;
        private TextBox txtListarProdutos;
        private ComboBox cmbListaFiltroProdutos;
        private Button btnCancelar;
        private Button btnEditarProduto;
        private Button btnNovoProduto;
    }
}