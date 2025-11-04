namespace AppDelivery
{
    partial class PainelUsuariosFRM
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
            groupBox1 = new GroupBox();
            dataGridView1 = new DataGridView();
            btnBuscar = new Button();
            txtboxFiltro = new TextBox();
            cmbFiltroAtendente = new ComboBox();
            label1 = new Label();
            btnAplicar = new Button();
            btnCancelar = new Button();
            btnSalvar = new Button();
            btnEditar = new Button();
            btnNovo = new Button();
            groupBox2 = new GroupBox();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 621);
            panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Controls.Add(btnBuscar);
            groupBox1.Controls.Add(txtboxFiltro);
            groupBox1.Controls.Add(cmbFiltroAtendente);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnAplicar);
            groupBox1.Controls.Add(btnCancelar);
            groupBox1.Controls.Add(btnSalvar);
            groupBox1.Controls.Add(btnEditar);
            groupBox1.Controls.Add(btnNovo);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 341);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 69);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(602, 240);
            dataGridView1.TabIndex = 28;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(502, 30);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(112, 34);
            btnBuscar.TabIndex = 37;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // txtboxFiltro
            // 
            txtboxFiltro.Location = new Point(217, 30);
            txtboxFiltro.Name = "txtboxFiltro";
            txtboxFiltro.Size = new Size(279, 31);
            txtboxFiltro.TabIndex = 36;
            // 
            // cmbFiltroAtendente
            // 
            cmbFiltroAtendente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroAtendente.FormattingEnabled = true;
            cmbFiltroAtendente.Items.AddRange(new object[] { "Automático", "ID", "Nome" });
            cmbFiltroAtendente.Location = new Point(112, 30);
            cmbFiltroAtendente.Name = "cmbFiltroAtendente";
            cmbFiltroAtendente.Size = new Size(99, 33);
            cmbFiltroAtendente.TabIndex = 35;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 30);
            label1.Name = "label1";
            label1.Size = new Size(100, 25);
            label1.TabIndex = 34;
            label1.Text = "Buscar por:";
            // 
            // btnAplicar
            // 
            btnAplicar.Location = new Point(652, 211);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(112, 34);
            btnAplicar.TabIndex = 33;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(652, 171);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 34);
            btnCancelar.TabIndex = 32;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(652, 131);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(112, 34);
            btnSalvar.TabIndex = 31;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(652, 91);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(112, 34);
            btnEditar.TabIndex = 30;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(652, 51);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(112, 34);
            btnNovo.TabIndex = 29;
            btnNovo.Text = "Novo";
            btnNovo.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Location = new Point(18, 375);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(758, 234);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "groupBox2";
            // 
            // PainelUsuariosFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 621);
            Controls.Add(panel1);
            Name = "PainelUsuariosFRM";
            Text = "PainelUsuariosFRM";
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private Button btnBuscar;
        private TextBox txtboxFiltro;
        private ComboBox cmbFiltroAtendente;
        private Label label1;
        private Button btnAplicar;
        private Button btnCancelar;
        private Button btnSalvar;
        private Button btnEditar;
        private Button btnNovo;
        private GroupBox groupBox2;
    }
}