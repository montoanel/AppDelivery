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
            txtID = new TextBox();
            lblID = new Label();
            lblNome = new Label();
            txtNome = new TextBox();
            lblStatus = new Label();
            lblComissao = new Label();
            txtComissao = new TextBox();
            cmbStatus = new ComboBox();
            dataGridView1 = new DataGridView();
            btnNovo = new Button();
            btnEditar = new Button();
            btnSalvar = new Button();
            btnCancelar = new Button();
            btnAplicar = new Button();
            label1 = new Label();
            cmbFiltroAtendente = new ComboBox();
            txtboxFiltro = new TextBox();
            btnBuscar = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(txtID);
            panel1.Controls.Add(lblID);
            panel1.Controls.Add(lblNome);
            panel1.Controls.Add(txtNome);
            panel1.Controls.Add(lblStatus);
            panel1.Controls.Add(lblComissao);
            panel1.Controls.Add(txtComissao);
            panel1.Controls.Add(cmbStatus);
            panel1.Location = new Point(15, 295);
            panel1.Name = "panel1";
            panel1.Size = new Size(504, 174);
            panel1.TabIndex = 0;
            // 
            // txtID
            // 
            txtID.Location = new Point(39, 14);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(150, 31);
            txtID.TabIndex = 2;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(3, 14);
            lblID.Name = "lblID";
            lblID.Size = new Size(30, 25);
            lblID.TabIndex = 1;
            lblID.Text = "ID";
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Location = new Point(4, 74);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(61, 25);
            lblNome.TabIndex = 3;
            lblNome.Text = "Nome";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(60, 68);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(441, 31);
            txtNome.TabIndex = 4;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(205, 14);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(60, 25);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "Status";
            // 
            // lblComissao
            // 
            lblComissao.AutoSize = true;
            lblComissao.Location = new Point(4, 126);
            lblComissao.Name = "lblComissao";
            lblComissao.Size = new Size(90, 25);
            lblComissao.TabIndex = 7;
            lblComissao.Text = "Comissão";
            // 
            // txtComissao
            // 
            txtComissao.Location = new Point(100, 126);
            txtComissao.Name = "txtComissao";
            txtComissao.Size = new Size(87, 31);
            txtComissao.TabIndex = 8;
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Items.AddRange(new object[] { "Ativo", "Inativo" });
            cmbStatus.Location = new Point(271, 14);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(230, 33);
            cmbStatus.TabIndex = 6;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(15, 49);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(602, 240);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(623, 49);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(112, 34);
            btnNovo.TabIndex = 9;
            btnNovo.Text = "Novo";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(623, 89);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(112, 34);
            btnEditar.TabIndex = 10;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(623, 129);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(112, 34);
            btnSalvar.TabIndex = 11;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(623, 169);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 34);
            btnCancelar.TabIndex = 12;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAplicar
            // 
            btnAplicar.Location = new Point(623, 209);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(112, 34);
            btnAplicar.TabIndex = 13;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = true;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 9);
            label1.Name = "label1";
            label1.Size = new Size(100, 25);
            label1.TabIndex = 14;
            label1.Text = "Buscar por:";
            // 
            // cmbFiltroAtendente
            // 
            cmbFiltroAtendente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroAtendente.FormattingEnabled = true;
            cmbFiltroAtendente.Items.AddRange(new object[] { "Automático", "ID", "Nome" });
            cmbFiltroAtendente.Location = new Point(115, 9);
            cmbFiltroAtendente.Name = "cmbFiltroAtendente";
            cmbFiltroAtendente.Size = new Size(99, 33);
            cmbFiltroAtendente.TabIndex = 15;
            // 
            // txtboxFiltro
            // 
            txtboxFiltro.Location = new Point(220, 12);
            txtboxFiltro.Name = "txtboxFiltro";
            txtboxFiltro.Size = new Size(279, 31);
            txtboxFiltro.TabIndex = 16;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(505, 12);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(112, 34);
            btnBuscar.TabIndex = 17;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // FuncionariosFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 479);
            Controls.Add(dataGridView1);
            Controls.Add(btnBuscar);
            Controls.Add(txtboxFiltro);
            Controls.Add(cmbFiltroAtendente);
            Controls.Add(label1);
            Controls.Add(btnAplicar);
            Controls.Add(btnCancelar);
            Controls.Add(btnSalvar);
            Controls.Add(btnEditar);
            Controls.Add(btnNovo);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FuncionariosFRM";
            Text = "Cadastro de Funcionários";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private Label label1;
        private ComboBox cmbFiltroAtendente;
        private TextBox txtboxFiltro;
        private Button btnBuscar;
    }
}