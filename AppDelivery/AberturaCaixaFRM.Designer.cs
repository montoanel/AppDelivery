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
            lblValorAbertura = new Label();
            txtValorAbertura = new TextBox();
            lblAtendente = new Label();
            cmbAtendente = new ComboBox();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblValorAbertura
            // 
            lblValorAbertura.AutoSize = true;
            lblValorAbertura.Location = new Point(12, 19);
            lblValorAbertura.Name = "lblValorAbertura";
            lblValorAbertura.Size = new Size(213, 25);
            lblValorAbertura.TabIndex = 0;
            lblValorAbertura.Text = "Valor de Abertura (Troco):";
            // 
            // txtValorAbertura
            // 
            txtValorAbertura.Location = new Point(231, 19);
            txtValorAbertura.Name = "txtValorAbertura";
            txtValorAbertura.Size = new Size(181, 31);
            txtValorAbertura.TabIndex = 1;
            // 
            // lblAtendente
            // 
            lblAtendente.AutoSize = true;
            lblAtendente.Location = new Point(12, 74);
            lblAtendente.Name = "lblAtendente";
            lblAtendente.Size = new Size(94, 25);
            lblAtendente.TabIndex = 2;
            lblAtendente.Text = "Atendente";
            // 
            // cmbAtendente
            // 
            cmbAtendente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAtendente.FormattingEnabled = true;
            cmbAtendente.Location = new Point(231, 66);
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
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(358, 215);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(176, 34);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // AberturaCaixaFRM
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(cmbAtendente);
            Controls.Add(lblAtendente);
            Controls.Add(txtValorAbertura);
            Controls.Add(lblValorAbertura);
            Name = "AberturaCaixaFRM";
            Text = "AberturaCaixaFRM";
            Load += AberturaCaixaFRM_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblValorAbertura;
        private TextBox txtValorAbertura;
        private Label lblAtendente;
        private ComboBox cmbAtendente;
        private Button btnConfirmar;
        private Button btnCancelar;
    }
}