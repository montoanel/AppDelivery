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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AtendimentosFRM));
            panel1 = new Panel();
            groupBox2 = new GroupBox();
            label5 = new Label();
            pctReceber = new PictureBox();
            groupBox1 = new GroupBox();
            btnNovoAtendimento = new Button();
            menuNovoAtendimento = new ContextMenuStrip(components);
            toolStripVendaRapida = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripDelivery = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripRetirada = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripEncomenda = new ToolStripMenuItem();
            btnEntregar = new Button();
            btnExibirAtendimento = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            pctEncomenda = new PictureBox();
            pctVendaRapida = new PictureBox();
            pctRetiradaBalcao = new PictureBox();
            pctDelivery = new PictureBox();
            panel2 = new Panel();
            btnResetSequencia = new Button();
            button2 = new Button();
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
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctReceber).BeginInit();
            groupBox1.SuspendLayout();
            menuNovoAtendimento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctEncomenda).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctVendaRapida).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctRetiradaBalcao).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctDelivery).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Location = new Point(1, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(1895, 251);
            panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(pctReceber);
            groupBox2.Location = new Point(509, 11);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(437, 111);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Informações Financeiras";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(19, 87);
            label5.Name = "label5";
            label5.Size = new Size(66, 21);
            label5.TabIndex = 11;
            label5.Text = "Receber";
            // 
            // pctReceber
            // 
            pctReceber.Image = Properties.Resources.forma_de_pagamento;
            pctReceber.Location = new Point(6, 30);
            pctReceber.Name = "pctReceber";
            pctReceber.Size = new Size(79, 53);
            pctReceber.SizeMode = PictureBoxSizeMode.StretchImage;
            pctReceber.TabIndex = 5;
            pctReceber.TabStop = false;
            pctReceber.MouseEnter += pctGenerico_MouseEnter;
            pctReceber.MouseLeave += pctGenerico_MouseLeave;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnNovoAtendimento);
            groupBox1.Controls.Add(btnEntregar);
            groupBox1.Controls.Add(btnExibirAtendimento);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(pctEncomenda);
            groupBox1.Controls.Add(pctVendaRapida);
            groupBox1.Controls.Add(pctRetiradaBalcao);
            groupBox1.Controls.Add(pctDelivery);
            groupBox1.Location = new Point(8, 7);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(471, 210);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Atendimentos";
            // 
            // btnNovoAtendimento
            // 
            btnNovoAtendimento.ContextMenuStrip = menuNovoAtendimento;
            btnNovoAtendimento.Location = new Point(13, 29);
            btnNovoAtendimento.Name = "btnNovoAtendimento";
            btnNovoAtendimento.Size = new Size(209, 34);
            btnNovoAtendimento.TabIndex = 8;
            btnNovoAtendimento.Text = "➕ Novo Atendimento";
            btnNovoAtendimento.UseVisualStyleBackColor = true;
            btnNovoAtendimento.Click += btnNovoAtendimento_Click;
            // 
            // menuNovoAtendimento
            // 
            menuNovoAtendimento.ImageScalingSize = new Size(24, 24);
            menuNovoAtendimento.Items.AddRange(new ToolStripItem[] { toolStripVendaRapida, toolStripSeparator1, toolStripDelivery, toolStripSeparator2, toolStripRetirada, toolStripSeparator3, toolStripEncomenda });
            menuNovoAtendimento.Name = "menuNovoAtendimento";
            menuNovoAtendimento.Size = new Size(194, 150);
            // 
            // toolStripVendaRapida
            // 
            toolStripVendaRapida.Name = "toolStripVendaRapida";
            toolStripVendaRapida.Size = new Size(193, 32);
            toolStripVendaRapida.Text = "Venda Rápida";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(190, 6);
            // 
            // toolStripDelivery
            // 
            toolStripDelivery.Name = "toolStripDelivery";
            toolStripDelivery.Size = new Size(193, 32);
            toolStripDelivery.Text = "Delivery";
            toolStripDelivery.Click += toolStripDelivery_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(190, 6);
            // 
            // toolStripRetirada
            // 
            toolStripRetirada.Name = "toolStripRetirada";
            toolStripRetirada.Size = new Size(193, 32);
            toolStripRetirada.Text = "Retirada";
            toolStripRetirada.Click += toolStripRetirada_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(190, 6);
            // 
            // toolStripEncomenda
            // 
            toolStripEncomenda.Name = "toolStripEncomenda";
            toolStripEncomenda.Size = new Size(193, 32);
            toolStripEncomenda.Text = "Encomenda";
            toolStripEncomenda.Click += toolStripEncomenda_Click;
            // 
            // btnEntregar
            // 
            btnEntregar.Location = new Point(13, 69);
            btnEntregar.Name = "btnEntregar";
            btnEntregar.Size = new Size(154, 34);
            btnEntregar.TabIndex = 13;
            btnEntregar.Text = "Em trânsito";
            btnEntregar.UseVisualStyleBackColor = true;
            // 
            // btnExibirAtendimento
            // 
            btnExibirAtendimento.Location = new Point(228, 30);
            btnExibirAtendimento.Name = "btnExibirAtendimento";
            btnExibirAtendimento.Size = new Size(204, 34);
            btnExibirAtendimento.TabIndex = 11;
            btnExibirAtendimento.Text = "Exibir Atendimento";
            btnExibirAtendimento.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(15, 166);
            label4.Name = "label4";
            label4.Size = new Size(91, 21);
            label4.TabIndex = 10;
            label4.Text = "Encomenda";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(109, 166);
            label3.Name = "label3";
            label3.Size = new Size(68, 21);
            label3.TabIndex = 9;
            label3.Text = "Retirada";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(197, 169);
            label2.Name = "label2";
            label2.Size = new Size(67, 21);
            label2.TabIndex = 8;
            label2.Text = "Delivery";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(278, 169);
            label1.Name = "label1";
            label1.Size = new Size(75, 21);
            label1.TabIndex = 7;
            label1.Text = "V. Rápida";
            // 
            // pctEncomenda
            // 
            pctEncomenda.Image = (Image)resources.GetObject("pctEncomenda.Image");
            pctEncomenda.Location = new Point(15, 110);
            pctEncomenda.Name = "pctEncomenda";
            pctEncomenda.Size = new Size(79, 53);
            pctEncomenda.SizeMode = PictureBoxSizeMode.StretchImage;
            pctEncomenda.TabIndex = 4;
            pctEncomenda.TabStop = false;
            pctEncomenda.Click += pctEncomenda_Click;
            pctEncomenda.MouseEnter += pctGenerico_MouseEnter;
            pctEncomenda.MouseLeave += pctGenerico_MouseLeave;
            // 
            // pctVendaRapida
            // 
            pctVendaRapida.Image = Properties.Resources.add;
            pctVendaRapida.Location = new Point(270, 110);
            pctVendaRapida.Name = "pctVendaRapida";
            pctVendaRapida.Size = new Size(79, 53);
            pctVendaRapida.SizeMode = PictureBoxSizeMode.StretchImage;
            pctVendaRapida.TabIndex = 1;
            pctVendaRapida.TabStop = false;
            // 
            // pctRetiradaBalcao
            // 
            pctRetiradaBalcao.Image = (Image)resources.GetObject("pctRetiradaBalcao.Image");
            pctRetiradaBalcao.Location = new Point(100, 110);
            pctRetiradaBalcao.Name = "pctRetiradaBalcao";
            pctRetiradaBalcao.Size = new Size(79, 53);
            pctRetiradaBalcao.SizeMode = PictureBoxSizeMode.StretchImage;
            pctRetiradaBalcao.TabIndex = 3;
            pctRetiradaBalcao.TabStop = false;
            pctRetiradaBalcao.Click += pctRetiradaBalcao_Click;
            pctRetiradaBalcao.MouseEnter += pctGenerico_MouseEnter;
            pctRetiradaBalcao.MouseLeave += pctGenerico_MouseLeave;
            // 
            // pctDelivery
            // 
            pctDelivery.Image = (Image)resources.GetObject("pctDelivery.Image");
            pctDelivery.Location = new Point(185, 110);
            pctDelivery.Name = "pctDelivery";
            pctDelivery.Size = new Size(79, 53);
            pctDelivery.SizeMode = PictureBoxSizeMode.StretchImage;
            pctDelivery.TabIndex = 2;
            pctDelivery.TabStop = false;
            pctDelivery.Click += pctDelivery_Click;
            pctDelivery.MouseEnter += pctGenerico_MouseEnter;
            pctDelivery.MouseLeave += pctGenerico_MouseLeave;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnResetSequencia);
            panel2.Controls.Add(button2);
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
            panel2.Location = new Point(12, 258);
            panel2.Name = "panel2";
            panel2.Size = new Size(1220, 132);
            panel2.TabIndex = 1;
            // 
            // btnResetSequencia
            // 
            btnResetSequencia.Location = new Point(849, 94);
            btnResetSequencia.Name = "btnResetSequencia";
            btnResetSequencia.Size = new Size(220, 34);
            btnResetSequencia.TabIndex = 15;
            btnResetSequencia.Text = "Reiniciar Sequêncial";
            btnResetSequencia.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Image = Properties.Resources.impressora;
            button2.Location = new Point(722, 94);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 3;
            button2.Text = "Imprimir";
            button2.UseVisualStyleBackColor = true;
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
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;
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
            dataGridView1.Location = new Point(12, 428);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1223, 421);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 861);
            panel3.Name = "panel3";
            panel3.Size = new Size(1898, 43);
            panel3.TabIndex = 2;
            // 
            // AtendimentosFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 904);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Name = "AtendimentosFRM";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AtendimentosFRM";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pctReceber).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            menuNovoAtendimento.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pctEncomenda).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctVendaRapida).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctRetiradaBalcao).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctDelivery).EndInit();
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
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label5;
        private Button button2;
        private Button btnResetSequencia;
        private Button btnExibirAtendimento;
        private Button btnEntregar;
        private Button btnNovoAtendimento;
        private ContextMenuStrip menuNovoAtendimento;
        private ToolStripMenuItem toolStripVendaRapida;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripDelivery;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem toolStripRetirada;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem toolStripEncomenda;
    }
}