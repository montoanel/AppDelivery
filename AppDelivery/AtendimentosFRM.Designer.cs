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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AtendimentosFRM));
            panel1 = new Panel();
            pctEncomenda = new PictureBox();
            pctRetiradaBalcao = new PictureBox();
            pctDelivery = new PictureBox();
            pctVendaRapida = new PictureBox();
            panel2 = new Panel();
            dateTimePicker2 = new DateTimePicker();
            dateTimePicker1 = new DateTimePicker();
            checkBoxCancelado = new CheckBox();
            checkBoxRecebido = new CheckBox();
            checkBoxEmAtendimento = new CheckBox();
            checkBoxAberto = new CheckBox();
            lblSituacao = new Label();
            checkBox5 = new CheckBox();
            button1 = new Button();
            checkBox4 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            lblTipoAtendimento = new Label();
            dataGridView1 = new DataGridView();
            pctReceber = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctEncomenda).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctRetiradaBalcao).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctDelivery).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctVendaRapida).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctReceber).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(pctReceber);
            panel1.Controls.Add(pctEncomenda);
            panel1.Controls.Add(pctRetiradaBalcao);
            panel1.Controls.Add(pctDelivery);
            panel1.Controls.Add(pctVendaRapida);
            panel1.Location = new Point(1, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(1895, 111);
            panel1.TabIndex = 0;
            // 
            // pctEncomenda
            // 
            pctEncomenda.Image = (Image)resources.GetObject("pctEncomenda.Image");
            pctEncomenda.Location = new Point(413, 3);
            pctEncomenda.Name = "pctEncomenda";
            pctEncomenda.Size = new Size(118, 93);
            pctEncomenda.SizeMode = PictureBoxSizeMode.StretchImage;
            pctEncomenda.TabIndex = 4;
            pctEncomenda.TabStop = false;
            // 
            // pctRetiradaBalcao
            // 
            pctRetiradaBalcao.Image = (Image)resources.GetObject("pctRetiradaBalcao.Image");
            pctRetiradaBalcao.Location = new Point(270, 0);
            pctRetiradaBalcao.Name = "pctRetiradaBalcao";
            pctRetiradaBalcao.Size = new Size(118, 93);
            pctRetiradaBalcao.SizeMode = PictureBoxSizeMode.StretchImage;
            pctRetiradaBalcao.TabIndex = 3;
            pctRetiradaBalcao.TabStop = false;
            // 
            // pctDelivery
            // 
            pctDelivery.Image = (Image)resources.GetObject("pctDelivery.Image");
            pctDelivery.Location = new Point(135, 3);
            pctDelivery.Name = "pctDelivery";
            pctDelivery.Size = new Size(118, 93);
            pctDelivery.SizeMode = PictureBoxSizeMode.StretchImage;
            pctDelivery.TabIndex = 2;
            pctDelivery.TabStop = false;
            // 
            // pctVendaRapida
            // 
            pctVendaRapida.Image = Properties.Resources.add;
            pctVendaRapida.Location = new Point(11, 3);
            pctVendaRapida.Name = "pctVendaRapida";
            pctVendaRapida.Size = new Size(118, 93);
            pctVendaRapida.SizeMode = PictureBoxSizeMode.StretchImage;
            pctVendaRapida.TabIndex = 1;
            pctVendaRapida.TabStop = false;
            pctVendaRapida.MouseEnter += pctNovoAtendimento_MouseEnter;
            pctVendaRapida.MouseLeave += pctNovoAtendimento_MouseLeave;
            // 
            // panel2
            // 
            panel2.Controls.Add(dateTimePicker2);
            panel2.Controls.Add(dateTimePicker1);
            panel2.Controls.Add(checkBoxCancelado);
            panel2.Controls.Add(checkBoxRecebido);
            panel2.Controls.Add(checkBoxEmAtendimento);
            panel2.Controls.Add(checkBoxAberto);
            panel2.Controls.Add(lblSituacao);
            panel2.Controls.Add(checkBox5);
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
            // dateTimePicker2
            // 
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Location = new Point(864, 37);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.ShowUpDown = true;
            dateTimePicker2.Size = new Size(235, 31);
            dateTimePicker2.TabIndex = 13;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(623, 37);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.ShowUpDown = true;
            dateTimePicker1.Size = new Size(235, 31);
            dateTimePicker1.TabIndex = 5;
            // 
            // checkBoxCancelado
            // 
            checkBoxCancelado.AutoSize = true;
            checkBoxCancelado.Location = new Point(413, 99);
            checkBoxCancelado.Name = "checkBoxCancelado";
            checkBoxCancelado.Size = new Size(128, 29);
            checkBoxCancelado.TabIndex = 12;
            checkBoxCancelado.Text = "Cancelados";
            checkBoxCancelado.UseVisualStyleBackColor = true;
            // 
            // checkBoxRecebido
            // 
            checkBoxRecebido.AutoSize = true;
            checkBoxRecebido.Location = new Point(294, 99);
            checkBoxRecebido.Name = "checkBoxRecebido";
            checkBoxRecebido.Size = new Size(119, 29);
            checkBoxRecebido.TabIndex = 11;
            checkBoxRecebido.Text = "Recebidos";
            checkBoxRecebido.UseVisualStyleBackColor = true;
            // 
            // checkBoxEmAtendimento
            // 
            checkBoxEmAtendimento.AutoSize = true;
            checkBoxEmAtendimento.Checked = true;
            checkBoxEmAtendimento.CheckState = CheckState.Checked;
            checkBoxEmAtendimento.Location = new Point(119, 99);
            checkBoxEmAtendimento.Name = "checkBoxEmAtendimento";
            checkBoxEmAtendimento.Size = new Size(169, 29);
            checkBoxEmAtendimento.TabIndex = 10;
            checkBoxEmAtendimento.Text = "Em atendimento";
            checkBoxEmAtendimento.UseVisualStyleBackColor = true;
            // 
            // checkBoxAberto
            // 
            checkBoxAberto.AutoSize = true;
            checkBoxAberto.Checked = true;
            checkBoxAberto.CheckState = CheckState.Checked;
            checkBoxAberto.Location = new Point(20, 99);
            checkBoxAberto.Name = "checkBoxAberto";
            checkBoxAberto.Size = new Size(93, 29);
            checkBoxAberto.TabIndex = 9;
            checkBoxAberto.Text = "Aberto";
            checkBoxAberto.UseVisualStyleBackColor = true;
            // 
            // lblSituacao
            // 
            lblSituacao.AutoSize = true;
            lblSituacao.Location = new Point(11, 71);
            lblSituacao.Name = "lblSituacao";
            lblSituacao.Size = new Size(79, 25);
            lblSituacao.TabIndex = 8;
            lblSituacao.Text = "Situação";
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(11, 39);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(147, 29);
            checkBox5.TabIndex = 7;
            checkBox5.Text = "Venda Rápida";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(1105, 34);
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
            checkBox4.Location = new Point(529, 39);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(86, 29);
            checkBox4.TabIndex = 5;
            checkBox4.Text = "Todos";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(392, 39);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(131, 29);
            checkBox3.TabIndex = 4;
            checkBox3.Text = "Encomenda";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(270, 39);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(102, 29);
            checkBox2.TabIndex = 3;
            checkBox2.Text = "Retirada";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(164, 39);
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
            dataGridView1.Location = new Point(0, 141);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1895, 168);
            dataGridView1.TabIndex = 0;
            // 
            // pctReceber
            // 
            pctReceber.Image = Properties.Resources.forma_de_pagamento;
            pctReceber.Location = new Point(555, 11);
            pctReceber.Name = "pctReceber";
            pctReceber.Size = new Size(96, 75);
            pctReceber.SizeMode = PictureBoxSizeMode.StretchImage;
            pctReceber.TabIndex = 5;
            pctReceber.TabStop = false;
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
            ((System.ComponentModel.ISupportInitialize)pctEncomenda).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctRetiradaBalcao).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctDelivery).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctVendaRapida).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctReceber).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pctVendaRapida;
        private Panel panel2;
        private DataGridView dataGridView1;
        private Label lblTipoAtendimento;
        private CheckBox checkBox1;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private Button button1;
        private PictureBox pctEncomenda;
        private PictureBox pctRetiradaBalcao;
        private PictureBox pctDelivery;
        private CheckBox checkBox5;
        private CheckBox checkBoxAberto;
        private Label lblSituacao;
        private CheckBox checkBoxCancelado;
        private CheckBox checkBoxRecebido;
        private CheckBox checkBoxEmAtendimento;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker1;
        private PictureBox pctReceber;
    }
}