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
            lblStatusCaixa = new Label();
            btnAbrirCaixa = new Button();
            btnFecharCaixa = new Button();
            btnSuprimento = new Button();
            btnSangria = new Button();
            btnSair = new Button();
            SuspendLayout();
            // 
            // lblNomeCaixa
            // 
            lblNomeCaixa.AutoSize = true;
            lblNomeCaixa.Location = new Point(12, 20);
            lblNomeCaixa.Name = "lblNomeCaixa";
            lblNomeCaixa.Size = new Size(59, 25);
            lblNomeCaixa.TabIndex = 0;
            lblNomeCaixa.Text = "label1";
            // 
            // lblStatusCaixa
            // 
            lblStatusCaixa.AutoSize = true;
            lblStatusCaixa.Location = new Point(138, 20);
            lblStatusCaixa.Name = "lblStatusCaixa";
            lblStatusCaixa.Size = new Size(59, 25);
            lblStatusCaixa.TabIndex = 1;
            lblStatusCaixa.Text = "label1";
            // 
            // btnAbrirCaixa
            // 
            btnAbrirCaixa.Location = new Point(214, 20);
            btnAbrirCaixa.Name = "btnAbrirCaixa";
            btnAbrirCaixa.Size = new Size(112, 34);
            btnAbrirCaixa.TabIndex = 2;
            btnAbrirCaixa.Text = "Abrir Caixa";
            btnAbrirCaixa.UseVisualStyleBackColor = true;
            btnAbrirCaixa.Click += btnAbrirCaixa_Click;
            // 
            // btnFecharCaixa
            // 
            btnFecharCaixa.Location = new Point(332, 20);
            btnFecharCaixa.Name = "btnFecharCaixa";
            btnFecharCaixa.Size = new Size(142, 34);
            btnFecharCaixa.TabIndex = 3;
            btnFecharCaixa.Text = "Fechar Caixa";
            btnFecharCaixa.UseVisualStyleBackColor = true;
            // 
            // btnSuprimento
            // 
            btnSuprimento.Location = new Point(480, 20);
            btnSuprimento.Name = "btnSuprimento";
            btnSuprimento.Size = new Size(142, 34);
            btnSuprimento.TabIndex = 4;
            btnSuprimento.Text = "Suprimeiro";
            btnSuprimento.UseVisualStyleBackColor = true;
            // 
            // btnSangria
            // 
            btnSangria.Location = new Point(628, 20);
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
            // GestaoCaixaFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSair);
            Controls.Add(btnSangria);
            Controls.Add(btnSuprimento);
            Controls.Add(btnFecharCaixa);
            Controls.Add(btnAbrirCaixa);
            Controls.Add(lblStatusCaixa);
            Controls.Add(lblNomeCaixa);
            Name = "GestaoCaixaFRM";
            Text = "GestaoCaixaFRM";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNomeCaixa;
        private Label lblStatusCaixa;
        private Button btnAbrirCaixa;
        private Button btnFecharCaixa;
        private Button btnSuprimento;
        private Button btnSangria;
        private Button btnSair;
    }
}