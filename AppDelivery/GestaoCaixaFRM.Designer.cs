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
            SuspendLayout();
            // 
            // lblNomeCaixa
            // 
            lblNomeCaixa.AutoSize = true;
            lblNomeCaixa.Location = new Point(433, 20);
            lblNomeCaixa.Name = "lblNomeCaixa";
            lblNomeCaixa.Size = new Size(102, 25);
            lblNomeCaixa.TabIndex = 0;
            lblNomeCaixa.Text = "NomeCaixa";
            // 
            // lblSituacaoCaixa
            // 
            lblSituacaoCaixa.AutoSize = true;
            lblSituacaoCaixa.Location = new Point(685, 20);
            lblSituacaoCaixa.Name = "lblSituacaoCaixa";
            lblSituacaoCaixa.Size = new Size(79, 25);
            lblSituacaoCaixa.TabIndex = 1;
            lblSituacaoCaixa.Text = "Situacao";
            // 
            // btnAbrirCaixa
            // 
            btnAbrirCaixa.Location = new Point(78, 142);
            btnAbrirCaixa.Name = "btnAbrirCaixa";
            btnAbrirCaixa.Size = new Size(142, 34);
            btnAbrirCaixa.TabIndex = 2;
            btnAbrirCaixa.Text = "Abrir Caixa";
            btnAbrirCaixa.UseVisualStyleBackColor = true;
            btnAbrirCaixa.Click += btnAbrirCaixa_Click;
            // 
            // btnFecharCaixa
            // 
            btnFecharCaixa.Location = new Point(78, 208);
            btnFecharCaixa.Name = "btnFecharCaixa";
            btnFecharCaixa.Size = new Size(142, 34);
            btnFecharCaixa.TabIndex = 3;
            btnFecharCaixa.Text = "Fechar Caixa";
            btnFecharCaixa.UseVisualStyleBackColor = true;
            // 
            // btnSuprimento
            // 
            btnSuprimento.Location = new Point(329, 142);
            btnSuprimento.Name = "btnSuprimento";
            btnSuprimento.Size = new Size(142, 34);
            btnSuprimento.TabIndex = 4;
            btnSuprimento.Text = "Suprimento";
            btnSuprimento.UseVisualStyleBackColor = true;
            // 
            // btnSangria
            // 
            btnSangria.Location = new Point(568, 142);
            btnSangria.Name = "btnSangria";
            btnSangria.Size = new Size(142, 34);
            btnSangria.TabIndex = 5;
            btnSangria.Text = "Sangria";
            btnSangria.UseVisualStyleBackColor = true;
            // 
            // btnSair
            // 
            btnSair.Location = new Point(329, 208);
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
            lblIdCaixa.Location = new Point(171, 20);
            lblIdCaixa.Name = "lblIdCaixa";
            lblIdCaixa.Size = new Size(30, 25);
            lblIdCaixa.TabIndex = 7;
            lblIdCaixa.Text = "ID";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(145, 25);
            label1.TabIndex = 8;
            label1.Text = "ID Caixa Estação:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(243, 20);
            label2.Name = "label2";
            label2.Size = new Size(176, 25);
            label2.TabIndex = 9;
            label2.Text = "Nome Caixa Estação:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(550, 20);
            label3.Name = "label3";
            label3.Size = new Size(129, 25);
            label3.TabIndex = 10;
            label3.Text = "Situação Caixa:";
            // 
            // GestaoCaixaFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblIdCaixa);
            Controls.Add(btnSair);
            Controls.Add(btnSangria);
            Controls.Add(btnSuprimento);
            Controls.Add(btnFecharCaixa);
            Controls.Add(btnAbrirCaixa);
            Controls.Add(lblSituacaoCaixa);
            Controls.Add(lblNomeCaixa);
            Name = "GestaoCaixaFRM";
            Text = "GestaoCaixaFRM";
            Load += GestaoCaixaFRM_Load;
            ResumeLayout(false);
            PerformLayout();
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
    }
}