namespace AppDelivery
{
    partial class GestaoCaixaFRM
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
            lblNomeCaixa = new Label();
            lblSituacaoCaixa = new Label();
            btnAbrirCaixa = new Button();
            btnFecharCaixa = new Button();
            btnSuprimento = new Button();
            btnSangria = new Button();
            btnSair = new Button();
            lblIdCaixa = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            panel1 = new Panel();
            groupBox2 = new GroupBox();
            groupBox1 = new GroupBox();
            panel1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lblNomeCaixa
            // 
            lblNomeCaixa.AutoSize = true;
            lblNomeCaixa.Location = new Point(291, 61);
            lblNomeCaixa.Name = "lblNomeCaixa";
            lblNomeCaixa.Size = new Size(102, 25);
            lblNomeCaixa.TabIndex = 0;
            lblNomeCaixa.Text = "NomeCaixa";
            // 
            // lblSituacaoCaixa
            // 
            lblSituacaoCaixa.AutoSize = true;
            lblSituacaoCaixa.Location = new Point(444, 61);
            lblSituacaoCaixa.Name = "lblSituacaoCaixa";
            lblSituacaoCaixa.Size = new Size(79, 25);
            lblSituacaoCaixa.TabIndex = 1;
            lblSituacaoCaixa.Text = "Situacao";
            lblSituacaoCaixa.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnAbrirCaixa
            // 
            btnAbrirCaixa.Location = new Point(67, 61);
            btnAbrirCaixa.Name = "btnAbrirCaixa";
            btnAbrirCaixa.Size = new Size(142, 34);
            btnAbrirCaixa.TabIndex = 2;
            btnAbrirCaixa.Text = "Abrir Caixa";
            btnAbrirCaixa.UseVisualStyleBackColor = true;
            btnAbrirCaixa.Click += btnAbrirCaixa_Click;
            // 
            // btnFecharCaixa
            // 
            btnFecharCaixa.Location = new Point(67, 101);
            btnFecharCaixa.Name = "btnFecharCaixa";
            btnFecharCaixa.Size = new Size(142, 34);
            btnFecharCaixa.TabIndex = 3;
            btnFecharCaixa.Text = "Fechar Caixa";
            btnFecharCaixa.UseVisualStyleBackColor = true;
            // 
            // btnSuprimento
            // 
            btnSuprimento.Location = new Point(215, 61);
            btnSuprimento.Name = "btnSuprimento";
            btnSuprimento.Size = new Size(142, 34);
            btnSuprimento.TabIndex = 4;
            btnSuprimento.Text = "Suprimento";
            btnSuprimento.UseVisualStyleBackColor = true;
            // 
            // btnSangria
            // 
            btnSangria.Location = new Point(215, 101);
            btnSangria.Name = "btnSangria";
            btnSangria.Size = new Size(142, 34);
            btnSangria.TabIndex = 5;
            btnSangria.Text = "Sangria";
            btnSangria.UseVisualStyleBackColor = true;
            // 
            // btnSair
            // 
            btnSair.Location = new Point(363, 79);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(142, 34);
            btnSair.TabIndex = 6;
            btnSair.Text = "Sair";
            btnSair.UseVisualStyleBackColor = true;
            btnSair.Click += btnSair_Click;
            // 
            // lblIdCaixa
            // 
            lblIdCaixa.AutoSize = true;
            lblIdCaixa.Location = new Point(133, 61);
            lblIdCaixa.Name = "lblIdCaixa";
            lblIdCaixa.Size = new Size(30, 25);
            lblIdCaixa.TabIndex = 7;
            lblIdCaixa.Text = "ID";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(78, 36);
            label1.Name = "label1";
            label1.Size = new Size(141, 25);
            label1.TabIndex = 8;
            label1.Text = "ID Caixa Estação";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(256, 36);
            label2.Name = "label2";
            label2.Size = new Size(172, 25);
            label2.TabIndex = 9;
            label2.Text = "Nome Caixa Estação";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(506, 36);
            label3.Name = "label3";
            label3.Size = new Size(129, 25);
            label3.TabIndex = 10;
            label3.Text = "Situação Caixa:";
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 450);
            panel1.TabIndex = 11;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnSangria);
            groupBox2.Controls.Add(btnSair);
            groupBox2.Controls.Add(btnSuprimento);
            groupBox2.Controls.Add(btnAbrirCaixa);
            groupBox2.Controls.Add(btnFecharCaixa);
            groupBox2.Location = new Point(12, 141);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(523, 149);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Ações";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(lblIdCaixa);
            groupBox1.Controls.Add(lblNomeCaixa);
            groupBox1.Controls.Add(lblSituacaoCaixa);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(785, 111);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Informações do Caixa";
            // 
            // GestaoCaixaFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Name = "GestaoCaixaFRM";
            Text = "GestaoCaixaFRM";
            Load += GestaoCaixaFRM_Load;
            panel1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblNomeCaixa;
        private Label lblSituacaoCaixa;
        private Button btnAbrirCaixa;
        private Button btnFecharCaixa;
        private Button btnSuprimento;
        private Button btnSangria;
        private Button btnSair;
        private Label lblIdCaixa;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}