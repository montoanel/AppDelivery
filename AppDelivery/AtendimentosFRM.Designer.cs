namespace AppDelivery
{
    partial class AtendimentosFRM
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
            pctNovoAtendimento = new PictureBox();
            panel2 = new Panel();
            button1 = new Button();
            checkBox4 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            lblTipoAtendimento = new Label();
            dataGridView1 = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctNovoAtendimento).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(pctNovoAtendimento);
            panel1.Location = new Point(1, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(1895, 111);
            panel1.TabIndex = 0;
            // 
            // pctNovoAtendimento
            // 
            pctNovoAtendimento.Image = Properties.Resources.add;
            pctNovoAtendimento.Location = new Point(11, 3);
            pctNovoAtendimento.Name = "pctNovoAtendimento";
            pctNovoAtendimento.Size = new Size(118, 93);
            pctNovoAtendimento.SizeMode = PictureBoxSizeMode.StretchImage;
            pctNovoAtendimento.TabIndex = 1;
            pctNovoAtendimento.TabStop = false;
            pctNovoAtendimento.MouseEnter += pctNovoAtendimento_MouseEnter;
            pctNovoAtendimento.MouseLeave += pctNovoAtendimento_MouseLeave;
            // 
            // panel2
            // 
            panel2.Controls.Add(button1);
            panel2.Controls.Add(checkBox4);
            panel2.Controls.Add(checkBox3);
            panel2.Controls.Add(checkBox2);
            panel2.Controls.Add(checkBox1);
            panel2.Controls.Add(lblTipoAtendimento);
            panel2.Controls.Add(dataGridView1);
            panel2.Location = new Point(1, 133);
            panel2.Name = "panel2";
            panel2.Size = new Size(1895, 309);
            panel2.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(455, 39);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 6;
            button1.Text = "Filtrar";
            button1.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Checked = true;
            checkBox4.CheckState = CheckState.Checked;
            checkBox4.Location = new Point(363, 39);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(86, 29);
            checkBox4.TabIndex = 5;
            checkBox4.Text = "Todos";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(226, 39);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(131, 29);
            checkBox3.TabIndex = 4;
            checkBox3.Text = "Encomenda";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(118, 39);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(102, 29);
            checkBox2.TabIndex = 3;
            checkBox2.Text = "Retirada";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(11, 39);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(101, 29);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Delivery";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // lblTipoAtendimento
            // 
            lblTipoAtendimento.AutoSize = true;
            lblTipoAtendimento.Location = new Point(11, 11);
            lblTipoAtendimento.Name = "lblTipoAtendimento";
            lblTipoAtendimento.Size = new Size(156, 25);
            lblTipoAtendimento.TabIndex = 1;
            lblTipoAtendimento.Text = "Tipo Atendimento";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 78);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1895, 231);
            dataGridView1.TabIndex = 0;
            // 
            // AtendimentosFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 1024);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "AtendimentosFRM";
            Text = "AtendimentosFRM";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pctNovoAtendimento).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pctNovoAtendimento;
        private Panel panel2;
        private DataGridView dataGridView1;
        private Label lblTipoAtendimento;
        private CheckBox checkBox1;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private Button button1;
    }
}