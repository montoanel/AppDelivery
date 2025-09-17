namespace AppDelivery
{
    partial class FormasPagamentoFRM
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
            dataGridView1 = new DataGridView();
            btNovo = new Button();
            btEditar = new Button();
            btSalvar = new Button();
            btCancelar = new Button();
            lblID = new Label();
            lblNome = new Label();
            txtID = new TextBox();
            txtboxNome = new TextBox();
            cmbStatus = new ComboBox();
            btAplicar = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(776, 255);
            dataGridView1.TabIndex = 0;
            // 
            // btNovo
            // 
            btNovo.Location = new Point(168, 374);
            btNovo.Name = "btNovo";
            btNovo.Size = new Size(110, 38);
            btNovo.TabIndex = 1;
            btNovo.Text = "Novo";
            btNovo.UseVisualStyleBackColor = true;
            // 
            // btEditar
            // 
            btEditar.Location = new Point(284, 374);
            btEditar.Name = "btEditar";
            btEditar.Size = new Size(110, 38);
            btEditar.TabIndex = 2;
            btEditar.Text = "Editar";
            btEditar.UseVisualStyleBackColor = true;
            // 
            // btSalvar
            // 
            btSalvar.Location = new Point(400, 374);
            btSalvar.Name = "btSalvar";
            btSalvar.Size = new Size(110, 38);
            btSalvar.TabIndex = 3;
            btSalvar.Text = "Salvar";
            btSalvar.UseVisualStyleBackColor = true;
            // 
            // btCancelar
            // 
            btCancelar.Location = new Point(516, 374);
            btCancelar.Name = "btCancelar";
            btCancelar.Size = new Size(110, 38);
            btCancelar.TabIndex = 4;
            btCancelar.Text = "Cancelar";
            btCancelar.UseVisualStyleBackColor = true;
            btCancelar.Click += btCancelar_Click;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(12, 287);
            lblID.Name = "lblID";
            lblID.Size = new Size(30, 25);
            lblID.TabIndex = 5;
            lblID.Text = "ID";
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Location = new Point(12, 321);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(61, 25);
            lblNome.TabIndex = 6;
            lblNome.Text = "Nome";
            // 
            // txtID
            // 
            txtID.Location = new Point(39, 284);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(193, 31);
            txtID.TabIndex = 7;
            // 
            // txtboxNome
            // 
            txtboxNome.Location = new Point(79, 321);
            txtboxNome.Name = "txtboxNome";
            txtboxNome.Size = new Size(150, 31);
            txtboxNome.TabIndex = 8;
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Items.AddRange(new object[] { "Ativo", "Inativo" });
            cmbStatus.Location = new Point(606, 287);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(182, 33);
            cmbStatus.TabIndex = 9;
            // 
            // btAplicar
            // 
            btAplicar.Location = new Point(347, 418);
            btAplicar.Name = "btAplicar";
            btAplicar.Size = new Size(110, 38);
            btAplicar.TabIndex = 10;
            btAplicar.Text = "Aplicar";
            btAplicar.UseVisualStyleBackColor = true;
            // 
            // FormasPagamentoFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 477);
            Controls.Add(btAplicar);
            Controls.Add(cmbStatus);
            Controls.Add(txtboxNome);
            Controls.Add(txtID);
            Controls.Add(lblNome);
            Controls.Add(lblID);
            Controls.Add(btCancelar);
            Controls.Add(btSalvar);
            Controls.Add(btEditar);
            Controls.Add(btNovo);
            Controls.Add(dataGridView1);
            Name = "FormasPagamentoFRM";
            Text = "FormasPagamentoFRM";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btNovo;
        private Button btEditar;
        private Button btSalvar;
        private Button btCancelar;
        private Label lblID;
        private Label lblNome;
        private TextBox txtID;
        private TextBox txtboxNome;
        private ComboBox cmbStatus;
        private Button btAplicar;
    }
}