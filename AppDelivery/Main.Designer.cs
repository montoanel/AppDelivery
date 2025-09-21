namespace AppDelivery
{
    partial class Main
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
            pctBoxClientes = new PictureBox();
            pctBoxProdutos = new PictureBox();
            menuStrip1 = new MenuStrip();
            cadastrosToolStripMenuItem = new ToolStripMenuItem();
            clientesToolStripMenuItem = new ToolStripMenuItem();
            produtosToolStripMenuItem = new ToolStripMenuItem();
            unidadeDeMedidaToolStripMenuItem = new ToolStripMenuItem();
            tipoProdutosToolStripMenuItem = new ToolStripMenuItem();
            gruposDeProdutosToolStripMenuItem = new ToolStripMenuItem();
            formasDePagamentoToolStripMenuItem = new ToolStripMenuItem();
            pctAtendimentos = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pctBoxClientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctBoxProdutos).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctAtendimentos).BeginInit();
            SuspendLayout();
            // 
            // pctBoxClientes
            // 
            pctBoxClientes.Image = Properties.Resources.clientes;
            pctBoxClientes.Location = new Point(9, 43);
            pctBoxClientes.Name = "pctBoxClientes";
            pctBoxClientes.Size = new Size(85, 90);
            pctBoxClientes.SizeMode = PictureBoxSizeMode.StretchImage;
            pctBoxClientes.TabIndex = 0;
            pctBoxClientes.TabStop = false;
            pctBoxClientes.Click += pctBoxClientes_Click;
            pctBoxClientes.MouseEnter += pctBoxClientes_MouseEnter;
            pctBoxClientes.MouseLeave += pctBoxClientes_MouseLeave;
            // 
            // pctBoxProdutos
            // 
            pctBoxProdutos.Image = Properties.Resources.produtos;
            pctBoxProdutos.Location = new Point(105, 43);
            pctBoxProdutos.Name = "pctBoxProdutos";
            pctBoxProdutos.Size = new Size(85, 90);
            pctBoxProdutos.SizeMode = PictureBoxSizeMode.StretchImage;
            pctBoxProdutos.TabIndex = 1;
            pctBoxProdutos.TabStop = false;
            pctBoxProdutos.Click += pctBoxProdutos_Click;
            pctBoxProdutos.MouseEnter += pctBoxProdutos_MouseEnter;
            pctBoxProdutos.MouseLeave += pctBoxProdutos_MouseLeave;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { cadastrosToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1898, 33);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // cadastrosToolStripMenuItem
            // 
            cadastrosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clientesToolStripMenuItem, produtosToolStripMenuItem, unidadeDeMedidaToolStripMenuItem, tipoProdutosToolStripMenuItem, gruposDeProdutosToolStripMenuItem, formasDePagamentoToolStripMenuItem });
            cadastrosToolStripMenuItem.Name = "cadastrosToolStripMenuItem";
            cadastrosToolStripMenuItem.Size = new Size(107, 29);
            cadastrosToolStripMenuItem.Text = "Cadastros";
            // 
            // clientesToolStripMenuItem
            // 
            clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            clientesToolStripMenuItem.Size = new Size(293, 34);
            clientesToolStripMenuItem.Text = "Clientes";
            clientesToolStripMenuItem.Click += clientesToolStripMenuItem_Click;
            // 
            // produtosToolStripMenuItem
            // 
            produtosToolStripMenuItem.Name = "produtosToolStripMenuItem";
            produtosToolStripMenuItem.Size = new Size(293, 34);
            produtosToolStripMenuItem.Text = "Produtos";
            produtosToolStripMenuItem.Click += produtosToolStripMenuItem_Click;
            // 
            // unidadeDeMedidaToolStripMenuItem
            // 
            unidadeDeMedidaToolStripMenuItem.Name = "unidadeDeMedidaToolStripMenuItem";
            unidadeDeMedidaToolStripMenuItem.Size = new Size(293, 34);
            unidadeDeMedidaToolStripMenuItem.Text = "Unidade de Medida";
            // 
            // tipoProdutosToolStripMenuItem
            // 
            tipoProdutosToolStripMenuItem.Name = "tipoProdutosToolStripMenuItem";
            tipoProdutosToolStripMenuItem.Size = new Size(293, 34);
            tipoProdutosToolStripMenuItem.Text = "Tipo Produtos";
            // 
            // gruposDeProdutosToolStripMenuItem
            // 
            gruposDeProdutosToolStripMenuItem.Name = "gruposDeProdutosToolStripMenuItem";
            gruposDeProdutosToolStripMenuItem.Size = new Size(293, 34);
            gruposDeProdutosToolStripMenuItem.Text = "Grupos de Produtos";
            gruposDeProdutosToolStripMenuItem.Click += gruposDeProdutosToolStripMenuItem_Click;
            // 
            // formasDePagamentoToolStripMenuItem
            // 
            formasDePagamentoToolStripMenuItem.Name = "formasDePagamentoToolStripMenuItem";
            formasDePagamentoToolStripMenuItem.Size = new Size(293, 34);
            formasDePagamentoToolStripMenuItem.Text = "Formas de Pagamento";
            formasDePagamentoToolStripMenuItem.Click += formasDePagamentoToolStripMenuItem_Click;
            // 
            // pctAtendimentos
            // 
            pctAtendimentos.Image = Properties.Resources.icons8_lista_100;
            pctAtendimentos.Location = new Point(196, 43);
            pctAtendimentos.Name = "pctAtendimentos";
            pctAtendimentos.Size = new Size(100, 90);
            pctAtendimentos.SizeMode = PictureBoxSizeMode.StretchImage;
            pctAtendimentos.TabIndex = 3;
            pctAtendimentos.TabStop = false;
            pctAtendimentos.Click += pctAtendimentos_Click;
            pctAtendimentos.MouseEnter += pctAtendimentos_MouseEnter;
            pctAtendimentos.MouseLeave += pctAtendimentos_MouseLeave;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 1024);
            Controls.Add(pctAtendimentos);
            Controls.Add(pctBoxProdutos);
            Controls.Add(pctBoxClientes);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Main";
            Text = "Main";
            MouseEnter += Main_MouseEnter;
            ((System.ComponentModel.ISupportInitialize)pctBoxClientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctBoxProdutos).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pctAtendimentos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pctBoxClientes;
        private PictureBox pctBoxProdutos;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem cadastrosToolStripMenuItem;
        private ToolStripMenuItem clientesToolStripMenuItem;
        private ToolStripMenuItem produtosToolStripMenuItem;
        private ToolStripMenuItem unidadeDeMedidaToolStripMenuItem;
        private ToolStripMenuItem tipoProdutosToolStripMenuItem;
        private ToolStripMenuItem gruposDeProdutosToolStripMenuItem;
        private ToolStripMenuItem formasDePagamentoToolStripMenuItem;
        private PictureBox pctAtendimentos;
    }
}