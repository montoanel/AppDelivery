namespace AppDelivery
{
    partial class FuncionariosFRM
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
            panel1 = new Panel();
            dataGridView1 = new DataGridView();
            lblID = new Label();
            txtID = new TextBox();
            txtNome = new TextBox();
            lblNome = new Label();
            lblStatus = new Label();
            lblComissao = new Label();
            txtComissao = new TextBox();
            btnNovo = new Button();
            btnEditar = new Button();
            btnSalvar = new Button();
            cmbStatus = new ComboBox();
            btnCancelar = new Button();
            btnAplicar = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(dataGridView1);
            panel1.Location = new Point(12, 198);
            panel1.Name = "panel1";
            panel1.Size = new Size(504, 240);
            panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(504, 240);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(12, 26);
            lblID.Name = "lblID";
            lblID.Size = new Size(30, 25);
            lblID.TabIndex = 1;
            lblID.Text = "ID";
            // 
            // txtID
            // 
            txtID.Location = new Point(67, 23);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(150, 31);
            txtID.TabIndex = 2;
            // 
            // txtNome
            // 
            txtNome.Location = new Point(77, 67);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(424, 31);
            txtNome.TabIndex = 4;
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Location = new Point(12, 70);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(61, 25);
            lblNome.TabIndex = 3;
            lblNome.Text = "Nome";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(255, 26);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(60, 25);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "Status";
            // 
            // lblComissao
            // 
            lblComissao.AutoSize = true;
            lblComissao.Location = new Point(12, 115);
            lblComissao.Name = "lblComissao";
            lblComissao.Size = new Size(90, 25);
            lblComissao.TabIndex = 7;
            lblComissao.Text = "Comissão";
            // 
            // txtComissao
            // 
            txtComissao.Location = new Point(108, 112);
            txtComissao.Name = "txtComissao";
            txtComissao.Size = new Size(87, 31);
            txtComissao.TabIndex = 8;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(12, 155);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(112, 34);
            btnNovo.TabIndex = 9;
            btnNovo.Text = "Novo";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(140, 155);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(112, 34);
            btnEditar.TabIndex = 10;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(268, 155);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(112, 34);
            btnSalvar.TabIndex = 11;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Items.AddRange(new object[] { "Ativo", "Inativo" });
            cmbStatus.Location = new Point(321, 23);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(180, 33);
            cmbStatus.TabIndex = 6;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(396, 155);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 34);
            btnCancelar.TabIndex = 12;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAplicar
            // 
            btnAplicar.Location = new Point(396, 115);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(112, 34);
            btnAplicar.TabIndex = 13;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = true;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // FuncionariosFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(528, 450);
            Controls.Add(btnAplicar);
            Controls.Add(btnCancelar);
            Controls.Add(btnSalvar);
            Controls.Add(btnEditar);
            Controls.Add(btnNovo);
            Controls.Add(txtComissao);
            Controls.Add(lblComissao);
            Controls.Add(cmbStatus);
            Controls.Add(lblStatus);
            Controls.Add(txtNome);
            Controls.Add(lblNome);
            Controls.Add(txtID);
            Controls.Add(lblID);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FuncionariosFRM";
            Text = "Cadastro de Funcionários";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private DataGridView dataGridView1;
        private Label lblID;
        private TextBox txtID;
        private TextBox txtNome;
        private Label lblNome;
        private Label lblStatus;
        private Label lblComissao;
        private TextBox txtComissao;
        private Button btnNovo;
        private Button btnEditar;
        private Button btnSalvar;
        private ComboBox cmbStatus;
        private Button btnCancelar;
        private Button btnAplicar;
    }
}