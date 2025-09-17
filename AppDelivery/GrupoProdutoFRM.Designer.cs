namespace AppDelivery
{
    partial class GrupoProdutoFRM
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
            lblNomeGrupo = new Label();
            dataGridView1 = new DataGridView();
            txtNomeGrupo = new TextBox();
            btnNovo = new Button();
            btnEditar = new Button();
            btnSalvar = new Button();
            btnCancelar = new Button();
            chkStatus = new CheckBox();
            btnSelecionar = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lblNomeGrupo
            // 
            lblNomeGrupo.AutoSize = true;
            lblNomeGrupo.Location = new Point(12, 256);
            lblNomeGrupo.Name = "lblNomeGrupo";
            lblNomeGrupo.Size = new Size(61, 25);
            lblNomeGrupo.TabIndex = 0;
            lblNomeGrupo.Text = "Nome";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(579, 225);
            dataGridView1.TabIndex = 1;
            // 
            // txtNomeGrupo
            // 
            txtNomeGrupo.Location = new Point(77, 256);
            txtNomeGrupo.Name = "txtNomeGrupo";
            txtNomeGrupo.Size = new Size(404, 31);
            txtNomeGrupo.TabIndex = 2;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(597, 12);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(112, 34);
            btnNovo.TabIndex = 4;
            btnNovo.Text = "Novo";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(597, 52);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(112, 34);
            btnEditar.TabIndex = 5;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(597, 92);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(112, 34);
            btnSalvar.TabIndex = 6;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(597, 132);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 34);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // chkStatus
            // 
            chkStatus.AutoSize = true;
            chkStatus.Location = new Point(12, 302);
            chkStatus.Name = "chkStatus";
            chkStatus.Size = new Size(80, 29);
            chkStatus.TabIndex = 8;
            chkStatus.Text = "Ativo";
            chkStatus.UseVisualStyleBackColor = true;
            // 
            // btnSelecionar
            // 
            btnSelecionar.Location = new Point(228, 356);
            btnSelecionar.Name = "btnSelecionar";
            btnSelecionar.Size = new Size(112, 34);
            btnSelecionar.TabIndex = 9;
            btnSelecionar.Text = "Selecionar";
            btnSelecionar.UseVisualStyleBackColor = true;
            btnSelecionar.Click += btnSelecionar_Click;
            // 
            // GrupoProdutoFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSelecionar);
            Controls.Add(chkStatus);
            Controls.Add(btnCancelar);
            Controls.Add(btnSalvar);
            Controls.Add(btnEditar);
            Controls.Add(btnNovo);
            Controls.Add(txtNomeGrupo);
            Controls.Add(dataGridView1);
            Controls.Add(lblNomeGrupo);
            Name = "GrupoProdutoFRM";
            Text = "GrupoProdutoFRM";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNomeGrupo;
        private DataGridView dataGridView1;
        private TextBox txtNomeGrupo;
        private Button btnNovo;
        private Button btnEditar;
        private Button btnSalvar;
        private Button btnCancelar;
        private CheckBox chkStatus;
        private Button btnSelecionar;
    }
}