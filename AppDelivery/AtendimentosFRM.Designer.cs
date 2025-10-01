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
            pctReceber = new PictureBox();
            pctEncomenda = new PictureBox();
            pctRetiradaBalcao = new PictureBox();
            pctDelivery = new PictureBox();
            pctVendaRapida = new PictureBox();
            panel2 = new Panel();
            checkBoxEmTransito = new CheckBox();
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
            panel3 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctReceber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctEncomenda).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctRetiradaBalcao).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctDelivery).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctVendaRapida).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
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
            panel1.Size = new Size(1895, 120);
            panel1.TabIndex = 0;
            // 
            // pctReceber
            // 
            pctReceber.Image = Properties.Resources.forma_de_pagamento;
            pctReceber.Location = new Point(530, 20);
            pctReceber.Name = "pctReceber";
            pctReceber.Size = new Size(96, 75);
            pctReceber.SizeMode = PictureBoxSizeMode.StretchImage;
            pctReceber.TabIndex = 5;
            pctReceber.TabStop = false;
            // 
            // pctEncomenda
            // 
            pctEncomenda.Image = (Image)resources.GetObject("pctEncomenda.Image");
            pctEncomenda.Location = new Point(400, 10);
            pctEncomenda.Name = "pctEncomenda";
            pctEncomenda.Size = new Size(118, 93);
            pctEncomenda.SizeMode = PictureBoxSizeMode.StretchImage;
            pctEncomenda.TabIndex = 4;
            pctEncomenda.TabStop = false;
            // 
            // pctRetiradaBalcao
            // 
            pctRetiradaBalcao.Image = (Image)resources.GetObject("pctRetiradaBalcao.Image");
            pctRetiradaBalcao.Location = new Point(270, 10);
            pctRetiradaBalcao.Name = "pctRetiradaBalcao";
            pctRetiradaBalcao.Size = new Size(118, 93);
            pctRetiradaBalcao.SizeMode = PictureBoxSizeMode.StretchImage;
            pctRetiradaBalcao.TabIndex = 3;
            pctRetiradaBalcao.TabStop = false;
            // 
            // pctDelivery
            // 
            pctDelivery.Image = (Image)resources.GetObject("pctDelivery.Image");
            pctDelivery.Location = new Point(140, 10);
            pctDelivery.Name = "pctDelivery";
            pctDelivery.Size = new Size(118, 93);
            pctDelivery.SizeMode = PictureBoxSizeMode.StretchImage;
            pctDelivery.TabIndex = 2;
            pctDelivery.TabStop = false;
            // 
            // pctVendaRapida
            // 
            pctVendaRapida.Image = Properties.Resources.add;
            pctVendaRapida.Location = new Point(10, 10);
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
            panel2.Controls.Add(checkBoxEmTransito);
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
            panel2.Location = new Point(1, 125);
            panel2.Name = "panel2";
            panel2.Size = new Size(1220, 142);
            panel2.TabIndex = 1;
            // 
            // checkBoxEmTransito
            // 
            checkBoxEmTransito.AutoSize = true;
            checkBoxEmTransito.Checked = true;
            checkBoxEmTransito.CheckState = CheckState.Checked;
            checkBoxEmTransito.Location = new Point(294, 99);
            checkBoxEmTransito.Name = "checkBoxEmTransito";
            checkBoxEmTransito.Size = new Size(128, 29);
            checkBoxEmTransito.TabIndex = 14;
            checkBoxEmTransito.Text = "Em trânsito";
            checkBoxEmTransito.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Location = new Point(800, 37);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.ShowUpDown = true;
            dateTimePicker2.Size = new Size(170, 31);
            dateTimePicker2.TabIndex = 13;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(620, 37);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.ShowUpDown = true;
            dateTimePicker1.Size = new Size(170, 31);
            dateTimePicker1.TabIndex = 5;
            // 
            // checkBoxCancelado
            // 
            checkBoxCancelado.AutoSize = true;
            checkBoxCancelado.Location = new Point(553, 99);
            checkBoxCancelado.Name = "checkBoxCancelado";
            checkBoxCancelado.Size = new Size(128, 29);
            checkBoxCancelado.TabIndex = 12;
            checkBoxCancelado.Text = "Cancelados";
            checkBoxCancelado.UseVisualStyleBackColor = true;
            // 
            // checkBoxRecebido
            // 
            checkBoxRecebido.AutoSize = true;
            checkBoxRecebido.Location = new Point(428, 99);
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
            lblSituacao.Location = new Point(10, 75);
            lblSituacao.Name = "lblSituacao";
            lblSituacao.Size = new Size(79, 25);
            lblSituacao.TabIndex = 8;
            lblSituacao.Text = "Situação";
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(10, 39);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(147, 29);
            checkBox5.TabIndex = 7;
            checkBox5.Text = "Venda Rápida";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(980, 36);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 6;
            button1.Text = "Filtrar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Checked = true;
            checkBox4.CheckState = CheckState.Checked;
            checkBox4.Location = new Point(540, 39);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(86, 29);
            checkBox4.TabIndex = 5;
            checkBox4.Text = "Todos";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(400, 39);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(131, 29);
            checkBox3.TabIndex = 4;
            checkBox3.Text = "Encomenda";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(280, 39);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(102, 29);
            checkBox2.TabIndex = 3;
            checkBox2.Text = "Retirada";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(160, 39);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(101, 29);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Delivery";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // lblTipoAtendimento
            // 
            lblTipoAtendimento.AutoSize = true;
            lblTipoAtendimento.Location = new Point(10, 10);
            lblTipoAtendimento.Name = "lblTipoAtendimento";
            lblTipoAtendimento.Size = new Size(156, 25);
            lblTipoAtendimento.TabIndex = 1;
            lblTipoAtendimento.Text = "Tipo Atendimento";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(1, 273);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1885, 676);
            dataGridView1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 956);
            panel3.Name = "panel3";
            panel3.Size = new Size(1898, 66);
            panel3.TabIndex = 2;
            // 
            // AtendimentosFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 1022);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Name = "AtendimentosFRM";
            Text = "AtendimentosFRM";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pctReceber).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctEncomenda).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctRetiradaBalcao).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctDelivery).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctVendaRapida).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
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
        private Panel panel3;
        private CheckBox checkBoxEmTransito;
    }
}