namespace AppDelivery
{
    partial class AberturaCaixaFRM
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
            label1 = new Label();
            txtValorAbertura = new TextBox();
            label2 = new Label();
            cmbAtendente = new ComboBox();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(213, 25);
            label1.TabIndex = 0;
            label1.Text = "Valor de Abertura (Troco):";
            // 
            // txtValorAbertura
            // 
            txtValorAbertura.Location = new Point(231, 19);
            txtValorAbertura.Name = "txtValorAbertura";
            txtValorAbertura.Size = new Size(181, 31);
            txtValorAbertura.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 74);
            label2.Name = "label2";
            label2.Size = new Size(94, 25);
            label2.TabIndex = 2;
            label2.Text = "Atendente";
            // 
            // cmbAtendente
            // 
            cmbAtendente.FormattingEnabled = true;
            cmbAtendente.Location = new Point(112, 71);
            cmbAtendente.Name = "cmbAtendente";
            cmbAtendente.Size = new Size(182, 33);
            cmbAtendente.TabIndex = 3;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(176, 215);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(176, 34);
            btnConfirmar.TabIndex = 4;
            btnConfirmar.Text = "Confirmar Abertura";
            btnConfirmar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(358, 215);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(176, 34);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // AberturaCaixaFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(cmbAtendente);
            Controls.Add(label2);
            Controls.Add(txtValorAbertura);
            Controls.Add(label1);
            Name = "AberturaCaixaFRM";
            Text = "AberturaCaixaFRM";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtValorAbertura;
        private Label label2;
        private ComboBox cmbAtendente;
        private Button btnConfirmar;
        private Button btnCancelar;
    }
}