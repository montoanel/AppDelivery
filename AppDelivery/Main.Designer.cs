namespace AppDelivery
{
    partial class Main
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
            pctBoxClientes = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pctBoxClientes).BeginInit();
            SuspendLayout();
            // 
            // pctBoxClientes
            // 
            pctBoxClientes.Image = Properties.Resources.target;
            pctBoxClientes.Location = new Point(12, 12);
            pctBoxClientes.Name = "pctBoxClientes";
            pctBoxClientes.Size = new Size(90, 90);
            pctBoxClientes.SizeMode = PictureBoxSizeMode.StretchImage;
            pctBoxClientes.TabIndex = 0;
            pctBoxClientes.TabStop = false;
            pctBoxClientes.Click += pctBoxClientes_Click;
            pctBoxClientes.MouseEnter += pctBoxClientes_MouseEnter;
            pctBoxClientes.MouseLeave += pctBoxClientes_MouseLeave;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 1024);
            Controls.Add(pctBoxClientes);
            Name = "Main";
            Text = "Main";
            MouseEnter += Main_MouseEnter;
            ((System.ComponentModel.ISupportInitialize)pctBoxClientes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pctBoxClientes;
    }
}