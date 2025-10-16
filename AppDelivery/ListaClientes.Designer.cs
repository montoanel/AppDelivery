namespace AppDelivery
{
    partial class ListaClientes
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
            dgvListaClientes = new DataGridView();
            btnNovo = new Button();
            btnEditar = new Button();
            btnCancelar = new Button();
            cmbListaFiltroClientes = new ComboBox();
            txtListarCliente = new TextBox();
            btnFiltrar = new Button();
            btnAplicar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvListaClientes).BeginInit();
            SuspendLayout();
            // 
            // dgvListaClientes
            // 
            dgvListaClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListaClientes.Location = new Point(2, 1);
            dgvListaClientes.Name = "dgvListaClientes";
            dgvListaClientes.ReadOnly = true;
            dgvListaClientes.RowHeadersWidth = 62;
            dgvListaClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvListaClientes.Size = new Size(1891, 641);
            dgvListaClientes.TabIndex = 0;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(566, 895);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(112, 34);
            btnNovo.TabIndex = 1;
            btnNovo.Text = "Novo";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += bntNovo_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(684, 895);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(112, 34);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(802, 895);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 34);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // cmbListaFiltroClientes
            // 
            cmbListaFiltroClientes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbListaFiltroClientes.FormattingEnabled = true;
            cmbListaFiltroClientes.Location = new Point(2, 648);
            cmbListaFiltroClientes.Name = "cmbListaFiltroClientes";
            cmbListaFiltroClientes.Size = new Size(182, 33);
            cmbListaFiltroClientes.TabIndex = 4;
            // 
            // txtListarCliente
            // 
            txtListarCliente.Location = new Point(190, 650);
            txtListarCliente.Name = "txtListarCliente";
            txtListarCliente.Size = new Size(488, 31);
            txtListarCliente.TabIndex = 5;
            // 
            // btnFiltrar
            // 
            btnFiltrar.Location = new Point(684, 646);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(112, 34);
            btnFiltrar.TabIndex = 6;
            btnFiltrar.Text = "Buscar";
            btnFiltrar.UseVisualStyleBackColor = true;
            btnFiltrar.Click += btnFiltrar_Click;
            // 
            // btnAplicar
            // 
            btnAplicar.Location = new Point(802, 855);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(112, 34);
            btnAplicar.TabIndex = 7;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = true;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // ListaClientes
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 1024);
            Controls.Add(btnAplicar);
            Controls.Add(btnFiltrar);
            Controls.Add(txtListarCliente);
            Controls.Add(cmbListaFiltroClientes);
            Controls.Add(btnCancelar);
            Controls.Add(btnEditar);
            Controls.Add(btnNovo);
            Controls.Add(dgvListaClientes);
            Name = "ListaClientes";
            Text = "Cadastros de Clientes";
            ((System.ComponentModel.ISupportInitialize)dgvListaClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dgvListaClientes;
        private Button btnNovo;
        private Button btnEditar;
        private Button btnCancelar;
        private ComboBox cmbListaFiltroClientes;
        private TextBox txtListarCliente;
        private Button btnFiltrar;
        private Button btnAplicar;
    }
}